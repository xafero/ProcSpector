using Microsoft.Extensions.Logging;
using ProcSpector.Grpc;

// ReSharper disable NotAccessedField.Local

namespace ProcSpector.Server.Services
{
    public class InspectorService : Inspector.InspectorBase
    {
        private readonly ILogger<InspectorService> _log;

        public InspectorService(ILogger<InspectorService> log)
            => _log = log;
    }
}