namespace WebApplication1
{
    public interface IUserService
    {
        Task<User> GetUser(int  id);    
    }
}
