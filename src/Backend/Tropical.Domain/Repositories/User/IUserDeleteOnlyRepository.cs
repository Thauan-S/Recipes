namespace Tropical.Domain.Repositories.User
{
    public interface IUserDeleteOnlyRepository
    {
        Task DeleteAccount(Guid userIdentifyer);
    }
}
