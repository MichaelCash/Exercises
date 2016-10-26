using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckExistedUsername
{
    public class UserService
    {
        public const string UserNameCache = "user_name_cached";

        public static List<UserModel> UserInDB = new List<UserModel>();

        public static object lockObject = new object(); 

        public static int Add(UserModel user)
        {
            // Todo: insert user to database.;
            if (!CheckExistedUsername(user.Username))
            {
                InsertToDB(user);
            }
            return user.UserId;
        }

        public static bool CheckExistedUsername(string username) {
            lock (lockObject)
            {
                var usernameList = AppCache.GetOrSet(UserNameCache, GetUsernames);
                if (usernameList.Contains(username))
                {
                    return true;
                }
                else
                {
                    usernameList.Add(username);
                    return false;
                }
            }
        }

        private static int InsertToDB(UserModel user)
        {
            // fail if password = empty
            if (user.Age == 0)
            {
                var usernameList = AppCache.GetOrSet(UserNameCache, GetUsernames);
                lock (usernameList)
                {
                    usernameList.Remove(user.Username);
                }
                return 0;
            }
            user.UserId = UserInDB.Count + 1;
            UserInDB.Add(user);
            return user.UserId;
        }




        public static HashSet<string> GetUsernames()
        {
            return new HashSet<string>();
        }

        public static void ResetDB()
        {
            UserInDB = new List<UserModel>();
        }
    }
}
