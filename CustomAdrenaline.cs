using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;

namespace Sniper_NTF.item
{
    public class CustomSpeedAndHealingSyringe : CustomItem
    {
        public override uint Id { get; set; } = 56; // Unique ID for the custom item
        public override ItemType Type { get; set; } = ItemType.Adrenaline; // Base item type
        public override string Name { get; set; } = "Ultra Speed Syringe"; // Name of the item
        public override string Description { get; set; } = "A syringe that grants ultra speed and restores 1 HP every 10 seconds."; // Description of the item
        public override float Weight { get; set; }
        public override SpawnProperties SpawnProperties { get; set; }

        // Speed multiplier
        public float SpeedMultiplier { get; set; } = 2.5f; // 250% speed increase

        // Heal interval and total duration
        public float HealInterval { get; set; } = 10f; // Heal every 10 seconds
        public int HealAmount { get; set; } = 1; // Heal 1 HP per interval
        public float EffectDuration { get; set; } = 30f; // Total duration of the effects

        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.UsedItem += OnUsedItem;
            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.UsedItem -= OnUsedItem;
            base.UnsubscribeEvents();
        }

        private void OnUsedItem(UsedItemEventArgs ev)
        {
            // Ensure the item used is the custom item
            if (!Check(ev.Item))
                return;

            Player player = ev.Player;

            // Apply the speed boost
            player.EnableEffect(Exiled.API.Enums.EffectType.Scp207, EffectDuration, true);

            // Notify the player
            player.ShowHint($"You feel an extreme surge of speed and slight healing! Speed increased by {SpeedMultiplier * 100 - 200}% for {EffectDuration} seconds.", 5);

            // Start healing over time
            Timing.RunCoroutine(HealOverTime(player));
        }

        private IEnumerator<float> HealOverTime(Player player)
        {
            float elapsedTime = 0f;

            while (elapsedTime < EffectDuration)
            {
                yield return Timing.WaitForSeconds(HealInterval);

                if (player == null || !player.IsAlive)
                    yield break;

                player.Health += HealAmount;
                player.ShowHint($"You have been healed by {HealAmount} HP. Total Health: {player.Health}", 5);

                elapsedTime += HealInterval;
            }
        }
    }
}