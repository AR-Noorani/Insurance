using System;

namespace DomainModels.Utilities
{
    public static class TraceIdGenerator
    {
        public static string Generate()
        {
            return Guid.NewGuid().ToString("N");
        }
    }

}
