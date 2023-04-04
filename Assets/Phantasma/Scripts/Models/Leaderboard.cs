/*using System.Numerics;
using LunarLabs.Parser;
using Phantasma.Core.Cryptography;

namespace Phantasma.SDK
{
    public struct Leaderboard
    {

        public string name;
        public LeaderboardRow[] rows;

        public struct LeaderboardRow
        {
            public Address address;
            public BigInteger score;

            public static LeaderboardRow FromNode(DataNode node)
            {
                LeaderboardRow result;
                result.address = Address.FromText(node.GetString("address"));
                result.score = node.GetInt32("value");
                return result;
            }
        }

        public static Leaderboard FromNode(DataNode node)
        {
            Leaderboard result;
            result.name = node.GetString("name");
            var rows_array = node.GetNode("rows");
            if (rows_array != null)
            {
                result.rows = new LeaderboardRow[rows_array.ChildCount];
                for (int i = 0; i < rows_array.ChildCount; i++)
                {
                    result.rows[i] = LeaderboardRow.FromNode(rows_array.GetNodeByIndex(i));
                }
            }
            else
            {
                result.rows = new LeaderboardRow[0];
            }

            return result;
        }
    }
}*/