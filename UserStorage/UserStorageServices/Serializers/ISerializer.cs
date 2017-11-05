namespace UserStorageServices.Serializers
{
    public interface ISerializer<T>
    {
        void Serialize(T data);

        T Deserialize();
    }
}
