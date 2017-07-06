using Microsoft.Practices.Unity;
using SmartGlass.Core.Commanding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SmartGlass.Core.Module
{
    public class ModuleManager : IDisposable
    {
        private IList<IModule> _Modules = new List<IModule>();

        private readonly IUnityContainer _Container;
        private readonly VoiceCommander _Commander;

        public ModuleManager(IUnityContainer container, VoiceCommander commander)
        {
            _Container = container;
            _Commander = commander;
        }

        public async Task LoadModulesFromAssembliesAsync()
        {
            _Modules.Clear();

            var files = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFilesAsync();
            if (files == null)
                return;

            foreach (var file in files.Where(file => file.DisplayName.StartsWith("SmartGlass.")))
            {
                try
                {
                    var assembly = Assembly.Load(new AssemblyName(file.DisplayName));
                    var moduleTypes = assembly.GetTypes().Where(type => type.GetInterfaces().Contains(typeof(IModule)));

                    foreach (var moduleType in moduleTypes)
                    {
                        _Modules.Add(_Container.Resolve(moduleType) as IModule);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }

        public void InitializeModules()
        {
            foreach (var module in _Modules)
            {
                module.Initialize();
            }
        }

        public async Task RegisterCommandProcessorsAsync()
        {
            foreach (var module in _Modules)
            {
                var processor = module.GetVoiceCommandProcessor();
                if(processor != null)
                    await _Commander.RegisterCommandProcessorAsync(processor);
            }
        }

        public void Dispose()
        {
            foreach (var module in _Modules)
            {
                module.Dispose();
            }
        }
    }
}
