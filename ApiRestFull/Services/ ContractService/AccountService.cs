using System;
using Microsoft.AspNetCore.Authentication;
using Nethereum.Web3;
using Nethereum.Web3.Accounts.Managed;
using System.Security.Claims;
using System.Text;

namespace ApiRestFull.Services.ContractService
{
    public class AccountService : IAccountService
    {
        private ManagedAccount _account;
        private ContractService _contractService;
        private Web3 _web3;
        private bool _isAuthenticated = false;
        public AccountService(IConfiguration config, ContractService contractService, IServiceProvider service, IHttpContextAccessor context)
        {
            var auth = context.HttpContext.AuthenticateAsync();
            // var res = auth?.AuthenticateAsync();
            if (auth.Result.Succeeded)
                _isAuthenticated = true;

            _config = config;
            _contractService = contractService;

            SetAccount();
            SetWeb3();

        }
        private readonly IConfiguration _config;
        List<AccountDAO> _accounts = new List<AccountDAO>(){
            new AccountDAO(){Address = "0x12890d2cce102216644c59dae5baed380d84830c", Password = "password"},
            new AccountDAO(){Address = "0x13f022d72158410433cbd66f5dd8bf6d2d129924", Password = "password"}
        };


        public AccountDAO Authenticate(string address, string password)
        {
            var account = _accounts.SingleOrDefault(x => x.Address.Equals(address) && x.Password.Equals(password));
            if (account == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config.GetSection("Auth").GetSection("Secret").Value);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, account.Address)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            account.Token = tokenHandler.WriteToken(token);
            account.Password = null;
            SetAccount();
            SetWeb3();
            return account;
        }

        private void SetAccount()
        {
            if (_isAuthenticated)
                _account = GetDefaultAccount();
        }

        public ManagedAccount GetAccount()
        {
            return _account;
        }

        private void SetWeb3()
        {
            if (_isAuthenticated && _account != null)
                _web3 = _contractService.GetDefaultWeb3(_account);
        }
        public Web3 GetWeb3()
        {
            return _web3;
        }
        private ManagedAccount GetDefaultAccount()
        {
            return new ManagedAccount(Constants.DEFAULT_TEST_ACCOUNT_ADDRESS, Constants.DEFAULT_TEST_ACCOUNT_PASSWORD);
        }
    }
}

