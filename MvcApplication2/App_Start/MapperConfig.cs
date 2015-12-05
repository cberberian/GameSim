using System.Text;
using AutoMapper;
using SimGame.Website.Models;

namespace SimGame.Website
{
    public class MapperConfig
    {
        public static void Configure()
        {
            Mapper.CreateMap<Product, Domain.Product>();
            Mapper.CreateMap<ProductType, Domain.ProductType>();
            Mapper.CreateMap<ManufacturerType, Domain.ManufacturerType>();
            Mapper.CreateMap<BuildingUpgrade, Domain.BuildingUpgrade>();

            Mapper.CreateMap<Domain.ProductType, ProductType>()
                .ForMember(x => x.RequiredProductsToolTip, opt => opt.ResolveUsing<RequiredProductTooltipResolve>());
            Mapper.CreateMap<Domain.ManufacturerType, ManufacturerType>();
            Mapper.CreateMap<Domain.BuildingUpgrade, BuildingUpgrade>();
            Mapper.CreateMap<Domain.Product, Product>()
                .ForMember(x=>x.Name, opt=> opt.MapFrom(y=>y.ProductType.Name))
                .ForMember(x=>x.RequiredBy, opt=>opt.MapFrom(y=>y.RequiredBy != null ? y.RequiredBy.Name : string.Empty));

        }
    }

    public class RequiredProductTooltipResolve : ValueResolver<SimGame.Domain.ProductType, string>
    {
        protected override string ResolveCore(SimGame.Domain.ProductType source)
        {

            var sb = new StringBuilder();
//            sb.Append("<div>");
//            sb.AppendFormat("<b><u>{0}</u></b><br/>", source.Name);
//            if (source.RequiredProducts == null || source.RequiredProducts.Count == 0)
//                sb.Append("No Dependent products");
//            else
//                foreach (var x in source.RequiredProducts)
//                {
//                    sb.AppendFormat("{0} {1}<br/>", x.Quantity, x.ProductType.Name);
//                }
//            sb.Append("</div>");
                        sb.AppendFormat("{0}\\n", source.Name);
                        if (source.RequiredProducts == null || source.RequiredProducts.Count == 0)
                            sb.Append("No Dependent products");
                        else
                            foreach (var x in source.RequiredProducts)
                            {
                                sb.AppendFormat("{0} {1}\\n", x.Quantity, x.ProductType.Name);
                            }
            return sb.ToString();
        }
    }
}