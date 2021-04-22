using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using MDEV1014PracticeProject.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDEV1014PracticeProject
{
    public enum MainNavOption {
        WelcomePage,
        AfterLoginPage
    }

    public partial class App : Application
    {
        

        public App()
        {
            InitializeComponent();
            CheckLogin();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }


        private void CheckLogin() {

            var savedUsername = GetProperty(KEYS.KEY_USERNAME);
            var savedUserToken = GetProperty(KEYS.KEY_USERTOKEN);
            if (savedUsername != null && savedUserToken != null)
            {
                Debug.WriteLine($"A logged in user found:{savedUsername}  with token:{savedUserToken}");
                NavigateMain(MainNavOption.AfterLoginPage);
            }
            else {
                Debug.WriteLine($"No logged in user found");
                NavigateMain(MainNavOption.WelcomePage);
            }

        }


        public void SignIn() {
            NavigateMain(MainNavOption.AfterLoginPage);
        }

        public async Task SignOutAsync() {
            Properties.Remove(KEYS.KEY_USERNAME);
            Properties.Remove(KEYS.KEY_USERTOKEN);
            await SavePropertiesAsync();
            NavigateMain(MainNavOption.WelcomePage);
        }


        private void NavigateMain(MainNavOption navOption) {

            switch (navOption) {
                case MainNavOption.WelcomePage:
                    MainPage = new NavigationPage(new WelcomePage());
                    break;
                case MainNavOption.AfterLoginPage:
                    MainPage = new AfterLoginPage();
                    break;

            }
        }


        //App Persistent storage

        public async Task<bool> SavePropertyAsync(string key, object value) {
            Properties[key] = value;
            await SavePropertiesAsync();
            return true;
        }

        public object GetProperty(string key) {
            if (Properties.ContainsKey(key))
                return Properties[key];

            return null;
        }



    }
}
