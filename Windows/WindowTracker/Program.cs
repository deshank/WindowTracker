using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

namespace WindowTracker
{
    class Program
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        private string URL = "http://192.168.111.171:8080/log/abc";

        private string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
                //return handle.ToString();
            }
            return null;
        }

        private string SendDATA(string json)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
                httpWebRequest.ContentType = "text/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    Console.WriteLine(httpResponse);
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        return result;
                    }
                }
            }
            catch (WebException ex)
            {
                //return ex.ToString();
                return "Exception Encountered";
            }
        }

        private string GetUserName()
        { 
            return System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            //return Environment.UserName;
        }

        private string GetLocalIPAddress()
        {
            return null;
        }

        static void Main(string[] args)
        {
            Program a = new Program();

            while (true)
            {
                string json = new JavaScriptSerializer().Serialize(new
                {
                    data = DateTime.Now + " " + a.GetActiveWindowTitle(),
                });
                //Console.WriteLine(json);
                Console.WriteLine(a.SendDATA(json));
                System.Threading.Thread.Sleep(5000);
            }
        }
    }
}
