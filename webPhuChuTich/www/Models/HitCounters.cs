using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace www.Models
{
    public class HitCounters
    {
        public bool AddHitCounter()
        {
            try
            {
                string IPClient = "";
                string BrowserClient = "";
                HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
                BrowserClient = browser.Type + "/" + browser.Browser + "/" + browser.Version
                   + "/" + browser.EcmaScriptVersion
                   + "/" + browser.Platform;

                System.Web.HttpContext context = System.Web.HttpContext.Current;
                string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (!string.IsNullOrEmpty(ipAddress))
                {
                    string[] addresses = ipAddress.Split(',');
                    if (addresses.Length != 0)
                    {
                        IPClient = addresses[0];
                    }
                }
                IPClient = context.Request.ServerVariables["REMOTE_ADDR"];

                HitCounterEntity db = new HitCounterEntity();
                db.HitCounters.Add(new HitCounter { visitTime = DateTime.Now, visitBrowser = BrowserClient, visitIp = IPClient, visitPage = context.Request.Url.AbsoluteUri.ToString() });
                db.SaveChanges();
                return true;
            }
            catch
            {
                return true;
            }
        }
    }
}