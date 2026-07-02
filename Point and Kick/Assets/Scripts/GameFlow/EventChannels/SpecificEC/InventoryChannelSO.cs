using Inventory;
using UnityEngine;

namespace GameFlow.EventChannels.SpecificEC {
    [CreateAssetMenu(fileName = "InventoryChannel", menuName = "Events/Inventory Channel", order = 4)]
    public class InventoryChannelSO : ScriptableObject {
        public delegate void ItemStackCallback(ItemStack itemStack);
        public event ItemStackCallback OnInventoryModified;

        public delegate void ItemAmountCallback(int item, int amount);
        public event ItemAmountCallback OnAddItem;

        public void AlertInventoryModification(ItemStack itemStack) {
            OnInventoryModified?.Invoke(itemStack);
        }

        public void AddItem(int item, int amount) {
            OnAddItem?.Invoke(item, amount);
        }
    }
}