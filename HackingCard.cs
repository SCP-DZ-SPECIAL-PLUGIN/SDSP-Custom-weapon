using System;
using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;

namespace Sniper_NTF.item
{
    public class HackingCard : CustomItem
    {
        public override uint Id { get; set; } = 54; // Unique ID for the custom item
        public override ItemType Type { get; set; } = ItemType.KeycardJanitor; // Base item type (weak keycard)
        public override string Name { get; set; } = "Hacking Card"; // Name of the card
        public override string Description { get; set; } = "A weak hacking card that can forcefully open a door once every minute."; // Description of the card
        public override float Weight { get; set; }
        public override SpawnProperties SpawnProperties { get; set; }

        // Cooldown time in seconds
        private const int CooldownTime = 60;

        // Store the last usage time for each player
        private readonly Dictionary<Player, DateTime> lastUsageTime = new();

        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.InteractingDoor += OnInteractingDoor;
            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.InteractingDoor -= OnInteractingDoor;
            base.UnsubscribeEvents();
        }

        private void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            // Ensure the player is holding the custom item
            if (!Check(ev.Player.CurrentItem))
                return;

            // Check if the player is within the cooldown period
            if (lastUsageTime.TryGetValue(ev.Player, out DateTime lastUsed) && (DateTime.Now - lastUsed).TotalSeconds < CooldownTime)
            {
                double secondsLeft = CooldownTime - (DateTime.Now - lastUsed).TotalSeconds;
                ev.Player.ShowHint($"You must wait {Math.Ceiling(secondsLeft)} seconds before hacking another door!", 3);
                return;
            }

            // Hack the door to open it
            HackDoor(ev.Door, ev.Player);

            // Update the last usage time
            lastUsageTime[ev.Player] = DateTime.Now;

            // Notify the player
            ev.Player.ShowHint("You have successfully hacked the door!", 5);

            // Start the cooldown countdown
            Timing.RunCoroutine(ShowCooldownTimer(ev.Player, CooldownTime));
        }

        private void HackDoor(Door door, Player player)
        {
            // Force open the door
            door.IsOpen = true;

            // Lock the door temporarily to simulate hacking
            door.ChangeLock(DoorLockType.SpecialDoorFeature); // Lock the door
            Timing.CallDelayed(5f, () => door.ChangeLock(DoorLockType.None)); // Unlocks the door after 5 seconds
        }
        private IEnumerator<float> ShowCooldownTimer(Player player, int cooldown)
        {
            for (int i = cooldown; i > 0; i--)
            {
                player.ShowHint($"Hacking Card cooldown: {i} seconds remaining.", 1);
                yield return Timing.WaitForSeconds(1f);
            }
        }
    }
}