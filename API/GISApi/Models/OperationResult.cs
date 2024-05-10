using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GISApi.Models
{
    public class OperationResult
    {
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

    }

    public class OperationResult<TResult> : OperationResult
    {
        public TResult Data { get; set; }
    }
}
