using Microsoft.VisualStudio.TestTools.UnitTesting;
using CityPowerAndLight.Services;
using CityPowerAndLight.Model;
using Microsoft.Identity.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace CityPowerAndLightTest
{
    [TestClass]
    public sealed class AccountServiceTest
    {
        private Mock<IOrganizationService> _mockOrganizationService;
        private AccountService _accountService;

        [TestInitialize]
        public void Setup()
        {
            _mockOrganizationService = new Mock<IOrganizationService>();
            _accountService = new AccountService(_mockOrganizationService.Object);
        }

        [TestMethod]
        public void GetAllAccounts_ShouldReturnListOfAccounts()
        {
            // Arrange
            var expectedAccounts = new List<Account>
              {
              new Account { Name = "Account 1" },
              new Account { Name = "Account 2" }
              };

            var entityCollection = new EntityCollection(expectedAccounts.Select(a => a.ToEntity<Entity>()).ToList());
            _mockOrganizationService.Setup(s => s.RetrieveMultiple(It.IsAny<QueryExpression>())).Returns(entityCollection);

            // Act
            var result = _accountService.GetAllAccounts();

            // Assert
            Assert.AreEqual(expectedAccounts.Count, result.Count);
            Assert.AreEqual(expectedAccounts[0].Name, result[0].Name);
            Assert.AreEqual(expectedAccounts[1].Name, result[1].Name);
        }
    }
}
