using CardOrderApp.BLL;
using CardOrderApp.Core.Dtos.Customer;
using CardOrderApp.Core.InputModels;
using CardOrderApp.Core.Interfaces;
using CardOrderApp.DAL;
using CardOrderApp.DAL.CustomerRepository;
using System.Windows;

namespace WpfAppUI
{
    /// <summary>
    /// Interaction logic for AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {
        private readonly ICustomerManager _customerManager;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerDocumentRepository _customerDocumentRepository;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        Dictionary<string, DocumetTypeDto> _documentTypeDict;



        public AddClientWindow(
            ICustomerManager customerManager,
            ICustomerRepository customerRepository,
            ICustomerDocumentRepository customerDocumentRepository,
            IDocumentTypeRepository documentTypeRepository)
        {
            _customerManager = customerManager;
            _customerRepository = customerRepository;
            _customerDocumentRepository= customerDocumentRepository;
            _documentTypeRepository = documentTypeRepository;
            _documentTypeDict = _documentTypeRepository.GetAllDocumentTypes();
            InitializeComponent();


        }





        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // 1. Data Validation
            if (string.IsNullOrWhiteSpace(FullNameTextBox.Text))
            {
                MessageBox.Show("Введите ФИО клиента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            
            var nameParts = FullNameTextBox.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (nameParts.Length < 2)
            {
                MessageBox.Show("Введите фамилию и имя (через пробел)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_documentTypeDict == null)
            {
                MessageBox.Show("Выберите тип документа", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!string.IsNullOrEmpty(PhoneTextBox.Text) &&
                           !System.Text.RegularExpressions.Regex.IsMatch(PhoneTextBox.Text,
                           @"^[\d\s\-\+\(\)]+$"))
            {
                MessageBox.Show("Телефон не соответствует формату", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

                try
            {
                // 2. Create Customer
                var newClient = new CustomerInputModel
                {
                    FirstName = nameParts[0],
                    LastName = nameParts[1],
                    MiddleName = nameParts.Length > 2 ? nameParts[2] : null, 
                    BirthDate = Datapicker.SelectedDate ?? DateTime.Now, 
                    PhoneNumber = PhoneTextBox.Text,
                    Email = EmailTextBox.Text,
                    IsActive = true,
                    RegistrationDate = DateTime.Now
                };



                CustomerDto customer = _customerManager.AddClient(newClient);
                int idCustomer = _customerRepository.CreateCustomer(customer);


            

                // 3. Create Document
                 var newDocument = new CustomerDocumentInputModel
                 {
                     СustomerId = idCustomer,
                     DocumentTypeId = _documentTypeDict[ComboBox.Text].DocumentTypeId,
                     Series = PSeries.Text,
                     Number = PNum.Text,
                     IssueDate = Datapicker2.SelectedDate ?? DateTime.Now, 
                     DepartmentCode = KP.Text
                 };

                 CustomerDocumentDto customerDocumentDto = _customerManager.AddDocument(newDocument);
                 _customerDocumentRepository.CreateDocument(customerDocumentDto);

                 DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
               MessageBox.Show($"Произошла ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}

