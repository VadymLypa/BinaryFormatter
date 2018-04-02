using System.Windows;
using MyTechnicalTask.Abstractions;

namespace MyTechnicalTask.Services
{
    public class NotificationService : INotificationService
    {
        public void SuccessfulSerialization()
        {
            MessageBox.Show("My Congratulation you are successful serialize your files!", "Congratulatuib", MessageBoxButton.OK,
                MessageBoxImage.Information, MessageBoxResult.OK,
                MessageBoxOptions.ServiceNotification);
        }

        public void DirNotFoundException()
        {
            MessageBox.Show("Directory not found", "Error!", MessageBoxButton.OK,
                MessageBoxImage.Error, MessageBoxResult.OK,
                MessageBoxOptions.ServiceNotification);
        }

        public void OthersExceptions(string mes)
        {
            MessageBox.Show(mes, "Error!", MessageBoxButton.OK,
                MessageBoxImage.Error, MessageBoxResult.OK,
                MessageBoxOptions.ServiceNotification);
        }

        public void ErrorSerialization(string mes)    
        {
            MessageBox.Show(mes, "Error!", MessageBoxButton.OK,
                MessageBoxImage.Error, MessageBoxResult.OK,
                MessageBoxOptions.ServiceNotification);
        }

        public void UnauthorizedAccessException(string mes)
        {
            MessageBox.Show(mes,"Error!",MessageBoxButton.OK,
                MessageBoxImage.Error, MessageBoxResult.OK,
                MessageBoxOptions.ServiceNotification);
        }

        public void SuccessfulDeserialization()
        {
            MessageBox.Show("My Congratulation you are successful deserialize your files!", "Congratulatuib", MessageBoxButton.OK,
                MessageBoxImage.Information, MessageBoxResult.OK,
                MessageBoxOptions.ServiceNotification);
        }

        public void ErrorDerialization(string mes)
        {
            MessageBox.Show("Some data deserialize, but " + mes, "Error!", MessageBoxButton.OK,
                MessageBoxImage.Error, MessageBoxResult.OK,
                MessageBoxOptions.ServiceNotification);
        }

        public void ErrorFileNotExist()
        {
            MessageBox.Show("Sorry! You can't deserialize. File not exists!", "Error!", MessageBoxButton.OK,
                MessageBoxImage.Error, MessageBoxResult.OK,
                MessageBoxOptions.ServiceNotification);
        }
    }
}