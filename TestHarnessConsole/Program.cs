using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TweetSharp;

namespace TestHarnessConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            TwitterClientInfo twitterClientInfo = new TwitterClientInfo();
            twitterClientInfo.ConsumerKey = ConsumerKey; //Read ConsumerKey out of the app.config
            twitterClientInfo.ConsumerSecret = ConsumerSecret; //Read the ConsumerSecret out the app.config
            
            TwitterService twitterService = new TwitterService(twitterClientInfo);

            //Now we need the Token and TokenSecret

            

            //Firstly we need the RequestToken and the AuthorisationUrl
            TweetSharp.OAuthRequestToken requestToken = twitterService.GetRequestToken();
            Uri authUrl = twitterService.GetAuthorizationUri(requestToken);
            twitterService.AuthenticateWith(ConsumerKey, ConsumerSecret, AccessToken, AccessTokenSecret);

            IEnumerable<TwitterUser> user =   twitterService.SearchForUser(new SearchForUserOptions());

            var test = "";




            
            //authUrl is just a URL we can open IE and paste it in if we want
            Console.WriteLine("Please Allow This App to send Tweets on your behalf");
            Process.Start(authUrl.ToString()); //Launches a browser that'll go to the AuthUrl.
        }

        #region ConsumerKey & ConsumerSecret
        private static string ConsumerSecret
        {
            get { return ConfigurationManager.AppSettings["TwitterConsumerSecret"]; }
        }

        private static string ConsumerKey
        {
            get { return ConfigurationManager.AppSettings["TwitterConsumerKey"]; }
        }

        private static string AccessToken{

            get { return ConfigurationManager.AppSettings["TwitterAccessToken"]; }
        }
        private static string AccessTokenSecret
        {

            get { return ConfigurationManager.AppSettings["TwitterAccessTokenSecret"]; }

        }

        #endregion
    }
}
