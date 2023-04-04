using LunarLabs.Parser;

namespace Phantasma.SDK
{
    public struct Validator
    {
        public string address; //
        public string type; //

        public static Validator FromNode(DataNode node)
        {
            Validator result;

            result.address = node.GetString("address");
            result.type = node.GetString("type");

            return result;
        }
    }
}