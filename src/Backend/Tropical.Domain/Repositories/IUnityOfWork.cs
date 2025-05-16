namespace Tropical.Domain.Repositories
{
    public interface IUnityOfWork
    {
        public  Task Commit();
    }
}
