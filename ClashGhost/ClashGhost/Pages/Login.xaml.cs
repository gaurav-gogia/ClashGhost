using ClashGhost.Managers;
using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ClashGhost.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        RegisterData data;
        public Login()
        {
            this.InitializeComponent();
            data = new RegisterData();
        }

        private async void next_Click(object sender, RoutedEventArgs e)
        {
            next.Visibility = Visibility.Collapsed;
            logring.IsActive = true;
            logring.Visibility = Visibility.Visible;

            data.UID = username.Text;
            #region nono zone
            string privkey = Constants.PRIV;
            string pubkey = Constants.PUBL;
            #endregion
            data.Password = HashKarenge.CreateHash(privkey, pubkey, passbox.Password);
            
            try
            {
                var resultant = await LoginCall.LogInsideAsync(data);
                
                if (!(resultant.Equals("Failed :(")))
                    Frame.Navigate(typeof(Profile), data);
            }

            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            finally
            {
                logring.IsActive = false;
                logring.Visibility = Visibility.Visible;
                next.Visibility = Visibility.Visible;
            }
        }
    }
}
