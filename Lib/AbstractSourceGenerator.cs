﻿using Microsoft.CodeAnalysis;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Lib
{
    public abstract class AbstractSourceGenerator : ISourceGenerator
    {
        private readonly bool _isDebuggingEnabled;

        protected AbstractSourceGenerator(bool isDebuggingEnabled = false)
        {
            _isDebuggingEnabled = isDebuggingEnabled;
        }

        protected static readonly Encoding Encoding = Encoding.UTF8;

        public string Namespace => this.GetType().Namespace.Split('.').Last();

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
