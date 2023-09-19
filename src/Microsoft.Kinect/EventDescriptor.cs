using System.Diagnostics.Tracing;

namespace Microsoft.Kinect
{
    internal struct EventDescriptor
    {
        public readonly int id;
        public readonly byte version;
        public readonly byte channel;
        public readonly byte level;
        public readonly byte opcode;
        public readonly int task;
        public readonly long keywords;

        public EventDescriptor(int id, byte version, byte channel, byte level, byte opcode, int task, long keywords)
        {
            this.id = id;
            this.version = version;
            this.channel = channel;
            this.level = level;
            this.opcode = opcode;
            this.task = task;
            this.keywords = keywords;
        }

        public EventLevel Level => (EventLevel)level;

        public EventKeywords Keywords => (EventKeywords)keywords;
    }
}