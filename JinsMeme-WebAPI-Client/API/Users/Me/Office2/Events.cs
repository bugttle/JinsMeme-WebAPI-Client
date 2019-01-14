using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace JinsMeme_WebAPI_Client.API.Users.Me.Office2
{
    /**
     * OFFICE APPイベント取得
     * https://jins-meme.github.io/sdkdoc/api/get_office2.html#office-appイベント取得
     */

    public class Events
    {
        public enum Fields
        {
            id,
            updated_at,
            json_data,
            data_file_url,
            created_at,
        }

        public enum Sort
        {
            updated_at,
            created_at,
        }

        public class Request : IRequestable
        {
            public string AccessToken { get; }

            // type
            public string Type { get; }

            // id
            public string Id { get; }

            // measured_from
            public string MeasuredFrom { get; }

            // measured_to
            public string MeasuredTo { get; }

            // limit
            public int? Limit { get; }

            // cursor
            public string Cursor { get; }

            // fields
            public IEnumerable<Fields> Fields { get; }

            // sort
            public Sort? Sort { get; }

            public Request(string accessToken, string type, string id = null, string measured_from = null, string measured_to = null, int? limit = null, string cursor = null, IEnumerable<Fields> fields = null, Sort? sort = null)
            {
                AccessToken = accessToken;
                Type = type;
                Id = id;
                MeasuredFrom = measured_from;
                MeasuredTo = measured_to;
                Limit = limit;
                Cursor = cursor;
                Fields = fields;
                Sort = sort;
            }

            public HttpRequestMessage ToHttpRequestMessage()
            {
                var query = HttpUtility.ParseQueryString(string.Empty);
                query["type"] = Type;
                if (!string.IsNullOrEmpty(Id))
                {
                    query["id"] = Id;
                }
                if (!string.IsNullOrEmpty(MeasuredFrom))
                {
                    query["measured_from"] = MeasuredFrom;
                }
                if (!string.IsNullOrEmpty(MeasuredTo))
                {
                    query["measured_to"] = MeasuredTo;
                }
                if (Limit.HasValue)
                {
                    query["limit"] = Limit.ToString();
                }
                if (!string.IsNullOrEmpty(Cursor))
                {
                    query["cursor"] = Cursor;
                }
                if (Fields != null)
                {
                    query["fields"] = string.Join(",", Fields);
                }
                if (Sort.HasValue)
                {
                    query["sort"] = Sort.ToString();
                }

                var builder = new UriBuilder(Constants.URL.API.Users.Me.Office2.Events);
                builder.Query = query.ToString();

                var message = new HttpRequestMessage(HttpMethod.Get, builder.ToString());
                message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
                return message;
            }
        }
    }
}
