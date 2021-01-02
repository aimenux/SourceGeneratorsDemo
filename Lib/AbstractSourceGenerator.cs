using Microsoft.CodeAnalysis;
using System.Diagnostics;

namespace Lib
{
    public abstract class AbstractSourceGenerator : ISourceGenerator
    {
        private readonly bool _isDebuggingEnabled;

        protected AbstractSourceGenerator(bool isDebuggingEnabled = false)
        {
            _isDebuggingEnabled = isDebuggingEnabled;
        }

        public abstract void Execute(GeneratorExecutionContext context);

        public virtual void Initialize(GeneratorInitializationContext context)
        {
#if DEBUG
            if (_isDebuggingEnabled && !Debugger.IsAttached)
            {
                Debugger.Launch();
            }
#endif
        }
    }
}
