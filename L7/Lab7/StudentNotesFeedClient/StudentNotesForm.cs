using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml;
using Newtonsoft.Json.Linq;

namespace StudentNotesFeedClient
{
    public partial class StudentNotesForm : Form
    {
        public StudentNotesForm()
        {
            InitializeComponent();
            FeedFormatInput.Items.AddRange(new string[] { "json", "atom" });
            FeedFormatInput.SelectedIndex = 0;
        }

        private void FetchButton_Click(object sender, System.EventArgs e)
        {
            //HttpWebRequest request = WebRequest.Create($@"http://localhost:8107/WsRamSyndicationService/StudentNotesFeed/{StudentIdInput.Value}?format={FeedFormatInput.SelectedText.ToLower()}");
            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //var content = new StreamReader(response.GetResponseStream()).ReadToEnd();
            //ContentTextBox.Text = content;

            string format = FeedFormatInput.SelectedItem.ToString();
            string url = $"http://localhost:8107/WsRamSyndicationService/StudentNotesFeed/{StudentIdInput.Value}?format={format}";

            try
            {
                string content;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    content = reader.ReadToEnd();
                }

                RawContentTextBox.Text = content;

                if (format == "json")
                {
                    DisplayJson(content);
                }
                else if (format == "atom")
                {
                    DisplayAtom(content);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayJson(string jsonResponse)
        {
            var json = JObject.Parse(jsonResponse);
            
            StringBuilder formattedResult = new StringBuilder();
            formattedResult.AppendLine($"Title: {json["title"]}");
            formattedResult.AppendLine($"Subtitle: {json["subtitle"]}");
            formattedResult.AppendLine($"Updated: {json["updated"]}");
            formattedResult.AppendLine();
            formattedResult.AppendLine("Entries:");
            foreach (var entry in json["entries"])
            {
                formattedResult.AppendLine($" - {entry["title"]}: {entry["content"]}");
            }

            ContentTextBox.Text = formattedResult.ToString();
        }

        private void DisplayAtom(string atomResponse)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(atomResponse);

            StringBuilder formattedResult = new StringBuilder();

            var nsManager = new XmlNamespaceManager(doc.NameTable);
            nsManager.AddNamespace("atom", "http://www.w3.org/2005/Atom");

            var title = doc.SelectSingleNode("//atom:title", nsManager)?.InnerText;
            var subtitle = doc.SelectSingleNode("//atom:subtitle", nsManager)?.InnerText;
            var updated = doc.SelectSingleNode("//atom:updated", nsManager)?.InnerText;

            formattedResult.AppendLine($"Title: {title}");
            formattedResult.AppendLine($"Subtitle: {subtitle}");
            formattedResult.AppendLine($"Updated: {updated}");
            formattedResult.AppendLine();
            formattedResult.AppendLine("Entries:");
            foreach (XmlNode entry in doc.SelectNodes("//atom:entry", nsManager))
            {
                var entryTitle = entry.SelectSingleNode("atom:title", nsManager)?.InnerText;
                var entryContent = entry.SelectSingleNode("atom:content", nsManager)?.InnerText;

                formattedResult.AppendLine($" - {entryTitle}: {entryContent}");
            }

            ContentTextBox.Text = formattedResult.ToString();
        }

    }
}