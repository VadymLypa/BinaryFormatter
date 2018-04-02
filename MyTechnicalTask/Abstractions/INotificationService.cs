namespace MyTechnicalTask.Abstractions
{
    public interface INotificationService
    {
        void SuccessfulSerialization();
        void UnauthorizedAccessException(string mes);
        void DirNotFoundException();
        void OthersExceptions(string mes);
        void ErrorSerialization(string mes);
        void SuccessfulDeserialization();
        void ErrorDerialization(string mes);
        void ErrorFileNotExist();
    }
}