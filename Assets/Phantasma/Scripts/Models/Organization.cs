using LunarLabs.Parser;

namespace Phantasma.SDK
{
    public struct Organization
    {
        public string id;
        public string name;
        public string[] members;

        public static Organization FromNode(DataNode node)
        {
            var result = new Organization();
            result.id = node.GetString("id");
            result.name = node.GetString("name");

            var members_array = node.GetNode("members");
            if (members_array != null)
            {
                result.members = new string[members_array.ChildCount];
                for (int i = 0; i < members_array.ChildCount; i++)
                {

                    result.members[i] = members_array.GetNodeByIndex(i).AsString();
                }
            }
            else
            {
                result.members = new string[0];
            }
            return result;
        }
    }
}