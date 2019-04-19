
using Oc.Carbon.DataAccess;
using Oc.Carbon.DataAccess.Contracts;

namespace Oc.Carbon.DataAccess.Implementations
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private SoPlatformEntities dataContext;
        public SoPlatformEntities Get()
        {
            return dataContext ?? (dataContext = new SoPlatformEntities());
        }
        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}
