using LunarLabs.Parser;

namespace Phantasma.SDK
{
    public struct Oracle
    {
        public string url; //
        public string content; //

        public static Oracle FromNode(DataNode node)
        {
            Oracle result;

            result.url = node.GetString("url");
            result.content = node.GetString("content");

            return result;
        }
    }
}