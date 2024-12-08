using System.IO;
using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.ServiceModel.Web;
using WSRAMModel;

namespace WsRamSyndicationService
{
    [ServiceContract]
    [ServiceKnownType(typeof(Atom10FeedFormatter))]
    [ServiceKnownType(typeof(Rss20FeedFormatter))]
    public interface IStudentNotesFeed
    {
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/{studentId}"
        )]
        Stream GetStudentNotes(string studentId);
    }
}
