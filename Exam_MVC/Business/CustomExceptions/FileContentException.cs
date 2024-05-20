using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CustomExceptions
{
    public class FileContentException : Exception
    {
        public string V {  get; set; }
        public FileContentException(string v,string? message) : base(message)
        {
            V = v;
        }
    }
}
