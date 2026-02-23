using System;

namespace WB.Logging;

internal sealed class DelegateDisposable(Action action) : IDisposable
{
    // ┌─────────────────────────────────────────────────────────────────────────────┐
    // │ Public Methods                                                              │
    // └─────────────────────────────────────────────────────────────────────────────┘
    public void Dispose()
        => action();
}