using System.Windows;
using Extensibility;
using MessageBox = System.Windows.MessageBox;
using MessageBoxOptions = System.Windows.MessageBoxOptions;

namespace Services
{
    public class NotificationService : INotificationService
    {
        private const string SuccessfulSerializationMessage =
            "My congratulation you are successful serialize your files!";

        private const string SuccessfulDeserializationMessage =
            "My congratulation you are successful deserialize your files!";

        private const string DirNotFoundMessage = "Directory not found!";

        public static void ShowSuccessulMessageBox(string message)
        {
            MessageBox.Show(message, "Congratulation", MessageBoxButton.OK, MessageBoxImage.Information,
                MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
        }
        
        public static void ShowErrorMessageBox(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error,
                MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
        }

        public void SuccessfulSerialization() => ShowSuccessulMessageBox(SuccessfulSerializationMessage);

        public void DirNotFoundException() => ShowErrorMessageBox(DirNotFoundMessage);

        public void ErrorSerialization(string message) => ShowErrorMessageBox(message);

        public void ErrorDeSerialization(string message) => ShowErrorMessageBox(message);

        public void SuccessfulDeserialization() => ShowSuccessulMessageBox(SuccessfulDeserializationMessage);

    }
}