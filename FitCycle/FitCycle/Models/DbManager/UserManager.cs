using FitCycle.Views;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FitCycle.Models.DbManager
{
    public partial class UserManager
    {
        static UserManager defaultInstance = new UserManager();
        MobileServiceClient client;
        IMobileServiceTable<User> userTable;
        User currentUser;

        private UserManager()
        {
            
        }

        public void Init()
        {
            client = new MobileServiceClient(Constants.ApplicationURL);
            userTable = client.GetTable<User>();
        }

        public static UserManager DefaultManager
        {
            get
            {
                return defaultInstance;
            }
            private set
            {
                defaultInstance = value;
            }
        }
        public MobileServiceClient CurrentClient
        {
            get { return client; }
        }

        public async Task<User> Login(string mailadress, string password)
        {
            Init();

            try
            {
                IEnumerable<User> userdata = await userTable.Where(u => u.MailAdress == mailadress).ToEnumerableAsync();
                var passwordsalt_fromServer = userdata.First().Password_Salt;
                var passwordhash_fromServer = userdata.First().Password;
                SHA256Managed sha256hash = new SHA256Managed();
                var buff = System.Text.Encoding.UTF8.GetBytes(password + passwordsalt_fromServer);
                var buffhash = sha256hash.ComputeHash(buff);
                var passwordhash_fromUser = "#";
                foreach (int num in buffhash)
                {
                    passwordhash_fromUser += num;
                }
                if (passwordhash_fromServer == passwordhash_fromUser)
                    return userdata.FirstOrDefault();
                else
                    return null;
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine("Invalid sync operation: {0}", new[] { msioe.Message });
                return null;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
                return null;
            }

        }



        public async Task<bool> CreateUser(User user,string password)
        {
            Init();
            currentUser = user;
            CreatePassword(password);
            try
            {
                await userTable.InsertAsync(user);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
                return false;
            }
            return true;
        }


        private void CreatePassword(string password)
        {
            var passwordsalt = CreatePasswordSalt();
            SHA256Managed sha256hash = new SHA256Managed();
            var buff = System.Text.Encoding.UTF8.GetBytes(password + passwordsalt);
            var buffhash = sha256hash.ComputeHash(buff);
            var passwordhash = "#";
            foreach (int num in buffhash)
            {
                passwordhash += num;
            }
            currentUser.Password = passwordhash;
            currentUser.Password_Salt = passwordsalt;
        }
        private string CreatePasswordSalt()
        {
            Random rnd = new Random();
            var buff = new byte[2];
            var passwordsalt = string.Empty;
            rnd.NextBytes(buff);
            foreach (int num in buff)
            {
                passwordsalt += num;
            }
            return passwordsalt;
        }
    }

}
