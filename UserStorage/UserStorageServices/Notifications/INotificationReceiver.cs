namespace UserStorageServices.Notifications
{
    public interface INotificationReceiver
    {
        void Receive(string receiveString);
    }
}
