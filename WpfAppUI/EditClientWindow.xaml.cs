using CardOrderApp.BLL;
using CardOrderApp.Core.Dtos.Customer;
using CardOrderApp.Core.InputModels;
using CardOrderApp.Core.Interfaces;
using CardOrderApp.DAL;
using CardOrderApp.DAL.CustomerRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfAppUI
{
    /// <summary>
    /// Interaction logic for EditClientWindow.xaml
    /// </summary>
    public partial class EditClientWindow : Window
    {
        private Client _editingClient;
        private ICustomerRepository _customerRepository;
        private  IDocumentTypeRepository _documentTypeRepository;
        private  ICustomerDocumentRepository _customerDocumentRepository;
        Dictionary<string, DocumetTypeDto> _documentTypeDict;

        public EditClientWindow(Client client, ICustomerRepository customerRepository, IDocumentTypeRepository documentTypeRepository,ICustomerDocumentRepository customerDocumentRepository)
        {
            InitializeComponent();
            _editingClient = client;
            LoadClientData();
            _customerRepository = customerRepository;
            _documentTypeRepository = documentTypeRepository;
            _customerDocumentRepository = customerDocumentRepository;
        }


        private void LoadClientData()
        {
            // Загрузка данных клиента в поля формы
            FullNameTextBox.Text = _editingClient.FullName;
            BirthDatePicker.SelectedDate = _editingClient.BirthDate;
            PhoneTextBox.Text = _editingClient.Phone;
            EmailTextBox.Text = _editingClient.Email;

            

            // Загрузка типа документа
            if (!string.IsNullOrEmpty(_editingClient.DocumentType))
            {
                foreach (ComboBoxItem item in DocumentTypeComboBox.Items)
                {
                    if (item.Content.ToString() == _editingClient.DocumentType)
                    {
                        DocumentTypeComboBox.SelectedItem = item;
                        break;
                    }
                }
            }
            else
            {
                DocumentTypeComboBox.SelectedIndex = 0; 
            }

            // Загрузка данных документа
            SeriesTextBox.Text = _editingClient.PassportSeries;
            NumberTextBox.Text = _editingClient.PassportNumber;
            IssueDatePicker.SelectedDate = _editingClient.DocumentIssueDate;
            
        }
    


    private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Валидация данных
            if (string.IsNullOrWhiteSpace(FullNameTextBox.Text))
            {
                MessageBox.Show("Поле ФИО обязательно для заполнения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Сохранение изменений
            var nameParts = FullNameTextBox.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            
            _editingClient.BirthDate = BirthDatePicker.SelectedDate;
            _editingClient.Phone = PhoneTextBox.Text.Trim();
            _editingClient.Email = EmailTextBox.Text.Trim();
            CustomerDto customerDto = new CustomerDto()
            {
                Id= _editingClient.Id,
                FirstName = nameParts[0],
                LastName = nameParts[1],
                MiddleName = nameParts.Length > 2 ? nameParts[2] : null,
                BirthDate = BirthDatePicker.SelectedDate ?? DateTime.Now,
                PhoneNumber = PhoneTextBox.Text,
                Email = EmailTextBox.Text,
                IsActive = true

            };
            _customerRepository.UpdateCustomer(customerDto);

            _documentTypeDict = _documentTypeRepository.GetAllDocumentTypes();

            List<CustomerDocumentDto> getDocumentsByCustomerId = _customerDocumentRepository.GetDocumentsByCustomerId(customerDto.Id);

            int DocumentTypeId = _documentTypeDict[DocumentTypeComboBox.Text].DocumentTypeId;
            getDocumentsByCustomerId.Where(x => x.DocumentTypeId == DocumentTypeId).ToList();


            // Сохранение данных документа
            _editingClient.DocumentType = DocumentTypeComboBox.SelectedItem != null ?
                ((ComboBoxItem)DocumentTypeComboBox.SelectedItem).Content.ToString() : string.Empty;
            _editingClient.PassportSeries = SeriesTextBox.Text.Trim();
            _editingClient.PassportNumber = NumberTextBox.Text.Trim();
            _editingClient.DocumentIssueDate = IssueDatePicker.SelectedDate;
            _editingClient.DepartmentCode = DepartmentCodeTextBox.Text.Trim();

            CustomerDocumentDto customerDocumentDto = new CustomerDocumentDto()
            {
                DocumentId = getDocumentsByCustomerId[0].DocumentId,
                СustomerId = customerDto.Id,
                DocumentTypeId = DocumentTypeId,
                Series = SeriesTextBox.Text.Trim(),
                Number = NumberTextBox.Text.Trim(),
                IssueDate = (DateTime)IssueDatePicker.SelectedDate,
                DepartmentCode = DepartmentCodeTextBox.Text.Trim()
            };
            _customerDocumentRepository.UpdateDocumet(customerDocumentDto);



            Close();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

    }
}
