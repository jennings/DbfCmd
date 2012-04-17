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
                            case 'n':
                                result.OutputHeaders = false;
                                break;
                        }
                    }
                }
                else if (haveReadDirectory)
                {
                    result.Query = arg;
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

            if (result.Directory == null)
            {
                throw new Exception("Must set a directory.");
            }

            if (result.Query == null)
            {
                throw new Exception("Must set a query");
            }

            return result;
        }

        public class Arguments
        {
            public string Directory { get; set; }
            
            public string Query { get; set; }

            public bool OutputCsv { get; set; }

            public bool OutputHeaders { get; set; }
        }
    }
}
