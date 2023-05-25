using System;

namespace SuperScale.Services
{
    public abstract class Service : IDisposable
    {
        public abstract void Dispose();
    } 
}
