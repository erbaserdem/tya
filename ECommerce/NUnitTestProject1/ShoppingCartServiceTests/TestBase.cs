using System;
using System.Collections.Generic;
using System.Text;
using ECommerce.Models;
using ECommerce.Services;
using ECommerce.Services.Interfaces;
using Moq;
using NUnit.Framework;

namespace ECommerce.UnitTests.ShoppingCartServiceTests
{
    public abstract class TestBase
    {
        protected readonly Mock<IProductService> productService = new Mock<IProductService>();
        protected readonly Mock<IDeliveryCostCalculatorService> deliveryCostCalculatorService = new Mock<IDeliveryCostCalculatorService>();
        protected readonly Mock<ICampaignService> campaignService = new Mock<ICampaignService>();
        protected Product productToAddCart;
        protected ShoppingCart cart = new ShoppingCart();
        protected ShoppingCartService cartService;
        protected IList<Campaign> campaigns;
        protected readonly int quantity = 15;


        [OneTimeSetUp]
        public virtual void Setup()
        {
            cartService = new ShoppingCartService(productService.Object, deliveryCostCalculatorService.Object, campaignService.Object);
            SetUpData();
            SetUpMocks();
        }

        protected virtual void SetUpData()
        {
        }

        protected virtual void SetUpMocks()
        {
        }
    }
}
