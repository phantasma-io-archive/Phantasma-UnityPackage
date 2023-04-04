using LunarLabs.Parser;

namespace Phantasma.SDK
{
    public struct Signature
    {
        public string Kind;
        public string Data;

        public static Signature FromNode(DataNode node)
        {
            Signature result;

            result.Kind = node.GetString("Kind");
            result.Data = node.GetString("Data");

            return result;
        }
    }
}