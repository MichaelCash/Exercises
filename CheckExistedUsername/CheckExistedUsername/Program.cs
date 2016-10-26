using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckExistedUsername
{
    class Program
    {
        static void Main(string[] args)
        {
            var users = new List<UserModel>() {
                new UserModel() { Username = "Michael", Age= 0},
                new UserModel() { Username = "Linda", Age= 2},
                new UserModel() { Username = "Thor", Age= 3},
                new UserModel() { Username = "william", Age= 0},
                new UserModel() { Username = "Michael", Age= 5},
                new UserModel() { Username = "Michael", Age= 6},
                new UserModel() { Username = "Michael", Age= 7},
                new UserModel() { Username = "Michael", Age= 8},
                new UserModel() { Username = "Michael", Age= 9},
                new UserModel() { Username = "Tester", Age= 1},
                new UserModel() { Username = "Michael", Age= 1},
                new UserModel() { Username = "Michael", Age= 1},
                new UserModel() { Username = "Michael", Age= 1},
            };
            Parallel.ForEach(users, u =>
            {
                UserService.Add(u);
            });
          //  UserService.ResetDB();
            
            //UserService.Add(user, true);

            foreach (var u in UserService.UserInDB) {
                Console.WriteLine(u.Username +  ",Age =" + u.Age);
            }

            var usernameList = AppCache.GetOrSet(UserService.UserNameCache, UserService.GetUsernames);
            Console.WriteLine("-----------------------");
            foreach (var u in usernameList)
            {
                Console.WriteLine(u);
            }
            Console.ReadLine();
        }
    }
}
