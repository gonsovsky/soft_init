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

namespace MyFirstService
{
    public partial class WindowsService : ServiceBase
    {

        Timer timer = new Timer();
        public WindowsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("I'm started");
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 5000;
            timer.Enabled = true;
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("I'm stopped");
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            eventLog1.WriteEntry("I'm alive");
        }
    }
}
