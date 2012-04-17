//------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="The DbfCmd Project">
//   Copyright 2011 Various Contributors. Licensed under the Apache License, Version 2.0.
// </copyright>
//------------------------------------------------------------------------------------

namespace DbfCmd
{
    using System;
    using System.Collections.Generic;
    using System.Data.Odbc;
    using System.Data.OleDb;
    using System.IO;
    using System.Text;

    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Program.DisplayHelp();
                Environment.Exit(0);
            }

            var arguments = ArgumentParser.Parse(args);

            var runner = new QueryRunner(arguments.Directory, arguments.OutputCsv, arguments.OutputHeaders);
            string result = runner.Run(arguments.Query);

            Console.WriteLine(result);
        }

        private static void DisplayHelp()
        {
            Console.WriteLine("Usage: dbfcmd.exe [options] <path> <query>");
            Console.WriteLine("");
            Console.WriteLine("Options:");
            Console.WriteLine("  -c       Output as CSV");
            Console.WriteLine("  -n       Do not output headers");
        }
    }
}
