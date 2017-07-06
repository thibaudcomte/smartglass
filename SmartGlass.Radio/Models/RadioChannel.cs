using System;

namespace SmartGlass.Radio.Models
{
    public class RadioChannel
    {
        public string Name { get; }

        public Uri StreamUri { get; }

        public RadioChannel(string name, Uri streamUri)
        {
            Name = name;
            StreamUri = streamUri;
        }
    }
}
