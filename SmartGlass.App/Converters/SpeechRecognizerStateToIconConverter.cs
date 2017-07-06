using System;
using Windows.Media.SpeechRecognition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace SmartGlass.App.Converters
{
    internal class SpeechRecognizerStateToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var state = (SpeechRecognizerState)value;

            switch (state)
            {
                case SpeechRecognizerState.Idle:
                case SpeechRecognizerState.SoundEnded:
                case SpeechRecognizerState.Paused:
                    return '\uE198';
                case SpeechRecognizerState.Capturing:
                case SpeechRecognizerState.Processing:
                case SpeechRecognizerState.SoundStarted:
                case SpeechRecognizerState.SpeechDetected:
                    return '\uE15D';
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
