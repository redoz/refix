using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refix
{
    public class Resource
    {
        public Resource(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }

        public Stream GetStream()
        {
            throw new NotImplementedException();
        }
    }
}
