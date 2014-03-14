using AutoMapper;
using WebApp.Domain.Entities;
using WebApp.Web.Areas.Admin.ViewModels;
using StructureMap;

namespace WebApp.Web
{
    public static class AutoMapperInitializer
    {
        public static void Initialize()
        {
            Mapper.Initialize(c =>
                {
                    c.ConstructServicesUsing(ObjectFactory.GetInstance);

                    c.CreateMap<EditFileTypeViewModel, FileType>();
                    c.CreateMap<FileType, EditFileTypeViewModel>();
                });
        }
    }
}