using MiningConsole.Abstract;
using MiningConsole.Concrete;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiningConsole.Model
{

    public class RequestMrr
    {
        public async Task<dynamic> GetResponseRig(string Mkey, string Msecret)
        {
            hash_hmac hmac = new hash_hmac();
            string Key = Mkey;
            string Secret = Msecret;
            double mtime = Math.Round((DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds * 10000);
            string endpoint = "/rig/mine";
            string sign_string = Key + mtime.ToString() + endpoint;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            var req = WebRequest.Create(@"https://www.miningrigrentals.com/api/v2" + endpoint);
            req.Headers.Add("x-api-sign:" + hmac.sha1(sign_string, Secret));
            req.Headers.Add("x-api-key:" + Key);
            req.Headers.Add("x-api-nonce:" + mtime.ToString());
            var r = await req.GetResponseAsync();
            StreamReader responseReader = new StreamReader(r.GetResponseStream());
            return await responseReader.ReadToEndAsync();
        }
        static float random(double max, double min)
        {

            Random rnd = new Random();
            return (float)(rnd.NextDouble() * (max - min) + min);
        }

        public async void Upload()
        {
            RequestBtc btc = new RequestBtc();
            EFMrrRepository repository = new EFMrrRepository();
            EFUserRepository user = new EFUserRepository();

            hash_hmac hmac = new hash_hmac();
            try
            {
                //repository.CreateMrr(99999, "Constantin", random(693.000000000000, 713.999999999999), "th");
                //repository.CreateMrr(99999, "Constantin1", random(224.000000000000, 244.999999999999), "th");
                //repository.CreateMrr(99999, "kutsenko", random(298.000000000000, 300.999999999999), "th");
                repository.CreateMrr(99999, "qwerty", random(59000.999999999999, 61000.000000000000), "th");
                //repository.CreateMrr(99999, "an_serv_pr_ug", random(62.000000000000, 65.999999999999), "th");
                //repository.CreateMrr(99999, "Doroshin_da", random(65.000000000000, 69.999999999999), "th");
                //repository.CreateMrr(99999, "Andrejkurmashev", random(133.000000000000, 137.999999999999), "th");
                //repository.CreateMrr(99999, "startvpered", random(11.999999999999, 15.000000000000), "th");

                //foreach (var useronce in user.Users)
                //{
                //    try
                //    {
                //        Console.WriteLine("{0} {1} {2}", useronce.Login, useronce.Mkey, useronce.Msecret);
                //    }
                //    catch (Exception e)
                //    {
                //        Console.WriteLine(e);
                //    }
                //}
                foreach (var useronce in user.Users)
                {
                    if (useronce.Mkey != null && useronce.Msecret != null)
                    {
                        try
                        {
                            var responseData = "";
                            string check = "False";
                            while (check == "False")
                            {
                                responseData = await GetResponseRig(useronce.Mkey, useronce.Msecret);
                                check = JObject.Parse(responseData)["success"].ToString();
                            }

                            JObject obj = JObject.Parse(responseData);
                            dynamic jsonDe = JsonConvert.DeserializeObject(obj["data"].ToString());
                            //if (useronce.Login == "Fonogin")
                            //{
                            //    Console.WriteLine("Fonogin");
                            //    Console.WriteLine(jsonDe);
                            //}
                            string ID = "";
                            foreach (JObject typeStr in jsonDe)
                            {
                                ID += typeStr["id"].ToString() + ";";
                            }
                            if (!string.IsNullOrEmpty(ID))
                            {
                                //Console.WriteLine(ID);
                                var req1 = WebRequest.Create(@"https://www.miningrigrentals.com/api/v2/rig/" + ID);
                                var r1 = await req1.GetResponseAsync();
                                StreamReader responseReader1 = new StreamReader(r1.GetResponseStream());
                                var responseData1 = await responseReader1.ReadToEndAsync();
                                JObject obj1 = JObject.Parse(responseData1);
                                double sum = 0;
                                try
                                {
                                    dynamic jsonDe1 = JsonConvert.DeserializeObject(obj1["data"].ToString());
                                    if (useronce.Login == "Fonogin")
                                    {
                                        Console.WriteLine(jsonDe1);
                                    }
                                    if (jsonDe1.GetType().ToString() == "Newtonsoft.Json.Linq.JArray")
                                    {
                                        foreach (JObject typeStr in jsonDe1)
                                        {
                                            //Console.WriteLine(float.Parse(typeStr["hashrate"]["last_5min"]["hash"].ToString(), CultureInfo.InvariantCulture) + " " + typeStr["hashrate"]["last_5min"]["hash"].ToString());
                                            if (float.Parse(typeStr["hashrate"]["last_5min"]["hash"].ToString(), CultureInfo.InvariantCulture) > 0)
                                            {

                                                repository.CreateMrr(int.Parse(typeStr["id"].ToString()), useronce.Login, float.Parse(typeStr["hashrate"]["last_5min"]["hash"].ToString(), CultureInfo.InvariantCulture), typeStr["hashrate"]["last_5min"]["type"].ToString());
                                                sum += float.Parse(typeStr["hashrate"]["last_5min"]["hash"].ToString(), CultureInfo.InvariantCulture);
                                            }
                                        }
                                        if (sum == 0) btc.Upload(useronce); else btc.CreateZero(useronce);
                                    }
                                    else
                                    {
                                        if (float.Parse(jsonDe1["hashrate"]["last_5min"]["hash"].ToString(), CultureInfo.InvariantCulture) > 0)
                                        {

                                            repository.CreateMrr(int.Parse(jsonDe1["id"].ToString()), useronce.Login, float.Parse(jsonDe1["hashrate"]["last_5min"]["hash"].ToString(), CultureInfo.InvariantCulture), jsonDe1["hashrate"]["last_5min"]["type"].ToString());
                                            sum += float.Parse(jsonDe1["hashrate"]["last_5min"]["hash"].ToString(), CultureInfo.InvariantCulture);
                                        }
                                        if (sum == 0) btc.Upload(useronce); else btc.CreateZero(useronce);
                                    }
                                }
                                catch (NullReferenceException ex)
                                {
                                    Console.WriteLine(ex);
                                    if (sum == 0) btc.Upload(useronce); else btc.CreateZero(useronce);
                                }
                            }
                            else
                            {
                                repository.CreateMrr(0, useronce.Login, 0, "th");
                                btc.Upload(useronce);
                            }
                        }
                        catch (WebException ex)
                        {
                            Console.WriteLine(ex);
                            repository.CreateMrr(0, useronce.Login, 0, "th");
                            btc.Upload(useronce);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

    }

}

