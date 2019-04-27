using System;

namespace Twitter
{
    public class TwitterItem
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Tweet { get; set; }
        public DateTime Timestamp { get; set; }
        public int? ImageSeed { get; set; }

        public int Retweets { get; set; }
        public int Hearts { get; set; }
        public int Comment { get; set; }
    }
}