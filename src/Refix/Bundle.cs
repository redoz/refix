using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refix
{
    public class Bundle : IEnumerable<Resource>
    {
        private readonly Resource[] _resources;

        public Bundle(string name, Resource[] resources)
        {
            this.Name = name;
            this._resources = resources;
        }

        public string Name { get; private set; }

        public IEnumerator<Resource> GetEnumerator()
        {
            return this._resources.Cast<Resource>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
