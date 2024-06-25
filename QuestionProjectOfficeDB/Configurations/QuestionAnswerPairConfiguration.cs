using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestionProjectOfficeDb.Entities;

namespace QuestionProjectOfficeDb.Configurations
{
    internal class QuestionAnswerPairConfiguration : IEntityTypeConfiguration<QuestionAnswerPair>
    {
        public void Configure(EntityTypeBuilder<QuestionAnswerPair> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.QuestionCategory).
                WithMany(c => c.QuestionAnswerPairs).
                HasForeignKey(fk => fk.QuestionCategoryId);
        }
    }
}
