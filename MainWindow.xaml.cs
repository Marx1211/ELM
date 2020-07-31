using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ELM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Abbreviations abbreviations = new Abbreviations("D:\\UNI\\SET09102\\ELM\\textwords.csv");
        List<string> mentionsList = new List<string>();
        Dictionary<string, int> hashtagTrend = new Dictionary<string, int>();
        List<string> SIRList = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void endSession_btn_Click(object sender, RoutedEventArgs e)
        {
            SessionEndPopup popupWindow = new SessionEndPopup(mentionsList,hashtagTrend,SIRList);
            popupWindow.Show();
            this.Close();
        }

        private void process_btn_Click(object sender, RoutedEventArgs e)
        {
            
            try {
                char howToProcess = header_txt.Text[0];
                switch (howToProcess)
                {
                    case 'S':
                        SMS newSMS = new SMS(header_txt.Text, body_txt.Text.Split(new char[] { '\r', '\n' }).FirstOrDefault(), abbreviations.ExpandAbbreviations(body_txt.Text.Split(new char[] { '\r', '\n' }).Last()));
                        string output = JsonConvert.SerializeObject(newSMS);
                        string where = String.Format("D:\\UNI\\SET09102\\ELM\\{0}.json", newSMS.Header);
                        this.showOnscreen(newSMS.Sender, newSMS.Body);
                        File.WriteAllText(where, output);
                        header_txt.Clear();
                        body_txt.Clear();
                        break;
                    case 'E':
                        if(body_txt.Text.Split(new char[] { '\r', '\n' })[2].Substring(0, 3).Equals("SIR"))
                        {
                            SIR newSIR = new SIR(header_txt.Text, body_txt.Text.Split(new char[] { '\r', '\n' }).FirstOrDefault(), body_txt.Text.Split(new char[] { '\r', '\n' })[2], body_txt.Text.Split(new char[] { '#' }).Last());
                            SIRList.Add(newSIR.Code + ": " + newSIR.NatureOfIncident);
                            string outputEmail = JsonConvert.SerializeObject(newSIR);
                            string whereEmail = String.Format("D:\\UNI\\SET09102\\ELM\\{0}.json", newSIR.Header);
                            this.showOnscreen(newSIR.Sender, newSIR.Body, newSIR.Subject);
                        }
                        else
                        {
                            Email newEmail = new Email(header_txt.Text, body_txt.Text.Split(new char[] { '\r', '\n' }).FirstOrDefault(), body_txt.Text.Split(new char[] { '\r', '\n' })[2], body_txt.Text.Split(new char[] { '\r', '\n' }).Last());
                            string outputEmail = JsonConvert.SerializeObject(newEmail);
                            string whereEmail = String.Format("D:\\UNI\\SET09102\\ELM\\{0}.json", newEmail.Header);
                            this.showOnscreen(newEmail.Sender, newEmail.Body, newEmail.Subject);
                        }
                        header_txt.Clear();
                        body_txt.Clear();
                        break;
                    case 'T':
                        Tweet newTweet = new Tweet(header_txt.Text, body_txt.Text.Split(new char[] { '\r', '\n' }).FirstOrDefault(), abbreviations.ExpandAbbreviations(body_txt.Text.Split(new char[] { '\r', '\n' }).Last()));
                        string outputTweet = JsonConvert.SerializeObject(newTweet);
                        string whereTweet = String.Format("D:\\UNI\\SET09102\\ELM\\{0}.json", newTweet.Header);
                        var ments = newTweet.checkTweet(newTweet.Body, 0);
                        foreach (var ment in ments) mentionsList.Add(ment);
                        var hashtags = newTweet.checkTweet(newTweet.Body, 1);
                        foreach(var hash in hashtags)
                        {
                            if (hashtagTrend.ContainsKey(hash)) hashtagTrend[hash] += 1;
                            else hashtagTrend.Add(hash, 1);
                        }
                        this.showOnscreen(newTweet.Sender, newTweet.Body);
                        File.WriteAllText(whereTweet, outputTweet);
                        header_txt.Clear();
                        body_txt.Clear();
                        break;
                    default:
                        MessageBox.Show("Wrong header");
                        break;
                }

                
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        public void showOnscreen(string sender, string body, string subject = "")
        {
            displaySender_txt.Text = sender;
            displaySubject_txt.Text = subject;
            displayMessage_txt.Text = body;
            
        }
    }
}
