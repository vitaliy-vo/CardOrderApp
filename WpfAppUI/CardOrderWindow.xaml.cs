
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using CardOrderApp.BLL;
using CardOrderApp.Core.Dtos.OrderDto;
using CardOrderApp.Core.Dtos.ProductDto;
using CardOrderApp.Core.Interfaces;
using CardOrderApp.DAL;
using CardOrderApp.DAL.OrderRepository;


namespace WpfAppUI
{
    /// <summary>
    /// Interaction logic for CardOrderWindow.xaml
    /// </summary>
    public partial class CardOrderWindow : Window, INotifyPropertyChanged
    {
        private string _orderStatus = "Ready to order";
        private string _orderDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        private ICustomerRepository  _customerRepository;
        private ICustomerManager? _customerManager;
        private ICardTypeRepository _cardTypeRepository;
        private IOrderRepository _orderRepository;
        private int _customerId;
        private List<string> _paymentSystems = new List<string>();
        private string? _selectedPaymentSystem;
        private List<CardTypeDto> _availableCardTypes = new List<CardTypeDto>();
        private CardTypeDto? _selectedCardType;

        public List<string> PaymentSystems  
        { 
            get => _paymentSystems;
            set
            {
                _paymentSystems = value;
                OnPropertyChanged();
            }
        }

        public string? SelectedPaymentSystem
        {
            get => _selectedPaymentSystem;
            set
            {
                _selectedPaymentSystem = value;
                OnPropertyChanged();
              
                LoadAvailableCardTypes();
            }
        }

        public List<CardTypeDto> AvailableCardTypes
        {
            get => _availableCardTypes;
            set
            {
                _availableCardTypes = value;
                OnPropertyChanged();
            }
        }

        public CardTypeDto? SelectedCardType
        {
            get => _selectedCardType;
            set
            {
                _selectedCardType = value;
                OnPropertyChanged();
            }
        }
        public CardOrderWindow(int customerId,ICustomerRepository customerRepository,ICardTypeRepository cardTypeRepository)
        {
            InitializeComponent();
            _customerRepository = customerRepository;
            _cardTypeRepository = cardTypeRepository;
            DataContext = this;
            _customerId = customerId;
            OrderDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            LoadPaymentSystemData();
            OrderStatus = "Ready to order";
           
            
            // Загружаем данные клиента если ID передан
            if (customerId > 0)
            {
                LoadCustomerData(customerId);
            }
        }

       
        public string OrderStatus
        {
            get => _orderStatus;
            set
            {
                _orderStatus = value;
                OnPropertyChanged();
            }
        }

        public string OrderDateTime
        {
            get => _orderDateTime;
            set
            {
                _orderDateTime = value;
                OnPropertyChanged();
            }
        }

        
        private void LoadPaymentSystemData()
        {
            try
            {    _cardTypeRepository = new CardTypeRepository();
                List<CardTypeDto> allCardTypes = _cardTypeRepository.GetAllTypeCard();
                
              //  var allCardTypes = _cardTypeRepository.GetAllTypeCard();
                
                if (allCardTypes != null && allCardTypes.Any())
                {
                    var paymentSystems = allCardTypes
                        .Where(x => !string.IsNullOrEmpty(x.PaymentSystem))
                        .Select(x => x.PaymentSystem)
                        .Distinct()
                        .ToList();
                    
                    PaymentSystems = paymentSystems;
                    OrderStatus = $"Загружено {paymentSystems.Count} платежных систем";
                }
                else
                {
                    PaymentSystems = new List<string>();
                    OrderStatus = "Нет доступных платежных систем";
                }
            }
            catch (Exception ex)
            {
                OrderStatus = "Ошибка загрузки платежных систем: " + ex.Message;
                PaymentSystems = new List<string>();
            }
        }

        private void LoadAvailableCardTypes()
        {
            try
            {
                if (!string.IsNullOrEmpty(SelectedPaymentSystem))
                {
                    _cardTypeRepository = new CardTypeRepository();
                    List<CardTypeDto> allCardTypes = _cardTypeRepository.GetAllTypeCard();
                    var filteredCardTypes = allCardTypes
                        .Where(x => x.PaymentSystem == SelectedPaymentSystem)
                        .ToList();
                    
                    AvailableCardTypes = filteredCardTypes;
                    OrderStatus = $"Загружено {filteredCardTypes.Count} типов карт для {SelectedPaymentSystem}";
                }
                else
                {
                    AvailableCardTypes = new List<CardTypeDto>();
                }
            }
            catch (Exception ex)
            {
                OrderStatus = "Ошибка загрузки типов карт: " + ex.Message;
                AvailableCardTypes = new List<CardTypeDto>();
            }
        }
        
        
        private void LoadCustomerData(int customerId)
        {
            
            try
            {
               var customer =_customerRepository.GetCustomerById(customerId);
                
                
                TextBoxFullName.Text= customer.FirstName + " " + customer.LastName+" "+customer.MiddleName;
                TextBoxCustomerEmail.Text =customer.Email;
                TextBoxCustomerPhone.Text = customer.PhoneNumber;
                
                
                OrderStatus = "Данные клиента загружены";
            }
            catch (Exception ex)
            {
                OrderStatus = "Ошибка загрузки данных клиента: " + ex.Message;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CreateOrder(object sender, RoutedEventArgs e)
        {
           
          
            _orderRepository = new OrderRepository();
            _orderRepository.CreateOrder(new OrderDto()
            {
               CustomerId = _customerId,
               CardTypeId = Convert.ToInt32(TextBlockCardTypeId.Text),
               OrderDate = DateTime.Now,
               Comment = TextBlockCommetsCard.Text
               
            });
            Close();
        }
    }
}
