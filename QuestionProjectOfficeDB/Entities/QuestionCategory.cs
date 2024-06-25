using System.Collections.ObjectModel;

namespace QuestionProjectOfficeDb.Entities
{
    public class QuestionCategory
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public ObservableCollection<QuestionAnswerPair> QuestionAnswerPairs { get; set; } = [];
    }
}
