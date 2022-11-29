using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using WeEatKholodets.Data;
using WeEatKholodets.Models;
using WeEatKholodets.Pages;

namespace WeEatKholodets.Tests;

public class WantToEatModelTest
{
    [Fact]
    public void OnGetAsync_IsDidCustomerEatTodayEqualTrue_WhenCustomerAteToday()
    {
        // Arrange
        List<Meal> meals = new List<Meal>(){
            new Meal(){
                Id = 0,
                Date = DateTime.Today,
                User = new User(){
                    Id = "000"
                }
            }
        };
        var mockMealRep = new Mock<IMealRepository>();
        mockMealRep.Setup(r => r.GetMealsByUserId("000")).Returns(meals.AsQueryable());
        var pageModel = new WantToEatModel(mockMealRep.Object, TestUserManager<User>().Object);

        //Act
        pageModel.OnGet();

        //Assert
        Assert.True(pageModel.DidCustomerEatToday);
    }

    [Fact]
    public void OnGetAsync_IsDidCustomerEatTodayEqualFalse_WhenCustomerDidNotEatToday()
    {
        // Arrange
        List<Meal> meals = new List<Meal>(){
            new Meal(){
                Id = 0,
                Date = DateTime.Today.AddDays(-1),
                User = new User(){
                    Id = "000"
                }
            }
        };
        var mockMealRep = new Mock<IMealRepository>();
        mockMealRep.Setup(r => r.GetMealsByUserId("000")).Returns(meals.AsQueryable());
        var pageModel = new WantToEatModel(mockMealRep.Object, TestUserManager<User>().Object);

        //Act
        pageModel.OnGet();

        //Assert
        Assert.False(pageModel.DidCustomerEatToday);
    }

    public static Mock<UserManager<TUser>> TestUserManager<TUser>() where TUser : class
    {
        var store = new Mock<IUserStore<TUser>>();
        var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
        mgr.Object.UserValidators.Add(new UserValidator<TUser>());
        mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

        mgr.Setup(x => x.GetUserId(null)).Returns("000");

        return mgr;
    }
}
