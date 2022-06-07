using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

using GHR.Application.Dtos.Contas;

using GHR.Application.Services.Contracts.Contas;

using GHR.Domain.DataBase.Contas;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GHR.Application
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<Conta> _userManager;
        private readonly IMapper _mapper;
        public readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config,
                            UserManager<Conta> userManager,
                            IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public async Task<string> CadastrarToken(ContaAtualizarDto contaAtualizarDto)
        {
            var user = _mapper.Map<Conta>(contaAtualizarDto);

            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            var roles = await _userManager.GetRolesAsync(user);

            claims.AddRange(roles.Select(
                role => new Claim(ClaimTypes.Role, role)
            ));

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(10),
                SigningCredentials = creds
            };


            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);

        }
    }
}