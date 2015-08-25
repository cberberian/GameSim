using System.Collections.Generic;
using System.Linq;
using Moq;
using MvcApplication1;
using MvcApplication1.Controllers;
using NUnit.Framework;
using Should;
using SimGame.Data.Interface;
using SimGame.Data.Mock;
using SimGame.Domain;
using Product = MvcApplication1.Models.Product;

namespace SimGame.MvcApplication1Tests
{
    [TestFixture]
    public class ProductTypeControllerTests
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            AutoMapperConfig.ConfigureMappings();
        }

        [Test]
        public void Scenario1_null_request()
        {
            var gameSimContext = new Mock<IGameSimContext>();
            var controller = new ProductTypeController(gameSimContext.Object);

            var ret = controller.Post(null);
            ret.ShouldBeNull();
        }
        [Test]
        public void Scenario2_new_product_type()
        {
            var gameSimContext = new Mock<IGameSimContext>();
            

            var pDs = new List<ProductType>()
            {
                new ProductType
                {
                    Id = 1
                }
            };
            var productTypes = new FakeDbSet<ProductType>(pDs);
            gameSimContext.Setup(x => x.ProductTypes).Returns(productTypes);
            var controller = new ProductTypeController(gameSimContext.Object);

            var productTypeToSave = new MvcApplication1.Models.ProductType
            {
                Name = "test"
            };
            var ret = controller.Post(productTypeToSave);
            productTypes.Count().ShouldEqual(2);
            productTypes.ToArray()[1].Id.ShouldEqual(2);
            ret.Id.ShouldEqual(2);
        }

        [Test]
        public void Scenario2_existing_product_type()
        {
            var gameSimContext = new Mock<IGameSimContext>();

            var domainProductType = new ProductType
            {
                Id = 1
            };
            var pDs = new List<ProductType>()
            {
                domainProductType
            };
            var productTypes = new FakeDbSet<ProductType>(pDs);
            gameSimContext.Setup(x => x.ProductTypes).Returns(productTypes);
            var controller = new ProductTypeController(gameSimContext.Object);

            var productTypeToSave = new MvcApplication1.Models.ProductType
            {
                Id = 1,
                Name = "test",
                ManufacturerTypeId = 1,
                Products = new List<Product>(),
                RequiredProducts = new List<Product>(),
                TimeToManufacture = 10,
                SalePriceInDollars = 10
            };
            var ret = controller.Post(productTypeToSave);
            productTypes.Count().ShouldEqual(1);
            ret.Id.ShouldEqual(1);
            gameSimContext.Verify(x=>x.SetValues(It.IsAny<ProductType>(), It.IsAny<ProductType>()));
            
        }

        [Test]
        public void Scenario3_required_product_added()
        {
            var gameSimContext = new Mock<IGameSimContext>();

            var domainProductType = new ProductType
            {
                Id = 1,
                RequiredProducts = new List<Domain.Product>()
            };
            var pDs = new List<ProductType>()
            {
                domainProductType
            };
            var productTypes = new FakeDbSet<ProductType>(pDs);
            gameSimContext.Setup(x => x.ProductTypes).Returns(productTypes);
            var controller = new ProductTypeController(gameSimContext.Object);

            var productTypeToSave = new MvcApplication1.Models.ProductType
            {
                Id = 1,
                Name = "test",
                ManufacturerTypeId = 1,
                Products = new List<Product>(),
                RequiredProducts = new List<Product>
                {
                    new Product
                    {
                        Quantity = 1,
                        ProductTypeId = 2
                    }
                },
                TimeToManufacture = 10,
                SalePriceInDollars = 10
            };
            var ret = controller.Post(productTypeToSave);
            productTypes.Count().ShouldEqual(1);
            ret.Id.ShouldEqual(1);
            gameSimContext.Verify(x=>x.SetValues(It.IsAny<ProductType>(), It.IsAny<ProductType>()));
            domainProductType.RequiredProducts.Any().ShouldBeTrue();
            domainProductType.RequiredProducts.First().Quantity.ShouldEqual(1);
            domainProductType.RequiredProducts.First().ProductTypeId.ShouldEqual(2);
            
        }


    }
}
