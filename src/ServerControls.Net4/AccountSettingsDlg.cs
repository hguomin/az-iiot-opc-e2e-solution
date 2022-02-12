//
//Guomin Huang @ 2019.10.26
//For UA server account setting
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
using Opc.Ua.Configuration;

namespace Opc.Ua.Server.Controls
{
    public partial class AccountSettingsDlg : Form
    {
        private UserNameCreator _userNameCreator;
        private Dictionary<string, UserNameIdentityToken> _userIdTokens;
        private string _applicationName;
        private string _username = "";
        private string _password = "";

        public bool SettingsChanged { get; set; }
        public AccountSettingsDlg(string applicationName)
        {
            _applicationName = applicationName;
            _userNameCreator = new UserNameCreator(applicationName);
            _userIdTokens = UserNameCreator.LoadUserName(applicationName);

            if (_userIdTokens.Count > 0)
            {
                foreach (var kv in _userIdTokens)
                {
                    _username = kv.Value.UserName;
                    _password = kv.Value.DecryptedPassword;
                }
            }

            InitializeComponent();

            tbUsername.Text = _username;
            tbPassword.Text = _password;

            SettingsChanged = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(_username != tbUsername.Text.Trim() || _password != tbPassword.Text.Trim())
            {
                if(_username.Length > 0)
                {
                    _userNameCreator.Delete(_applicationName, _username);
                }

                _username = tbUsername.Text.Trim();
                _password = tbPassword.Text.Trim();

                _userNameCreator.Add(_applicationName, _username, _password);

                SettingsChanged = true;
            }

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
