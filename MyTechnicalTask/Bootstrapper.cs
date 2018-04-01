using System.Windows;
using MyTechnicalTask.Abstractions;
using MyTechnicalTask.Services;
using Microsoft.Practices.Unity;
using MyTechnicalTask.Views;
using Prism.Unity;

namespace MyTechnicalTask
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            Container.RegisterType<INotificationService, NotificationService>();

            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            if (Application.Current.MainWindow != null)
                Application.Current.MainWindow.Show();
        }
    }
}
