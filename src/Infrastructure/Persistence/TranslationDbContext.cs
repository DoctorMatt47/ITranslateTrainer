using System.Threading.Tasks;
using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Infrastructure.Persistence
{
    public class TranslationDbContext : DbContext, ITranslateDbContext
    {
        public DbSet<Text> Texts { get; set; }
        public DbSet<Translation> Translations { get; set; }

        public Task SaveChangesAsync() => base.SaveChangesAsync();
    }
}
