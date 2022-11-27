using Microsoft.AspNetCore.SignalR;

namespace WeEatKholodets.Hubs;

public class MealsHub : Hub
{
    public async Task SendMeal(string user){
        await Clients.Others.SendAsync("ReceiveMeal", user);
    }
}