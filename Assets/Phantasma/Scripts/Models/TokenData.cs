using System;
using LunarLabs.Parser;

namespace Phantasma.SDK
{
    public struct TokenData
    {
        public string ID; //
        public string series; //
        public string mint; //
        public string chainName; //
        public string ownerAddress; //
        public string creatorAddress; //
        public string ram; //
        public string rom; //
        public string status; //
        public Boolean forSale; //

        public static TokenData FromNode(DataNode node)
        {
            TokenData result;

            result.ID = node.GetString("ID");
            result.series = node.GetString("series");
            result.mint = node.GetString("mint");
            result.chainName = node.GetString("chainName");
            result.ownerAddress = node.GetString("ownerAddress");
            result.creatorAddress = node.GetString("creatorAddress");
            result.ram = node.GetString("ram");
            result.rom = node.GetString("rom");
            result.status = node.GetString("status");
            result.forSale = node.GetBoolean("forSale");

            return result;
        }
    }
}