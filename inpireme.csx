//this should be a gist, but....
//any way drop this into your azure function:
using System.Net;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info("C# HTTP trigger function processed a request.");
    
    var inspirobotUrl = "http://inspirobot.me/api?generate=true";
    
    // call inspiro got to get the image url
    string resultContent = "";
    using(var client = new HttpClient())
    {
        var result = await client.GetAsync(inspirobotUrl);
        resultContent = await result.Content.ReadAsStringAsync();
        log.Info(resultContent);
    }
    
    return req.CreateResponse(
        HttpStatusCode.OK, 
        new { 
            response_type = "in_channel",
            attachments = new []{
                new {image_url = resultContent }
        }}
    );
}
