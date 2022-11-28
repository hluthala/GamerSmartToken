using System;
using System.Numerics;

namespace ApiRestFull.Models.RequestWrappers
{
    public class TransactionRequest
    {
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public BigInteger Amount { get; set; }
    }
}

