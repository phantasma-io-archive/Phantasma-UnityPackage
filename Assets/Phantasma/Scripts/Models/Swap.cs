using LunarLabs.Parser;

namespace Phantasma.SDK
{
    public struct Swap
    {
        public string sourcePlatform; //
        public string sourceChain; //
        public string sourceHash; //
        public string sourceAddress; //
        public string destinationPlatform; //
        public string destinationChain; //
        public string destinationHash; //
        public string destinationAddress; //
        public string symbol; //
        public string value; //

        public static Swap FromNode(DataNode node)
        {
            Swap result;

            result.sourcePlatform = node.GetString("sourcePlatform");
            result.sourceChain = node.GetString("sourceChain");
            result.sourceHash = node.GetString("sourceHash");
            result.sourceAddress = node.GetString("sourceAddress");
            result.destinationPlatform = node.GetString("destinationPlatform");
            result.destinationChain = node.GetString("destinationChain");
            result.destinationHash = node.GetString("destinationHash");
            result.destinationAddress = node.GetString("destinationAddress");
            result.symbol = node.GetString("symbol");
            result.value = node.GetString("value");

            return result;
        }
    }
}