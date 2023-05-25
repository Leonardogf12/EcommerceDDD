namespace ApplicationApp.Interfaces
{
    public interface InterfaceGenericApp<T> where T : class
    {
        Task Add(T Object);

        Task Update(T Object);

        Task Delete(T Object);

        Task<T> GetEntityById(int Id);

        Task<List<T>> List();
    }
}
