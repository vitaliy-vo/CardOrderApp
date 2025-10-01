using System.Windows;

namespace ClientSearch
{
    public partial class AddClientWindow : Window
    {
        public AddClientWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Валидация данных
            if (string.IsNullOrWhiteSpace(FullNameTextBox.Text))
            {
                MessageBox.Show("Введите ФИО клиента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Создание нового клиента
            var newClient = new Client
            {
                FullName = FullNameTextBox.Text,
                PassportNumber = PassportTextBox.Text,
                CardNumber = CardNumberTextBox.Text,
                Phone = PhoneTextBox.Text,
                Email = EmailTextBox.Text,
                RegistrationDate = DateTime.Now
            };

            // Сохранение в базу данных
            // YourDataService.AddClient(newClient);

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