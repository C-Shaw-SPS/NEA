using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenOpusDatabase.Lib.Responses
{
    public interface IResponse<T>
    {
        public List<T> Values { get; set; }
    }
}