using ConsoleTeat;
using Newtonsoft.Json;
using System.Net.Http.Headers;

var filters = new
{
    spLanguageId = new
    {
        value = 5
    }
};
while (true)
{
HttpClient client = new HttpClient();
HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://online.pdp.uz/");
    await client.SendAsync(httpRequestMessage);
}