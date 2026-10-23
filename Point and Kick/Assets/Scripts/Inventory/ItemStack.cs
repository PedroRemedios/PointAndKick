using UnityEngine;

namespace Inventory {
    public class ItemStack {
        private int item;
        private int amount;
        
        public ItemStack(int newItem, int newAmount) {
            item = newItem;
            amount = newAmount;
        }

        public int GetItem() {
            return item;
        }

        public int GetAmount() {
            return amount;
        }
    }
}