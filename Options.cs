using System;
using System.Collections.Generic;
using CommandLine;

namespace NugetBulkDoer
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Options
    {
        [Option("all", HelpText = "Unlist all package versions.")]
        public bool All { get; set; }

        [Option("range", HelpText = "Unlist a range of package versions.")]
        public bool Range { get; set; }

        [Option("previews", HelpText = "Unlist pre-release package versions.")]
        public bool Previews { get; set; }

        [Option("packageID", Required = true, HelpText = "Provide the name of your package. \n" + 
            "Version unlisting will apply only to the package specified.")]
        public string PackageID { get; set; }
    }
}