//------------------------------------------------------------------------------------
// <copyright file="QueryRunner.cs" company="The DbfCmd Project">
//   Copyright 2011 Various Contributors. Licensed under the Apache License, Version 2.0.
// </copyright>
//------------------------------------------------------------------------------------

namespace DbfCmd
{
    using System;
    using System.Collections.Generic;
    using System.Data.OleDb;
    using System.Text;

    internal class QueryRunner
    {
        public QueryRunner(string directory, bool outputCsv, bool outputHeaders)
        {
            this.Directory = directory;
            this.OutputCsv = outputCsv;
            this.OutputHeaders = outputHeaders;
        }

        public bool OutputCsv { get; set; }

        public bool OutputHeaders { get; set; }

        public string Directory { get; set; }

        public string Run(string query)
        {
            var csb = new OleDbConnectionStringBuilder();
            csb.Provider = @"Microsoft.Jet.OLEDB.4.0";
            csb.DataSource = this.Directory;
            csb.Add("Extended Properties", "dBASE IV");

            string result = "";

            using (var db = new OleDbConnection(csb.ConnectionString))
            using (var cmd = new OleDbCommand())
            {
                cmd.Connection = db;
                cmd.CommandText = query;

                db.Open();

                if (query.StartsWith("select", StringComparison.InvariantCultureIgnoreCase))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        var numColumns = reader.FieldCount;

                        if (this.OutputHeaders)
                        {
                            for (int i = 0; i < numColumns; i++)
                            {
                                if (i != 0)
                                {
                                    result += ",";
                                }

                                result += reader.GetName(i);
                            }
                        }

                        while (reader.Read())
                        {
                            for (int i = 0; i < numColumns; i++)
                            {
                                if (i != 0)
                                {
                                    result += ",";
                                }

                                result += reader[i].ToString();
                            }

                            result += "\n";
                        }
                    }
                }
                else
                {
                    var recordsAffected = cmd.ExecuteNonQuery();
                    result = String.Format("{0} records affected.", recordsAffected);
                }

                db.Close();
            }

            return result;
        }
    }
}
