using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace StudentNotesFeedClient
{
    public partial class StudentNotesForm : Form
    {
        public StudentNotesForm()
        {
            InitializeComponent();
            FeedFormatInput.Items.AddRange(new string[] { "atom", "rss", "json" });
            FeedFormatInput.SelectedIndex = 0;
        }

        private void FetchButton_Click(object sender, System.EventArgs e)
        {
            string format = FeedFormatInput.SelectedItem.ToString();
            string url =
                $"http://localhost:8107/WsRamSyndicationService/StudentNotesFeed/{StudentIdInput.Value}?format={format}";

            try
            {
                string content;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (
                    StreamReader reader = new StreamReader(
                        response.GetResponseStream(),
                        Encoding.UTF8
                    )
                )
                {
                    content = reader.ReadToEnd();
                }

                RawContentTextBox.Text = content;

                if (format == "atom")
                {
                    DisplayAtom(content);
                }
                else if (format == "rss")
                {
                    DisplayRss(content);
                } else if (format == "json")
                {
                    DisplayJson(content);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
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

        private void DisplayRss(string rssResponse)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(rssResponse);

            StringBuilder formattedResult = new StringBuilder();

            var nsManager = new XmlNamespaceManager(doc.NameTable);
            nsManager.AddNamespace("rss", "http://purl.org/rss/1.0/");

            var title = doc.SelectSingleNode("//channel/title")?.InnerText;
            var description = doc.SelectSingleNode("//channel/description")?.InnerText;

            formattedResult.AppendLine($"Title: {title}");
            formattedResult.AppendLine($"Description: {description}");
            formattedResult.AppendLine();
            formattedResult.AppendLine("Entries:");
            foreach (XmlNode item in doc.SelectNodes("//item"))
            {
                var entryTitle = item.SelectSingleNode("title")?.InnerText;
                var entryDescription = item.SelectSingleNode("description")?.InnerText;

                formattedResult.AppendLine($" - {entryTitle}: {entryDescription}");
            }

            ContentTextBox.Text = formattedResult.ToString();
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
    }
}
