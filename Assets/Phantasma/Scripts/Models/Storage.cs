using LunarLabs.Parser;

namespace Phantasma.SDK
{
    public struct Storage
    {
        public int available;
        public int used;
        public string avatar;

        public static Storage FromNode(DataNode node)
        {
            Storage result;
            result.available = node.GetInt32("available");
            result.used = node.GetInt32("used");
            result.avatar = node.GetString("avatar");

            return result;
        }
    }
}