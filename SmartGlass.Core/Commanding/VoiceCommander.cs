using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Globalization;
using Windows.Media.SpeechRecognition;
using Windows.Storage;

namespace SmartGlass.Core.Commanding
{
    public class VoiceCommander : IDisposable
    {
        private readonly SpeechRecognizer _Recognizer;
        private readonly List<IVoiceCommandProcessor> _Processors;

        private VoiceCommanderAbstractState _State;
        internal VoiceCommanderAbstractState State
        {
            get
            {
                return _State;
            }

            set
            {
                if (_State != value)
                {
                    if(_State != null)
                    {
                        _State.StatusChanged -= _State_StatusChanged;
                    }

                    _State = value;
                    VoiceCommanderStateStatusChanged?.Invoke(this, State.Status);
                    _State.StatusChanged += _State_StatusChanged;
                }
            }
        }

        public string StateStatus => State.Status;
        public SpeechRecognizerState SpeechRecognizerState => _Recognizer.State;

        private void _State_StatusChanged(object sender, EventArgs e)
        {
            VoiceCommanderStateStatusChanged?.Invoke(this, State.Status);
        }

        public VoiceCommander(Language language)
        {
            _Recognizer = new SpeechRecognizer(language);
            _Processors = new List<IVoiceCommandProcessor>();
            State = new VoiceCommanderPassiveState(this);

            _Recognizer.Constraints.Add(new SpeechRecognitionListConstraint(new string[]
            {
                "Hey, Smart Glass",
                "Smart Glass"
            }, "beginInteraction"));

            _Recognizer.Timeouts.InitialSilenceTimeout = TimeSpan.FromSeconds(6.0);
            _Recognizer.Timeouts.BabbleTimeout = TimeSpan.FromSeconds(4.0);
            _Recognizer.Timeouts.EndSilenceTimeout = TimeSpan.FromSeconds(1.2);
        }

        public async Task RegisterCommandProcessorAsync(IVoiceCommandProcessor processor)
        {
            if (processor == null)
                throw new ArgumentNullException(nameof(processor));

            _Recognizer.Constraints.Add(
                new SpeechRecognitionGrammarFileConstraint(
                    await StorageFile.GetFileFromApplicationUriAsync(
                        processor.GetVoiceCommandGrammarFileUri(_Recognizer.CurrentLanguage)
                        ??
                        throw new Exception($"No grammar defined by processor {processor.GetType().Name} for the language {_Recognizer.CurrentLanguage.LanguageTag}."))));

            _Processors.Add(processor);
        }

        public async Task BeginAsync()
        {
            var compilationResult = await _Recognizer.CompileConstraintsAsync();
            if (compilationResult.Status != SpeechRecognitionResultStatus.Success)
                throw new Exception("Unable to compile the constraints.");

            _Recognizer.StateChanged += Recognizer_StateChanged;
            _Recognizer.RecognitionQualityDegrading += _Recognizer_RecognitionQualityDegrading;
            _Recognizer.ContinuousRecognitionSession.Completed += ContinuousRecognitionSession_Completed;
            _Recognizer.ContinuousRecognitionSession.ResultGenerated += ContinuousRecognitionSession_ResultGenerated;

            await _Recognizer.ContinuousRecognitionSession.StartAsync();
        }

        private void _Recognizer_RecognitionQualityDegrading(SpeechRecognizer sender, SpeechRecognitionQualityDegradingEventArgs args)
        {
            var pb = args.Problem;
        }

        private void Recognizer_StateChanged(SpeechRecognizer sender, SpeechRecognizerStateChangedEventArgs args)
        {
            VoiceCommanderSpeechRecognizerStateChanged?.Invoke(this, args.State);
        }

        private async void ContinuousRecognitionSession_ResultGenerated(SpeechContinuousRecognitionSession sender,
            SpeechContinuousRecognitionResultGeneratedEventArgs args)
        {
            await _State.ProcessAsync(args.Result, _Processors);
        }

        private async void ContinuousRecognitionSession_Completed(SpeechContinuousRecognitionSession sender,
            SpeechContinuousRecognitionCompletedEventArgs args)
        {
            // rarely happens but if it does, then restart the recognition session right away
            await _Recognizer.ContinuousRecognitionSession.StartAsync();
        }

        public async Task EndAsync()
        {
            await _Recognizer.ContinuousRecognitionSession.StopAsync();
            _Recognizer.StateChanged -= Recognizer_StateChanged;
            _Recognizer.ContinuousRecognitionSession.Completed -= ContinuousRecognitionSession_Completed;
            _Recognizer.ContinuousRecognitionSession.ResultGenerated -= ContinuousRecognitionSession_ResultGenerated;
        }

        public async void Dispose()
        {
            await EndAsync();
            _Recognizer?.Dispose();
            _Processors.Clear();
        }

        public event EventHandler<SpeechRecognizerState> VoiceCommanderSpeechRecognizerStateChanged;
        public event EventHandler<string> VoiceCommanderStateStatusChanged;
    }
}
