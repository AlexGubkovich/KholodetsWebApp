using Microsoft.AspNetCore.SignalR;

namespace Kholodets.Core.Hubs;

public class MealsHub : Hub
{
    public async Task SendMeal(string user)
    {
        await Clients.Others.SendAsync("ReceiveMeal", user);
    }
}