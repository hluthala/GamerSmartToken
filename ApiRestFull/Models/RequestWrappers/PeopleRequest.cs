using System;
using System.Numerics;

namespace ApiRestFull.Models.RequestWrappers
{
    public class PeopleRequest
    {
        public string ToAdd { get; set; }
        public string ToRemove { get; set; }
        public string ToInquire { get; set; }
        public string MapTo { get; set; }
        public BigInteger Allowance { get; set; }
    }
}

