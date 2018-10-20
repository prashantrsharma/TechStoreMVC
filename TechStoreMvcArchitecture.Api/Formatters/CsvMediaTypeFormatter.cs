using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace TechStoreMvcArchitecture.Api.Formatters
{
    public class CsvMediaTypeFormatter : MediaTypeFormatter
    {
        public CsvMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/csv"));
            SupportedEncodings.Add(System.Text.Encoding.UTF8);
        }

        public CsvMediaTypeFormatter(MediaTypeMapping mediaTypeMapping) : this()
        {
            MediaTypeMappings.Add(mediaTypeMapping);
        }

        public CsvMediaTypeFormatter(IEnumerable<MediaTypeMapping> mediaTypeMappings) : this()
        {
            foreach (MediaTypeMapping mapping in mediaTypeMappings)
            {
                MediaTypeMappings.Add(mapping);
            }
        }

        public override bool CanReadType(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return IsTypeOfIEnumerable(type);
        }

        public override bool CanWriteType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return IsTypeOfIEnumerable(type);
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content,
            TransportContext transportContext, CancellationToken cancellationToken)
        {
            WriteStream(type, value, writeStream, content.Headers);
            var tcs = new TaskCompletionSource<int>();
            tcs.SetResult(0);
            return tcs.Task;
        }

        public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger,
            CancellationToken cancellationToken)
        {
            ReadStream(type, readStream);
            var tcs = new TaskCompletionSource<object>();
            tcs.SetResult(null);
            return tcs.Task;
        }

        private bool IsTypeOfIEnumerable(Type type)
        {
            foreach (Type interfaceType in type.GetInterfaces())
            {
                if (interfaceType == typeof(IList))
                {
                    return true;
                }
            }

            return false;
        }

        private void WriteStream(Type type, object value, Stream stream, HttpContentHeaders contentHeaders)
        {
            Type itemType = type.GetGenericArguments()[0];
            StringWriter stringWriter = new StringWriter();
            stringWriter.WriteLine(string.Join<string>(",", itemType.GetProperties().Select(x => x.Name)));
            foreach (var obj in (IEnumerable<object>)value)
            {

                var propertyInfo = obj.GetType().GetProperties().Select(
                    pi => new
                    {
                        Value = pi.GetValue(obj, null)
                    }
                );

                string valueLine = string.Empty;

                foreach (var property in propertyInfo)
                {

                    if (property.Value != null)
                    {

                        var propertyValue = property.Value.ToString();

                        //Check if the value contans a comma and place it in quotes if so
                        if (propertyValue.Contains(","))
                            propertyValue = string.Concat("\"", propertyValue, "\"");

                        //Replace any \r or \n special characters from a new line with a space
                        if (propertyValue.Contains("\r"))
                            propertyValue = propertyValue.Replace("\r", " ");
                        if (propertyValue.Contains("\n"))
                            propertyValue = propertyValue.Replace("\n", " ");

                        valueLine = string.Concat(valueLine, propertyValue, ",");

                    }
                    else
                    {

                        valueLine = string.Concat(valueLine, ",");
                    }
                }
                stringWriter.WriteLine(valueLine.TrimEnd(','));
            }
            var streamWriter = new StreamWriter(stream);
            streamWriter.Write(stringWriter.ToString());

        }

        private object ReadStream(Type type, Stream stream)
        {
            Type itemType;
            var typeIsArray = false;
            IList list;
            if (type.GetGenericArguments().Length > 0)
            {
                itemType = type.GetGenericArguments()[0];
                list = (IList)Activator.CreateInstance(type);
            }
            else
            {
                typeIsArray = true;
                itemType = type.GetElementType();

                var listType = typeof(List<>);
                var constructedListType = listType.MakeGenericType(itemType);

                list = (IList)Activator.CreateInstance(constructedListType);
            }

            var reader = new StreamReader(stream, Encoding.GetEncoding("UTF8"));

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split();
                var itemTypeInGeneric = list.GetType().GetTypeInfo().GenericTypeArguments[0];
                var item = Activator.CreateInstance(itemTypeInGeneric);
                var properties = true
                    ? item.GetType().GetProperties().Where(pi => !pi.GetCustomAttributes<JsonIgnoreAttribute>().Any()).ToArray()
                    : item.GetType().GetProperties();

                for (int i = 0; i < values.Length; i++)
                {
                    properties[i].SetValue(item, Convert.ChangeType(values[i], properties[i].PropertyType), null);
                }

                list.Add(item);
            }

            if (typeIsArray)
            {
                Array array = Array.CreateInstance(itemType, list.Count);

                for (int t = 0; t < list.Count; t++)
                {
                    array.SetValue(list[t], t);
                }
                return array;
            }

            return list;
        }
    }
}
