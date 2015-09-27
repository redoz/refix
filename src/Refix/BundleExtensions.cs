using System.Collections.Generic;
using System.Linq;

namespace Refix
{
    public static class BundleExtensions
    {
        public static IEnumerable<Script> GetScripts(this Bundle bundle)
        {
            return bundle.OfType<Script>();
        } 
    }
}