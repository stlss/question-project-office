using CommunityToolkit.Mvvm.ComponentModel;
using QuestionProjectOffice.Models;
using QuestionProjectOfficeDb;

namespace QuestionProjectOffice.ViewModels.Windows
{
    internal class MainWindowViewModel : ObservableObject
    {
        #region Поля
        private readonly QuestionProjectOfficeContext _dbContext;
        #endregion


        public MainWindowViewModel()
        {
            _dbContext = CreateDbContext();
        }


        #region Методы

        #region Создание контекста базы данных и её заполнение
        private static QuestionProjectOfficeContext CreateDbContext()
        {
            var connectionString = ConnectionQuestionProjectOfficeDb.PostgreSqlConnectionString;
            var providerDb = ProviderDb.PostgreSql;

            var options = QuestionProjectOfficeContext.BuildDbContextOptions(connectionString, providerDb);

            var _dbContext = new QuestionProjectOfficeContext(options);
            var isDatabaseCreated = _dbContext.Database.EnsureCreated();

            if (isDatabaseCreated)
                FillDataBase();

            return _dbContext;
        }

        private static void FillDataBase()
        {

        }
        #endregion

        #endregion
    }
}
