namespace QuestionProjectOfficeDb.Entities
{
    public class QuestionAnswerPair
    {
        public Guid Id { get; set; }

        public string Question { get; set; } = null!;

        public string Answer { get; set; } = null!;

        public QuestionCategory? Category { get; set; }

        public int Populatiry { get; set; } = 0;

        public DateTime DateTime { get; set; }
    }
}
