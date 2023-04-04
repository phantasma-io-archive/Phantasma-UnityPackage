using LunarLabs.Parser;

namespace Phantasma.SDK
{
    public struct Platform
    {
        public string platform; //
        public string chain; //
        public string fuel; //
        public string[] tokens; //
        public Interop[] interop; //

        public static Platform FromNode(DataNode node)
        {
            Platform result;

            result.platform = node.GetString("platform");
            result.chain = node.GetString("chain");
            result.fuel = node.GetString("fuel");
            var tokens_array = node.GetNode("tokens");
            if (tokens_array != null)
            {
                result.tokens = new string[tokens_array.ChildCount];
                for (int i = 0; i < tokens_array.ChildCount; i++)
                {

                    result.tokens[i] = tokens_array.GetNodeByIndex(i).AsString();
                }
            }
            else
            {
                result.tokens = new string[0];
            }

            var interop_array = node.GetNode("interop");
            if (interop_array != null)
            {
                result.interop = new Interop[interop_array.ChildCount];
                for (int i = 0; i < interop_array.ChildCount; i++)
                {

                    result.interop[i] = Interop.FromNode(interop_array.GetNodeByIndex(i));

                }
            }
            else
            {
                result.interop = new Interop[0];
            }


            return result;
        }
    }
}