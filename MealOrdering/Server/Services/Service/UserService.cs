using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MealOrdering.Server.Data.Context;
using MealOrdering.Server.Data.Models;
using MealOrdering.Server.Services.Infrastructure;
using MealOrdering.Shared.Dtos;
using MealOrdering.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace MealOrdering.Server.Services.Service
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly MealOrderingDbContext _context;
        private readonly IConfiguration _configuration;

        public UserService(IMapper mapper, MealOrderingDbContext context, IConfiguration configuration)
        {
            _mapper = mapper;
            _context = context;
            _configuration = configuration;
        }

        public async Task<UserDto> GetUserById(Guid id)
        {
            return await _context.Users.Where(x => x.Id == id)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<List<UserDto>> GetUsers()
        {
            return await _context.Users
                 .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                 .ToListAsync();
        }

        public async Task<UserDto> CreateUser(UserDto user)
        {
            var dbUser = await _context.Users.Where(x => x.Id == user.Id).FirstOrDefaultAsync();
            if (dbUser != null)
            {
                throw new Exception("İlgili kayır zaten mevcut");
            }

            dbUser = _mapper.Map<User>(user);

            dbUser.Password = PasswordEncrypter.Encrypt(user.Password);

            await _context.Users.AddAsync(dbUser);
            int result = await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(dbUser);
        }

        public async Task<UserDto> UpdateUser(UserDto user)
        {
            var dbUser = await _context.Users.Where(x => x.Id == user.Id).FirstOrDefaultAsync();
            if (dbUser == null)
            {
                throw new Exception("İlgili kayıt bulunamadı");
            }

            _mapper.Map(user, dbUser);

            dbUser.Password = PasswordEncrypter.Encrypt(user.Password);

            int result = await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(dbUser);
        }

        public async Task<bool> DeleteUserById(Guid id)
        {
            var dbUser = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (dbUser == null)
            {
                throw new Exception("Kullanıcı bulunamadı");
            }

            _context.Users.Remove(dbUser);
            int result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<UserLoginResponseDto> Login(string email, string password)
        {
            //db kullanıcı dogrulama işlemleri yapıldı

            var encryptedPassword = PasswordEncrypter.Encrypt(password);

            var dbUser = await _context.Users.FirstOrDefaultAsync(i => i.EmailAddress == email && i.Password == encryptedPassword);

            if (dbUser == null)
                throw new Exception("User not found or given information is wrong");

            if (!dbUser.IsActive)
                throw new Exception("User state is Passive!");

            UserLoginResponseDto result = new UserLoginResponseDto();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expire = DateTime.Now.AddDays(int.Parse(_configuration["JwtExpiryInDays"]));

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, dbUser.FirstName + " " + dbUser.LastName),
                new Claim(ClaimTypes.UserData, dbUser.Id.ToString())
            };

            var token = new JwtSecurityToken(
            issuer: _configuration["JwtIssuer"],
            audience: _configuration["JwtAudience"],
            claims: claims,
            notBefore: null,
            expires: expire,
            signingCredentials: creds
                );

            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

            result.ApiToken = tokenStr.ToString();
            result.User = _mapper.Map<UserDto>(dbUser);

            return result;
        }
    }
}
