using MiningConsole.Concrete;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.IO;
using System.Net;


namespace MiningConsole.Model
{
    public class RequestMinear
    {

        public async void Upload()
        {
            EFMinearRepository repository = new EFMinearRepository();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var req = WebRequest.Create(@"https://pool.api.btc.com/v1/account/earn-stats?access_key=r_7SMzk7xJ2i2Jm&puid=402205");
                var r = await req.GetResponseAsync();
                StreamReader responseReader = new StreamReader(r.GetResponseStream());
                var responseData = await responseReader.ReadToEndAsync();
                JObject obj = JObject.Parse(responseData);
                try
                {
                    dynamic jsonDe = JsonConvert.DeserializeObject(obj["data"].ToString());
                    repository.UpdateEar(responseData);
                    //Console.WriteLine(jsonDe["fpps_mining_earnings"].ToString(), CultureInfo.InvariantCulture);
                }
                catch (NullReferenceException ex)
                {
                    Console.WriteLine(ex);
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
