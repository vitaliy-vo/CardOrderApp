// Обновленный код в файле MainWindow.xaml.cs
using System.Windows;
using System.Windows.Controls;

namespace ClientSearch
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Загрузка данных при старте
            RefreshData();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            PerformSearch();
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            // Автопоиск при изменении текста (опционально)
            // PerformSearch();
        }

        private void PerformSearch()
        {
            string fioSearch = FioSearchTextBox.Text.ToLower();
            string passportSearch = PassportSearchTextBox.Text.ToLower();
            string cardSearch = CardSearchTextBox.Text.ToLower();

            // Логика поиска (замените на реальную)
            /*
            var filteredClients = allClients.Where(client =>
                (string.IsNullOrEmpty(fioSearch) || client.FullName.ToLower().Contains(fioSearch)) &&
                (string.IsNullOrEmpty(passportSearch) || client.PassportNumber.ToLower().Contains(passportSearch)) &&
                (string.IsNullOrEmpty(cardSearch) || client.CardNumber.ToLower().Contains(cardSearch))
            ).ToList();

            SearchResultsListView.ItemsSource = filteredClients;
            StatusTextBlock.Text = $"Найдено клиентов: {filteredClients.Count}";
            */
        }

        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            // Открытие окна добавления клиента
            var addClientWindow = new AddClientWindow();
            if (addClientWindow.ShowDialog() == true)
            {
                // Клиент добавлен, обновляем данные
                RefreshData();
                StatusTextBlock.Text = "Клиент успешно добавлен";
            }
        }

        private void EditClientButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var client = button?.DataContext as Client;

            if (client != null)
            {
               /* // Открытие окна редактирования
                var editWindow = new EditClientWindow(client);
                if (editWindow.ShowDialog() == true)
                {
                    RefreshData();
                    StatusTextBlock.Text = "Данные клиента обновлены";
                }*/
            }
        }

        private void DeleteClientButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var client = button?.DataContext as Client;

            if (client != null)
            {
                var result = MessageBox.Show($"Вы уверены, что хотите удалить клиента {client.FullName}?",
                    "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Логика удаления клиента
                    // YourDataService.DeleteClient(client.Id);
                    RefreshData();
                    StatusTextBlock.Text = "Клиент удален";
                }
            }
        }

        private void ClearFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            FioSearchTextBox.Text = "";
            PassportSearchTextBox.Text = "";
            CardSearchTextBox.Text = "";
            PerformSearch();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            // Логика экспорта в Excel
            StatusTextBlock.Text = "Экспорт в Excel...";
        }

        private void RefreshData()
        {
            // Загрузка/обновление данных
            // SearchResultsListView.ItemsSource = YourDataService.GetAllClients();
            StatusTextBlock.Text = "Данные обновлены";
        }
    }

    // Модель клиента с дополнительными полями
    public class Client
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PassportNumber { get; set; }
        public string CardNumber { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}