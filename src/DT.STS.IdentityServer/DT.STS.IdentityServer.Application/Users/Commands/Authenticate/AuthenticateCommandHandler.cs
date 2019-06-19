using DT.STS.IdentityServer.Persistence;
using IdentityServer3.Core.Models;
using MediatR;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wdc.DirectoryLib;
using Wdc.DirectoryLib.Types;

namespace DT.STS.IdentityServer.Application.Users.Commands
{
    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, AuthenticateDto>
    {
        private readonly STSDbContext _context;
        private string Domain => ConfigurationManager.AppSettings["domain"];
        public AuthenticateCommandHandler(STSDbContext context)
        {
            _context = context;
        }

        public async Task<AuthenticateDto> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            AuthenticateDto authenticateDto = null;
            if (!request.Domain.ToLower().Equals("customer"))
            {
                Context context = new Context(Domain);
                UserAccount userAccount = context.GetUser(Domain, request.UserName);
                AccountStatus status = context.Authenticate(userAccount, request.Password);

                authenticateDto = new AuthenticateDto
                {
                    AccountStatus = status,
                    User = userAccount
                };

                switch (status)
                {
                    case AccountStatus.InvalidPassword:
                        authenticateDto.Message = "Mật khẩu không đúng. Xin vui lòng nhập lại";
                        // Password is incorrect. Please try again.
                        break;
                    case AccountStatus.ExpiredPassword:
                        authenticateDto.Message = "Mật khẩu hết hạn. Xin vui lòng đặt lại mật khẩu";
                        // Password has expired. Please reset your password.
                        break;
                    case AccountStatus.MustChangePassword:
                        authenticateDto.Message = "Account flagged as 'User must change password at next log on.'";
                        // Account flagged as 'User must change password at next log on.';
                        break;
                    case AccountStatus.UserLockedOut:
                        authenticateDto.Message = "Người dùng đã bị khóa. Liên hệ với IT để mở khóa cho tài khoản của bạn";
                        // Account is locked. Contact IT to unlock your account.
                        break;
                    case AccountStatus.UserNotFound:
                        authenticateDto.Message = "Người dùng không tồn tại trong Active Directory";
                        // Account does not exist in Active Directory.
                        break;
                    case AccountStatus.Success:
                        // Password is correct, so do stuff
                        break;
                }

                return authenticateDto;
            } else
            {
                request.Password = request.Password.Sha256();
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName
                && u.Password == request.Password);

                if(user != null && !string.IsNullOrEmpty(user.UserName))
                {
                    var userAccount = new UserAccount {
                        Department = user.DepartmentName,
                        GivenName = user.FirstName,
                        SurName = user.LastName,
                        DisplayName = $"{user.LastName} {user.FirstName}",
                        SamAccountName = user.UserName
                    };
                    authenticateDto = new AuthenticateDto {
                        AccountStatus = AccountStatus.Success,
                        User = userAccount
                    };
                }
                else
                {
                    authenticateDto = new AuthenticateDto
                    {
                        User = new UserAccount(),
                        AccountStatus = AccountStatus.UserNotFound,
                        Message = "Người dùng hoặc mật khẩu không tồn tại"
                    };
                }
            }
            return authenticateDto;
        }
    }
}
