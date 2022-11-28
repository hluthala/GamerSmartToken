using System;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace ContractInterface.Common.Entities
{
    [FunctionOutput]
    public class EOAHolderDAO : AccountDAO
    {
        public List<EOAAppointeeDAO> Appointees { get; set; }

    }
}

