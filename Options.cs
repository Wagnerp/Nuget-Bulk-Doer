using System;
using System.Collections.Generic;
using CommandLine;

namespace NuGetUnlister
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

        [Option("packageName", Required = true, HelpText = "Provide the name of your package. \n" + 
            "Version unlisting will apply only to the package specified.")]
        public str PackageName { get; set; }
    }
}