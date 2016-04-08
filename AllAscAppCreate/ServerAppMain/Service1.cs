using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;

namespace ServerAppMain
{
    public partial class TestService : ServiceBase
    {
        private StreamWriter file;

        public TestService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            file = new StreamWriter(new FileStream("MyLog.log", FileMode.Append));
            this.file.WriteLine("Start Service");
            this.file.Flush();
        }

        protected override void OnStop()
        {
            this.file.WriteLine("Stop service");
            this.file.Flush();
            this.file.Close();
        }
    }
}
