using System;

namespace ProcSpector.Core.Plugins
{
    public sealed record CtxMenuItem(
        CtxMenu Target,
        string Title,
        EventHandler<object> Handler
    )
    {
    }
}