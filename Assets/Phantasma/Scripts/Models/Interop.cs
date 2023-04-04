using LunarLabs.Parser;

namespace Phantasma.SDK
{
    public struct Interop
    {
        public string local; //
        public string external; //

        public static Interop FromNode(DataNode node)
        {
            Interop result;

            result.local = node.GetString("local");
            result.external = node.GetString("external");

            return result;
        }
    }
}