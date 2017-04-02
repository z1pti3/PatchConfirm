using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WUApiInterop;

namespace PatchConfirm
{
    class Program
    {
        static void Main(string[] args)
        {
            var updateServiceManager = new UpdateServiceManager();
            var updateSession = new UpdateSession();
            var UpdateService = updateServiceManager.AddScanPackageService("Offline sync Service", Directory.GetCurrentDirectory()+"\\wsusscn2.cab", 1);
            var updateSearcher = updateSession.CreateUpdateSearcher();

            updateSearcher.ServerSelection = ServerSelection.ssOthers;
            updateSearcher.ServiceID = UpdateService.ServiceID;

            Console.WriteLine("Searching...");
            var SearchResult = updateSearcher.Search("IsInstalled=0");
            for (int i =0; i < SearchResult.Updates.Count-1; i++)
            {
                Console.WriteLine(SearchResult.Updates[i].Title);
            }
            Console.WriteLine("Search completed!");
            Console.ReadLine();
        }
    }
}
