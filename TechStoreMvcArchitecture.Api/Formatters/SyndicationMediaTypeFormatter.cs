using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace TechStoreMvcArchitecture.Api.Formatters
{
    public class SyndicationMediaTypeFormatter : MediaTypeFormatter
    {
        public const string Atom = "application/atom+xml";
        public const string Rss = "application/rss+xml";

        public SyndicationMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(Atom));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(Rss));
            SupportedEncodings.Add(System.Text.Encoding.UTF8);
        }
        

        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            return true;
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content,TransportContext transportContext) // <3>
        {
            var tsc = new TaskCompletionSource<object>(); // <4>
            tsc.SetResult(null);
            var items = new List<SyndicationItem>();
            if (value is IEnumerable)
            {
                foreach (var model in (IEnumerable) value)
                {
                    var item = MapToItem(model);
                    items.Add(item);
                }
            }
            else
            {
                var item = MapToItem(value);
                items.Add(item);
            }

            var feed = new SyndicationFeed(items);
            SyndicationFeedFormatter formatter = null;
            if (content.Headers.ContentType.MediaType == Atom)
            {
                formatter = new Atom10FeedFormatter(feed);
            }
            else if (content.Headers.ContentType.MediaType == Rss)
            {
                formatter = new Rss20FeedFormatter(feed);
            }
            else
            {
                throw new Exception("Not supported media type");
            }

            using (var writer = XmlWriter.Create(writeStream))
            {
                formatter.WriteTo(writer);
                writer.Flush();
                writer.Close();
            }

            return tsc.Task;
        }

        protected SyndicationItem MapToItem(object model)
        {
            var item = new SyndicationItem();
            item.ElementExtensions.Add(model);
            return item;
        }

    }
}