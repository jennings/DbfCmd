//----------------------------------------------------------------------------------------
// <copyright file="Csv.cs" company="The DbfCmd Project">
//   Copyright 2011 Various Contributors. Licensed under the Apache License, Version 2.0.
// </copyright>
//----------------------------------------------------------------------------------------

namespace DbfCmd
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;

    class Csv
    {
        public static string Format(DataTable data, bool showHeaders)
        {
            string result = "";

            var numColumns = data.Columns.Count;

            if (showHeaders)
            {
                for (int i = 0; i < numColumns; i++)
                {
                    if (i != 0)
                    {
                        result += ",";
                    }

                    result += @"""" + data.Columns[i].ColumnName + @"""";
                }

                result += "\n";
            }

            foreach (DataRow row in data.Rows)
            {
                bool first = true;
                foreach (var field in row.ItemArray)
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        result += ",";
                    }

                    if (field is string)
                    {
                        result += @"""" + (string)field + @"""";
                    }
                    else if (field is DateTime)
                    {
                        result += @"""" + ((DateTime)field).ToString("yyyy-MM-dd HH:mm:ss") + @"""";
                    }
                    else
                    {
                        result += field.ToString();
                    }
                }

                result += "\n";
            }

            return result;
        }
    }
}
