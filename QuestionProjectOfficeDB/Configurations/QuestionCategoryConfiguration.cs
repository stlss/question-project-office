﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestionProjectOfficeDb.Entities;

namespace QuestionProjectOfficeDb.Configurations
{
    internal class QuestionCategoryConfiguration : IEntityTypeConfiguration<QuestionCategory>
    {
        public void Configure(EntityTypeBuilder<QuestionCategory> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasMany(c => c.QuestionAnswerPairs).
                WithOne(p => p.QuestionCategory).
                HasForeignKey(fk => fk.QuestionCategoryId).
                OnDelete(DeleteBehavior.SetNull);
        }
    }
}
