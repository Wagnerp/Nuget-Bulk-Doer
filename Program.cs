using System;
using System.Reflection;
using System.IO;
using CommandLine;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NuGet.Common;
using NuGet.Frameworks;
using NuGet.Packaging;
using NuGet.Packaging.Core;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;

namespace NugetBulkDoer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed(async (options) => await Execute(options));
        }

        public static async Task Execute(Options options)
        {
            Console.Write($"Provide the API key associated with this package. \n" + 
            "Make sure you have created the key with unlisting privileges. \n");
            string ApiKey = Console.ReadLine();            
            Console.Write($"Provide the ID of the package you wish to unlist from. \n");
            string PackageID = Console.ReadLine();

            ILogger logger = NullLogger.Instance;
            CancellationToken cancellationToken = CancellationToken.None;

            SourceCacheContext cache = new SourceCacheContext();
            SourceRepository repository = Repository.Factory.GetCoreV3("https://api.nuget.org/v3/index.json");
            FindPackageByIdResource resource = await repository.GetResourceAsync<FindPackageByIdResource>();
            IEnumerable<NuGetVersion> versions = await resource.GetAllVersionsAsync(
                PackageID,
                cache,
                logger,
                cancellationToken);

            Console.Write($"Which versions would you like to unlist? (Options: some/all/previews) \n");
            string mode = Console.ReadLine();

            if (mode.Equals("all"))
            {
                UnlistSome(PackageID, ApiKey, versions, ".");
            }
            else if (mode.Equals("previews"))
            {
                UnlistSome(PackageID, ApiKey, versions, "-");
			} else if (mode.Equals("some"))
            {
                Console.Write($"Please enter the character or substring to search for.");
                string InputText = Console.ReadLine();
                UnlistSome(PackageID, ApiKey, versions, InputText);
			} else {
                Console.Write("Input not valid.");
            }
        }

        /// <summary>
        ///   Prints to console all package versions to be unlisted
        ///   Returns a boolean based on whether user confirms or not
        /// </summary>
        /// <param name="SelectedPackages">Package versions to be unlisted</param>
        /// <returns></returns>
          public static Boolean Confirm(List<NuGetVersion> toUnlist)
        {
            if (!toUnlist.Any())
            {
                Console.WriteLine("We did not find any versions of the package that matched that description.");
                return false;
            }
            foreach (NuGetVersion version in toUnlist)
            {
                Console.WriteLine($"{version}");
            }
            Console.Write($"Please confirm to unlist the above versions (y/n)");
            string userConfirmation = Console.ReadLine();
			return userConfirmation.Equals("y");
		}  
        
        /// <summary>
        ///     Finds all package versions that are currently not unlisted
        ///     Confirms the selection with the user
        ///     Unlists all package versions
        ///     Output confirmation
        /// </summary>
        /// <param name="PackageID">Package to be modified</param>
        /// <param name="ApiKey">Credentials for package version modification</param>
        public static void Unlist(string PackageID, string ApiKey, IEnumerable<NuGetVersion> versions)
        {
/*  */      foreach (NuGetVersion version in versions)
            {
                Console.WriteLine($"Unlisting version {version}. Please press 'y' to continue.");
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                //startInfo.UseShellExecute = false;
                //startInfo.RedirectStandardInput = true;
                startInfo.RedirectStandardOutput = true;
                startInfo.FileName = "CMD.exe";
                process.StartInfo = startInfo;
                startInfo.Arguments = $"/C dotnet nuget delete {PackageID} {version} -k --non-interactive {ApiKey} -s https://www.nuget.org";
                process.Start();
                //string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
            }
		}

        /// <summary>
        ///     Finds all package versions that are currently not unlisted and contains the character '-'
        ///         Denotes a pre-release version
        ///     Confirms the selection with the user
        ///     Unlists all pre-release package versions
        ///     Output confirmation
        /// </summary>
        /// <param name="PackageID">Package to be modified</param>
        /// <param name="ApiKey">Credentials for package version modification</param>
        public static void UnlistSome(string PackageID, string ApiKey, IEnumerable<NuGetVersion> versions, string Keyword)
        {
            List<NuGetVersion> previews = new List<NuGetVersion>();

            foreach (NuGetVersion version in versions)
            {
                if (version.ToString().Contains(Keyword))
                {
                    previews.Add(version);
                }
		    }
            if (Confirm(previews))
            {
                    Unlist(PackageID, ApiKey, previews);
             
            }
        }
    }
}
