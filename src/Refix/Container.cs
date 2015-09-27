using System;
using System.Collections.Generic;
using System.Threading;

namespace Refix
{
    public class Container
    {
        public Container(string name)
        {
            this.Name = name;
        }

        public Container(IRepositoryNavigator navigator)
        {
            this.Name = navigator.Name;
        }

        public string Name { get; private set; }

        public IEnumerable<Container> GetContainers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bundle> GetBundles()
        {
            throw new NotImplementedException();
        } 
    }
}