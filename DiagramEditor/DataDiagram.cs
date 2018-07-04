using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiagramEditor
{
    class DataDiagram
    {
        public String Name { get; set; }
        public int Procent { get; set; }
        public DataDiagram(string n, int p)
        {
            Name = n;
            Procent = p;
        }

    }
}
