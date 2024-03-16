
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace WebApplication1
{
    public class UserService : IUserService
    {
        private readonly IDistributedCache _cache;

        public UserService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<User> GetUser(int id)
        {
            User? user = null;
            var userString = await _cache.GetStringAsync(id.ToString());
            if (userString != null) user = JsonConvert.DeserializeObject<User>(userString);

            if (user == null)
            {
                Thread.Sleep(5000);
                user = Users.First(x => x.Id == id);
                if (user != null)
                {
                    userString = JsonConvert.SerializeObject(user);
                    await _cache.SetStringAsync(user.Id.ToString(), userString, new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
                    });
                }
            }
            else
            {
                Console.WriteLine($"{user.Name} извлечен из кэша");
            }
            return user;

        }
        public List<User> Users = new List<User>()
        {
            new User { Id = 1, Name = "User1" },
            new User { Id = 2, Name = "User2" },
            new User { Id = 3, Name = "User3" },
            new User { Id = 4, Name = "User4" },
            new User { Id = 5, Name = "User5" },
        };
    }
}
