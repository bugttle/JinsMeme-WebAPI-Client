using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace JinsMeme_WebAPI_Client.API.Users.Me.Office2
{
    /**
     * OFFICE サマリデータ取得
     * https://jins-meme.github.io/sdkdoc/api/get_office2.html#office-サマリデータ取得
     */

    public class SummarizedData
    {
        public class Request : IRequestable
        {
            public string AccessToken { get; }

            // date_from
            public string DateFrom { get; }

            // date_to
            public string DateTo { get; }

            public Request(string accessToken, string dateFrom, string dateTo)
            {
                AccessToken = accessToken;
                DateFrom = dateFrom;
                DateTo = dateTo;
            }

            public HttpRequestMessage ToHttpRequestMessage()
            {
                var query = HttpUtility.ParseQueryString(string.Empty);
                query["date_from"] = DateFrom;
                query["date_to"] = DateTo;

                var builder = new UriBuilder(Constants.URL.API.Users.Me.Office2.SummarizedData);
                builder.Query = query.ToString();

                var message = new HttpRequestMessage(HttpMethod.Get, builder.ToString());
                message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
                return message;
            }
        }
    }
}
