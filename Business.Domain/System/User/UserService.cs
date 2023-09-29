using Business.Models;
using Data.BaseRepository;
using Data.Entity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Utilities.ServiceResult;

namespace Business.Domain.System.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        public UserService(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, IConfiguration config, RoleManager<AppRole> roleManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<string>> AuthenticateAsync(LoginRequestEntity request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return new ServiceErrorResult<string>("Tài khoản không tồn tại");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, true, true);
            if (!result.Succeeded)
            {
                return new ServiceErrorResult<string>("Mật khẩu không đúng");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                /*new Claim(ClaimTypes.Role, string.Join(";",roles)),*/
                new Claim(ClaimTypes.Name, request.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            return new ServiceSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
        }

        public async Task<ServiceResult<bool>> CreateUserOrderInformationAsync(Guid userId, UserOrderInformationEntity request)
        {
            try
            {
                if (request.IsDefault)
                {
                    var infos = await _unitOfWork.UserOrderInformationRepository.GetAllAsync();
                    foreach(var info in infos)
                    {
                        if(info.IsDefault)
                        {
                            info.IsDefault = false;
                        }
                    }
                    await _unitOfWork.SaveChanageAsync();
                }

                var userOrderInfo = new UserOrderInformation()
                {
                    UserId = userId,
                    Name = request.Name,
                    PhoneNumber = request.PhoneNumber,
                    Province = request.Province,
                    District = request.District,
                    Ward = request.Ward,
                    Address = request.Address,
                    IsDefault = request.IsDefault,
                    AddressType = request.AddressType,
                };
                await _unitOfWork.UserOrderInformationRepository.AddAsync(userOrderInfo);
                await _unitOfWork.SaveChanageAsync();

                return new ServiceSuccessResult<bool>();
            }
            catch (Exception ex)
            {
                return new ServiceErrorResult<bool>(ex.Message);
            }



            throw new NotImplementedException();
        }

        public async Task<ServiceResult<bool>> DeleteUserAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null) return new ServiceErrorResult<bool>("Không tìm thấy tài khoản");

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return new ServiceSuccessResult<bool>();
            }
            return new ServiceErrorResult<bool>("Xóa không thành công");
        }

        public async Task<ServiceResult<IEnumerable<string>>> GetAllRolesAsync()
        {
            return new ServiceSuccessResult<IEnumerable<string>>(await _roleManager.Roles.Select(x => x.Name.ToString()).ToListAsync());
        }

        public async Task<ServiceResult<IEnumerable<UserResponseEntity>>> GetAllUserInformationAsync()
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();

                var userResponses = new List<UserResponseEntity>();

                foreach (var user in users)
                {
                    var roles = await _userManager.IsInRoleAsync(user, "admin");
                    var userResponse = new UserResponseEntity
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Dob = user.Dob,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        IsAdmin = roles
                    };
                    userResponses.Add(userResponse);
                }
                return new ServiceSuccessResult<IEnumerable<UserResponseEntity>>(userResponses);
            }
            catch(Exception ex)
            {
                return new ServiceErrorResult<IEnumerable<UserResponseEntity>>(ex.Message);
            }
        }

        public async Task<ServiceResult<IEnumerable<UserOrderInformationEntity>>> GetUserOrderInformationAsync(Guid userId)
        {
            try
            {
                var query = await _unitOfWork.UserOrderInformationRepository.GetManyAsync(x => x.UserId == userId);
                var userOrderInfo = query.Select(x => new UserOrderInformationEntity()
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    Name = x.Name,
                    PhoneNumber = x.PhoneNumber,
                    Province = x.Province,
                    District = x.District,
                    Ward =  x.Ward,
                    Address = x.Address,    
                    IsDefault = x.IsDefault,
                    AddressType = x.AddressType,
                });

                return new ServiceSuccessResult<IEnumerable<UserOrderInformationEntity>>(userOrderInfo);
            }
            catch(Exception ex)
            {
                return new ServiceErrorResult<IEnumerable<UserOrderInformationEntity>>(ex.Message);
            }
        }

        public async Task<ServiceResult<bool>> RegisterAsync(RegisterRequestEntity request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                return new ServiceErrorResult<bool>("Tài khoản đã tồn tại");
            }
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return new ServiceErrorResult<bool>("Emai đã tồn tại");
            }

            user = new AppUser()
            {
                Dob = request.Dob,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                return new ServiceSuccessResult<bool>();
            }

            return new ServiceErrorResult<bool>("Đăng ký không thành công");
        }

        public async Task<ServiceResult<bool>> RemoveUserOrderInformationAsync(Guid userId, int userOrderInfoId)
        {
            try
            {
                var userInfo = await _unitOfWork.UserOrderInformationRepository.GetByIdAsync(userOrderInfoId);

                _unitOfWork.UserOrderInformationRepository.Remove(userInfo);

                await _unitOfWork.SaveChanageAsync();
                return new ServiceSuccessResult<bool>();
            }
            catch (Exception ex)
            {
                return new ServiceErrorResult<bool>(ex.Message);
            }
        }

        public async Task<ServiceResult<bool>> RoleAssignAsync(Guid Id, IEnumerable<string> roleList)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(Id.ToString());
                if (user == null)
                {
                    return new ServiceErrorResult<bool>("Tài khoản không tồn tại");
                }

                var roles = await _roleManager.Roles.Select(x => x.Name.ToString()).ToListAsync();

                await _userManager.RemoveFromRolesAsync(user, roles);

                await _userManager.AddToRolesAsync(user, roleList);

                return new ServiceSuccessResult<bool>();
            }
            catch (Exception ex)
            {
                return new ServiceErrorResult<bool>(ex.Message);
            }
        }

        public async Task<ServiceResult<bool>> SetAdmin(Guid id, bool isAdmin)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                if (user == null)
                {
                    return new ServiceErrorResult<bool>();
                }
                var isCurrentAdmin = await _userManager.IsInRoleAsync(user, "admin");
                if(isAdmin != isCurrentAdmin)
                {
                    if (isAdmin)
                    {
                        await _userManager.AddToRoleAsync(user, "admin");
                    }
                    else
                    {
                        await _userManager.RemoveFromRoleAsync(user, "admin");
                    }
                }
                return new ServiceSuccessResult<bool>();
            }catch (Exception ex)
            {
                return new ServiceErrorResult<bool>(ex.Message);
            }



            throw new NotImplementedException();
        }
    }
}
