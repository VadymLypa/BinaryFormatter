namespace MyTechnicalTask.Abstractions
{
    public interface INotificationService
    {
        void SuccessfulSerialization();
        void ErrorSerialization();
        void SuccessfulDeserialization();
        void ErrorDerialization();
        void ErrorFileNotExist();
    }
}