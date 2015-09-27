using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;

namespace Refix
{
    // this might be a good reason to not write code at 3 am
    internal class Location
    {
        private readonly ImmutableStack<string> _stack;
        private readonly Lazy<Location> _lazyParent;
        private readonly Lazy<bool> _lazyIsRoot;
        private readonly Lazy<Location> _lazyRoot;

        public Location()
        {
            this._stack = ImmutableStack<string>.Empty;
            this._lazyParent = new Lazy<Location>(this.GetParent);
            this._lazyIsRoot = new Lazy<bool>(this.GetIsRoot);
            this._lazyRoot = new Lazy<Location>(this.GetRoot);
        }

        private Location GetRoot()
        {
            if (this.IsRoot)
                return this;

            return new Location(ImmutableStack<string>.Empty);
        }

        private Location GetParent()
        {
            return new Location(this._stack.Pop());
        }

        private bool GetIsRoot()
        {
            return this._stack.IsEmpty;
        }

        private Location(ImmutableStack<string> stack)
        {
            this._stack = stack;
        }

        public Location Root => this._lazyRoot.Value;
        public string Name => this._stack.Peek();
        public Location Parent => this._lazyParent.Value;
        public bool IsRoot => this._lazyIsRoot.Value;
    }

    internal class RepositoryNavigator : IRepositoryNavigator
    {
        private readonly LibGit2Sharp.IRepository _git;
        private Location _location;
        private Branch _branch;
//        private GitOBj _object;

        public RepositoryNavigator(LibGit2Sharp.IRepository git, Location location = null)
        {
            this._git = git;
            this._location = location;
  //          this._object
        }

        public IRepositoryNavigator CreateNavigator()
        {
            return new RepositoryNavigator(this._git, this._location);
        }

        public string Name
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                return this._location.Name;
            }
        }

        public ItemType Type { get; }

        public bool HasChildren { get; }

        public bool MoveToFirstChild()
        {
            
            throw new NotImplementedException();
        }

        public bool MoveToChild(string name)
        {
            throw new NotImplementedException();
        }

        public bool MoveToChild(ItemType type)
        {
            throw new NotImplementedException();
        }

        public bool MoveToNext()
        {
            throw new NotImplementedException();
        }

        public bool MoveToNext(string name)
        {
            throw new NotImplementedException();
        }

        public bool MoveToNext(ItemType type)
        {
            throw new NotImplementedException();
        }

        public bool MoveToPrevious()
        {
            throw new NotImplementedException();
        }

        public bool MoveToParent()
        {
            if (this._location.IsRoot)
                return false;
            else
            {
                this._location = this._location.Parent;
                return true;
            }
        }

        public void MoveToRoot()
        {
            this._location = this._location.Root;
        }
    }
    public class Repository : IRepository, IDisposable, IRepositoryNavigable
    {
        private LibGit2Sharp.IRepository _git;

        public Repository(string path)
        {
            Contract.Requires<ArgumentNullException>(path != null, "path");

            RepositoryOptions repositoryOptions = new RepositoryOptions();
            this._git = new LibGit2Sharp.Repository(path, repositoryOptions);
            var navigator = new RepositoryNavigator(this._git);
            this.Root = new Container(navigator);
        }

        public Container Root { get; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Repository()
        {
            this.Dispose(false);
        }

        public IRepositoryNavigator CreateNavigator()
        {
            return new RepositoryNavigator(this._git);
        }

        private void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                
            }
        }
    }

}
