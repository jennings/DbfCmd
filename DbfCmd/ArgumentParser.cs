//------------------------------------------------------------------------------------
// <copyright file="ArgumentParser.cs" company="The DbfCmd Project">
//   Copyright 2011 Various Contributors. Licensed under the Apache License, Version 2.0.
// </copyright>
//------------------------------------------------------------------------------------

namespace DbfCmd
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    internal class ArgumentParser
    {
        public static Arguments Parse(string[] args)
        {
            var result = new Arguments()
            {
                ShowHelp = false,
                OutputCsv = false,
                OutputHeaders = true
            };

            bool haveReadDirectory = false;

            foreach (var arg in args)
            {
                if (arg.StartsWith("-") || arg.StartsWith("/"))
                {
                    var switches = arg.Substring(1, arg.Length - 1);
                    foreach (char @switch in switches)
                    {
                        switch (@switch)
                        {
                            case 'c':
                                result.OutputCsv = true;
                                break;
                            case 'h':
                                result.ShowHelp = true;
                                break;
                            case 'n':
                                result.OutputHeaders = false;
                                break;
                        }
                    }
                }
                else if (haveReadDirectory)
                {
                    if (result.Query != null)
                    {
                        throw new CommandLineException("Can only run one query");
                    }

                    if (arg.StartsWith("@"))
                    {
                        var filename = arg.Substring(1, arg.Length - 1);
                        var text = File.ReadAllText(filename);
                        result.Query = text;
                    }
                    else
                    {
                        result.Query = arg;
                    }
                }
                else
                {
                    if (Directory.Exists(arg))
                    {
                        result.Directory = arg;
                        haveReadDirectory = true;
                    }
                }
            }

            if (result.Query == null || result.Directory == null)
            {
                result.ShowHelp = true;
            }

            return result;
        }

        public class Arguments
        {
            public string Directory { get; set; }

            public string Query { get; set; }

            public bool ShowHelp { get; set; }

            public bool OutputCsv { get; set; }

            public bool OutputHeaders { get; set; }
        }
    }
}
