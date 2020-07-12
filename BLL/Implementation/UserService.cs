using AutoMapper;
using BLL.Abstract;
using Common.Exceptions;
using DAL.Abstract;
using DAL.Enities;
using DomainModels.DomainModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Implementation
{
    public class UserService : IUserService
    {
        #region Fields

        private readonly IUserRepository _userRepo;

        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public UserService(IMapper mapper, IUserRepository userRepo)
        {
            _mapper = mapper;
            _userRepo = userRepo;
        }

        #endregion

        #region Operations

        public async Task<List<UserModel>> GetAllAsync(string category = null)
        {
            List<User> users = await _userRepo.GetAllAsync();
            List<UserModel> userModels = _mapper.Map<List<UserModel>>(users);

            if (userModels is null || userModels.Count == 0)
            {
                throw new NotFoundException("There are no users.");
            }

            if (category is null)
            {
                return userModels;
            }

            userModels = userModels.Where(cat => cat.Category.Equals(category))?.ToList();

            if (userModels.Count == 0)
            {
                throw new NotFoundException("There are no users.");
            }

            return userModels;
        }

        public async Task<UserModel> GetByIdAsync(int id)
        {
            User user = await _userRepo.GetByIdAsync(id);

            if (user is null)
            {
                throw new NotFoundException("User doesn't exist.");
            }

            UserModel userModel = _mapper.Map<UserModel>(user);

            return userModel;
        }

        public async Task InsertAsync(UserModel userModel)
        {
            if (userModel == null)
            {
                throw new BadRequestException("User is null.");
            }

            if (await _userRepo.GetByEmailAsync(userModel.Email) != null)
            {
                throw new BadRequestException("User already exist.");
            }

            User user = MapUserModelToUser(userModel);

            await _userRepo.InsertAsync(user);
        }

        public async Task UpdateAsync(UserModel userModel)
        {
            if (userModel is null)
            {
                throw new BadRequestException("User is null.");
            }

            User user = await _userRepo.GetByEmailAsync(userModel.Email);

            if (user is null)
            {
                throw new NotFoundException("User doesn't exist.");
            }

            userModel.Id = user.Id;
            user = MapUserModelToUser(userModel);

            await _userRepo.UpdateAsync(user);
        }

        public async Task DeleteAsync(int userId)
        {
            User user = await _userRepo.GetByIdAsync(userId);
            if (user is null)
            {
                throw new NotFoundException("User doesn't exist.");
            }

            await _userRepo.DeleteAsync(user);
        }

        #endregion

        #region Implementation

        private User MapUserModelToUser(UserModel userModel)
        {
            User user = _mapper.Map<User>(userModel);

            return user;
        }

        #endregion
    }
}