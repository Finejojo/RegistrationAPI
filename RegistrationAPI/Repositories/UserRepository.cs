using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RegistrationAPI.Data;
using RegistrationAPI.DTO;
using RegistrationAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static RegistrationAPI.Repositories.UserRepository;

namespace RegistrationAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public UserRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        public async Task<ApiResponse<UserDTO>> RegisterAsync(UserDTO userDTO)
        {

            if (await _context.Users.AnyAsync(u => u.Phone == userDTO.Phone || u.Email == userDTO.Email))
            {
                throw new Exception("User with this phone or email already exists");
            }

            var newUser = new User
            {
                FullName = userDTO.FullName,
                Email = userDTO.Email,
                Phone = userDTO.Phone,
                Password = userDTO.Password
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

                return new ApiResponse<UserDTO>()
                {
                    Success = true,
                    Message = "user registered successfully"

                };
        }
        public async Task<ApiResponse<string>> LoginAsync(LoginDTO loginDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Phone == loginDTO.Username && u.Password == loginDTO.Password);

            if(user == null)
            {
                return new ApiResponse<string>()
                {
                    Success = false,
                    Message = "Wrong username or Password",

                };
            }
            var token = GenerateJwtToken(user);

            return new ApiResponse<string>()
            {
                Success = true,
                Message = "Login successfully",
                Data = token.ToString()

            };
        }

        public async Task<ApiResponse<User>> GetProfileAsync(GetProfile getProfile)
        {
            var profile = await _context.Users.FirstOrDefaultAsync(u => u.Phone == getProfile.Phone);

           if(profile != null)
            {
                return new ApiResponse<User>()
                {
                    Success = true,
                    Message = "User details",
                    Data = profile
                };
            }

            return new ApiResponse<User>()
            {
                Success = true,
                Message = "User details not found"

            };
        }
        private object GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.Phone),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            // Set a default expiration in minutes if "AccessTokenExpiration" is missing or not a valid numeric value.
            if (!double.TryParse(jwtSettings["AccessTokenExpiration"], out double accessTokenExpirationMinutes))
            {
                accessTokenExpirationMinutes = 30; // Default expiration of 30 minutes.
            }

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(accessTokenExpirationMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}


