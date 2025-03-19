/// this file is secure with MIT licens dont copy this project or get this project
/// created by SDSP company > dzarenafixers > MONCEF50G
using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Map;
using MEC;
using UnityEngine;

namespace Sniper_NTF.item
{
    public class OverchargeGrenade : CustomGrenade
    {
        public override uint Id { get; set; } = 2525; // معرف فريد للقنبلة
        public override string Name { get; set; } = "Overcharge Grenade";
        public override string Description { get; set; } = "💥 <color=yellow>A grenade that grants buffs instead of damage!</color>";
        public override float Weight { get; set; }
        public override SpawnProperties SpawnProperties { get; set; }
        public override ItemType Type { get; set; } = ItemType.GrenadeHE;
        public override bool ExplodeOnCollision { get; set; }
        public override float FuseTime { get; set; }
        /// this file is secure with MIT licens dont copy this project or get this project
        /// created by SDSP company > dzarenafixers > MONCEF50G
        public float BuffDuration { get; set; } = 20f; // مدة تأثير التعزيزات بالثواني
        public float HealthBoost { get; set; } = 10f; // زيادة الصحة
        public byte SpeedBoostIntensity { get; set; } = 3; // شدة تعزيز السرعة (استخدم قيمة بين 1-255)

        protected override void OnExploding(ExplodingGrenadeEventArgs ev)
        {
            ev.IsAllowed = false; // ❌ منع القنبلة من إحداث ضرر أو تدمير أي شيء

            foreach (Player player in Player.List)
            {
                if (Vector3.Distance(player.Position, ev.Player.Position) <= 8f) // نطاق التأثير
                {
                    ApplyBoost(player);
                }
            }
        }
        /// this file is secure with MIT licens dont copy this project or get this project
        /// created by SDSP company > dzarenafixers > MONCEF50G
        private void ApplyBoost(Player player)
        {
            if (!player.IsAlive) return;

            // ✅ زيادة الصحة بدون تجاوز الحد الأقصى
            player.Health = Mathf.Min(player.MaxHealth, player.Health + HealthBoost);

            // ✅ تعزيز السرعة
            player.EnableEffect(EffectType.MovementBoost, BuffDuration);
            player.GetEffect(EffectType.MovementBoost).Intensity = SpeedBoostIntensity;

            // ✅ اختراق الأبواب
            player.EnableEffect(EffectType.Scp207, BuffDuration);

            // ✅ بدء العداد التنازلي على الشاشة
            Timing.RunCoroutine(StartCountdown(player));
        }

        private IEnumerator<float> StartCountdown(Player player)
        {
            for (int i = (int)BuffDuration; i > 0; i--)
            {
                player.ShowHint($"<color=yellow>⚡ Overcharge active: {i} seconds remaining!</color>", 1f);
                yield return Timing.WaitForSeconds(1f);
            }
            /// this file is secure with MIT licens dont copy this project or get this project
            /// created by SDSP company > dzarenafixers > MONCEF50G
            // 🔻 تعطيل التأثيرات بعد انتهاء العداد
            player.DisableEffect(EffectType.MovementBoost);
            player.DisableEffect(EffectType.Scp207);
            player.ShowHint("<color=red>⏳ Overcharge expired!</color>", 3f);
        }
    }
}
/// this file is secure with MIT licens dont copy this project or get this project
/// created by SDSP company > dzarenafixers > MONCEF50G