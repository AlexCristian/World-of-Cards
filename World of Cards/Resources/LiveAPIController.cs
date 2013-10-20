using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Live;
using Microsoft.Live.Controls;
using System.Windows.Media.Imaging;
using Windows.Security.Authentication.OnlineId;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using System.IO;
using System.Net;
using System.Windows.Threading;
using System.Diagnostics;

namespace World_of_Cards.Resources
{
    /*
     *  Make sure to be logged in before attempting to initialize the class or else it won't work ;) 
     */
    public static class LiveAPIController
    {
        private static LiveConnectClient client;
        private static LiveConnectSession session;
        public static String appClientID = "0000000040101A45";
        public static String userName { get; set; }
        public static BitmapImage userImage { get; set; }
        public static String photoURI { get; set; }
        public static bool isInitialized = false;

        public static async Task InitializeControllerAsync(MainPage page)
        {
            if (isInitialized)
                return;
            await LoginWithMicrosoft();
            userName = await fetchUserNameAsync();
            userImage = await fetchUserProfileImageAsync();
            page.LoginName.Text = "Welcome back, " + LiveAPIController.userName;
        }

        public async static void attemptAutoConnect(MainPage page)
        {
            var auth = new LiveAuthClient(appClientID);
            LiveLoginResult result;
            page.LoginName.Text = "Logging you in...";
            result = await auth.InitializeAsync(new[] { "wl.basic", "wl.offline_access" });
            if (result.Status == LiveConnectSessionStatus.Connected)
            {
                client = new LiveConnectClient(result.Session);
                session = result.Session;
                isInitialized = true;
                userName = await fetchUserNameAsync();
                userImage = await fetchUserProfileImageAsync();
                page.LoginName.Text = "Welcome back, " + LiveAPIController.userName;
            }
            else
            {
                page.LoginName.Text = "Hi, Guest!";
            }
        }
        public async static Task LoginWithMicrosoft()
        {
            var auth = new LiveAuthClient(appClientID);
            LiveLoginResult result;
            result = await auth.InitializeAsync(new[] { "wl.basic", "wl.offline_access" });
            if (result.Status == LiveConnectSessionStatus.Connected)
            {
                client = new LiveConnectClient(result.Session);
                session = result.Session;
                isInitialized = true;
            }
            else
            {
                while (result.Status != LiveConnectSessionStatus.Connected)
                {
                    result = await auth.LoginAsync(new[] { "wl.basic", "wl.offline_access" });
                }
                client = new LiveConnectClient(result.Session);
                session = result.Session;
                isInitialized = true;
            }
        }
    
        public static async Task<String> fetchUserNameAsync()
        {
            client = new LiveConnectClient(session);
            LiveOperationResult operationResult = await client.GetAsync("me");
            try
            {
                dynamic meResult = operationResult.Result;
                if (meResult.first_name != null &&
                    meResult.last_name != null)
                {
                    return meResult.first_name + " " +
                           meResult.last_name;
                }
                else
                {
                    return "User";
                }
            }
            catch (LiveConnectException exception)
            {
                return "User";
            }
        }

        public static async Task<BitmapImage> fetchUserProfileImageAsync()
        {
            try
            {
                LiveConnectClient liveClient = new LiveConnectClient(session);
                LiveOperationResult operationResult = await liveClient.GetAsync("me/picture");
                dynamic result = operationResult.Result;
                BitmapImage image = new BitmapImage(new Uri(result.location, UriKind.Absolute));
                // Load stream with bitmap data asynchronously above

                photoURI = result.location;

                return image;

            }
            catch (Exception exception) //Catches everything
            {
                Debug.WriteLine(exception.StackTrace);
                return new BitmapImage(new Uri("Assets/DefaultUserImage.png"));
            }
        }

    }
}
