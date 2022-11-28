using System;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Nethereum.Web3.Accounts.Managed;

namespace ApiRestFull.Services.ContractService
{
    public class ContractService
    {
        private readonly IContractFacade _contractFacade;
        private readonly IConfiguration _config;
        public ContractService(IContractFacade contractFacade, IConfiguration configuration)
        {
            _contractFacade = contractFacade;
            _config = configuration;
        }
        public async Task<DeploymentResult> DeployGamerSmartToken()
        {
            var abi = await _contractFacade.GetAbi("GamerToken", false, null);
            var byteCode = await _contractFacade.GetByteCode("GamerToken", false, null);
            return await _contractFacade.Deploy("GamerToken",
                                abi,
                                byteCode,
                                Constants.DEFAULT_TEST_ACCOUNT_ADDRESS,
                                Constants.DEFAULT_TEST_ACCOUNT_PASSWORD,
                                new HexBigInteger(Constants.DEFAULT_GAS));
        }

        public Web3 GetDefaultWeb3(ManagedAccount account)
        {
            return new Web3(account, _config.GetSection(Constants.GETH_RPC).Value);
        }



    }
}

