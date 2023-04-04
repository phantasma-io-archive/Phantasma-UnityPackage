using LunarLabs.Parser;

namespace Phantasma.SDK
{
    public struct TokenSeries
    {
        public int seriesID; //
        public string currentSupply; //
        public string maxSupply; //
        public string burnedSupply; //
        public string mode; //
        public string script; //

        public static TokenSeries FromNode(DataNode node)
        {
            TokenSeries result = new TokenSeries();

            result.seriesID = node.GetInt32("seriesID");
            result.currentSupply = node.GetString("currentSupply");
            result.maxSupply = node.GetString("maxSupply");
            result.burnedSupply = node.GetString("burnedSupply");
            result.mode = node.GetString("mode");

            return result;
        }

    }
}