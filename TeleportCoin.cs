using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using UnityEngine;

namespace Sniper_NTF.item
{
    public class TeleportCoin : CustomItem
    {
        public override uint Id { get; set; } = 53; // Unique ID for the custom item
        public override ItemType Type { get; set; } = ItemType.Coin; // Base item type (Coin)
        public override string Name { get; set; } = "Teleport Coin"; // Name of the coin
        public override string Description { get; set; } = "A magical coin that teleports you to a random location when flipped. Usable once per minute."; // Description of the coin
        public override float Weight { get; set; }
        public override SpawnProperties SpawnProperties { get; set; }

        // Cooldown time in seconds
        private const int CooldownTime = 60;

        // Store the last usage time for each player
        private readonly Dictionary<Player, DateTime> lastUsageTime = new();

        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.UsingItem += OnUsingItem;
            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.UsingItem -= OnUsingItem;
            base.UnsubscribeEvents();
        }

        private void OnUsingItem(UsingItemEventArgs ev)
        {
            // Ensure the used item is the custom item
            if (!Check(ev.Player.CurrentItem))
                return;

            // Check if the player is within the cooldown period
            if (lastUsageTime.TryGetValue(ev.Player, out DateTime lastUsed) && (DateTime.Now - lastUsed).TotalSeconds < CooldownTime)
            {
                double secondsLeft = CooldownTime - (DateTime.Now - lastUsed).TotalSeconds;
                ev.Player.ShowHint($"You must wait {Math.Ceiling(secondsLeft)} seconds to use the Teleport Coin again!", 3);
                return;
            }

            // Perform the teleportation
            TeleportPlayer(ev.Player);

            // Update the last usage time
            lastUsageTime[ev.Player] = DateTime.Now;

            // Start the countdown hint
            Timing.RunCoroutine(ShowCooldownTimer(ev.Player, CooldownTime));
        }

        private void TeleportPlayer(Player player)
        {
            // Get a random room
            Room randomRoom = Room.List.ElementAt(UnityEngine.Random.Range(0, Room.List.Count));            if (randomRoom == null)
            {
                player.ShowHint("Failed to find a random location to teleport to!", 3);
                return;
            }

            // Teleport the player to the center of the random room
            player.Position = randomRoom.Position + Vector3.up * 1.5f; // Adjust position slightly above the floor
            player.ShowHint("You have been teleported to a random location!", 5);
        }

        private IEnumerator<float> ShowCooldownTimer(Player player, int cooldown)
        {
            for (int i = cooldown; i > 0; i--)
            {
                player.ShowHint($"Teleport Coin cooldown: {i} seconds remaining.", 1);
                yield return Timing.WaitForSeconds(1f);
            }
        }
    }
}