using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TweetSharp;
using MongoDB.Driver;

namespace TestHarnessConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            var mongoDbConnStr = "server=127.0.0.1;database:test";

            var server = MongoServer.Create("mongodb://127.0.0.1");
            var db = server.GetDatabase("test");


            //var coll = db.GetCollection("maroon_place");

            var coll2 = db.GetCollection<Place>("maroon_place");

            var cursor = coll2.FindAll();

            //var places = cursor.Select(x => x.City == "Riverside");
            var result = from r in cursor
                         //where r.City == "Riverside"
                         select r;

            foreach (var p in result)
            {
                Console.WriteLine(p.Name);
                Console.WriteLine(p.Address1);
                Console.WriteLine(p.City);
                Console.WriteLine(p.State);
                Console.WriteLine(p.ZipCode);
                Console.WriteLine(p.County);
                Console.WriteLine(p.Country);

                Console.WriteLine(Environment.NewLine);

            }

            //for (int i = 0; i < coll2.Count(); i++)
            //{
            //    coll2[
            //}
            //foreach (Place p in coll2)
            //{
            //    Console.WriteLine(p.Name);
            //}

            Place mvPlace = new Place
            {
                Name = "TestPlace",
                Address1 = "1234 Any Street",
                City = "AnyTown",
                State = "CA",
                ZipCode = 90000,
                County = "AnyCounty",
                Country =  "USA"
            };

            //var wcr = coll2.Save<Place>(mvPlace);



            Console.Read();

            
            //TwitterClientInfo twitterClientInfo = new TwitterClientInfo();
            //twitterClientInfo.ConsumerKey = ConsumerKey; //Read ConsumerKey out of the app.config
            //twitterClientInfo.ConsumerSecret = ConsumerSecret; //Read the ConsumerSecret out the app.config

            //TwitterService twitterService = new TwitterService(twitterClientInfo);

            ////Now we need the Token and TokenSecret



            ////Firstly we need the RequestToken and the AuthorisationUrl
            //TweetSharp.OAuthRequestToken requestToken = twitterService.GetRequestToken();
            //Uri authUrl = twitterService.GetAuthorizationUri(requestToken);
            //twitterService.AuthenticateWith(ConsumerKey, ConsumerSecret, AccessToken, AccessTokenSecret);

            //IEnumerable<TwitterUser> user =   twitterService.SearchForUser(new SearchForUserOptions());

            //var test = "";


            ////authUrl is just a URL we can open IE and paste it in if we want
            //Console.WriteLine("Please Allow This App to send Tweets on your behalf");
            //Process.Start(authUrl.ToString()); //Launches a browser that'll go to the AuthUrl.


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

        private static string AccessToken
        {

            get { return ConfigurationManager.AppSettings["TwitterAccessToken"]; }
        }
        private static string AccessTokenSecret
        {

            get { return ConfigurationManager.AppSettings["TwitterAccessTokenSecret"]; }

        }

        #endregion
    }
}
