using surveyapi.Dtos.User;
using surveyapi.Entities;
using surveyapi.Exceptions;
using surveyapi.Extentions;
using surveyapi.Models;
using surveyapi.Repositories;

namespace surveyapi.Services;

internal sealed class AuthService : IAuthRepository
{
    private readonly IGenericRepository<User> _genericRepository;
    private readonly IGenericRepository<UserRole> _userRoleRepository;
    private readonly IGenericRepository<Role> _roleRepository;
    private readonly ITokenRepository _tokenGenerator;

    public AuthService(
        IGenericRepository<User> genericRepository,
        ITokenRepository tokenGenerator,
        IGenericRepository<UserRole> userRoleRepository, IGenericRepository<Role> roleRepository)
    {
        _genericRepository = genericRepository;
        _tokenGenerator = tokenGenerator;
        _userRoleRepository = userRoleRepository;
        _roleRepository = roleRepository;   
    }

    public async Task<string> Login(LoginDto loginDto)
    {
        if (loginDto.Email != null)
        {
            var user = await _genericRepository.GetAsync(u => u.Email == loginDto.Email);

            if (user != null)
            {
                bool verify = Verify(loginDto.Password, user.Password);

                if (verify)
                {
                  
                    var userRole = await _userRoleRepository.GetAsync(ur => ur.UserId == user.Id);
                    if (userRole != null)
                    {
                        var role = await _roleRepository.GetAsync(r => r.Id == userRole.RoleId);
                        if (role != null)
                        {
                          
                            return _tokenGenerator.CreateToken(user, role.Name);
                        
                        }
                    }
                }
                else
                {
                    throw new SurveyException(401, "incorrect_password");
                }
            }
            else
            {
                throw new SurveyException(404, "user_not_found");
            }
        }
        throw new SurveyException(404, "user_not_found");
    }

    public async ValueTask<UserModel> Registration(UserForCreationDto user)
    {
        var existingUser = await _genericRepository.GetAsync(u => u.Email == user.Email);

        if (existingUser == null)
        {
            string passwordHash = user.Password.Encrypt();

            User newUser = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = passwordHash
            };

            var registeredUser = await _genericRepository.CreateAsync(newUser);
            await _genericRepository.SaveChangesAsync();

           
            UserRole userRole = new UserRole
            {
                UserId = registeredUser.Id,
                RoleId = 2 
            };

            await _userRoleRepository.CreateAsync(userRole);
            await _userRoleRepository.SaveChangesAsync();
            await _genericRepository.SaveChangesAsync();

            return new UserModel().MapFromEntity(registeredUser);
        }
        else
        {
            throw new SurveyException(401, "user_already_exist");
        }
    }

    public static bool Verify(string password, string hashedPassword)
    {
        string hashedInputPassword = password.Encrypt();
        return string.Equals(hashedInputPassword, hashedPassword);
    }
}


