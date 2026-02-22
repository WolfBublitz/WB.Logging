using System;

namespace WB.Logging;

internal sealed class DelegateDisposable(Action action) : IDisposable
{
    public void Dispose()
        => action();
}