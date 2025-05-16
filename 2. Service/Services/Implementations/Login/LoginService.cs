using Abstractions.Interfaces;
using Abstractions.Interfaces.Mail;
using AutoMapper;
using Common.Helpers;
using Common.Runtime.Security;
using Common.Unknown;
using DTOs.Buffalo;
using Entities.Buffalo;
using EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ISendMailService _sendMailService;

        private readonly AWSInfoOptions _awsOptions;

        private readonly IMapper _mapper;

        public LoginService(
            IUnitOfWork unitOfWork
            , ISendMailService sendMailService
            , IOptions<AWSInfoOptions> awsOptions
            , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _sendMailService = sendMailService;
            _awsOptions = awsOptions.Value;
            _mapper = mapper;
        }

        public async Task<LoginResult> LoginAsync(LoginDto dto)
        {
            var userFromDB = await _unitOfWork.GetRepository<User>()
                .GetAll()
                .FirstOrDefaultAsync(x => x.UserName == dto.UserName);

            if (userFromDB == null)
                return new LoginResult(LoginResultType.InvalidUserNameOrPassword);

            if (userFromDB.Password != LoginHelper.EncryptPassword(dto.Password))
            {
                return new LoginResult(LoginResultType.InvalidUserNameOrPassword);
            }    

            if (!userFromDB.IsActive)
            {
                return new LoginResult(LoginResultType.UserIsNotActive);
            }    
                
            var claimIdentity = GenerateClaimsIdentity(userFromDB);
            await _unitOfWork.CompleteAsync();

            return new LoginResult(claimIdentity);
        }

        private ClaimsIdentity GenerateClaimsIdentity(User user)
        {
            var userClaims = new List<Claim>
            {
                new Claim(GlotechClaimTypes.Id, user == null ? string.Empty : user.Id.ToString()),
                new Claim(GlotechClaimTypes.UserImageURL, user == null ? string.Empty : user.UserImageURL.ToString()),
            };

            return new ClaimsIdentity(new GenericIdentity(user == null ? string.Empty : user.UserName.ToString(), "Token"), userClaims);
        }
    }
}
