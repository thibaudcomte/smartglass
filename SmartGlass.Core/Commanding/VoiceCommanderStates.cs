using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Media.SpeechRecognition;

namespace SmartGlass.Core.Commanding
{
    public abstract class VoiceCommanderAbstractState
    {
        internal readonly VoiceCommander _Commander;
        internal string _Status;
        internal string Status
        {
            get { return _Status; }
            set
            {
                _Status = value;
                StatusChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        internal event EventHandler StatusChanged;

        internal VoiceCommanderAbstractState(VoiceCommander commander)
        {
            _Commander = commander;
        }

        internal abstract Task ProcessAsync(SpeechRecognitionResult input, 
            IEnumerable<IVoiceCommandProcessor> processors);
    }

    internal class VoiceCommanderPassiveState : VoiceCommanderAbstractState
    {
        internal VoiceCommanderPassiveState(VoiceCommander commander)
            : base(commander)
        {
            Status = "Say \"Hey, Smart Glass!\" to enter commanding mode.";
        }

        internal override Task ProcessAsync(SpeechRecognitionResult input, 
            IEnumerable<IVoiceCommandProcessor> processors = null)
        {
            if (input.Confidence != SpeechRecognitionConfidence.Rejected &&
                input.Constraint != null &&
                input.Constraint.Tag == "beginInteraction")
            {
                _Commander.State = new VoiceCommanderActiveState(_Commander);
            }

            return Task.CompletedTask;
        }
    }

    internal class VoiceCommanderActiveState : VoiceCommanderAbstractState
    {
        internal VoiceCommanderActiveState(VoiceCommander commander)
            : base(commander)
        {
            Status = "Please say a command.";
        }

        internal override async Task ProcessAsync(SpeechRecognitionResult input, 
            IEnumerable<IVoiceCommandProcessor> processors)
        {
            if (input.Confidence == SpeechRecognitionConfidence.Rejected)
            {
                Status = "Sorry, I didn't catch that. Please say a command.";
                return;
            }

            Status = $"Heard you say \"{input.Text}\".";

            var tags = new Dictionary<string, string>();
            foreach (var prop in input.SemanticInterpretation.Properties)
            {
                tags[prop.Key] = prop.Value[0];
            }

            foreach (var processor in processors)
            {
                await processor.ProcessRecognizedVoiceCommandAsync(input.Text, tags);
            }

            _Commander.State = new VoiceCommanderPassiveState(_Commander);
        }
    }
}
