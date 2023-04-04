using LunarLabs.Parser;

namespace Phantasma.SDK
{
    public struct Receipt
    {
        public string nexus; //
        public string channel; //
        public string index; //
        public uint timestamp; //
        public string sender; //
        public string receiver; //
        public string script; //

        public static Receipt FromNode(DataNode node)
        {
            Receipt result;

            result.nexus = node.GetString("nexus");
            result.channel = node.GetString("channel");
            result.index = node.GetString("index");
            result.timestamp = node.GetUInt32("timestamp");
            result.sender = node.GetString("sender");
            result.receiver = node.GetString("receiver");
            result.script = node.GetString("script");

            return result;
        }
    }
}