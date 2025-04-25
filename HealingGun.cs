using Exiled.API.Features;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;

namespace Sniper_NTF.item
{
    public class HealingGun : CustomItem
    {
        public override uint Id { get; set; } = 57; // Unique ID for the custom item
        public override ItemType Type { get; set; } = ItemType.GunCOM15; // Base item type (e.g., COM15 pistol)
        public override string Name { get; set; } = "Healing Gun"; // Name of the item
        public override string Description { get; set; } = "A magical gun that heals players by 10 HP when shot."; // Description of the item
        public override float Weight { get; set; }
        public override SpawnProperties SpawnProperties { get; set; }

        // Amount of health to heal per shot
        public int HealAmount { get; set; } = 10;

        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Shooting += OnShooting;
            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Shooting -= OnShooting;
            base.UnsubscribeEvents();
        }

        private void OnShooting(ShootingEventArgs ev)
        {
            // Ensure the player is holding the custom item
            if (!Check(ev.Player.CurrentItem))
                return;

            // Find the target player
            Player target = Player.Get(ev.ClaimedTarget.Nickname);
            if (target == null || !target.IsAlive)
                return;

            // Heal the target player
            target.Health += HealAmount;

            // Notify the shooter and the target
            ev.Player.ShowHint($"You healed {target.Nickname} by {HealAmount} HP!", 3);
            target.ShowHint($"You were healed by {ev.Player.Nickname} for {HealAmount} HP!", 3);

            // Prevent the target from taking any damage
            ev.IsAllowed = false;
        }
    }
}