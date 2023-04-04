using LunarLabs.Parser;

namespace Phantasma.SDK
{
    public struct Crowdsale
    {
        public string hash;
        public string name;
        public string creator;
        public string flags;
        public uint startDate;
        public uint endDate;
        public string sellSymbol;
        public string receiveSymbol;
        public uint price;
        public string globalSoftCap;
        public string globalHardCap;
        public string userSoftCap;
        public string userHardCap;

        public static Crowdsale FromNode(DataNode node)
        {
            var result = new Crowdsale();
            result.hash = node.GetString("hash");
            result.name = node.GetString("name");
            result.creator = node.GetString("creator");
            result.flags = node.GetString("flags");
            result.startDate = node.GetUInt32("startDate");
            result.endDate = node.GetUInt32("endDate");
            result.sellSymbol = node.GetString("sellSymbol");
            result.receiveSymbol = node.GetString("receiveSymbol");
            result.price = node.GetUInt32("price");
            result.globalSoftCap = node.GetString("globalSoftCap");
            result.globalHardCap = node.GetString("globalHardCap");
            result.userSoftCap = node.GetString("userSoftCap");
            result.userHardCap = node.GetString("userHardCap");
            return result;
        }
    }
}