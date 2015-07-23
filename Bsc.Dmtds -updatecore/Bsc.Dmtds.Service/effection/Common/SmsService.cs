using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bsc.Dmtds.Service.effection.Common
{
    public class SmsService 
    {
        public Task SendAsync(Message message)
        {
            string url = "http://utf8.sms.webchinese.cn/?Uid={0}&Key={1}&smsMob={2}&smsText={3}";
            string uid = "Hdds";
            string Key = "620513ff4fd6512feef0";
            string smsMob = "15757821669";
            string smsText = "祝你生日快乐";
            string uri=string.Format(url,uid,Key,smsMob,smsText);
            HttpWebRequest hr = (HttpWebRequest)WebRequest.Create(uri);
            hr.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64)";
            hr.Method = "GET";
            hr.Timeout = 30 * 60 * 1000;
            WebResponse hs = hr.GetResponse();
            Stream sr = hs.GetResponseStream();
            StreamReader ser = new StreamReader(sr, Encoding.Default);
            Console.WriteLine(ser.ReadToEnd());


            //strRet = ser.ReadToEnd(); 
            // Plug in your sms service here to send a text message.
            //var Twilio = new TwilioRestClient(
            //ConfigurationManager.AppSettings["TwilioSid"],
            //ConfigurationManager.AppSettings["TwilioToken"]
            //);
            //var result = Twilio.SendMessage(
            //    ConfigurationManager.AppSettings["TwilioFromPhone"],
            //   message.Destination, message.Body);
            //Console.WriteLine(result.RestException.Message,null,null);
            //// Status is one of Queued, Sending, Sent, Failed or null if the number is not valid
            //Trace.TraceInformation(result.Status);

            // Twilio doesn't currently have an async API, so return success.


            return Task.FromResult(0);
        }
    }
    public class Message {
        public string Uid { get; set; }
        public string Key { get; set; }
        public string SmsMode { get; set; }
        public string SmsText { get; set; }
    }
}
