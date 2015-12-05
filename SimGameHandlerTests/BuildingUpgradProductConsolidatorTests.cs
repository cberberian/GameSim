using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Should;
using SimGame.Data.Interface;
using SimGame.Handler.Bootstrap;
using SimGame.Handler.Calculators;
using SimGame.Handler.Entities;
using SimGame.Handler.Interfaces;
using SpecsFor;
using ProductType = SimGame.Domain.ProductType;

namespace SimGameHandlerTests
{
    [TestFixture]
    public class BuildingUpgradProductConsolidatorTests
    {
        public BuildingUpgradProductConsolidatorTests()
        {
            HandlerAutomapper.Configure();
        }

        #region given_all_null_parameters
        public class given_all_null_parameters : SpecsFor<BuildingUpgradProductConsolidator>
        {
            private BuildingUpgradeProductConsolidatorResponse _result;

            protected override void When()
            {
                var request = new BuildingUpgradeProductConsoldatorRequest
                {
                    CityStorage   = null,
                    ProductTypes = null,
                    BuildingUpgrades = null
                };
                _result = SUT.GetConsolidatedProductQueue(request);
            }

            [Test]
            public void then_result_should_not_be_null()
            {
                _result.ShouldNotBeNull();
            }

            [Test]
            public void then_AvailableStorage_should_be_0()
            {
                _result.AvailableStorage.Count.ShouldEqual(0);
            }
            [Test]
            public void then_ConsolidatedRequiredProductQueue_should_be_0()
            {
                _result.ConsolidatedRequiredProductQueue.Count.ShouldEqual(0);
            }
            [Test]
            public void then_ConsolidatedRequiredProductsInStorage_should_be_0()
            {
                _result.ConsolidatedRequiredProductsInStorage.Count.ShouldEqual(0);
            }
            [Test]
            public void then_ConsolidatedTotalProductQueue_should_be_0()
            {
                _result.ConsolidatedTotalProductQueue.Count.ShouldEqual(0);
            }

        }
        #endregion

        #region given_null_productTypes
        public class given_null_productTypes : SpecsFor<BuildingUpgradProductConsolidator>
        {
            private BuildingUpgradeProductConsolidatorResponse _result;

            protected override void When()
            {
                IQueryable<ProductType> queryableProducts = new EnumerableQuery<ProductType>(FixtureHelper.GetDomainProductTypes());
                GetMockFor<IPropertyUpgradeUoW>().Setup(x => x.ProductTypeRepository.Get())
                    .Returns(queryableProducts);

                RequiredProductFlattenerResponse response = new RequiredProductFlattenerResponse
                {
                    Products = new Product[0]
                };
                GetMockFor<IRequiredProductFlattener>()
                    .Setup(x => x.GetFlattenedInventory(It.IsAny<RequiredProductFlattenerRequest>()))
                    .Returns(response);

                var request = new BuildingUpgradeProductConsoldatorRequest
                {
                    CityStorage = FixtureHelper.GetCityStorage(),
                    ProductTypes = null,
                    BuildingUpgrades = new BuildingUpgrade[0]
                };
                _result = SUT.GetConsolidatedProductQueue(request);
            }

            [Test]
            public void then_producttypeRepository_should_be_called()
            {
                GetMockFor<IPropertyUpgradeUoW>().Verify(x=>x.ProductTypeRepository.Get());
            }

        }
        #endregion

    }
}