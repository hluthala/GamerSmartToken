using System;
namespace ApiRestFull.Models.RequestWrappers
{
    public class ContractRequest
    {
        public string ContractAddress { get; set; }
        public string ContractName { get; set; }
        public bool IsDeployed { get; set; }
    }
}

