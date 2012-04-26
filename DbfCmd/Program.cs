//----------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="The DbfCmd Project">
//   Copyright 2011 Various Contributors. Licensed under the Apache License, Version 2.0.
// </copyright>
//----------------------------------------------------------------------------------------

namespace DbfCmd
{
    using System;
    using System.Collections.Generic;
    using System.Data.Odbc;
    using System.Data.OleDb;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var arguments = ArgumentParser.Parse(args);

                if (arguments.ShowHelp)
                {
                    Program.DisplayHelp();
                    Environment.Exit(0);
                }

                var runner = new QueryRunner(arguments.Directory, arguments.OutputCsv, arguments.OutputHeaders);
                var result = runner.Run(arguments.Query);

                var form = new Form();
                var grid = new DataGrid();
                grid.DataSource = result;
                grid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                grid.Size = form.ClientSize;
                form.Controls.Add(grid);
                form.Show();

                Application.Run(form);
            }
            catch (CommandLineException ex)
            {
                Console.WriteLine(ex.Message);
                Program.DisplayHelp();
                Environment.Exit(-1);
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
