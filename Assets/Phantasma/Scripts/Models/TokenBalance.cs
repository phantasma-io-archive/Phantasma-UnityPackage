using LunarLabs.Parser;

namespace Phantasma.SDK
{
    public struct TokenBalance
    {
        public string chain;
        public string amount;
        public string symbol;
        public int decimals;

        public static TokenBalance FromNode(DataNode node)
        {
            TokenBalance result = new TokenBalance();
            result.chain = node.GetString("chain");
            result.amount = node.GetString("amount");
            result.symbol = node.GetString("symbol");
            result.decimals = node.GetInt32("decimals");
            return result;
        }
    }
}