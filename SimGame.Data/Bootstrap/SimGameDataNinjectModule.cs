using System.Configuration;
using System.Data.Entity;
using System.Linq;
using cb.core.data;
using cb.core.interfaces;
using Ninject.Modules;
using SimGame.Data.Interface;
using SimGame.Data.Mock;
using SimGame.Data.Repository;
using SimGame.Data.UnitOfWork;
using SimGame.Domain;

namespace SimGame.Data.Bootstrap
{
    public class SimGameDataNinjectModule : NinjectModule
    {
        public override void Load()
        {
            bool mock;
            if (bool.TryParse(ConfigurationManager.AppSettings.Get("MockGameSimContext"), out mock) && mock)
                Bind<IGameSimContext>().To<MockGameSimContext>();
            else
                Bind<IGameSimContext>().To<GameSimContext>();
            if (bool.TryParse(ConfigurationManager.AppSettings.Get("MockManufacturerTypeRepository"), out mock) && mock)
                Bind<IManufacturerTypeRepository>().To<MockManufacturerTypeRepository>();
            else
                Bind<IManufacturerTypeRepository>().To<ManufacturerTypeRepository>();
            if (bool.TryParse(ConfigurationManager.AppSettings.Get("MockManufacturerTypeUnitOfWork"), out mock) && mock)
                Bind<IManufacturerTypeUnitOfWork>().To<MockManufacturerTypeUnitOfWork>();
            else
                Bind<IManufacturerTypeUnitOfWork>().To<ManufacturerTypeUnitOfWork>();

            if (bool.TryParse(ConfigurationManager.AppSettings.Get("MockPropertyUpgradeUoW"), out mock) && mock)
                Bind<IPropertyUpgradeUoW>().To<MockPropertyUpgradeUoW>();
            else
                Bind<IPropertyUpgradeUoW>().To<PropertyUpgradeUoW>();
            if (bool.TryParse(ConfigurationManager.AppSettings.Get("MockProductTypeRepository"), out mock) && mock)
                Bind<IProductTypeRepository>().To<MockProductTypeRepository>();
            else
                Bind<IProductTypeRepository>().To<ProductTypeRepository>();

            if (bool.TryParse(ConfigurationManager.AppSettings.Get("MockProductTypeRepository"), out mock) && mock)
                Bind<IProductRepository>().To<MockProducteRepository>();
            else
                Bind<IProductRepository>().To<ProductRepository>();

            if (bool.TryParse(ConfigurationManager.AppSettings.Get("MockProductTypeRepository"), out mock) && mock)
                Bind<ICityUpdateUnitOfWork>().To<MockCityUpdateUnitOfWork>();
            else
                Bind<ICityUpdateUnitOfWork>().To<CityUpdateUnitOfWork>();

            if (bool.TryParse(ConfigurationManager.AppSettings.Get("MockProductTypeRepository"), out mock) && mock)
                Bind<IBuildingUpgradeRepository>().To<MockBuildingUpgradeRepository>();
            else
                Bind<IBuildingUpgradeRepository>().To<BuildingUpgradeRepository>();

        }
    }

    public class BuildingUpgradeRepository : AbstractRepository<IGameSimContext, BuildingUpgrade>, IBuildingUpgradeRepository
    {
        protected override IDbSet<BuildingUpgrade> RepositoryDbSet
        {
            get { return Context.BuildingUpgrades; }
        }
    }

    public class MockBuildingUpgradeRepository : IBuildingUpgradeRepository
    {
        public IGameSimContext Context { get; set; }
        public IQueryable<BuildingUpgrade> Get()
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<BuildingUpgrade> Get(RepositoryRequest<BuildingUpgrade> request)
        {
            throw new System.NotImplementedException();
        }

        public void Add(BuildingUpgrade entity)
        {
            throw new System.NotImplementedException();
        }

        public void SetValues(BuildingUpgrade dest, BuildingUpgrade chng)
        {
            throw new System.NotImplementedException();
        }
    }

    public class MockCityUpdateUnitOfWork : ICityUpdateUnitOfWork
    {
        public IProductRepository ProductRepository { get; set; }
        public IBuildingUpgradeRepository BuildingUpgradeRepository { get; set; }
        public IGameSimContext GameSimContext { get; set; }
        public IGameSimContext EntityContext { get; set; }

        public void Commit()
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Product prod)
        {
            throw new System.NotImplementedException();
        }
    }

    public class MockProducteRepository : IProductRepository
    {
        public IGameSimContext Context { get; set; }
        public IQueryable<Product> Get()
        {
            return new EnumerableQuery<Product>(new Product[] {});
        }

        public IQueryable<Product> Get(RepositoryRequest<Product> request)
        {
            return new EnumerableQuery<Product>(new Product[] { });
        }

        public void Add(Product entity)
        {
            throw new System.NotImplementedException();
        }

        public void SetValues(Product dest, Product chng)
        {
            throw new System.NotImplementedException();
        }
    }
}