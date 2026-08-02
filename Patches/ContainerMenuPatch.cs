using EFT;
using EFT.Interactive;
using EFT.UI;
using HarmonyLib;
using SPT.Reflection.Patching;
using System;
using System.Reflection;
using UnityEngine;

namespace DrakiaXYZ.SearchOpenContainers.Patches
{
    internal class ContainerMenuPatch : ModulePatch
    {
        private static InteractionResult openInteractionResult = new InteractionResult(EInteractionType.Open);

        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(InteractionContextHelper), nameof(InteractionContextHelper.GetAvailableActions));
        }

        [PatchPostfix]
        public static void PatchPostfix(ref AvailableInteractionState __result, GamePlayerOwner owner, LootableContainer container)
        {
            // If the container is open, add "Search" to the top of the menu
            if (__result != null && container.DoorState == EDoorState.Open)
            {
                var actionHandler = new SearchActionHandler
                {
                    owner = owner,
                    container = container,
                    initialDistance = Vector3.Distance(owner.Player.Transform.position, container.transform.position)
                };

                var searchMenuItem = new InteractionAction
                {
                    Name = "Search".Localized(),
                    Action = new Action(actionHandler.StartOpenContainer)
                };

                __result.Actions.Insert(0, searchMenuItem);
            }
        }

        internal class SearchActionHandler
        {
            public GamePlayerOwner owner;
            public LootableContainer container;
            public float initialDistance;

            public void StartOpenContainer()
            {
                // First tell the player object we're interacting with the container
                owner.Player.ExecuteInteraction(container, openInteractionResult);

                // Then set the players interact callback to our OpenContainerCallback
                owner.Player.SetCallbackForInteraction(new Action<Action>(OpenContainerCallback));

                // Trigger the callback we just set
                owner.Player.TryInteractionCallback(container);
            }

            public void OpenContainerCallback(Action callback)
            {
                InteractionContextHelper.OnContainerOpen(owner, callback, container, initialDistance);
            }
        }
    }
}
