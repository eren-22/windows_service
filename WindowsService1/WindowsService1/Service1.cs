using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer();
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            DosyaYaz("Servis çalışmaya başladı" + DateTime.Now.ToString());
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 5000;
            timer.Enabled = true;
        }

        protected override void OnStop()
        {
            DosyaYaz("Servis durdu " + DateTime.Now.ToString());
        }

        private void OnElapsedTime(object source,ElapsedEventArgs e)
        {
            DosyaYaz("Servis çalışmaya devam ediyor " + DateTime.Now.ToString());
        }

        public void DosyaYaz (string mesaj)
        {
            string dosyaYolu = AppDomain.CurrentDomain.BaseDirectory + "/Logs";

            if (!Directory.Exists(dosyaYolu))
            {
                Directory.CreateDirectory(dosyaYolu);
            }

            string textYolu = AppDomain.CurrentDomain.BaseDirectory + "/Logs/servisim.txt";

            if (!File.Exists(textYolu))  //hata!!
            {
                using(StreamWriter sw = File.CreateText(textYolu))
                {
                    sw.WriteLine(mesaj);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(textYolu))
                {
                    sw.WriteLine(mesaj);
                }
            }
        }

        

    }
}
