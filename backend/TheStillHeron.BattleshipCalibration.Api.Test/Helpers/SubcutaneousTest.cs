using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace TheStillHeron.BattleshipCalibration.Api.Test.Helpers;

public abstract class SubcutaneousTest
{
    protected WebApplicationFactory<Program> Factory;

    private HttpClient? _client;
    private HttpClient Client
    {
        get
        {
            _client ??= Factory.CreateClient();
            return _client;
        }
    }

    [SetUp]
    public void SetupEachTest()
    {
        Factory = new WebApplicationFactory<Program>();
    }

    [TearDown]
    public void TearDownEachTest()
    {
        Factory.Dispose();
        _client?.Dispose();
        _client = null;
    }

    protected async Task<(TResponse?, HttpStatusCode, string)> PostRequest<TRequest, TResponse>(
        string path,
        TRequest request
    )
    {
        using StringContent jsonContent =
            new(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Client.PostAsync(path, jsonContent);

        var rawResponse = await response.Content.ReadAsStringAsync();
        TResponse? decodedResponse = default;
        try
        {
            decodedResponse = JsonConvert.DeserializeObject<TResponse>(rawResponse);
        }
        catch { }
        return (decodedResponse, response.StatusCode, rawResponse);
    }

    protected async Task<(TResponse?, HttpStatusCode, string)> PutRequest<TRequest, TResponse>(
        string path,
        TRequest request
    )
    {
        using StringContent jsonContent =
            new(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await Client.PutAsync(path, jsonContent);
        var rawResponse = await response.Content.ReadAsStringAsync();
        TResponse? decodedResponse = default;
        try
        {
            decodedResponse = JsonConvert.DeserializeObject<TResponse>(rawResponse);
        }
        catch { }
        return (decodedResponse, response.StatusCode, rawResponse);
    }
}
