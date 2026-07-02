using System;
using GameFlow.EventChannels.SpecificEC;
using UnityEngine;

namespace Inventory {
    public class InventoryManager : MonoBehaviour {
        [SerializeField] private InventoryChannelSO inventoryChannel;

        private void Awake() {
            inventoryChannel.OnAddItem += AddItem;
        }

        private void OnDestroy() {
            inventoryChannel.OnAddItem -= AddItem;
        }

        private void AddItem(int item, int amount) {
            ItemStack itemStack = new ItemStack(item, amount);
            // Add to Inventory, duh
            inventoryChannel.AlertInventoryModification(itemStack);
        }
    }
}