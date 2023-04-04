using LunarLabs.Parser;

namespace Phantasma.SDK
{
    public struct Peer
    {
        public string url; //
        public string version; //
        public string flags; //
        public string fee; //
        public uint pow; //
        public Port[] ports;

        public static Peer FromNode(DataNode node)
        {
            Peer result;

            result.url = node.GetString("url");
            result.version = node.GetString("version");
            result.flags = node.GetString("flags");
            result.fee = node.GetString("fee");
            result.pow = node.GetUInt32("pow");

            var ports_array = node.GetNode("methods");
            if (ports_array != null)
            {
                result.ports = new Port[ports_array.ChildCount];
                for (int i = 0; i < ports_array.ChildCount; i++)
                {

                    result.ports[i] = Port.FromNode(ports_array.GetNodeByIndex(i));

                }
            }
            else
            {
                result.ports = new Port[0];
            }

            return result;
        }
    }
}