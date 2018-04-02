using System.Windows;
using Extensibility;
using Microsoft.Practices.Unity;
using MyTechnicalTask.Views;
using Prism.Unity;
using Services;

namespace MyTechnicalTask
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            Container.RegisterType<INotificationService, NotificationService>();
            Container.RegisterType<IBinaryService, BinaryService>();

            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            if (Application.Current.MainWindow != null)
                Application.Current.MainWindow.Show();
        }
    }
}
