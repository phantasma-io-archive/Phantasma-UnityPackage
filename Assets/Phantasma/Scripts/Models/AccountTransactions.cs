/*using LunarLabs.Parser;

namespace Phantasma.SDK
{
    public struct AccountTransactions
    {
        public string address; //
        public Transaction[] txs; //

        public static AccountTransactions FromNode(DataNode node)
        {
            AccountTransactions result;

            result.address = node.GetString("address");
            var txs_array = node.GetNode("txs");
            if (txs_array != null)
            {
                result.txs = new Transaction[txs_array.ChildCount];
                for (int i = 0; i < txs_array.ChildCount; i++)
                {

                    result.txs[i] = Transaction.FromNode(txs_array.GetNodeByIndex(i));

                }
            }
            else
            {
                result.txs = new Transaction[0];
            }


            return result;
        }
    }
}*/