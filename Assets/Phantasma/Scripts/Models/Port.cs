using LunarLabs.Parser;

namespace Phantasma.SDK
{
    public struct Port
    {
        public string name;
        public int port;

        public static Port FromNode(DataNode node)
        {
            var result = new Port();
            result.name = node.GetString("name");
            result.port = node.GetInt32("port");
            return result;
        }
    }
}