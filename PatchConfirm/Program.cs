using System;
using System.IO;
using WUApiInterop;

namespace PatchConfirm
{
    class Program
    {
        static void Main(string[] args)
        {
            IUpdateServiceManager upServiceMan = new UpdateServiceManager();
            IUpdateSession upSession = new UpdateSession();
            IUpdateService upService = upServiceMan.AddScanPackageService("Offline sync Service", Directory.GetCurrentDirectory() + "\\wsusscn2.cab", 1);
            IUpdateSearcher upSearcher = upSession.CreateUpdateSearcher();

            upSearcher.ServerSelection = ServerSelection.ssOthers;
            upSearcher.ServiceID = upService.ServiceID;

            Console.WriteLine("Collecting update information...");
            ISearchResult SearchResult = upSearcher.Search("IsInstalled=0 or IsInstalled=1");
            Console.WriteLine("**Installed Updates**");
            foreach (IUpdate update in SearchResult.Updates)
            {
                if (update.IsInstalled)
                {
                    Console.WriteLine(update.MsrcSeverity + " - " + update.Title);
                }
            }

            Console.WriteLine("**Missing Updates**");
            foreach (IUpdate update in SearchResult.Updates)
            {
                if (!update.IsInstalled)
                {
                    Console.WriteLine(update.MsrcSeverity + " - " + update.Title);
                }
            }

            Console.WriteLine("Press enter to quit!");
            Console.ReadLine();
        }
    }
}
