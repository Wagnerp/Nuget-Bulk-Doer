using System;
using System.Reflection;
using System.IO;
using CommandLine;
using System.ComponentModel;
using NuGet.Configuration;
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
            Console.Write("EnteredLine");
            var osNameAndVersion = System.Runtime.InteropServices.RuntimeInformation.OSDescription;
            if (!osNameAndVersion.Contains("Windows"))
            {
                Console.WriteLine("This tool is currently only available for Windows, sorry for the inconvenience!\n");
                return;
            }
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed(async (options) => await Execute(options));
        }

        public static async Task Execute(Options options)
        {
            Console.WriteLine("Entered execute");
            if (CheckDisableLastAccess() != 2)
            {
                Console.Write("\nYour Last Access updates are not currently enabled so this tool will not work. \n" +
                    "To enable Last Access updates, run powershell as administrator and input:\n\n" +
                    "\tfsutil behavior set disablelastaccess 2\n\n" +
                    "You may be asked to reboot for the settings change to take effect\n\n" +
                    "Note: Last usage time of packages will only be tracked once the setting is enabled.\n" +
                    "As such, last package usage will only be determined from the enable date onward.\n" +
                    "For further information view documentation at https://github.com/chgill-MSFT/NuGetCleaner \n");
                return;
            }

            var settings = Settings.LoadDefaultSettings(".");
            var gpfPath = SettingsUtility.GetGlobalPackagesFolder(settings);

            string PackageID = options.PackageID;
            string ApiKey = "placeholder";
            Console.Write(PackageID);
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
            Console.Write("did the calls");
             foreach (NuGetVersion version in versions)
            {
                Console.WriteLine($"Found version {version}");
            }

            if (options.All)
            {
                UnlistAll(PackageID, ApiKey, versions);
            }
            else if (options.Range)
            {
                /// UnlistRange(PackageID, ApiKey, versions);
            } else if (options.Previews)
            {
                UnlistPreviews(PackageID, ApiKey, versions);
			} else 
            {
                UnlistSearch(PackageID, ApiKey, versions);
			} 
        }

                /// <summary>
        ///   Unlists packages
        ///   Returns a boolean based on whether user confirms or not
        /// </summary>
        /// <param name="SelectedPackages">Package versions to be unlisted</param>
        /// <returns></returns>
/*          public static Task Delete(HashSet<NuGetVersion> toUnlist)
        {
            foreach (NuGetVersion version in toUnlist)
            {
                ///delete
            }
		} */

        /// <summary>
        ///   Prints to console all package versions to be unlisted
        ///   Returns a boolean based on whether user confirms or not
        /// </summary>
        /// <param name="SelectedPackages">Package versions to be unlisted</param>
        /// <returns></returns>
/*          public static Boolean Confirm(HashSet<NuGetVersion> toUnlist)
        {
            foreach (NuGetVersion version in toUnlist)
            {
                Console.WriteLine($"{version}");
            }
            Console.WriteLine($"Please confirm to unlist the above versions (Y/N)");
		}  */

        public static int CheckDisableLastAccess()
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.FileName = "CMD.exe";
            startInfo.Arguments = "/c fsutil behavior query disablelastaccess";
            process.StartInfo = startInfo;
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            string[] outputArray = output.Split(" ");
            int setting = Convert.ToInt32(outputArray[2]);

            return setting;
        }
        
        /// <summary>
        ///     Finds all package versions that are currently not unlisted
        ///     Confirms the selection with the user
        ///     Unlists all package versions
        ///     Output confirmation
        /// </summary>
        /// <param name="PackageID">Package to be modified</param>
        /// <param name="ApiKey">Credentials for package version modification</param>
        public static void UnlistAll(string PackageID, string ApiKey, IEnumerable<NuGetVersion> versions)
        {
/*  */      foreach (NuGetVersion version in versions)
            {
                Console.WriteLine($"Found version {version}");
                /// delete
            }
		}
        /// <summary>
        ///     Queries user for a range in the format "first version - second version" (inclusive)
        ///     Finds all package versions that are currently not unlisted and falls into that range
        ///     Confirms the selection with the user
        ///     Unlists selected package versions 
        ///     Output confirmation
        /// </summary>
        /// <param name="PackageID">Package to be modified</param>
        /// <param name="ApiKey">Credentials for package version modification</param>
/*         public static void UnlistRange(string PackageID, string ApiKey, IEnumerable<NuGetVersion> versions)
        {
            foreach (NuGetVersion version in versions)
            {
                Console.WriteLine($"Found version {version}");
            } 

		} */
        /// <summary>
        ///     Finds all package versions that are currently not unlisted and contains the character '-'
        ///         Denotes a pre-release version
        ///     Confirms the selection with the user
        ///     Unlists all pre-release package versions
        ///     Output confirmation
        /// </summary>
        /// <param name="PackageID">Package to be modified</param>
        /// <param name="ApiKey">Credentials for package version modification</param>
        public static void UnlistPreviews(string PackageID, string ApiKey, IEnumerable<NuGetVersion> versions)
        {
            HashSet<NuGetVersion> previews = new HashSet<NuGetVersion>;

            foreach (NuGetVersion version in versions)
            {
                if (version.ToString().Contains("-"))
                {
                    previews.Add(version);
                }

                /* if (Confirm(previews))
                {
                    Delete(previews)
                } */
            }

		}
        /// <summary>
        ///     Queries user for keyword
        ///     Finds all package versions that are currently not unlisted and contains the keyword
        ///     Confirms the selection with the user
        ///     Unlists selected package versions
        ///     Output confirmation
        /// </summary>
        /// <param name="PackageID">Package to be modified</param>
        /// <param name="ApiKey">Credentials for package version modification</param>
        public static void UnlistSearch(string Keyword, string ApiKey, IEnumerable<NuGetVersion> versions)
        {
            HashSet<NuGetVersion> results = new HashSet<NuGetVersion>;

            foreach (NuGetVersion version in versions)
            {
                if (version.ToString().Contains(Keyword))
                {
                    results.Add(version);
                }

                /* if (Confirm(results))
                {
                    Delete(results)
                } */
            }

		}
    }
}
