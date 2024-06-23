using Microsoft.EntityFrameworkCore;
using QuestionProjectOfficeDb.Entities;

namespace QuestionProjectOfficeDb
{
    public class QuestionProjectOfficeContext(DbContextOptions<QuestionProjectOfficeContext> options) : DbContext(options)
    {
        public DbSet<QuestionAnswerPair> QuestionAnswerPairs { get; set; }

        public DbSet<QuestionCategory> QuestionCategories { get; set; }


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
