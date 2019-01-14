using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace JinsMeme_WebAPI_Client.Http
{
    public class Server
    {
        public Action<Uri, NameValueCollection> Callback = null;

        HttpListener listener = new HttpListener();

        public Server(string url)
        {
            listener.Prefixes.Add(url);
            Start();
        }

        async void Start()
        {
            listener.Start();

            while (listener.IsListening)
            {
                var context = await listener.GetContextAsync();

                // Request
                var request = context.Request;

                // Callback
                Callback?.Invoke(request.Url, request.QueryString);

                // Response
                var response = context.Response;
                var content = Encoding.UTF8.GetBytes(Constants.Browser.Html);
                response.ContentLength64 = content.Length;
                response.OutputStream.Write(content, 0, content.Length);
                response.Close();
            }
        }

        public void Stop()
        {
            if (listener.IsListening)
            {
                listener.Stop();
            }
        }
    }
}
