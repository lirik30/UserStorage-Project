namespace UserStorageServices
{
    public interface ISubscriber
    {
        void UserAdded(object sender, StorageChangeEventArgs eventArgs);

        void UserRemoved(object sender, StorageChangeEventArgs eventArgs);
    }
}
