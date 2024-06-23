﻿namespace QuestionProjectOfficeDb.Entities
{
    public class QuestionCategory
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public List<QuestionAnswerPair>? QuestionAnswerPairs { get; set; }
    }
}
