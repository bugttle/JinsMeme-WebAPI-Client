using Reactive.Bindings;
using System;
using System.ComponentModel;
using System.Reactive.Disposables;

namespace JinsMeme_WebAPI_Client.ViewModel
{
    public class MainModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        CompositeDisposable Disposable { get; } = new CompositeDisposable();

        // OAuth
        public OAuth.Authentication OAuth_Authentication { get; } = new OAuth.Authentication();

        // Office2
        public Users.Me.Office2.Events Office2_Events { get; } = new ViewModel.Users.Me.Office2.Events();

        public Users.Me.Office2.ComputedData Office2_ComputedData { get; } = new ViewModel.Users.Me.Office2.ComputedData();
        public Users.Me.Office2.SummarizedData Office2_SummarizedData { get; } = new ViewModel.Users.Me.Office2.SummarizedData();

        // Run
        public Users.Me.Run.Events Run_Events { get; } = new ViewModel.Users.Me.Run.Events();

        // Drive2
        public Users.Me.Drive2.ComputedData Drive2_ComputedData { get; } = new ViewModel.Users.Me.Drive2.ComputedData();

        public Users.Me.Drive2.SummarizedData Drive2_SummarizedData { get; } = new ViewModel.Users.Me.Drive2.SummarizedData();

        public ReactiveProperty<string> Result { get; } = new ReactiveProperty<string>("");

        public MainModel()
        {
            OAuth_Authentication.AccessToken.Subscribe(v =>
            {
                Office2_Events.AccessToken.Value = v;
                Office2_ComputedData.AccessToken.Value = v;
                Office2_SummarizedData.AccessToken.Value = v;
                Run_Events.AccessToken.Value = v;
                Drive2_ComputedData.AccessToken.Value = v;
                Drive2_SummarizedData.AccessToken.Value = v;
            });
        }

        public void Dispose()
        {
            Disposable.Dispose();
        }
    }
}
