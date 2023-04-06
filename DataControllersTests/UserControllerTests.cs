using Bogus;
using Data;
using Data.Controllers;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ControllersTests
{
    [TestClass]
    public class UserControllerTests
    {
        /*[TestMethod]
        public void AddNewUser()
        {
            *//*var userStub = new DatabaseGenerator().GenerateUserData(5);
            var userData = userStub.AsQueryable();
            var mockDbContext = new Mock<FmDbContext>();
            var userMockSet = new Mock<DbSet<User>>();
            userMockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userData.Provider);
            userMockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userData.ElementType);
            userMockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userData.Expression);
            userMockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => userData.GetEnumerator());
            mockDbContext.Setup(x => x.Users).Returns(userMockSet.Object);

            FmDbContext dbContext = mockDbContext.Object;
            UserController userController = new UserController(dbContext);

            var user = userController.GetAt(0);
            var user2 = dbContext.Users.First();

            Assert.AreEqual(user, user2);*//*
        }
        
        [TestMethod]
        public void AddMultipleNewUsers()
        {
            
            DatabaseGenerator databaseGenerator = new DatabaseGenerator();
            FmDbContext dbContext = databaseGenerator.GetFmDbContext();

            System.Diagnostics.Debug.WriteLine("Flights:");
            foreach (var f in dbContext.Flights)
            {
                foreach (PropertyDescriptor p in TypeDescriptor.GetProperties(f))
                {
                    System.Diagnostics.Debug.WriteLine($"{p.Name}: {p.GetValue(f)}");
                }
                System.Diagnostics.Debug.WriteLine("");
                System.Diagnostics.Debug.WriteLine("");
            }
            
            
        }*/
    }
}
