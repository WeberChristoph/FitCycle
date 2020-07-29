using FitCycle.Views;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FitCycle.Models.DbManager
{
    public partial class ExerciseManager
    {
        MainPage mainPage { get => Application.Current.MainPage as MainPage; }
        static ExerciseManager defaultInstance = new ExerciseManager();
        MobileServiceClient client;
        MobileServiceSQLiteStore store;
        IMobileServiceSyncTable<Exercise> exerciseTable;


        public async Task Init()
        {
            if (client?.SyncContext?.IsInitialized ?? false)
                return;
            client = new MobileServiceClient(Constants.ApplicationURL);
            store = new MobileServiceSQLiteStore(Constants.offlineDbPath);
            store.DefineTable<Exercise>();

            await client.SyncContext.InitializeAsync(store);
            exerciseTable = client.GetSyncTable<Exercise>();
        }

        public static ExerciseManager DefaultManager
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

        public async Task SyncExercise()
        {
            await Init();

            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                    return;
                await client.SyncContext.PushAsync();
                await exerciseTable.PullAsync("CurrentUser", exerciseTable.Where(d => d.UserId == mainPage.User.UserID));
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine("Invalid sync operation: {0}", new[] { msioe.Message });
            }
            catch (Exception e)
            {
                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
            }
        }

        public async Task<IEnumerable<Exercise>> GetExercises()
        {
            await Init();
            await SyncExercise();

            var data = await exerciseTable.OrderBy(d => d.Name).ToEnumerableAsync();
            return data;
        }


    }

}
