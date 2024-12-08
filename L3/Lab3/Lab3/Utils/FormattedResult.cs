using System.Net.Mime;
using System.Text.Json;
using System.Xml.Serialization;
using Lab3.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.Utils;

public class FormattedResult<T> : ContentResult
{
    private static readonly JsonSerializerOptions s_jsonOptions =
        new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public FormattedResult(T data, string? format = null, int statusCode = StatusCodes.Status200OK)
    {
        StatusCode = statusCode;
        var isXml = format?.ToLower() == DataFormat.Xml;
        ContentType = isXml ? MediaTypeNames.Application.Xml : MediaTypeNames.Application.Json;

        if (isXml)
        {
            var serializer = new XmlSerializer(typeof(T));
            using var writer = new StringWriter();
            serializer.Serialize(writer, data);
            Content = writer.ToString();
        }
        else
        {
            Content = JsonSerializer.Serialize(data, s_jsonOptions);
        }
    }
}
