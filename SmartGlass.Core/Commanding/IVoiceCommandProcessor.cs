using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Globalization;

namespace SmartGlass.Core.Commanding
{
    public interface IVoiceCommandProcessor
    {
        Uri GetVoiceCommandGrammarFileUri(Language language);

        Task<bool> ProcessRecognizedVoiceCommandAsync(string command, IReadOnlyDictionary<string, string> tags);
    }
}
