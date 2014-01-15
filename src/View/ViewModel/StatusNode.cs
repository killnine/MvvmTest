using System.Collections.Generic;
using Model;

namespace View.ViewModel
{
    public class StatusNode
    {
        public IList<Machine> Machines { get; set; }
        public Status Status { get; set; }
    }
}