using GlobalAeroTechnicalTest.Data;
using GlobalAeroTechnicalTest.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace GlobalAeroTechnicalTest
{
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);            
            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.DataContext = ServiceProvider.GetRequiredService<AirportViewModel>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<AirportDatabase>();
            services.AddTransient<AirportViewModel>();
            services.AddTransient<MainWindow>();
        }
    }
}