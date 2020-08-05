using System;
using System.Reflection;
using System.IO;
using CommandLine;
using System.ComponentModel;
using NuGet.Configuration;

namespace NugetBulkDoer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var osNameAndVersion = System.Runtime.InteropServices.RuntimeInformation.OSDescription;
            if (!osNameAndVersion.Contains("Windows"))
            {
                Console.WriteLine("This tool is currently only available for Windows, sorry for the inconvenience!\n");
                return;
            }

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(options => Execute(options));
        }

        public static void Execute(Options options)
        {
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

            str PackageName = options.packageName;

            if (options.All)
            {
                UnlistAll(gpfPath, PackageName);
            }
            else if (options.Range)
            {
                UnlistRange(gpfPath, PackageName);
            } else if (options.Previews)
            {
                UnlistPreviews(gpfPath, PackageName);
			} else 
            {
                UnlistSearch(gpfPath, PackageName);
			}
        }

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
    }
}
