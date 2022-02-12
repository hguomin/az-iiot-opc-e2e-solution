//
//Guomin Huang @2019.10.26
//for da server settings
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Opc.Ua.Server;
using Opc.Ua.Com.Client;

namespace Opc.Ua.Server.Controls
{
    public partial class DaServerSettingsDlg : Form
    {
        #region Private Fields
        private ApplicationConfiguration _configuration;
        private string _daServerUrl;
        private string _daServerName;
        #endregion

        public bool SettingsChanged { get; set; }

        public DaServerSettingsDlg(ApplicationConfiguration configuration)
        {
            _configuration = configuration;

            LoadDaServerConfiguration(_configuration);

            SettingsChanged = false;
            
            InitializeComponent();

            tbServerUrl.Text = _daServerUrl;
        }


        private void LoadDaServerConfiguration(ApplicationConfiguration configuration)
        {
            // get the configuration for the wrapper.
            ComWrapperServerConfiguration wrapperConfiguration = _configuration.ParseExtension<ComWrapperServerConfiguration>();

            if (wrapperConfiguration != null && wrapperConfiguration.WrappedServers != null)
            {
                foreach (ComClientConfiguration clientConfiguration in wrapperConfiguration.WrappedServers)
                {
                    if (clientConfiguration is ComDaClientConfiguration)
                    {
                        _daServerUrl = clientConfiguration.ServerUrl;
                        _daServerName = clientConfiguration.ServerName;
                    }
                }
            }
        }

        private void UpdateDaServerConfiguration(ApplicationConfiguration configuration)
        {
            // get the configuration for the wrapper.
            ComWrapperServerConfiguration wrapperConfiguration = _configuration.ParseExtension<ComWrapperServerConfiguration>();

            if (wrapperConfiguration != null && wrapperConfiguration.WrappedServers != null)
            {
                foreach (ComClientConfiguration clientConfiguration in wrapperConfiguration.WrappedServers)
                {
                    string namespaceUri = clientConfiguration.ServerUrl;

                    if (clientConfiguration is ComDaClientConfiguration)
                    {
                        clientConfiguration.ServerUrl = _daServerUrl;
                        clientConfiguration.ServerName = _daServerName;
                    }
                }
            }

            //Guomin Test
            configuration.UpdateExtension<ComWrapperServerConfiguration>(null, wrapperConfiguration);
            configuration.SaveToFile(configuration.SourceFilePath);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(_daServerUrl != tbServerUrl.Text.Trim())
            {
                _daServerUrl = tbServerUrl.Text.Trim();
                UpdateDaServerConfiguration(_configuration);

                SettingsChanged = true;
            }

            this.Close();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
