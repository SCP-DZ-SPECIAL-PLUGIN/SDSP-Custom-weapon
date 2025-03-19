using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using UnityEngine;

namespace CustomItems
{
    public class HealingPistol : CustomWeapon
    {
        public override uint Id { get; set; } = 30; // رقم فريد للسلاح
        public override string Name { get; set; } = "Healing Pistol";
        public override string Description { get; set; } = "🔫 Medic gun for + 10 HP.";
        public override ItemType Type { get; set; } = ItemType.GunCOM15; // نوع المسدس الأساسي
        public override float Weight { get; set; } = 1f;
        public override SpawnProperties SpawnProperties { get; set; }
        public override byte ClipSize { get; set; } = 8; // سعة المخزن
        public override float Damage { get; set; } = 0f; // لا يسبب ضررًا

        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Shot += OnShot;
            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Shot -= OnShot;
            base.UnsubscribeEvents();
        }

        private void OnShot(ShotEventArgs ev)
        {
            if (!Check(ev.Player.CurrentItem)) return;

            if (ev.Target == null || !ev.Target.IsAlive)
                return;

            // زيادة الصحة عند الإصابة
            float healAmount = 10f;
            ev.Target.Health = Mathf.Min(ev.Target.MaxHealth, ev.Target.Health + healAmount);

            ev.Target.ShowHint($"<color=green>💉 you healing {healAmount} HP!</color>", 2);
            ev.Player.ShowHint($"<color=blue>🔫 {ev.Target.Nickname} +{healAmount} HP</color>", 2);
        }
    }
}