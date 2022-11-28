using System;
using Nethereum.Web3;
using Nethereum.Web3.Accounts.Managed;

namespace ApiRestFull.Services.ContractService
{
    public interface IAccountService
    {
        AccountDAO Authenticate(string address, string password);
        ManagedAccount GetAccount();
        Web3 GetWeb3();
    }
}

