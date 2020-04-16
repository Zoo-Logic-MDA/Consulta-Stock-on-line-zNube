using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net;
using System.IO;

namespace Ejemplo_Consulta_Stock_On_Line_zNube
{
    public partial class Form1 : Form
    {

        //zNube ********************************
        public string tokenzNube = "9b4aaa00-e1f5-4a44-91f5-779b58779d2e";
        public string servidorzNube = "https://api.znube.com.ar:8081/Omnichannel/GetStock";
        public string baseDatoszNube = "34e126d1-3523-4cbe-9b36-e66982ce4f8e";


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(servidorzNube + "/?limit=1000&offset=0" + "/?Resources=" + baseDatoszNube);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            httpWebRequest.Headers.Add("zNube-token", tokenzNube);


            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var responseText = streamReader.ReadToEnd();
                    textBox1.Text = responseText.ToString();
                }

            }

            catch (WebException ex)
            {
                HttpWebResponse respuesta = ex.Response as HttpWebResponse;
                if (respuesta != null)
                {
                    MessageBox.Show(respuesta.StatusDescription, "ATENCION!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ex.Response.Close();
                }
                else
                {
                    throw;
                }

            }
        }
    }
}
