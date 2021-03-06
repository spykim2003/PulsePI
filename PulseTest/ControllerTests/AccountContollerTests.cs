using PulsePI.Models;
using Xunit;
using System;
using Moq;
using PulsePI.DataContracts;
using FluentAssertions;
using PulsePI.Controllers;
using PulsePI.Service;
using PulsePI.Service.ServiceInterfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PulsePI.MessageContracts;


namespace PulseTest.ControllerTest{

      public class AccountContollerTests {

        private Mock<IAccountService> mockAccountService = new Mock<IAccountService>();

        [Fact]
        public void AccountController_Login_Test()
        {
            //arrange 
            var mockController = new AccountController(mockAccountService.Object);
            var loginData = new LoginData()
            {
                username = "mary",
                password = "mary123"
            };

            //act 
            var result = mockController.Login(loginData);

            //assert
            Assert.True(result.IsCompletedSuccessfully);
        }

            public void AccountController_CreateAccount_Test()
        {
            //arrange 
            var mockController = new AccountController(mockAccountService.Object);
          var createAccData = new CreateAccountData()
            {
                username = "john",
                password = "john123",
                firstName = "John",
                lastName = "Doe",
                email = "JohnDoe123@email.com"
            };
            //act 
            var result = mockController.CreateAccount(createAccData);

            //assert
            Assert.True(result.IsCompletedSuccessfully);
        }

         public void AccountController_UpdateAccount_Test()
        {
            //arrange 
            var mockController = new AccountController(mockAccountService.Object);
          var updateAccData = new UpdateAccountData()
            {
                username = "sweetpotato",
                firstName = "Tom",
                middleName = "George",
                lastName = "Trumpp",
                email = "tomtrumpp123@email.com",
                  avatarUrl = "elsa.jpg",
            };
            //act 
            var result = mockController.UpdateAccount(updateAccData);

            //assert
            Assert.True(result.IsCompletedSuccessfully);
        }

      

      }

}