using Microsoft.EntityFrameworkCore;
using QuestionProjectOfficeDb.Configurations;
using QuestionProjectOfficeDb.Entities;

namespace QuestionProjectOfficeDb
{
    public class QuestionProjectOfficeContext(DbContextOptions<QuestionProjectOfficeContext> options) 
        : DbContext(options)
    {
        public DbSet<QuestionAnswerPair> QuestionAnswerPairs { get; set; }

        public DbSet<QuestionCategory> QuestionCategories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new QuestionAnswerPairConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionCategoryConfiguration());

            base.OnModelCreating(modelBuilder);
        }


        public static DbContextOptions<QuestionProjectOfficeContext> BuildDbContextOptions(string connectionString, ProviderDb providerDb)
        {
            var optionsBuilder = new DbContextOptionsBuilder<QuestionProjectOfficeContext>();

            switch (providerDb)
            {
                case ProviderDb.PostgreSql:
                    optionsBuilder.UseNpgsql(connectionString);
                    break;
            }

            return optionsBuilder.Options;
        }
    }
}
