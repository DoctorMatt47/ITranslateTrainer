using System.Linq.Expressions;
using ITranslateTrainer.Domain.Abstractions;
using ITranslateTrainer.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Common.Extensions;

public static class QueryableThrowExtensions
{
    public static async Task<TEntity> FirstByIdOrThrowAsync<TEntity, TId>(
        this IQueryable<TEntity> set,
        TId id,
        CancellationToken cancellationToken) where TEntity : class, IHasId<TId> where TId : notnull
    {
        var lambda = EqualsLambdaExpression<TEntity>(id, nameof(IHasId<TId>.Id));
        var entity = await set.FirstOrDefaultAsync(lambda, cancellationToken);
        if (entity is null) throw NotFoundException.DoesNotExist<TEntity>(id);

        return entity;
    }

    public static async Task<TEntity> FindOrThrowAsync<TEntity>(
        this DbSet<TEntity> set,
        object id,
        CancellationToken cancellationToken) where TEntity : class
    {
        var entity = await set.FindAsync(new[] {id}, cancellationToken);
        if (entity is null) throw NotFoundException.DoesNotExist<TEntity>(id);
        return entity;
    }

    private static Expression<Func<TEntity, bool>> EqualsLambdaExpression<TEntity>(
        object? constant,
        string propertyName)
        where TEntity : class
    {
        var parameter = Expression.Parameter(typeof(TEntity), nameof(TEntity));
        var equal = Expression.Equal(Expression.Property(parameter, propertyName), Expression.Constant(constant));
        return Expression.Lambda<Func<TEntity, bool>>(equal, parameter);
    }
}
