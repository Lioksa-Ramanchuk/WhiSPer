using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.ServiceModel.Syndication;
using System.ServiceModel.Web;
using System.Text;
using System.Xml;
using Newtonsoft.Json;
using WSRAMModel;

namespace WsRamSyndicationService
{
    public class StudentNotesFeed : IStudentNotesFeed
    {
        private const string _kServiceBaseUri = "http://localhost:56570/WsRamDataService.svc";

        public Stream GetStudentNotes(string studentId)
        {
            if (!int.TryParse(studentId, out int studentIdParsed))
            {
                return new MemoryStream();
            }

            string format = WebOperationContext
                .Current
                ?.IncomingRequest
                ?.UriTemplateMatch
                ?.QueryParameters["format"];
            bool usingAtom = format?.ToLower() == "atom";

            return usingAtom ? GetAtomFeed(studentIdParsed) : GetJsonResponse(studentIdParsed);
        }

        private Stream GetAtomFeed(int studentId)
        {
            var memoryStream = new MemoryStream();
            var feedData = GetFeedData(studentId);
            if (feedData == null)
            {
                return memoryStream;
            }

            var feed = new SyndicationFeed(feedData.Title, feedData.Subtitle, null)
            {
                Items = feedData
                    .Entries.Select(entry => new SyndicationItem(
                        title: entry.Title,
                        content: entry.Content,
                        itemAlternateLink: null
                    ))
                    .ToList(),
            };

            using (
                var writer = XmlWriter.Create(
                    memoryStream,
                    new XmlWriterSettings { CloseOutput = false }
                )
            )
            {
                new Atom10FeedFormatter(feed).WriteTo(writer);
                writer.Flush();
            }
            memoryStream.Position = 0;

            WebOperationContext.Current.OutgoingResponse.ContentType = "application/atom+xml";
            return memoryStream;
        }

        private Stream GetJsonResponse(int studentId)
        {
            var feedData = GetFeedData(studentId);
            if (feedData == null)
            {
                return new MemoryStream();
            }

            string jsonResult = JsonConvert.SerializeObject(
                new
                {
                    title = feedData.Title,
                    subtitle = feedData.Subtitle,
                    updated = feedData.Updated,
                    entries = feedData.Entries.Select(entry => new
                    {
                        title = entry.Title,
                        updated = feedData.Updated,
                        content = entry.Content,
                    }),
                }
            );

            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonResult));
        }

        private FeedData GetFeedData(int studentId)
        {
            var db = new WSRAMEntities(new Uri(_kServiceBaseUri));
            Student student;

            try
            {
                student = db.Students.Where(s => s.id == studentId).AsEnumerable().FirstOrDefault();
            }
            catch (DataServiceQueryException)
            {
                return null;
            }

            if (student == null)
            {
                return null;
            }

            var notes = db.Notes.Where(note => note.student_id == studentId).AsEnumerable();
            var now = DateTime.UtcNow;

            return new FeedData
            {
                Title = $"{student.name}'s Notes",
                Subtitle = $"Notes of the student with ID {studentId}",
                Updated = now,
                Entries = notes
                    .Select(note => new FeedEntry
                    {
                        Title = $"#{note.id}",
                        Content = $"{note.subj} - {note.note1}",
                    })
                    .ToList(),
            };
        }

        public class FeedData
        {
            public string Title { get; set; }
            public string Subtitle { get; set; }
            public DateTime Updated { get; set; }
            public List<FeedEntry> Entries { get; set; }
        }

        public class FeedEntry
        {
            public string Title { get; set; }
            public string Content { get; set; }
        }
    }
}
