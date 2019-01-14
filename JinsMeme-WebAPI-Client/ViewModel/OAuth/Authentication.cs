using JinsMeme_WebAPI_Client.Utils;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace JinsMeme_WebAPI_Client.ViewModel.OAuth
{
    public class Authentication : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        CompositeDisposable Disposable { get; } = new CompositeDisposable();

        public ReactiveProperty<string> ApplicationId { get; } = new ReactiveProperty<string>("");
        public ReactiveProperty<string> ApplicationSecret { get; } = new ReactiveProperty<string>("");
        public ReactiveProperty<string> RedirectUrl { get; } = new ReactiveProperty<string>(Constants.URL.RedirectUrl);
        public ReactiveProperty<string> EmailAddress { get; } = new ReactiveProperty<string>("");
        public ReactiveProperty<bool> Scope_office { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> Scope_run { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> Scope_drive { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<string> State { get; } = new ReactiveProperty<string>(StringUtils.RandomAlphabetNumbersString(Constants.Auth.StateLength));
        public ReactiveProperty<string> AccessToken { get; } = new ReactiveProperty<string>("");
        public ReactiveProperty<string> RefreshToken { get; } = new ReactiveProperty<string>("");

        public ReactiveCommand GetAccessTokenCommand { get; }
        public ReactiveCommand RefreshAccessTokenCommand { get; }
        public ReactiveCommand RevokeAccessTokenCommand { get; }

        public Authentication()
        {
            // Get access token
            GetAccessTokenCommand = new[]
            {
                ApplicationId.Select(v => !string.IsNullOrWhiteSpace(v)),
                ApplicationSecret.Select(v => !string.IsNullOrWhiteSpace(v)),
                RedirectUrl.Select(v => !string.IsNullOrWhiteSpace(v)),
                EmailAddress.Select(v => !string.IsNullOrWhiteSpace(v)),
            }
            .CombineLatestValuesAreAllTrue()
            .ToReactiveCommand(false)
            .AddTo(Disposable);

            // Refresh access token
            RefreshAccessTokenCommand = new[]
            {
                AccessToken.Select(v => !string.IsNullOrWhiteSpace(v)),
                RefreshToken.Select(v => !string.IsNullOrWhiteSpace(v)),
            }
            .CombineLatestValuesAreAllTrue()
            .ToReactiveCommand(false)
            .AddTo(Disposable);

            // Revoke access token
            RevokeAccessTokenCommand = new[]
            {
                AccessToken.Select(v => !string.IsNullOrWhiteSpace(v)),
                RefreshToken.Select(v => !string.IsNullOrWhiteSpace(v)),
            }
            .CombineLatestValuesAreAllTrue()
            .ToReactiveCommand(false)
            .AddTo(Disposable);
        }

        public void Dispose()
        {
            Disposable.Dispose();
        }
    }
}
