using MiningConsole.Abstract;
using MiningConsole.Concrete;
using MiningConsole.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiningConsole.Model
{
    class RequestRate
    {
        public async void Upload()
        {
            try
            {
                EFRateRepository repository = new EFRateRepository();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var req = WebRequest.Create("https://nl.bitstamp.net/api/ticker/");
                var r = await req.GetResponseAsync();
                StreamReader responseReader = new StreamReader(r.GetResponseStream());
                var responseData = await responseReader.ReadToEndAsync();
                Rate rate = JsonConvert.DeserializeObject<Rate>(responseData);
                repository.SaveRate(rate);
            }
            catch
            {

            }
        }
    }
}
