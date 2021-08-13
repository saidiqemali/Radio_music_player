using NAudio.Wave;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisioForge.Tools.TagLib.Mpeg;

namespace _1_Radio
{
    public partial class Radio : Form
    {
        Dictionary<string, string> stationDict;
        Dictionary<string, string> imgDict;
        MediaFoundationReader mediaFoundation;
        WaveOutEvent waveOut;
        public Radio()
        {
            InitializeComponent();
            waveOut = new WaveOutEvent();
        }

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox1.Load(imgDict[comboBox1.Text]);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            pictureBox1.Load(imgDict[comboBox1.Text]);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {

     

            string url = "https://api.laut.fm/stations";
           //  string url1 = "https://1000oldies.stream.laut.fm/1000oldies?t302=2021-08-12_11-03-07&uuid=ce304378-284e-4327-ad79-dcf80a583a23";
            HttpClient client = new HttpClient();




           




            string res = await client.GetStringAsync(url);
            var jsonDataSingle = JArray.Parse(res);
            imgDict = new Dictionary<string, string>();
            foreach (var station in jsonDataSingle)
            {
                try
                {
                    //comboBox1.Text = ((string)station["display_name"]);
                    comboBox1.Items.Add(station["stream_url"]);
                    imgDict.Add(station["stream_url"].ToString(), station["images"]["station_640x640"].ToString());


                }
                catch (Exception)
                {

                }

            }
            comboBox1.Refresh();


            

        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (waveOut.PlaybackState == PlaybackState.Playing)
            {
                waveOut.Stop();
                waveOut.Dispose();
                button1.Image = Properties.Resources.playogoo;
            }
            else
            {
                if (comboBox1.Text == "")
                {
                    MessageBox.Show("Keine URL");

                }
                else
                {
                    mediaFoundation = new MediaFoundationReader(comboBox1.Text);
                  
                    waveOut.Init(mediaFoundation);
                    waveOut.Play();
                    button1.Image = Properties.Resources.stoplogo;
                }
            }

           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
 



        //    OpenFileDialog open = new OpenFileDialog();

        //    if (open.ShowDialog() == DialogResult.OK)
        //    {
        //        pictureBox1.Image = new Bitmap(open.FileName);
        //    }
        }
    }
}
