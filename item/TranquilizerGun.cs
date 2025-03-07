using CustomSniperPlugin;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using UnityEngine;

namespace CustomWeapons
{
    public class TranquilizerGun : CustomWeapon
    {
        public override uint Id { get; set; } = 4001; // رقم مميز للسلاح
        public override string Name { get; set; } = "Tranquilizer Gun";
        public override string Description { get; set; } = "🔫 يطلق رصاصة مخدرة تسبب الشلل المؤقت.";
        public override ItemType Type { get; set; } = ItemType.GunCOM15; // استخدام مسدس كقاعدة
        public override float Weight { get; set; } = 3f;
        public override SpawnProperties SpawnProperties { get; set; }
        public override float Damage { get; set; } = 1f; // ضرر بسيط فقط لتنشيط الحدث
        public override byte ClipSize { get; set; } = 3; // 3 طلقات فقط قبل التذخير
        public override bool ShouldMessageOnGban { get; } = true;

        // مدة التخدير من `Config`
        private float StunDuration => Plugin.Instance.Config.TranquilizerStunTime;

        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Hurting += OnHurting;
            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Hurting -= OnHurting;
            base.UnsubscribeEvents();
        }

        private void OnHurting(HurtingEventArgs ev)
        {
            if (!Check(ev.Attacker.CurrentItem)) return;

            ev.IsAllowed = false; // منع الضرر الحقيقي
            ev.Player.EnableEffect(EffectType.Ensnared, StunDuration); // شلل مؤقت
            ev.Player.ShowHint("<color=red>💉 لقد تعرضت للتخدير!</color>", 3f);

            Log.Info($"{ev.Attacker.Nickname} أطلق رصاصة تخدير على {ev.Player.Nickname}!");
        }
    }
}