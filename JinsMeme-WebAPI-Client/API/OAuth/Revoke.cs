using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace JinsMeme_WebAPI_Client.API.OAuth
{
    /**
     * ログアウト
     * https://jins-meme.github.io/sdkdoc/api/logout.html
     */

    public class Revoke
    {
        public class Request : IRequestable
        {
            // token
            public string Token { get; }

            public Request(string token)
            {
                Token = token;
            }

            public HttpRequestMessage ToHttpRequestMessage()
            {
                var query = new Dictionary<string, string>()
                {
                    { "token", Token },
                };
                var message = new HttpRequestMessage(HttpMethod.Post, Constants.URL.API.OAuth.Token)
                {
                    Content = new FormUrlEncodedContent(query), // Content-Type: application/x-www-form-urlencoded
                };
                message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                return message;
            }
        }

        public class Response : IResponsable
        {
        }
    }
}
