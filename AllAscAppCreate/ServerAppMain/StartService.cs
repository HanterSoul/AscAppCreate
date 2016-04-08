using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;

namespace ServerAppMain
{
    [RunInstaller(true)]
    public partial class StartService : System.Configuration.Install.Installer
    {
        public StartService()
        {
            InitializeComponent();
        }
    }
}
