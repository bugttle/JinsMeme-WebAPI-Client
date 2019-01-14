using JinsMeme_WebAPI_Client.Http;
using JinsMeme_WebAPI_Client.ViewModel;
using System.Collections.Generic;
using System.Windows;

namespace JinsMeme_WebAPI_Client
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        Client client = new Client();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainModel();

            Http.Server server = new Http.Server(Constants.URL.RedirectUrl);
            server.Callback = (url, query) =>
            {
                if (url.AbsolutePath == "/")
                {
                    // callback?code=67890fgehi&state=12345abcde
                    var model = DataContext as MainModel;
                    var response = new Browser.Authorize.Response(query);
                    if (model.OAuth_Authentication.State.Value != response.State)
                    {
                        MessageBox.Show("Received illegal state", "Error", MessageBoxButton.OK);
                    }
                    else
                    {
                        HandleAuthCallback(response.Code);
                    }
                }
            };
        }

        async void HandleAuthCallback(string code)
        {
            var model = DataContext as MainModel;
            var grantType = API.OAuth.Token.GrantType.authorization_code;
            var redirectUri = model.OAuth_Authentication.RedirectUrl.Value;
            var clientId = model.OAuth_Authentication.ApplicationId.Value;
            var clientSecret = model.OAuth_Authentication.ApplicationSecret.Value;
            var request = new API.OAuth.Token.Request(grantType, redirectUri, clientId, clientSecret, code: code);
            var response = await client.SendRequestAsObjectAsync<API.OAuth.Token.Response>(request);
            model.OAuth_Authentication.AccessToken.Value = response.AccessToken;
            model.OAuth_Authentication.RefreshToken.Value = response.RefreshToken;
        }

        void OAuth_GetAccessTokenButton_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as MainModel;
            var clientId = model.OAuth_Authentication.ApplicationId.Value;
            var redirectUri = model.OAuth_Authentication.RedirectUrl.Value;
            var state = model.OAuth_Authentication.State.Value;
            var scopes = new List<Browser.Authorize.Scope>();
            if (model.OAuth_Authentication.Scope_office.Value)
            {
                scopes.Add(Browser.Authorize.Scope.office);
            }
            if (model.OAuth_Authentication.Scope_run.Value)
            {
                scopes.Add(Browser.Authorize.Scope.run);
            }
            if (model.OAuth_Authentication.Scope_drive.Value)
            {
                scopes.Add(Browser.Authorize.Scope.drive);
            }
            var loginHint = model.OAuth_Authentication.EmailAddress.Value;
            var request = new Browser.Authorize.Request(clientId, redirectUri, state, scopes, loginHint);
            client.OpenBrowser(request);
        }

        async void OAuth_RefreshAccessTokenButton_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as MainModel;
            var grantType = API.OAuth.Token.GrantType.refresh_token;
            var redirectUri = model.OAuth_Authentication.RedirectUrl.Value;
            var clientId = model.OAuth_Authentication.ApplicationId.Value;
            var clientSecret = model.OAuth_Authentication.ApplicationSecret.Value;
            var refreshToken = model.OAuth_Authentication.RefreshToken.Value;
            var request = new API.OAuth.Token.Request(grantType, redirectUri, clientId, clientSecret, refreshToken: refreshToken);
            var response = await client.SendRequestAsObjectAsync<API.OAuth.Token.Response>(request);
            model.OAuth_Authentication.AccessToken.Value = response.AccessToken;
            model.OAuth_Authentication.RefreshToken.Value = response.RefreshToken;
        }

        async void OAuth_RevokeAccessTokenButton_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as MainModel;
            var token = model.OAuth_Authentication.AccessToken.Value;
            var request = new API.OAuth.Revoke.Request(token);
            var response = await client.SendRequestAsObjectAsync<API.OAuth.Revoke.Response>(request);
            model.OAuth_Authentication.AccessToken.Value = "";
            model.OAuth_Authentication.RefreshToken.Value = "";
        }

        async void Office2_EventsButton_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as MainModel;

            var accessToken = model.Office2_Events.AccessToken.Value;
            var type = model.Office2_Events.Type.Value;
            var id = model.Office2_Events.Id.Value;
            var measuredFrom = model.Office2_Events.MeasuredFrom.Value;
            var measuredTo = model.Office2_Events.MeasuredTo.Value;
            var limit = model.Office2_Events.ParsedLimit;
            var cursor = model.Office2_Events.Cursor.Value;
            var fields = model.Office2_Events.Fields;
            var sort = model.Office2_Events.ParsedSort;
            var request = new API.Users.Me.Office2.Events.Request(accessToken, type, id, measuredFrom, measuredTo, limit, cursor, fields, sort);
            var result = await client.SendRequestAsStringAsync(request);
            model.Result.Value = result;
        }

        async void Office2_ComputedDataButton_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as MainModel;

            var accessToken = model.Office2_ComputedData.AccessToken.Value;
            var dateFrom = model.Office2_ComputedData.DateFrom.Value;
            var dateTo = model.Office2_ComputedData.DateTo.Value;
            var cursor = model.Office2_ComputedData.Cursor.Value;
            var request = new API.Users.Me.Office2.ComputedData.Request(accessToken, dateFrom, dateTo, cursor);
            var result = await client.SendRequestAsStringAsync(request);
            model.Result.Value = result;
        }

        async void Office2_SummarizedDataButton_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as MainModel;

            var accessToken = model.Office2_SummarizedData.AccessToken.Value;
            var dateFrom = model.Office2_SummarizedData.DateFrom.Value;
            var dateTo = model.Office2_SummarizedData.DateTo.Value;
            var request = new API.Users.Me.Office2.SummarizedData.Request(accessToken, dateFrom, dateTo);
            var result = await client.SendRequestAsStringAsync(request);
            model.Result.Value = result;
        }

        async void Run_EventsButton_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as MainModel;

            var accessToken = model.Run_Events.AccessToken.Value;
            var type = model.Run_Events.Type.Value;
            var id = model.Run_Events.Id.Value;
            var measuredFrom = model.Run_Events.MeasuredFrom.Value;
            var measuredTo = model.Run_Events.MeasuredTo.Value;
            var limit = model.Run_Events.ParsedLimit;
            var cursor = model.Run_Events.Cursor.Value;
            var fields = model.Run_Events.Fields;
            var sort = model.Run_Events.ParsedSort;
            var request = new API.Users.Me.Run.Events.Request(accessToken, type, id, measuredFrom, measuredTo, limit, cursor, fields, sort);
            var result = await client.SendRequestAsStringAsync(request);
            model.Result.Value = result;
        }

        async void Drive2_ComputedDataButton_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as MainModel;

            var accessToken = model.Drive2_ComputedData.AccessToken.Value;
            var dateFrom = model.Drive2_ComputedData.DateFrom.Value;
            var dateTo = model.Drive2_ComputedData.DateTo.Value;
            var cursor = model.Drive2_ComputedData.Cursor.Value;
            var request = new API.Users.Me.Drive2.ComputedData.Request(accessToken, dateFrom, dateTo, cursor);
            var result = await client.SendRequestAsStringAsync(request);
            model.Result.Value = result;
        }

        async void Drive2_SummarizedDataButton_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as MainModel;

            var accessToken = model.Drive2_SummarizedData.AccessToken.Value;
            var dateFrom = model.Drive2_SummarizedData.DateFrom.Value;
            var dateTo = model.Drive2_SummarizedData.DateTo.Value;
            var request = new API.Users.Me.Drive2.SummarizedData.Request(accessToken, dateFrom, dateTo);
            var result = await client.SendRequestAsStringAsync(request);
            model.Result.Value = result;
        }

        // Event
        void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var model = DataContext as MainModel;
            Properties.Settings.Default.Auth_ApplicationId = model.OAuth_Authentication.ApplicationId.Value;
            Properties.Settings.Default.Auth_ApplicationSecret = model.OAuth_Authentication.ApplicationSecret.Value;
            Properties.Settings.Default.Auth_EmailAddress = model.OAuth_Authentication.EmailAddress.Value;
            Properties.Settings.Default.Auth_Scope_office = model.OAuth_Authentication.Scope_office.Value;
            Properties.Settings.Default.Auth_Scope_run = model.OAuth_Authentication.Scope_run.Value;
            Properties.Settings.Default.Auth_Scope_drive = model.OAuth_Authentication.Scope_drive.Value;
            Properties.Settings.Default.Save();
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var model = DataContext as MainModel;
            model.OAuth_Authentication.ApplicationId.Value = Properties.Settings.Default.Auth_ApplicationId;
            model.OAuth_Authentication.ApplicationSecret.Value = Properties.Settings.Default.Auth_ApplicationSecret;
            model.OAuth_Authentication.EmailAddress.Value = Properties.Settings.Default.Auth_EmailAddress;
            model.OAuth_Authentication.Scope_office.Value = Properties.Settings.Default.Auth_Scope_office;
            model.OAuth_Authentication.Scope_run.Value = Properties.Settings.Default.Auth_Scope_run;
            model.OAuth_Authentication.Scope_drive.Value = Properties.Settings.Default.Auth_Scope_drive;
        }
    }
}
