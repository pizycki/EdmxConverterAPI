#load "common.csx"
#load "htmlHelpers.csx"

using System.IO.Compression;

public class Base64ToXmlConverter : IConvertRequestHandler
{
    public string Handle(EdmxConvertRequest request)
    {
        var base64 = request.Source;
        var edmx = ConvertToXml(base64);
        if (request.EncodeHtml)
        {
            edmx = HtmlHelpers.EncodeHtml(edmx);
        }
        return edmx;
    }    

    public static string ConvertToXml(string base64)
    {
        byte[] compressedBytes = Convert.FromBase64String(base64);
        var memoryStream = new MemoryStream(compressedBytes);
        var gzip = new GZipStream(memoryStream, CompressionMode.Decompress);
        var reader = new StreamReader(gzip);
        var xml = reader.ReadToEnd();
        return xml;
    }
}
