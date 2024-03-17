namespace CacheWithNewPackage
{
    public interface IUserService
    {
        Task<User> GetUser(int  id);    
    }
}
