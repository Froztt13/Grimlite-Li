using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdBankSwap : IBotCommand
    {
        public string BankItemName
        {
            get;
            set;
        }

        public string InventoryItemName
        {
            get;
            set;
        }

        public async Task Execute(IBotEngine instance)
        {
            string BankItemName = (instance.IsVar(this.BankItemName) ? Configuration.Tempvariable[instance.GetVar(this.BankItemName)] : this.BankItemName);
            string InventoryItemName = (instance.IsVar(this.InventoryItemName) ? Configuration.Tempvariable[instance.GetVar(this.InventoryItemName)] : this.InventoryItemName);

            BotData.BotState = BotData.State.Others;
            if (CanExecute())
            {
                Player.Bank.Swap(InventoryItemName, BankItemName);
                await instance.WaitUntil(() => !CanExecute());
            }
            bool CanExecute()
            {
                if (Player.Bank.GetItemByName(BankItemName) != null)
                {
                    return Player.Inventory.GetItemByName(InventoryItemName) != null;
                }
                return false;
            }
        }

        public override string ToString()
        {
            return "Bank swap {" + BankItemName + ", " + InventoryItemName + "}";
        }
    }
}