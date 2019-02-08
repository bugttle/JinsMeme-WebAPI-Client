using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace JinsMeme_WebAPI_Client.ViewModel.Users.Me.Drive2
{
    public class SummarizedData : INotifyPropertyChanged, IDisposable
    {
#pragma warning disable 0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        CompositeDisposable Disposable { get; } = new CompositeDisposable();

        public ReactiveProperty<string> AccessToken { get; } = new ReactiveProperty<string>("");
        public ReactiveProperty<string> DateFrom { get; } = new ReactiveProperty<string>("");
        public ReactiveProperty<string> DateTo { get; } = new ReactiveProperty<string>("");

        public ReactiveCommand ExecuteCommand { get; }

        public SummarizedData()
        {
            ExecuteCommand = new[]
            {
                AccessToken.Select(v => !string.IsNullOrWhiteSpace(v)),
                DateFrom.Select(v => !string.IsNullOrWhiteSpace(v)),
                DateTo.Select(v => !string.IsNullOrWhiteSpace(v)),
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
