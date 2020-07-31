using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ELM
{
    /// <summary>
    /// Interaction logic for SessionEndPopup.xaml
    /// </summary>
    public partial class SessionEndPopup : Window
    {
        public SessionEndPopup(List<string> mentions, Dictionary<string,int> hashtags,List<string> SIRList)
        {
            InitializeComponent();
            string mentionList = "";
            string trending = "";
            string sir = "";
            foreach(var mention in mentions)
            {
                mentionList += mention + "\r\n";
            }
            foreach(var hash in hashtags)
            {
                trending += hash.Key + "\r\n";
            }
            foreach(var sirs in SIRList)
            {
                sir += sirs + "\r\n";
            }
            mentions_txt.Text = mentionList;
            trending_txt.Text = trending;
            SIRList_txt.Text = sir;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ELM_Home home = new ELM_Home();
            home.Show();
        }
    }
}
