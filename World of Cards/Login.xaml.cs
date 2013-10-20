using System;
using System.Windows;
using System.Collections.Generic;
using Microsoft.Phone.Controls;
using Microsoft.Live;
using Microsoft.Live.Controls;
using World_of_Cards.Resources;

namespace World_of_Cards
{
    public partial class Login : PhoneApplicationPage
    {
        
        public Login()
        {
            InitializeComponent();
            
        }

        private void BackButton_Click(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.GoBack();
        }

        private async void btnSignin_SessionChanged(object sender, LiveConnectSessionChangedEventArgs e)
        {
            if (e.Status == LiveConnectSessionStatus.Connected)
            {
                //await LiveAPIController.InitializeControllerAsync();
                infoTextBlock.Text = LiveAPIController.userName;
                ProfilePhoto.Source = LiveAPIController.userImage;
            }
            else
            {
                infoTextBlock.Text = "Not signed in.";
            }
        }
    }
}
