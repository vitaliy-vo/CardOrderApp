using Autofac.Builder;
using CardOrderApp.BLL;
using CardOrderApp.Core.Interfaces;
using CardOrderApp.Core.OutputModels;
using CardOrderApp.DAL;
using CardOrderApp.DAL.CustomerRepository;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.Windows;
using System.Windows.Controls;
using CardOrderApp.DAL.OrderRepository;
using WpfAppUI;

namespace WpfAppUI
{
    public partial class MainWindow : Window
    {
        private readonly ICustomerManager _customerManager;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerDocumentRepository _customerDocumentRepository;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly ICardTypeRepository _cardTypeRepository;
        private ObservableCollection<Client> _clients = new ObservableCollection<Client>();
        public ObservableCollection<Client> Clients => _clients;

        public MainWindow()
        {
            InitializeComponent();
            _customerManager = new CustomerManager();
            _customerRepository = new CustomerRepository();
            _customerDocumentRepository = new CustomerDocumentRepository();
            _documentTypeRepository = new DocumentTypeRepository();
            this.DataContext = this;
            RefreshData();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            PerformSearch();
        }



        private void PerformSearch()
        {
            string fioSearch = FioSearchTextBox.Text.ToLower();
            string passportSearchSeries = PassportSeries.Text.ToLower();
            string passportSearchNumer = PassportNum.Text.ToLower();

            List<Client> clientList = _clients.ToList();

            var results = clientList.FindAll(c =>
                c.FullName.ToLower().Contains(fioSearch) && 
                c.PassportSeries.ToLower().Contains(passportSearchSeries)
                && c.PassportNumber.ToLower().Contains(passportSearchNumer));
            SearchResultsListView.ItemsSource = results.ToList();

        }

        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            // Открытие окна добавления клиента
            var addClientWindow = new AddClientWindow(_customerManager,
                _customerRepository,
                _customerDocumentRepository,
                _documentTypeRepository);
            if (addClientWindow.ShowDialog() == true)
            {
                
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
                var selectedItem = SearchResultsListView.SelectedItem;
                if (selectedItem != null)
                {
                    var item = selectedItem as Client;

                    EditClientWindow edit = new EditClientWindow(item, _customerRepository, _documentTypeRepository,_customerDocumentRepository);
                    edit.Show(); 

                }
            }
            RefreshData();
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
                    _customerRepository.UnAnctiveCustomer(_customerRepository.GetCustomerById(client.Id));
                    RefreshData();
                    StatusTextBlock.Text = "Клиент удален";
                }
            }
        }

        private void ClearFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            FioSearchTextBox.Text = "";
            PassportSeries.Text = "";
            PassportNum.Text = "";

            PerformSearch();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }


        private void RefreshData()
        {
            try
            {

                var customers = _customerManager.GetAllCustomer();
                _clients.Clear();
                foreach (var customer in customers)
                {
                    var client = new Client
                    {
                        Id = customer.CustomerId, 
                        FullName = customer.FullName,
                        PassportNumber = customer.PassportNumber,
                        PassportSeries = customer.PassportSeries,
                        BirthDate=customer.BirthDate,
                        Phone = customer.PhoneNumber,
                        Email = customer.Email


                    };
                    _clients.Add(client);

                }


                ;
                StatusTextBlock.Text = "Данные обновлены";
            }
            catch (Exception ex)
            {
                StatusTextBlock.Text = $"Ошибка загрузки: {ex.Message}";
            }
        }

        private void CreadeOrderCard(object sender, RoutedEventArgs e)
        {
            var selectedItem = SearchResultsListView.SelectedItem;
            if (selectedItem != null)
            {
                var item = selectedItem as Client;
                StatusTextBlock.Text = item.Id.ToString();
                
              
                var cardOrderWindow = new CardOrderWindow(item.Id,_customerRepository,_cardTypeRepository);
                cardOrderWindow.Show(); 
            }
           
        }
    }

    // Модель клиента с дополнительными полями
    public class Client
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string PassportSeries { get; set; }
        public string PassportNumber { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string DocumentType { get; set; }
        public DateTime? DocumentIssueDate { get; set; }
        public string DepartmentCode { get; set; }
        public DateTime RegistrationDate { get; set; }
    }


    
    
}
