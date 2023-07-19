using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Unity.Portal.Web.Hubs;

namespace Unity.Portal.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IHubContext<NotificationsHub, IUserNotification> _notificationsHub;

    public IndexModel(ILogger<IndexModel> logger, IHubContext<NotificationsHub, IUserNotification> notificationsHub)
    {
        _logger = logger;
        _notificationsHub = notificationsHub;
    }

    public async Task OnGetAsync()
    {
        await _notificationsHub.Clients.All.ReceiveNotification("user", "message");

        try
        {
            await CallScoringApiAsync();
        }
        catch{ }

        try
        {
            await CallWorkItemsApiAsync();
        }
        catch{ }
    }

    private async Task CallWorkItemsApiAsync()
    {
        string Baseurl = "http://host.docker.internal:5090/";
        using var client = new HttpClient();
        HttpRequestMessage request = new()
        {
            RequestUri = new Uri(Baseurl),
            Method = HttpMethod.Get
        };

        HttpResponseMessage response = await client.SendAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();
        var statusCode = response.StatusCode;

        if (response.IsSuccessStatusCode)
        {
            //API call success, Do your action
            WorkItemsMessage = responseString;
        }
        else
        {
            //API Call Failed, Check Error Details
        }
    }

    private async Task CallScoringApiAsync()
    {
        string Baseurl = "http://host.docker.internal:5080/";
        using var client = new HttpClient();
        HttpRequestMessage request = new()
        {
            RequestUri = new Uri(Baseurl),
            Method = HttpMethod.Get
        };

        HttpResponseMessage response = await client.SendAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();
        var statusCode = response.StatusCode;

        if (response.IsSuccessStatusCode)
        {
            //API call success, Do your action
            ScoringMessage = responseString;
        }
        else
        {
            //API Call Failed, Check Error Details
        }
    }

    public string? WorkItemsMessage { get; set; } = "Getting Work Items Message";
    public string? ScoringMessage { get; set; } = "Getting Scoring Message";
}
