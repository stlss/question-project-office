using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
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


        #region Свойства

        #region Категории вопросов
        public List<QuestionCategory> QuestionCategories { get; private set; }

        private QuestionCategory? _selectedQuestionCategory;
        public QuestionCategory? SelectedQuestionCategory
        {
            get => _selectedQuestionCategory;
            set
            {
                if (!SetProperty(ref _selectedQuestionCategory, value))
                    return;

                if (_selectedQuestionCategory != null)
                    SelectedQuestionAnswerPairs = _selectedQuestionCategory.QuestionAnswerPairs;

                UpdateQuestionCategoryCommand.NotifyCanExecuteChanged();
                DeleteQuestionCategoryCommand.NotifyCanExecuteChanged();
            }
        }
        #endregion

        #region Вопросы и ответы
        public List<QuestionAnswerPair> AllQuestionAnswerPairs { get; private set; }

        public List<QuestionAnswerPair> QuestionAnswerPairsWithoutCategory { get; private set; }

        private List<QuestionAnswerPair> _selectedQuestionAnswerPairs;
        public List<QuestionAnswerPair> SelectedQuestionAnswerPairs
        {
            get => _selectedQuestionAnswerPairs;
            set => SetProperty(ref _selectedQuestionAnswerPairs, value);
        }

        private QuestionAnswerPair? _selectedQuestionAnswerPair;
        public QuestionAnswerPair? SelectedQuestionAnswerPair 
        {
            get => _selectedQuestionAnswerPair;
            set
            {
                if (!SetProperty(ref _selectedQuestionAnswerPair, value))
                    return;

                UpdateQuestionCommand.NotifyCanExecuteChanged();
                DeleteQuestionCommand.NotifyCanExecuteChanged();
            }
        }
        #endregion

        #endregion


        #region Команды

        #region Добавление, изменение, удаление категорий вопросов

        #region Добавить категорию
        public RelayCommand CreateQuestionCategoryCommand { get; private set; }

        private void ExecuteCreateQuestionCategoryCommand()
        {

        }
        #endregion

        #region Изменить категорию
        public RelayCommand UpdateQuestionCategoryCommand { get; private set; }

        private void ExecuteUpdateQuestionCategoryCommand()
        {

        }

        private bool CanExecuteUpdateQuestionCategoryCommand() => _selectedQuestionCategory != null;
        #endregion

        #region Удалить категорию
        public RelayCommand DeleteQuestionCategoryCommand { get; private set; }

        private void ExecuteDeleteQuestionCategoryCommand()
        {

        }

        private bool CanExecuteDeleteQuestionCategoryCommand() => _selectedQuestionCategory != null;
        #endregion

        #endregion

        #region Выбор других категорий

        #region Выбрать все вопросы
        public RelayCommand SelectAllQuestionsCommand { get; private set; }

        private void ExecuteSelectAllQuestionsCommand()
        {
            SelectedQuestionCategory = null;
            SelectedQuestionAnswerPairs = AllQuestionAnswerPairs;
        }
        #endregion

        #region Выбрать вопросы без категорий
        public RelayCommand SelectQuestionsWithoutCategoriesCommand { get; private set; }

        private void ExecuteSelectQuestionsWithoutCategoriesCommand()
        {
            SelectedQuestionCategory = null;
            SelectedQuestionAnswerPairs = QuestionAnswerPairsWithoutCategory;
        }
        #endregion

        #endregion

        #region Добавление, изменение, удаление вопросов и ответов

        #region Добавить вопрос
        public RelayCommand CreateQuestionCommand { get; private set; }

        private void ExecuteCreateQuestionCommand()
        {

        }
        #endregion

        #region Изменить вопрос
        public RelayCommand UpdateQuestionCommand { get; private set; }

        private void ExecuteUpdateQuestionCommand()
        {

        }

        private bool CanExecuteUpdateQuestionCommand() => _selectedQuestionAnswerPair != null;
        #endregion

        #region Удалить вопрос
        public RelayCommand DeleteQuestionCommand { get; private set; }

        private void ExecuteDeleteQuestionCommand()
        {

        }

        private bool CanExecuteDeleteQuestionCommand() => _selectedQuestionAnswerPair != null;
        #endregion

        #endregion

        #endregion


        public MainWindowViewModel()
        {
            #region Создания контекста базы данных и заполнения её.
            var isCreatedDataBase = CreateDbContext(out _dbContext);
            var isFillDataBase = true;

            if (isCreatedDataBase && isFillDataBase)
                FillDataBase();
            #endregion

            #region Свойства
            QuestionCategories = _dbContext.QuestionCategories.OrderBy(c => c.Name).ToList();
            foreach (var c in QuestionCategories)
                c.QuestionAnswerPairs = _dbContext.QuestionAnswerPairs.Where(p => p.QuestionCategoryId == c.Id).ToList();

            AllQuestionAnswerPairs = _dbContext.QuestionAnswerPairs.OrderBy(p => p.DateTime).ToList();
            QuestionAnswerPairsWithoutCategory = _dbContext.QuestionAnswerPairs.Where(p => p.QuestionCategory == null).OrderBy(p => p.DateTime).ToList();
            _selectedQuestionAnswerPairs = AllQuestionAnswerPairs;
            #endregion

            #region Команды
            CreateQuestionCategoryCommand = new(ExecuteCreateQuestionCategoryCommand);
            UpdateQuestionCategoryCommand = new(ExecuteUpdateQuestionCategoryCommand, CanExecuteUpdateQuestionCategoryCommand);
            DeleteQuestionCategoryCommand = new(ExecuteDeleteQuestionCategoryCommand, CanExecuteDeleteQuestionCategoryCommand);

            SelectAllQuestionsCommand = new(ExecuteSelectAllQuestionsCommand);
            SelectQuestionsWithoutCategoriesCommand = new(ExecuteSelectQuestionsWithoutCategoriesCommand);

            CreateQuestionCommand = new(ExecuteCreateQuestionCommand);
            UpdateQuestionCommand = new(ExecuteUpdateQuestionCommand, CanExecuteUpdateQuestionCommand);
            DeleteQuestionCommand = new(ExecuteDeleteQuestionCommand, CanExecuteDeleteQuestionCommand);
            #endregion
        }


        #region Методы

        #region Создания контекста базы данных и заполнения её
        private static bool CreateDbContext(out QuestionProjectOfficeContext dbContext)
        {
            var connectionString = ConnectionQuestionProjectOfficeDb.LocalPostgreSqlConnectionString;
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


            var questionAnswerPairs = Enumerable.Range(0, 7).
                Select(x => new QuestionAnswerPair()
                {
                    Question = $"Вопрос №{7 - x}",
                    Answer = $"Ответ №{7 - x}",
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
