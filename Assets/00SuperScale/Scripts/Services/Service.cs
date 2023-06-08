using System;

namespace SuperScale.Services
{
    /// <summary>
    /// Service can be used to access data or methods all over the codebase.
    /// Once a Service is created, it needs to be added to the ServiceRegistry
    /// </summary>
    public abstract class Service : IDisposable
    {
        public abstract void Dispose();
    } 
}
