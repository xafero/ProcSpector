using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using ProcSpector.API;
using ProcSpector.Comm;
using ProcSpector.Grpc;

// ReSharper disable NotAccessedField.Local

namespace ProcSpector.Server.Services
{
    public class InspectorService : Inspector.InspectorBase
    {
    }
}