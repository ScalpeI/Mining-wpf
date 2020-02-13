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
using System.Threading.Tasks;

namespace MiningConsole.Model
{
    class RequestBtc
    {

        public async void Upload(dynamic useronce)
        {
            EFBtcRepository repository = new EFBtcRepository();
            //EFUserRepository user = new EFUserRepository();
            //foreach (var useronce in user.Users)
            //{
                if (useronce.Bkey != null && useronce.Bpuid != null)
                {
                    try
                    {

                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                        var req = WebRequest.Create(@"https://pool.api.btc.com/v1/worker/stats?access_key=" + useronce.Bkey + "&puid=" + useronce.Bpuid);
                        req.ContentType = "application/json";
                        var r = await req.GetResponseAsync();

                        StreamReader responseReader = new StreamReader(r.GetResponseStream());
                        var responseData = await responseReader.ReadToEndAsync();
                        JObject obj = JObject.Parse(responseData);
                        try
                        {
                            dynamic jsonDe = JsonConvert.DeserializeObject(obj["data"].ToString());

                            if (float.Parse(jsonDe["shares_5m"].ToString(), CultureInfo.InvariantCulture) > 0)
                                if (jsonDe["shares_unit"].ToString() == "G")
                                {
                                    repository.Create(useronce.Login, float.Parse(jsonDe["shares_5m"].ToString(), CultureInfo.InvariantCulture)/1024, "T");
                                }
                            else if (jsonDe["shares_unit"].ToString() == "P")
                            {
                                repository.Create(useronce.Login, float.Parse(jsonDe["shares_5m"].ToString(), CultureInfo.InvariantCulture) * 1024, "T");
                            }
                            else repository.Create(useronce.Login, float.Parse(jsonDe["shares_5m"].ToString(), CultureInfo.InvariantCulture), jsonDe["shares_unit"].ToString());

                            else repository.Create(useronce.Login, 0, "T");

                        }
                        catch (NullReferenceException ex)
                        {
                            Console.WriteLine(ex);
                        }
                    }
                    catch (WebException ex)
                    {
                        Console.WriteLine(ex);
                        repository.Create(useronce.Login, 0, "T");
                    }
                }
            //}
        }
        public void CreateZero(dynamic useronce)
        {
            EFBtcRepository repository = new EFBtcRepository();
            repository.Create(useronce.Login, 0, "T");
        }
    }
}
