using CommunityToolkit.Mvvm.ComponentModel;
using QuestionProjectOffice.Models;
using QuestionProjectOfficeDb;
using QuestionProjectOfficeDb.Entities;

namespace QuestionProjectOffice.ViewModels.Windows
{
    internal class MainWindowViewModel : ObservableObject
    {
        #region Поля
        private readonly QuestionProjectOfficeContext _dbContext;
        #endregion


        public MainWindowViewModel()
        {
            #region Создания контекста базы данных и заполнения её.
            var isCreatedDataBase = CreateDbContext(out _dbContext);
            var isFillDataBase = true;

            if (isCreatedDataBase && isFillDataBase)
                FillDataBase();
            #endregion
        }


        #region Методы

        #region Создания контекста базы данных и заполнения её
        private static bool CreateDbContext(out QuestionProjectOfficeContext dbContext)
        {
            var connectionString = ConnectionQuestionProjectOfficeDb.PostgreSqlConnectionString;
            var providerDb = ProviderDb.PostgreSql;

            var options = QuestionProjectOfficeContext.BuildDbContextOptions(connectionString, providerDb);

            dbContext = new QuestionProjectOfficeContext(options);
            return dbContext.Database.EnsureCreated();
        }

        private void FillDataBase()
        {
            var questionCategories = Enumerable.Range(1, 3).
                Select(x => new QuestionCategory() { Name = $"Категория вопроса №{x}" }).
                ToList();


            var questionAnswerPairs = Enumerable.Range(1, 7).
                Select(x => new QuestionAnswerPair()
                {
                    Question = $"Вопрос №{x}",
                    Answer = $"Ответ №{x}",
                    DateTime = DateTime.UtcNow.AddDays(-x / 2).AddHours(-x).AddMinutes(-x * 2).AddSeconds(-x * 4)
                }).
                ToList();

            questionAnswerPairs[0].QuestionCategory = questionCategories[0];
            questionAnswerPairs[2].QuestionCategory = questionCategories[1];
            questionAnswerPairs[3].QuestionCategory = questionCategories[2];
            questionAnswerPairs[0].QuestionCategory = questionCategories[0];
            questionAnswerPairs[4].QuestionCategory = questionCategories[1];

            questionAnswerPairs[3].Populatiry = 3;
            questionAnswerPairs[6].Populatiry = 4;


            _dbContext.QuestionCategories.AddRange(questionCategories);
            _dbContext.QuestionAnswerPairs.AddRange(questionAnswerPairs);

            _dbContext.SaveChanges();
        }
        #endregion

        #endregion
    }
}
