using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;

namespace JinsMeme_WebAPI_Client.Browser
{
    public class Authorize
    {
        public enum ResponseType
        {
            code,
        }

        public enum Scope
        {
            office,
            run,
            drive,
        }

        public enum ServiceId
        {
            meme
        }

        /**
         * 認可を要求する
         * https://jins-meme.github.io/sdkdoc/api/web_integration.html#認可を要求する
         */

        public class Request : IRequestable
        {
            // response_type
            public ResponseType ResponseType { get; } = ResponseType.code; // fixed

            // client_id
            public string ClientId { get; }

            // redirect_uri
            public string RedirectUri { get; }

            // state
            public string State { get; }

            // scope
            public IEnumerable<Scope> Scopes { get; }

            // service_id
            public ServiceId ServiceId { get; } = ServiceId.meme; // fixed

            // login_hint
            public string LoginHint { get; }

            public Request(string clientId, string redirectUri, string state, IEnumerable<Scope> scopes, string loginHint)
            {
                ClientId = clientId;
                RedirectUri = redirectUri;
                State = state;
                Scopes = scopes;
                LoginHint = loginHint;
            }

            public string ToURL()
            {
                var query = HttpUtility.ParseQueryString(string.Empty);
                query["response_type"] = ResponseType.ToString();
                query["client_id"] = ClientId;
                query["redirect_uri"] = RedirectUri;
                query["state"] = State;
                query["scope"] = string.Join(" ", Scopes);
                query["service_id"] = ServiceId.ToString();
                query["login_hint"] = LoginHint;
                return $"{Constants.URL.Browser.Authorize}?{query.ToString()}";
            }
        }

        /**
         * 認可コードを取得する
         * https://jins-meme.github.io/sdkdoc/api/web_integration.html#認可コードを取得する
         */

        public class Response
        {
            public string Code { get; }

            public string State { get; }

            public Response(NameValueCollection query)
            {
                Code = query["code"];
                State = query["state"];
            }
        }
    }
}
