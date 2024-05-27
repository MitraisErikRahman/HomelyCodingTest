using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Service.Responses
{
    public class ReturnResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Result { get; set; }
    }
}
