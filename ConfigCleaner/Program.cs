using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigCleaner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (!args.Any() || IsHelpSwitch(args.FirstOrDefault()))
            {
                WriteHelp();
                return;
            }

            var path = args.FirstOrDefault();
            if (path==null || !Directory.Exists(path))
            {
                Console.Out.WriteLine("Error: Input folder not found");
                return;
            }

            var p = new Processor();
            p.CleanFiles(args.FirstOrDefault());
        }

        private static bool IsHelpSwitch(string input)
        {
            return input == "-h" || input == "-help";
        }

        private static void WriteHelp()
        {
           Console.Out.WriteLine("Config Cleaner - Removes comments and orders AppSettings");
           Console.Out.WriteLine("Usuage: ConfigCleaner.exe <pathToConfigs>");
        }
    }
}
