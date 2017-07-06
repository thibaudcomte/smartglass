using SmartGlass.Core.Commanding;
using System;

namespace SmartGlass.Core.Module
{
    public interface IModule : IDisposable
    {
        void Initialize();

        IVoiceCommandProcessor GetVoiceCommandProcessor();
    }
}
