using AutoMapper;
using DAL.Enities;
using DomainModels.DomainModels;
using Users.DTO;

namespace Common.Mapper
{
    public class UserProfile : Profile
    {
        #region Constructor

        public UserProfile()
        {
            CreateMap<UserModel, UserDTO>();
            CreateMap<UserDTO, UserModel>();

            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>().ForSourceMember(src => src.Id, opt => opt.DoNotValidate());
        }

        #endregion
    }
}