using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using static JinsMeme_WebAPI_Client.API.Users.Me.Office2.Events;

namespace JinsMeme_WebAPI_Client.ViewModel.Users.Me.Office2
{
    public class Events : INotifyPropertyChanged, IDisposable
    {
#pragma warning disable 0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        CompositeDisposable Disposable { get; } = new CompositeDisposable();

        public ReactiveProperty<string> AccessToken { get; } = new ReactiveProperty<string>("");
        public ReactiveProperty<string> Type { get; } = new ReactiveProperty<string>("");
        public ReactiveProperty<string> Id { get; } = new ReactiveProperty<string>("");
        public ReactiveProperty<string> MeasuredFrom { get; } = new ReactiveProperty<string>("");
        public ReactiveProperty<string> MeasuredTo { get; } = new ReactiveProperty<string>("");
        public ReactiveProperty<string> Limit { get; } = new ReactiveProperty<string>("");

        public int? ParsedLimit
        {
            get
            {
                int limit;
                if (int.TryParse(Limit.Value, out limit))
                {
                    return limit;
                }
                return null;
            }
        }

        public ReactiveProperty<string> Cursor { get; } = new ReactiveProperty<string>("");
        public ReactiveProperty<bool> Fields_id { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> Fields_updated_at { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> Fields_json_data { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> Fields_data_file_url { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> Fields_data_created_at { get; } = new ReactiveProperty<bool>();

        public IEnumerable<Fields> Fields
        {
            get
            {
                if (Fields_id.Value || Fields_updated_at.Value || Fields_json_data.Value || Fields_data_file_url.Value || Fields_data_created_at.Value)
                {
                    var fields = new List<Fields>();
                    if (Fields_id.Value)
                    {
                        fields.Add(API.Users.Me.Office2.Events.Fields.id);
                    }
                    if (Fields_updated_at.Value)
                    {
                        fields.Add(API.Users.Me.Office2.Events.Fields.updated_at);
                    }
                    if (Fields_json_data.Value)
                    {
                        fields.Add(API.Users.Me.Office2.Events.Fields.json_data);
                    }
                    if (Fields_data_file_url.Value)
                    {
                        fields.Add(API.Users.Me.Office2.Events.Fields.data_file_url);
                    }
                    if (Fields_data_created_at.Value)
                    {
                        fields.Add(API.Users.Me.Office2.Events.Fields.created_at);
                    }
                    return fields;
                }
                return null;
            }
        }

        public ReactiveProperty<string> Sort { get; } = new ReactiveProperty<string>("");

        public Sort? ParsedSort
        {
            get
            {
                Sort sort;
                if (Enum.TryParse(Sort.Value, out sort))
                {
                    return sort;
                }
                return null;
            }
        }

        public ReactiveCommand ExecuteCommand { get; }

        public Events()
        {
            ExecuteCommand = new[]
            {
                AccessToken.Select(v => !string.IsNullOrWhiteSpace(v)),
                Type.Select(v => !string.IsNullOrWhiteSpace(v)),
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
