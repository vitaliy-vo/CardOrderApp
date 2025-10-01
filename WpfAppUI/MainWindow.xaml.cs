using CardOrderApp.BLL;
using CardOrderApp.Core.Interfaces;
using CardOrderApp.DAL.CustomerRepository;
using System.Windows;
using System.Windows.Controls;
using WpfAppUI;

namespace WpfAppUI
{
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
          
            RefreshData();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            PerformSearch();
        }

        

        private void PerformSearch()
        {
            string fioSearch = FioSearchTextBox.Text.ToLower();
            string passportSearch = PassportSearchTextBox.Text.ToLower();
            

          
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
                   
                    RefreshData();
                    StatusTextBlock.Text = "Клиент удален";
                }
            }
        }

        private void ClearFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            FioSearchTextBox.Text = "";
            PassportSearchTextBox.Text = "";
         
            PerformSearch();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }

        
        private void RefreshData()
        {
            CustomerManager customer =new CustomerManager();

           // _customerRepository = new CustomerRepository();
            // Загрузка/обновление данных
            SearchResultsListView.ItemsSource = customer.GetAllCustomer();
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