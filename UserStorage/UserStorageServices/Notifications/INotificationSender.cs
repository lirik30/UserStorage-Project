namespace UserStorageServices.Notifications
{
    public interface INotificationSender
    {
        void Send(NotificationContainer container);
    }
}
