using Abstractions.Interfaces;
using AutoMapper;
using Common.Exceptions;
using Common.Resources;
using DTOs.Buffalo.User;
using Entities.Buffalo;
using EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto dto)
        {
            await CheckDuplicateUser(dto);

            var user = _mapper.Map<User>(dto);

            var newUser = await _unitOfWork.GetRepository<User>().InsertAsync(user);

            await _unitOfWork.CompleteAsync();

            return await GetUserAsync(newUser.Id);
        }

        private async Task CheckDuplicateUser(CreateUserDto dto)
        {
            var existUser = await _unitOfWork.GetRepository<User>().GetAll().FirstOrDefaultAsync(x => x.UserName.Trim().ToUpper() == dto.UserId.Trim().ToUpper());

            if (existUser != null)
            {
                throw new BusinessException(ExceptionResource.DuplicationEmail);
            }
        }

        public async Task<UserDto> GetUserAsync(Guid userId)
        {
            var userFromDb = await _unitOfWork.GetRepository<User>()
                .GetAll()
                .FirstOrDefaultAsync(x => x.Id == userId);

            return _mapper.Map<UserDto>(userFromDb);
        }
    }
}
