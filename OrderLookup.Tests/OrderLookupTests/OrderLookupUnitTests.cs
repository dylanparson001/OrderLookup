using NSubstitute;
using NUnit.Framework;
using OrderLookup.Sql.Interfaces;
using OrderLookup.Sql.Models;
using OrderLookup.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderLookup.Tests.OrderLookupTests
{
    [TestFixture]
    internal class OrderLookupUnitTests
    {
        private IOrderLookupRepo mockLookupRepo;

        private List<Order> existingOrders = new List<Order>()
        {
            new Order() { BolId="test", OrderId="test", DestinationId="testDest", PickupId="Testpickup", Processing_Status="NULL", SourceId="testsource", Status_Date= DateTime.Now },
            new Order() { BolId="test", OrderId="test", DestinationId="testDest", PickupId="Testpickup", Processing_Status="NULL", SourceId="testsource", Status_Date= DateTime.Now },
            new Order() { BolId="test", OrderId="test", DestinationId="testDest", PickupId="Testpickup", Processing_Status="NULL", SourceId="testsource", Status_Date= DateTime.Now }
        };

        [SetUp]
        public async Task Setup()
        {
            mockLookupRepo = Substitute.For<IOrderLookupRepo>();
        }

        [Test]
        public async Task BolEntryTextCannotBeEmpty_ThrowsException()
        {


            mockLookupRepo.GetOrders(Arg.Any<string>()).Returns(existingOrders);

            var bolLookupViewModel = new BolLookupViewModel(mockLookupRepo);

            bolLookupViewModel.BolText = "";

            Assert.Throws<Exception>(async () => await bolLookupViewModel.RunOrderLookup());
        }
    }
}
