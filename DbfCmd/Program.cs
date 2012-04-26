//----------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="The DbfCmd Project">
//   Copyright 2011 Various Contributors. Licensed under the Apache License, Version 2.0.
// </copyright>
//----------------------------------------------------------------------------------------

namespace DbfCmd
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    public class Program
    {
        public static void Main(string[] args)
        {
            ArgumentParser.Arguments arguments;

            try
            {
                arguments = ArgumentParser.Parse(args);
            }
            catch (CommandLineException ex)
            {
                Console.WriteLine(ex.Message);
                Program.DisplayHelp();
                Environment.Exit(-1);
                return;
            }

            if (arguments.ShowHelp)
            {
                Program.DisplayHelp();
                Environment.Exit(0);
            }

            var runner = new QueryRunner(arguments.Directory, arguments.OutputCsv, arguments.OutputHeaders);
            var result = runner.Run(arguments.Query);

            if (arguments.OutputCsv)
            {
                var csv = Csv.Format(result, arguments.OutputHeaders);
                Console.Write(csv);
            }
            else
            {
                Application.Run(new HumanReadableDisplayForm(result));
            }
        }

        private static void DisplayHelp()
        {
            Console.WriteLine("Usage: dbfcmd.exe [options] <directory> <query>");
            Console.WriteLine("Usage: dbfcmd.exe [options] <directory> @<file>");
            Console.WriteLine("");
            Console.WriteLine("  <directory>    Directory containing DBF files to read/write");
            Console.WriteLine("  <query>        Query to run (surround with double-quotes)");
            Console.WriteLine("  @<file>        File containing query to run");
            Console.WriteLine("");
            Console.WriteLine("Options:");
            Console.WriteLine("  -c       Output as CSV");
            Console.WriteLine("  -h       Show this help message");
            Console.WriteLine("  -n       Do not output headers");
        }
    }
}
