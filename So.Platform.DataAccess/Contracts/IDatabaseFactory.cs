
using Oc.Carbon.DataAccess;
using System;

namespace Oc.Carbon.DataAccess.Contracts
{
    public interface IDatabaseFactory : IDisposable
    {
        SoPlatformEntities Get();
    }
}
