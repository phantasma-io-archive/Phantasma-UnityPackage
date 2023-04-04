using LunarLabs.Parser;

namespace Phantasma.SDK
{
    public struct ABIParameter
    {
        public string name; //
        public string type; //

        public static ABIParameter FromNode(DataNode node)
        {
            ABIParameter result;

            result.name = node.GetString("name");
            result.type = node.GetString("type");

            return result;
        }
    }
}