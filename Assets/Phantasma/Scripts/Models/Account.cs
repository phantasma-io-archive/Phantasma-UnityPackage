using LunarLabs.Parser;

namespace Phantasma.SDK
{
    public struct Account
    {
        public string address; //
        public string name; //
        public Stake stakes; //
        public string stake; //
        public string unclaimed;
        public string relay; //
        public string validator; //
        public Balance[] balances; //
        public Storage storage;
        public string[] txs;

        public static Account FromNode(DataNode node)
        {
            Account result;

            result.address = node.GetString("address");
            result.name = node.GetString("name");
            result.stakes = Stake.FromNode(node.GetNode("stakes"));
            result.stake = node.GetString("stake");
            result.unclaimed = node.GetString("unclaimed");
            result.relay = node.GetString("relay");
            result.validator = node.GetString("validator");
            result.storage = Storage.FromNode(node.GetNode("storage"));
            var balances_array = node.GetNode("balances");
            if (balances_array != null)
            {
                result.balances = new Balance[balances_array.ChildCount];
                for (int i = 0; i < balances_array.ChildCount; i++)
                {

                    result.balances[i] = Balance.FromNode(balances_array.GetNodeByIndex(i));

                }
            }
            else
            {
                result.balances = new Balance[0];
            }

            var txs_array = node.GetNode("txs");
            if (balances_array != null)
            {
                result.txs = new string[txs_array.ChildCount];
                for (int i = 0; i < txs_array.ChildCount; i++)
                {
                    result.txs[i] = txs_array.GetNodeByIndex(i).AsString();
                }
            }
            else
            {
                result.txs = new string[0];
            }

            return result;
        }
    }
}