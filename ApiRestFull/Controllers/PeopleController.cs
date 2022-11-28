using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using ApiRestFull.Models.RequestWrappers;
using ApiRestFull.Services.ContractService;
using ContractInterface.Common.Facade;
using ContractInterface.Common.Helpers.OperationResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nethereum.Web3;
using Nethereum.Web3.Accounts.Managed;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiRestFull.Controllers
{
    [Authorize]
    [Route("api/{contractAddress}/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly ILogger _logger;
        private IConfiguration _config;
        private IContractFacade _contractFacade;
        private IContractOperation _operation;
        private Web3 _web3;
        private ManagedAccount _account;
        private ContractService _contractService;
        private IAccountService _accountService;
        public PeopleController(
            IConfiguration configuration,
            IContractFacade contractFacade,
            IContractOperation operation,
            ILogger<ContractController> logger,
            ContractService contractService,
            IAccountService accountService)
        {
            _config = configuration;
            _contractFacade = contractFacade;
            _operation = operation;
            _logger = logger;
            _contractService = contractService;
            _accountService = accountService;
            _web3 = _accountService.GetWeb3();
            _account = _accountService.GetAccount();
        }


        [HttpPost("holder/add")]
        public async Task<ActionResult<TransactionResult>> AddHolders(PeopleRequest request, string contractAddress)
        {
            var contract = await _contractFacade.GetContract("GamerToken", true, contractAddress);
            return await _operation.AddHolder(contract.Contract, _web3, _account.Address, request.ToAdd);
        }

        [HttpPost("holder/remove")]
        public async Task<ActionResult<TransactionResult>> RemoveHolder(PeopleRequest request, string contractAddress)
        {
            var contract = await _contractFacade.GetContract("GamerToken", true, contractAddress);
            return await _operation.RemoveHolder(contract.Contract, _web3, _account.Address, request.ToRemove);
        }

        [HttpGet("holder/inquire")]
        public async Task<ActionResult<bool>> HolderExist(PeopleRequest request, string contractAddress)
        {
            var contract = await _contractFacade.GetContract("GamerToken", true, contractAddress);
            return await _operation.HolderExist(contract.Contract, request.ToInquire);
        }

        [HttpPost("appointee/remove")]
        public async Task<ActionResult<TransactionResult>> RemoveAppointee(PeopleRequest request, string contractAddress)
        {
            var contract = await _contractFacade.GetContract("GamerToken", true, contractAddress);
            return await _operation.RemoveAppointee(contract.Contract, _web3, _account.Address, request.ToRemove);
        }

        [HttpGet("appointee/inquire")]
        public async Task<ActionResult<BigInteger>> Allowance(PeopleRequest request, string contractAddress)
        {
            var contract = await _contractFacade.GetContract("GamerToken", true, contractAddress);
            return await _operation.Allowance(contract.Contract, _account.Address, request.MapTo, request.ToInquire);
        }

        [HttpPost("appointee/approve")]
        public async Task<ActionResult<TransactionResult>> Approve(PeopleRequest request, string contractAddress)
        {
            var contract = await _contractFacade.GetContract("GamerToken", true, contractAddress);
            return await _operation.Approve(contract.Contract, _web3, _account.Address, request.ToAdd, request.Allowance);
        }

        [HttpGet("balance")]
        public async Task<ActionResult<BigInteger>> GetBalance(PeopleRequest request, string contractAddress)
        {
            var contract = await _contractFacade.GetContract("GamerToken", true, contractAddress);
            return await _operation.GetBalanceOf(contract.Contract, request.ToInquire);
        }

        [HttpPost("transfer")]
        public async Task<ActionResult<TransactionResult>> Transfer(TransactionRequest request, string contractAddress)
        {
            var contract = await _contractFacade.GetContract("GamerToken", true, contractAddress);
            return await _operation.Transfer(contract.Contract, _web3, _account.Address, request.FromAddress, request.ToAddress, request.Amount);
        }

    }
}

