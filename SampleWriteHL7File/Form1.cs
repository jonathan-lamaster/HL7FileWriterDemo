using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleWriteHL7File
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sFilePath = @"C:\Test\sample.hl7";
            string sDateTime = DateTime.Now.ToString();

            NHapi.Base.Parser.PipeParser parser = new NHapi.Base.Parser.PipeParser();
            NHapi.Model.V23.Message.ORM_O01 qry = new NHapi.Model.V23.Message.ORM_O01();

            qry.MSH.EncodingCharacters.Value = @"^~\&";
            qry.MSH.MessageType.MessageType.Value = "ORM";
            qry.MSH.MessageType.TriggerEvent.Value = "O01";
            qry.MSH.FieldSeparator.Value = "|";
            qry.MSH.DateTimeOfMessage.TimeOfAnEvent.Value = System.DateTime.Now.ToString();
            qry.MSH.MessageControlID.Value = "Jonathan123";


            System.IO.StreamWriter oFileWriter = new System.IO.StreamWriter(sFilePath, true);
            oFileWriter.WriteLine("\n" + parser.Encode(qry));
            oFileWriter.Close();
        }
    }
}
