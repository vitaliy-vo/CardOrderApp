using Autofac;
using CardOrderApp.BLL;
using CardOrderApp.Core.Interfaces;
using CardOrderApp.DAL;
using CardOrderApp.DAL.CustomerRepository;
using System.Windows;


namespace WpfAppUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IContainer _container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Создаем контейнер
            var builder = new ContainerBuilder();

            // Регистрируем компоненты
            builder.RegisterType<CustomerManager>().As<ICustomerManager>();
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();
            builder.RegisterType<CustomerDocumentRepository>().As<ICustomerDocumentRepository>();
            builder.RegisterType<DocumentTypeRepository>().As<IDocumentTypeRepository>();
            builder.RegisterType<CardTypeRepository>().As<ICardTypeRepository>();

            builder.RegisterType<MainWindow>();
            _container = builder.Build();

        }

    }
}
