using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Threading;
namespace ZvonoYun
{
    class Program
    {
        static void Main(string[] args)
        {

            string browser = string.Empty;
            RegistryKey key = null;
            try
            {
                key = Registry.ClassesRoot.OpenSubKey(@"HTTP\shell\open\command");
                if (key != null)
                {
                    // Get default Browser
                    browser = key.GetValue(null).ToString().ToLower().Trim(new[] { '"' });
                }
                if (!browser.EndsWith("exe"))
                {
                    //Remove all after the ".exe"
                    browser = browser.Substring(0, browser.LastIndexOf(".exe", StringComparison.InvariantCultureIgnoreCase) + 4);
                }
            }
            finally
            {
                if (key != null)
                {
                    key.Close();
                }
            }
            // Open the browser.
            Process proc = Process.Start(browser, "http://192.168.20.11/arduino/digital/13/1");
            if (proc != null)
            {
                Thread.Sleep(15000);
                // Close the browser.
                proc.Kill();
            }
            proc = Process.Start(browser, "http://192.168.20.11/arduino/digital/13/0");
            if (proc != null)
            {
                Thread.Sleep(1000);
                // Close the browser.
                proc.Kill();
            }
        }
    }
}
