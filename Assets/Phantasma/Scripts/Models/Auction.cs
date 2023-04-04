using LunarLabs.Parser;

namespace Phantasma.SDK
{
    public struct Auction
    {
        public string creatorAddress; //
        public string chainAddress; //
        public uint startDate; //
        public uint endDate; //
        public string baseSymbol; //
        public string quoteSymbol; //
        public string tokenId; //
        public string price; //
        public string endPrice; //
        public string extensionPeriod; //
        public string type; //
        public string rom; //
        public string ram; //
        public string listingFee; //
        public string currentWinner; //

        public static Auction FromNode(DataNode node)
        {
            Auction result;

            result.creatorAddress = node.GetString("creatorAddress");
            result.chainAddress = node.GetString("chainAddress");
            result.startDate = node.GetUInt32("startDate");
            result.endDate = node.GetUInt32("endDate");
            result.baseSymbol = node.GetString("baseSymbol");
            result.quoteSymbol = node.GetString("quoteSymbol");
            result.tokenId = node.GetString("tokenId");
            result.price = node.GetString("price");
            result.endPrice = node.GetString("endPrice");
            result.extensionPeriod = node.GetString("extensionPeriod");
            result.type = node.GetString("type");
            result.rom = node.GetString("rom");
            result.ram = node.GetString("ram");
            result.listingFee = node.GetString("listingFee");
            result.currentWinner = node.GetString("currentWinner");

            return result;
        }
    }
}