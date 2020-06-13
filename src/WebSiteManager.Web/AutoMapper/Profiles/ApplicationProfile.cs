using AutoMapper;
using WebSiteManager.DataModels;
using WebSiteManager.Services;
using WebSiteManager.Web.AutoMapper.Resolvers;

namespace WebSiteManager.Web.AutoMapper.Profiles
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Models.Page, Page>();
            CreateMap<Paginated<WebSite>, Models.Paginated<Models.WebSiteDetailed>>();
            CreateMap<WebSite, Models.WebSiteDetailed>()
                .ForMember(dest => dest.Category,
                    o =>
                        o.MapFrom(s => s.Category.Name))
                .ForMember(dest => dest.Email,
                    o =>
                        o.MapFrom(s => s.Login.Username))
                .ForMember(dest => dest.Password,
                    o =>
                        o.MapFrom<PasswordConverter>());
        }
    }
}
