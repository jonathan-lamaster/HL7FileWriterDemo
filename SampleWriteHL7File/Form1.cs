using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data;

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
            /*Read the file*/
                        string inputFilePath = @"C:\Test\inputfile.csv";
            StreamReader oStreamReader = new StreamReader(inputFilePath);
            DataTable oDataTable = new DataTable();
            int rowCount = 0;

            string[] columnNames = null;
            string[] oStreamDataValues = null;

            while(!oStreamReader.EndOfStream)
            {
                string oStreamRowData = oStreamReader.ReadLine().Trim();

                if(oStreamRowData.Length > 0)
                {
                    oStreamDataValues = oStreamRowData.Split(',');
                    if (rowCount == 0)
                    {
                        rowCount = 1;
                        columnNames = oStreamDataValues;
                        foreach( string csvHeader in columnNames)
                        {
                            DataColumn oDataColum = new DataColumn(csvHeader.ToUpper(), typeof(string));

                            oDataColum.DefaultValue = string.Empty;
                            oDataTable.Columns.Add(oDataColum);

                        }

                    }
                }

                else
                {
                    DataRow oDataRow = oDataTable.NewRow();

                    for( int i=0; i<columnNames.Length; i++)
                    {
                        oDataRow[columnNames[i]] = oStreamDataValues[i] == null ?
                            string.Empty : oStreamDataValues[i].ToString();

                    }
                    oDataTable.Rows.Add(oDataRow);
                }
            }

            /*Write the HL7 Message*/
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

        void ReadFile()
        {
            string inputFilePath = @"C:\Test\input.csv";
            StreamReader oStreamReader = new StreamReader(inputFilePath);
            DataTable oDataTable = new DataTable();
            int rowCount = 0;

            string[] columnNames = null;
            string[] oStreamDataValues = null;

            while(!oStreamReader.EndOfStream)
            {
                string oStreamRowData = oStreamReader.ReadLine().Trim();

                if(oStreamRowData.Length > 0)
                {
                    oStreamDataValues = oStreamRowData.Split(',');
                    if (rowCount == 0)
                    {
                        rowCount = 1;
                        columnNames = oStreamDataValues;
                        foreach( string csvHeader in columnNames)
                        {
                            DataColumn oDataColum = new DataColumn(csvHeader.ToUpper(), typeof(string));

                            oDataColum.DefaultValue = string.Empty;
                            oDataTable.Columns.Add(oDataColum);

                        }

                    }
                }

                else
                {
                    DataRow oDataRow = oDataTable.NewRow();

                    for( int i=0; i<columnNames.Length; i++)
                    {
                        oDataRow[columnNames[i]] = oStreamDataValues[i] == null ?
                            string.Empty : oStreamDataValues[i].ToString();

                    }
                    oDataTable.Rows.Add(oDataRow);
                }
            }
            oStreamReader.Close();
            oStreamReader.Dispose();

            foreach(DataRow dr in oDataTable.Rows)
            {
                string RowValues = string.Empty;
                foreach (string csvColumns in columnNames)
                {
                    RowValues+= csvColumns+"="+dr[csvColumns].ToString()+"  ";
                }
            }
        }
    }
}
