//----------------------------------------------------------------------------------------
// <copyright file="HumanReadableDisplayForm.cs" company="The DbfCmd Project">
//   Copyright 2011 Various Contributors. Licensed under the Apache License, Version 2.0.
// </copyright>
//----------------------------------------------------------------------------------------

namespace DbfCmd
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using System.Windows.Forms;

    class HumanReadableDisplayForm : Form
    {
        public HumanReadableDisplayForm(DataTable data)
        {
            this.Text = "DbfCmd";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Width = 640;
            this.Height = 480;

            var grid = new DataGridView()
            {
                DataSource = data,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                Size = this.ClientSize,
                ColumnHeadersVisible = true,
                AllowDrop = false,
                RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders
            };

            this.Controls.Add(grid);
            this.Show();
        }
    }
}
