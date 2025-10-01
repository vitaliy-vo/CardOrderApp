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
        private ICustomerManager _customerManager=new CustomerManager();
        private ICustomerRepository _customerRepository;
        private ICustomerDocumentRepository _customerDocumentRepository;
        private IDocumetTypeRepository _documetTypeRepository;
        Dictionary<String, int> dicId;


        public AddClientWindow()
        {
            InitializeComponent();
            _documetTypeRepository = new DocumetTypeRepository();
            _customerDocumentRepository=new CustomerDocumentRepository();
            Dictionary<int, DocumetTypeDto> documentType = _documetTypeRepository.GetTypeDocuments();
            dicId = new Dictionary<string,int>();
            dicId.Clear();
            ComboBox.Items.Clear();
            foreach (var item in documentType)
            {
                ComboBox.Items.Add(item.Value.Name);
                dicId.Add(item.Value.Name, item.Value.DocumentTypeId);
            }
            

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _customerRepository = new CustomerRepository();
            

            // Валидация данных
            if (string.IsNullOrWhiteSpace(FullNameTextBox.Text))
            {
                MessageBox.Show("Введите ФИО клиента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            String fullName = FullNameTextBox.Text;

     

            // Создание нового клиента
            var newClient = new CustomerInputModel
            {
                FirstName = fullName.Split(' ')[0],
                LastName = fullName.Split(' ')[1],
                MiddleName = fullName.Split(' ')[2],
                BirthDate = Datapicker.DisplayDate,
                PhoneNumber = PhoneTextBox.Text,
                Email = EmailTextBox.Text,
                IsActive = true,
                RegistrationDate = DateTime.Now


            };



           CustomerDto customer= _customerManager.AddClient(newClient);
           
          int idCustomer= _customerRepository.CreateCustomer(customer);

            var newDocument = new CustomerDocumentInputModel
            {
      
            СustomerId= idCustomer,
                 DocumentTypeId= dicId.GetValueOrDefault(ComboBox.Text),
                Series= PSeries.Text,
                Number= PNum.Text,
                IssueDate= Datapicker2.DisplayDate,
                DepartmentCode= KP.Text

            };
            CustomerDocumentDto customerDocumentDto= _customerManager.AddDocument(newDocument);

            _customerDocumentRepository.CreateDocument(customerDocumentDto);




            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}

