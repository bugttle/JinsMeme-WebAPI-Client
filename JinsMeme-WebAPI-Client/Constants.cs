namespace JinsMeme_WebAPI_Client
{
    public static class Constants
    {
        public static class URL
        {
            public static readonly string RedirectUrl = "http://localhost:18080/";

            public static class Browser
            {
                public static readonly string Authorize = "https://accounts.jins.com/jp/ja/oauth/authorize";
            }

            public static class API
            {
                public static class OAuth
                {
                    public static readonly string Token = "https://apis.jins.com/meme/v1/oauth/token";
                    public static readonly string Revoke = "https://apis.jins.com/meme/v1/oauth/revoke";
                }

                public static class Users
                {
                    public static class Me
                    {
                        public static class Office2
                        {
                            public static readonly string Events = "https://apis.jins.com/meme/v1/users/me/office2/events";
                            public static readonly string ComputedData = "https://apis.jins.com/meme/v1/users/me/office2/computed_data";
                            public static readonly string SummarizedData = "https://apis.jins.com/meme/v1/users/me/office2/summarized_data";
                        }

                        public static class Run
                        {
                            public static readonly string Events = "https://apis.jins.com/meme/v1/users/me/run/events";
                        }

                        public static class Drive2
                        {
                            public static readonly string ComputedData = "https://apis.jins.com/meme/v1/users/me/run/computed_data";
                            public static readonly string SummarizedData = "https://apis.jins.com/meme/v1/users/me/drive2/summarized_data";
                        }
                    }
                }
            }
        }

        // state string length
        public static class Auth
        {
            public static int StateLength = 128;
        }

        public static class Browser
        {
            public static string Html = @"
<!DOCTYPE html>
<html lang=""ja"">
<head>
  <meta charset=""UTF-8"" />
  <title>OAuth完了</title>
</head>
<body>
OAuthによる認証が完了したので、自動的にこのウィンドウを閉じます。
<script type=""text/javascript"">
setTimeout(function() {
  window.close();
}, 3000);
</script>
</body>
</html>";
        }
    }
}
