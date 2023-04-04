using LunarLabs.Parser;

namespace Phantasma.SDK
{
    public struct Archive
    {
        public string name; //
        public string hash; //
        public uint time; //
        public uint size; //
        public string encryption; //
        public int blockCount; //
        public int[] missingBlocks;
        public string[] owners; //

        public static Archive FromNode(DataNode node)
        {
            Archive result;

            result.name = node.GetString("name");
            result.hash = node.GetString("hash");
            result.time = node.GetUInt32("time");
            result.size = node.GetUInt32("size");
            result.encryption = node.GetString("encryption");
            result.blockCount = node.GetInt32("blockCount");
            var missingBlocks_array = node.GetNode("missingBlocks");
            if (missingBlocks_array != null)
            {
                result.missingBlocks = new int[missingBlocks_array.ChildCount];
                for (int i = 0; i < missingBlocks_array.ChildCount; i++)
                {

                    result.missingBlocks[i] = missingBlocks_array.GetNodeByIndex(i).AsInt32();
                }
            }
            else
            {
                result.missingBlocks = new int[0];
            }

            var owners_array = node.GetNode("missingBlocks");
            if (owners_array != null)
            {
                result.owners = new string[owners_array.ChildCount];
                for (int i = 0; i < owners_array.ChildCount; i++)
                {

                    result.owners[i] = owners_array.GetNodeByIndex(i).AsString();
                }
            }
            else
            {
                result.owners = new string[0];
            }


            return result;
        }
    }
}