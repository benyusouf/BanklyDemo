using AutoMapper;
using BanklyDemo.Core.Complaints.Models;
using BanklyDemo.Core.Products.Models;
using BanklyDemo.Core.Users.Models;

namespace BanklyDemo.DomainServices.Bootstrap
{
    public class DomainServicesMapperProfile: Profile
    {
        public DomainServicesMapperProfile()
        {
            CreateMap<UserEntity, User>()              
                .ReverseMap()
                .ForMember(x => x.Id, y => y.Ignore())
                .ForMember(x => x.CreatedTimeUtc, y => y.Ignore())
                .ForMember(x => x.LoginProfile, y => y.Ignore());

            CreateMap<UserRegistrationModel, UserEntity>()
                .ForMember(x => x.Id, y => y.Ignore())
                .ForMember(x => x.CreatedTimeUtc, y => y.Ignore())
                .ForMember(x => x.LoginProfile, y => y.Ignore());

            CreateMap<ComplaintEntity, Complaint>().ReverseMap();

            CreateMap<ComplaintCreationModel, ComplaintEntity>()
                .ForMember(x => x.CreatedTimeUtc, y => y.Ignore())
                .ForMember(x => x.Status, y => y.Ignore())
                .ForMember(x => x.Id, y => y.Ignore())
                .ForMember(x => x.Product, y => y.Ignore());

            CreateMap<ComplaintUpdateModel, ComplaintEntity>()
                .ForMember(x => x.CreatedTimeUtc, y => y.Ignore())
                .ForMember(x => x.Status, y => y.Ignore())
                .ForMember(x => x.Id, y => y.Ignore())
                .ForMember(x => x.Product, y => y.Ignore())
                .ForMember(x => x.ProductId, y => y.Ignore())
                .ForMember(x => x.UserId, y => y.Ignore());

            CreateMap<ProductEntity, Product>().ReverseMap();

            CreateMap<SaveProductModel, ProductEntity>()
                .ForMember(x => x.CreatedTimeUtc, y => y.Ignore())
                .ForMember(x => x.Price, y => y.Ignore());
        }
    }
}
