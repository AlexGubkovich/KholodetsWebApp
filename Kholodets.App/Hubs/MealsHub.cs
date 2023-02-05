using Microsoft.AspNetCore.SignalR;

namespace Kholodets.App.Hubs;

public class MealsHub : Hub
{
    public async Task SendMeal(string user)
    {
        await Clients.Others.SendAsync("ReceiveMeal", user);
    }
}