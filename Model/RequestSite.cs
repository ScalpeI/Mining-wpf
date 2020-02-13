using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiningConsole.Model
{
    class RequestSite
    {
        public void req()
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var req = WebRequest.Create(@"http://mining-operator.org.uk");
            }
            catch
            {

            }
        }
    }
}
