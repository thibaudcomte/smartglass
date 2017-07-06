using Prism.Mvvm;
using SmartGlass.Core.Commanding;
using System.Threading;
using Windows.Media.SpeechRecognition;

namespace SmartGlass.App.ViewModels
{
    internal class StatusBarViewModel : BindableBase
    {
        private readonly SynchronizationContext _SynchronizationContext;

        private string _VoiceCommanderStateStatus;
        public string VoiceCommanderStateStatus
        {
            get { return _VoiceCommanderStateStatus; }
            private set { SetProperty(ref _VoiceCommanderStateStatus, value); }
        }

        private SpeechRecognizerState _VoiceCommanderSpeechRecognizerState;
        public SpeechRecognizerState VoiceCommanderSpeechRecognizerState
        {
            get { return _VoiceCommanderSpeechRecognizerState; }
            private set { SetProperty(ref _VoiceCommanderSpeechRecognizerState, value); }
        }

        public StatusBarViewModel(VoiceCommander commander)
        {
            _SynchronizationContext = SynchronizationContext.Current;

            VoiceCommanderStateStatus = commander.StateStatus;
            VoiceCommanderSpeechRecognizerState = commander.SpeechRecognizerState;

            commander.VoiceCommanderSpeechRecognizerStateChanged += VoiceCommanderSpeechRecognizerStateChanged;
            commander.VoiceCommanderStateStatusChanged += VoiceCommanderStateStatusChanged;
        }

        private void VoiceCommanderStateStatusChanged(object sender, string status)
        {
            _SynchronizationContext.Post((o) =>
            {
                VoiceCommanderStateStatus = status;
            }, null);
        }

        private void VoiceCommanderSpeechRecognizerStateChanged(object sender, SpeechRecognizerState state)
        {
            _SynchronizationContext.Post((o) =>
            {
                VoiceCommanderSpeechRecognizerState = state;
            }, null);
        }
    }
}
