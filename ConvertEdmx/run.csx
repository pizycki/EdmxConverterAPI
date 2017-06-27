#r "Newtonsoft.Json"

#load "common.csx"
#load "convertBase64ToXml.csx"

using System.Net;
using System.Text;
using System.IO.Compression;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage httpRequest, TraceWriter log)
{
    dynamic payload = await httpRequest.Content.ReadAsStringAsync();
    var convertRequest = JsonConvert.DeserializeObject<EdmxConvertRequest>(payload as string);
    var handler = ResolveRequestHandler(convertRequest);
    var content = handler.Handle(convertRequest);
    var response = new HttpResponseMessage(HttpStatusCode.OK)
    {
        Content = new StringContent(content, Encoding.UTF8, "text/plain")
    };
    return response;
}

public static IConvertRequestHandler ResolveRequestHandler(EdmxConvertRequest request)
{
    var targetType = EdmxTypes.GetAsEnum[request.TargetType];
    var sourceType = EdmxTypes.GetAsEnum[request.SourceType];
    var handlerType = new KeyValuePair<EdmxType,EdmxType>(sourceType, targetType);
    var handler = ConvertRequestHandlers[handlerType];
    var instance = (IConvertRequestHandler)Activator.CreateInstance(handler);
    return instance;
}

public static IReadOnlyDictionary<KeyValuePair<EdmxType, EdmxType>, Type> ConvertRequestHandlers = 
    new Dictionary<KeyValuePair<EdmxType, EdmxType>, Type>()
    {
        { new KeyValuePair<EdmxType, EdmxType>(EdmxType.Resource, EdmxType.Xml), typeof(Base64ToXmlConverter) },
        
        // Add more handlers here ...
    };