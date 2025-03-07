using CustomSniperPlugin;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using UnityEngine;
using System.Collections.Generic;
using Exiled.API.Features.Spawn;
using InventorySystem.Items.Firearms.Attachments;
using InventorySystem.Items.Firearms.Attachments.Components;
using MEC;

namespace CustomWeapons
{
    public class SniperRifle : CustomWeapon
    {
        public override uint Id { get; set; } = 3001;
        public override string Name { get; set; } = "M-98 Sniper";
        public override string Description { get; set; } = "🔭 سلاح قناص قوي مع سكوب تكبير، يمكن إطلاق طلقة كل 15 ثانية!";
        public override ItemType Type { get; set; } = ItemType.GunE11SR;
        public override float Weight { get; set; } = 6f;
        public override SpawnProperties SpawnProperties { get; set; }
        public override float Damage { get; set; } = Plugin.Instance.Config.SniperDamage;
        public override byte ClipSize { get; set; } = 1; // رصاصة واحدة فقط
        public override bool ShouldMessageOnGban { get; } = true;

        private Dictionary<Player, float> lastShotTime = new Dictionary<Player, float>();
        private const float CooldownTime = 15f;

        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.PickingUpItem += OnPickingUpItem;
            Exiled.Events.Handlers.Player.Shooting += OnShooting;
            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.PickingUpItem -= OnPickingUpItem;
            Exiled.Events.Handlers.Player.Shooting -= OnShooting;
            base.UnsubscribeEvents();
        }

        private void OnPickingUpItem(PickingUpItemEventArgs ev)
        {
            if (!Check(ev.Pickup)) return;

            if (ev.Player.CurrentItem is Firearm firearm)
            {
                firearm.AddAttachment(AttachmentName.ScopeSight);
                ev.Player.ShowHint("<color=green>🔭 تم تركيب سكوب عالي التكبير!</color>", 3f);
            }
        }

        private void OnShooting(ShootingEventArgs ev)
        {
            if (!Check(ev.Player.CurrentItem)) return;

            float currentTime = Time.time;
            if (lastShotTime.TryGetValue(ev.Player, out float lastTime))
            {
                float timeSinceLastShot = currentTime - lastTime;

                if (timeSinceLastShot < CooldownTime)
                {
                    float remainingTime = CooldownTime - timeSinceLastShot;
                    ev.Player.ShowHint($"<color=red>⏳ يجب الانتظار {remainingTime:F1} ثانية قبل الإطلاق مجدداً!</color>", 2f);
                    ev.IsAllowed = false;
                    return;
                }
            }

            lastShotTime[ev.Player] = currentTime;
            ev.Player.ShowHint("<color=red>🔄 تحتاج إلى إعادة التلقيم!</color>", 3f);

            // ✅ تشغيل العداد التنازلي
            Timing.RunCoroutine(ShowCooldown(ev.Player));

            if (ev.Player != null)
            {
                ev.Player.EnableEffect(EffectType.Concussed, 5f);
                Log.Info($"{ev.Player.Nickname} أطلق من سلاح القنص!");
            }
        }

        private IEnumerator<float> ShowCooldown(Player player)
        {
            for (float i = CooldownTime; i > 0; i -= 1f)
            {
                player.ShowHint($"<color=yellow>⏳ إعادة التلقيم: {i:F0} ثانية</color>", 1f);
                yield return Timing.WaitForSeconds(1f);
            }
        }
    }
}
