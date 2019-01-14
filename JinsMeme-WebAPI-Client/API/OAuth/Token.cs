using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace JinsMeme_WebAPI_Client.API.OAuth
{
    /**
     * アクセストークン取得
     * https://jins-meme.github.io/sdkdoc/api/get_token.html
     */

    public class Token
    {
        public enum GrantType
        {
            authorization_code,
            refresh_token,
        }

        public class Request : IRequestable
        {
            // grant_type
            public GrantType GrantType { get; }

            // code
            public string Code { get; }

            // redirect_uri
            public string RedirectUri { get; }

            // client_id
            public string ClientId { get; }

            // client_secret
            public string ClientSecret { get; }

            // refresh_token
            public string RefreshToken { get; }

            public Request(GrantType grantType, string redirectUri, string clientId, string clientSecret, string code = null, string refreshToken = null)
            {
                GrantType = grantType;
                RedirectUri = redirectUri;
                ClientId = clientId;
                ClientSecret = clientSecret;
                Code = code; // required when grant_type is "authorization_code"
                RefreshToken = refreshToken; // required when grant_type is "refresh_token"
            }

            public HttpRequestMessage ToHttpRequestMessage()
            {
                var query = new Dictionary<string, string>()
                {
                    { "grant_type", GrantType.ToString() },
                    { "redirect_uri", RedirectUri },
                    { "client_id", ClientId },
                    { "client_secret", ClientSecret },
                };
                switch (GrantType)
                {
                    case GrantType.authorization_code:
                        query.Add("code", Code);
                        break;

                    case GrantType.refresh_token:
                        query.Add("refresh_token", RefreshToken);
                        break;
                }
                var message = new HttpRequestMessage(HttpMethod.Post, Constants.URL.API.OAuth.Token)
                {
                    Content = new FormUrlEncodedContent(query), // Content-Type: application/x-www-form-urlencoded
                };
                message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return message;
            }
        }

        public class Response : IResponsable
        {
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }

            [JsonProperty("refresh_token")]
            public string RefreshToken { get; set; }

            [JsonProperty("token_type")]
            public string TokenType { get; set; }

            [JsonProperty("expires_in")]
            public int ExpiresIn { get; set; }

            [JsonProperty("scope")]
            public string Scope { get; set; }
        }
    }
}
