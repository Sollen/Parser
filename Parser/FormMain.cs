using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Parser.Core;
using Parser.Core.Habra;

namespace Parser
{
    public partial class FormMain : Form
    {
       private ParserWorker<string[]> parser;
        public FormMain()
        {
            InitializeComponent();
            parser = new ParserWorker<string[]>(
                new HabraParser()
                );
            parser.OnCompleted += Parser_OnCompleted;
            parser.OnNewData += Parser_OnNewData;
        }

        private void Parser_OnNewData(object arg1, string[] arg2)
        {
            ListTiles.Items.AddRange(arg2);
        }

        private void Parser_OnCompleted(object obj)
        {
            MessageBox.Show("Completed!");
        }

        private void ButtonStart_Click(object sender, System.EventArgs e)
        {
            parser.Settings = new HabraSettings((int) NumericStart.Value, (int) NumericEnd.Value);
            parser.Start();
        }

        private void ButtonAbort_Click(object sender, System.EventArgs e)
        {
            parser.Abort();
        }
    }
}

