namespace Extensibility
{
    public interface INotificationService
    {
        void SuccessfulSerialization();
        void SuccessfulDeserialization();
        void DirNotFoundException();
        void ErrorSerialization(string message);
        void ErrorDeSerialization(string message);
    }
}