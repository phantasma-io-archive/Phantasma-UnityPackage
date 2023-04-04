using LunarLabs.Parser;

namespace Phantasma.SDK
{
    public struct TokenExternal
    {
        public string platform;
        public string hash;

        public static TokenExternal FromNode(DataNode node)
        {
            TokenExternal result = new TokenExternal();
            result.platform = node.GetString("platform");
            result.hash = node.GetString("hash");
            return result;
        }
    }
}