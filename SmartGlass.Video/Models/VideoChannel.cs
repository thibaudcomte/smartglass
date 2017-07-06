namespace SmartGlass.Video.Models
{
    public class VideoChannel
    {
        public string Name { get; }

        public string Id { get; }

        public VideoChannel(string name, string id)
        {
            Name = name;
            Id = id;
        }
    }
}
