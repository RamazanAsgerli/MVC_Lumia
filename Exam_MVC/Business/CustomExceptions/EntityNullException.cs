using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CustomExceptions
{
    public class EntityNullException : Exception
    {
        public string V {  get; set; }
        public EntityNullException(string v,string? message) : base(message)
        {
            V = v;
        }
    }
}
