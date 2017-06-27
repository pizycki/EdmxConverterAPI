public enum EdmxType
{
    Resource, Byte, Xml
}

public class EdmxConvertRequest
{
    public string SourceType { get; set; }

    public string TargetType { get; set; }

    public string Source { get; set; }

    public bool EncodeHtml { get; set; }
}

public class ConvertToXmlRequest : EdmxConvertRequest
{
    public bool EncodeHtml { get; set;} 
}

public class EdmxTypes
{
    public const string Resource = "resource";
    public const string Byte = "byte";
    public const string Xml = "xml";

    public static IReadOnlyDictionary<EdmxType, string> GetAsString;

    public static IReadOnlyDictionary<string, EdmxType> GetAsEnum;

    static EdmxTypes()
    {
        GetAsString = new Dictionary<EdmxType, string>
        {
            { EdmxType.Resource, Resource },
            { EdmxType.Byte, Byte },
            { EdmxType.Xml, Xml }
        };

        GetAsEnum = GetAsString.ToDictionary(item => item.Value, item => item.Key);
    }
}


public interface IConvertRequestHandler
{
    string Handle(EdmxConvertRequest request);
}