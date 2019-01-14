using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace JinsMeme_WebAPI_Client.API.Users.Me.Office2
{
    /**
     * OFFICE 計測データ取得
     * https://jins-meme.github.io/sdkdoc/api/get_office2.html#office-計測データ取得
     */

    public class ComputedData
    {
        public class Request : IRequestable
        {
            public string AccessToken { get; }

            // date_from
            public string DateFrom { get; }

            // date_to
            public string DateTo { get; }

            // cursor
            public string Cursor { get; }

            public Request(string accessToken, string dateFrom, string dateTo, string cursor = null)
            {
                AccessToken = accessToken;
                DateFrom = dateFrom;
                DateTo = dateTo;
                Cursor = cursor;
            }

            public HttpRequestMessage ToHttpRequestMessage()
            {
                var query = HttpUtility.ParseQueryString(string.Empty);
                query["date_from"] = DateFrom;
                query["date_to"] = DateTo;
                if (!string.IsNullOrEmpty(Cursor))
                {
                    query["cursor"] = Cursor;
                }

                var builder = new UriBuilder(Constants.URL.API.Users.Me.Office2.ComputedData);
                builder.Query = query.ToString();

                var message = new HttpRequestMessage(HttpMethod.Get, builder.ToString());
                message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
                return message;
            }
        }
    }
}
