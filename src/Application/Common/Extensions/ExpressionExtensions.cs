using System.Linq.Expressions;
using ITranslateTrainer.Domain.Entities;

namespace ITranslateTrainer.Application.Common.Extensions;

internal static class Is
{
    public static Expression<Func<Test, bool>> Not(Expression<Func<Test, bool>> expression)
    {
        var parameter = expression.Parameters[0];
        var body = Expression.Not(expression.Body);
        return Expression.Lambda<Func<Test, bool>>(body, parameter);
    }
}
