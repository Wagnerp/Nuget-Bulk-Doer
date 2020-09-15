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
            Console.WriteLine("Got here");
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed(async (options) => await Execute(options));
        }

        public static async Task Execute(Options options)
        {

            string PackageID = options.PackageID;
            string ApiKey = "oy2d7y2i6cyj42qohabqxb2pthgy72ye5abpp2ptvlgsn4";
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
          public static async Task Delete(string ApiKey, string PackageID, HashSet<NuGetVersion> toUnlist)
        {
            PackageUpdateResource resource = await repository.GetResourceAsync<PackageUpdateResource>();
            foreach (NuGetVersion version in toUnlist)
            {
 
            }
		} 

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
         public static void UnlistRange(string PackageID, string ApiKey, IEnumerable<NuGetVersion> versions)
        {
            foreach (NuGetVersion version in versions)
            {
                Console.WriteLine($"Found version {version}");
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
        public static void UnlistPreviews(string PackageID, string ApiKey, IEnumerable<NuGetVersion> versions)
        {
            HashSet<NuGetVersion> previews = new HashSet<NuGetVersion>();

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
/*         /// <summary>
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
            HashSet<NuGetVersion> results = new HashSet<NuGetVersion>();

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

		} */
    }
}
