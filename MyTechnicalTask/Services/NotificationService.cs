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

        public void ErrorSerialization()
        {
            MessageBox.Show("Sorry! You can't serialize", "Error!", MessageBoxButton.OK,
                MessageBoxImage.Error, MessageBoxResult.OK,
                MessageBoxOptions.ServiceNotification);
        }

        public void SuccessfulDeserialization()
        {
            MessageBox.Show("My Congratulation you are successful deserialize your files!", "Congratulatuib", MessageBoxButton.OK,
                MessageBoxImage.Information, MessageBoxResult.OK,
                MessageBoxOptions.ServiceNotification);
        }

        public void ErrorDerialization()
        {
            MessageBox.Show("Sorry! You can't deserialize. Please input location or file to deserialize", "Error!", MessageBoxButton.OK,
                MessageBoxImage.Error, MessageBoxResult.OK,
                MessageBoxOptions.ServiceNotification);
        }
        
    }
}