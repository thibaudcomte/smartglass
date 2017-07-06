using Windows.Media.SpeechRecognition;

namespace SmartGlass.App.DesignViewModels
{
    internal class StatusBarViewModel
    {
        public string VoiceCommanderStateStatus { get; }

        public SpeechRecognizerState VoiceCommanderSpeechRecognizerState { get; }

        public StatusBarViewModel()
        {
            VoiceCommanderStateStatus = "Please say a command.";
            VoiceCommanderSpeechRecognizerState = SpeechRecognizerState.SpeechDetected;
        }
    }
}
