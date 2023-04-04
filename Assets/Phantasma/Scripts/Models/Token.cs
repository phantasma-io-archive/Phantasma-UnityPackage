using LunarLabs.Parser;

namespace Phantasma.SDK
{
    public class Token
    {
        public string symbol; //
        public string name; //
        public int decimals; //
        public string currentSupply; //
        public string maxSupply; //
        public string burnedSupply; //
        public string address; //
        public string owner; //
        public string flags; //
        public string script; //
        public TokenExternal[] external;
        public TokenSeries[] series;

        public static Token FromNode(DataNode node)
        {
            Token result = new Token();

            result.symbol = node.GetString("symbol");
            result.name = node.GetString("name");
            result.decimals = node.GetInt32("decimals");
            result.currentSupply = node.GetString("currentSupply");
            result.maxSupply = node.GetString("maxSupply");
            result.burnedSupply = node.GetString("burnedSupply");
            result.address = node.GetString("address");
            result.owner = node.GetString("owner");
            result.flags = node.GetString("flags");
            result.script = node.GetString("script");

            var series_array = node.GetNode("series");
            if (series_array != null)
            {
                result.series = new TokenSeries[series_array.ChildCount];
                for (int i = 0; i < series_array.ChildCount; i++)
                {
                    result.series[i] = TokenSeries.FromNode(series_array.GetNodeByIndex(i));
                }
            }

            var external_array = node.GetNode("external");
            if (external_array != null)
            {
                result.external = new TokenExternal[external_array.ChildCount];
                for (int i = 0; i < external_array.ChildCount; i++)
                {
                    result.external[i] = TokenExternal.FromNode(external_array.GetNodeByIndex(i));
                }
            }
            else
            {
                result.external = new TokenExternal[0];
            }

            return result;
        }
    }
}