using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace CacheWithNewPackage
{
    public class UserService : IUserService
    {
        private readonly ConnectionMultiplexer _cache;

        public UserService()
        {
            _cache = ConnectionMultiplexer.Connect("localhost:6379");
        }

        public async Task<User> GetUser(int id)
        {
            var db = _cache.GetDatabase();

            // Attempt to retrieve user from cache
            var userString = await db.StringGetAsync(id.ToString());
            if (!userString.IsNullOrEmpty)
            {
                Console.WriteLine($"User {id} retrieved from cache.");
                return JsonConvert.DeserializeObject<User>(userString);
            }

            // Simulate fetching user from a data source (e.g., database)
            Thread.Sleep(5000);
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                // Serialize user object and store in cache
                userString = JsonConvert.SerializeObject(user);
                await db.StringSetAsync(id.ToString(), userString, TimeSpan.FromMinutes(2));

                Console.WriteLine($"User {id} added to cache.");
            }

            return user;
        }

        // Simulated user data source
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
