using System;
using GameFlow.EventChannels.GeneralEC;
using GameFlow.EventChannels.SpecificEC;
using UnityEngine;

namespace Events {
    public class GameEvent : MonoBehaviour {
        [SerializeField] private VoidEventChannelSO eventTrigger;
        [SerializeField] private InventoryChannelSO inventoryChannel;

        private void Awake() {
            eventTrigger.OnVoidEventRaised += StartEvent;
        }

        private void OnDestroy() {
            eventTrigger.OnVoidEventRaised -= StartEvent;
        }

        private void StartEvent() {
            inventoryChannel.AddItem(17, 1);
        }
    }
}