using System;
using Exiled.API.Features;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;

namespace Sniper_NTF.item
{
    public class SCP127 : CustomWeapon
    {
        public override uint Id { get; set; } = 50; // معرف العنصر المخصص
        public override ItemType Type { get; set; } = ItemType.GunCrossvec; // نوع السلاح (مسدس COM15)
        public override float Damage { get; set; }= 0f;
        public override string Name { get; set; } = "SCP 127"; // اسم السلاح
        public override string Description { get; set; } = "Customized Crossvec This releases a full tank of infinite bullets, and you can reload after 20 seconds without carrying bullets\n"; // وصف السلاح
        public override float Weight { get; set; }
        public override SpawnProperties SpawnProperties { get; set; }

        // آخر وقت إطلاق لكل لاعب
        private readonly System.Collections.Generic.Dictionary<Player, DateTime> lastShotTime = new();

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
            // تحقق من أن اللاعب يستخدم هذا السلاح المخصص
            if (!Check(ev.Player.CurrentItem))
                return;

            // تحقق من الهدف
            if (ev.Player == null || ev.ClaimedTarget.LeadingTeam == ev.Player.LeadingTeam || ev.ClaimedTarget == ev.Player)
            {
                ev.Player.ShowHint("You cannot target yourself or your colleagues!", 5);
                return;
            }

            // تحقق من المهلة الزمنية
            if (lastShotTime.TryGetValue(ev.Player, out DateTime lastShot))
            {
                double secondsLeft = 20 - (DateTime.Now - lastShot).TotalSeconds;
                if (secondsLeft > 0)
                {
                    ev.Player.ShowHint($"Must wait\n {Math.Ceiling(secondsLeft)} seconds before using the weapon again!", 5);
                    return;
                }
            }


            // إضافة الصحة للهدف
            ev.Player.Health += 20;
            ev.Player.ShowHint($"Low ammo wait 20 s for reload \n {ev.Player.Nickname}!", 5);

            // تحديث وقت آخر استخدام
            lastShotTime[ev.Player] = DateTime.Now;
        }
    }
}