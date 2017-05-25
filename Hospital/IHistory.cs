using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    interface IHistory
    {
        Dictionary<DateTime, string> ShowHistory(Patient patient);
    }
}
