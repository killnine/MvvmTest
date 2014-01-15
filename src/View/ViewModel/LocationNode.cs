using System.Collections.Generic;
using Model;

namespace View.ViewModel
{
    public class LocationNode
    {
        public IList<Machine> Machines { get; set; }
        public string Location { get; set; }
    }
}