using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Map;
using Exiled.Events.EventArgs.Player;
using MEC;
using UnityEngine;

namespace Sniper_NTF.item
{
    public class SmokeGrenade : CustomWeapon
    {
        public override uint Id { get; set; } = 51; // معرف العنصر المخصص
        public override ItemType Type { get; set; } = ItemType.GrenadeHE; // نوع القنبلة
        public override float Damage { get; set; }= 0f;
        public override string Name { get; set; } = "Smoke Grenade"; // اسم العنصر
        public override string Description { get; set; } = "A smoke grenade that explodes on impact and releases smoke from SCP-244 without causing damage."; // الوصف
        public override float Weight { get; set; }
        public override SpawnProperties SpawnProperties { get; set; }

        // مدة الدخان
        public float SmokeDuration { get; set; } = 10f;

        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Map.ExplodingGrenade += OnGrenadeExploding;
            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Map.ExplodingGrenade -= OnGrenadeExploding;
            base.UnsubscribeEvents();
        }

        private void OnGrenadeExploding(ExplodingGrenadeEventArgs ev)
        {
            // تحقق من أن القنبلة هي القنبلة المخصصة
            if (!Check(ev.Projectile))
                return;

            // إلغاء الضرر الناتج عن القنبلة
            ev.IsAllowed = false;

            // إنشاء دخان مشابه لـ SCP-244
            Timing.RunCoroutine(SpawnSmoke(ev.Projectile.Transform.position));
        }

        private IEnumerator<float> SpawnSmoke(Vector3 position)
        {
            // إنشاء تأثير الدخان (نفس تأثير SCP-244)
            Room room = Room.Get(position);
            if (room != null)
            {
                var scp244 = UnityEngine.Object.Instantiate(
                    Exiled.API.Features.PrefabHelper.Spawn((PrefabType)ItemType.SCP244a),
                    position,
                    Quaternion.identity
                );

                // تحديد مدة الدخان
                Timing.CallDelayed(SmokeDuration, () =>
                {
                    UnityEngine.Object.Destroy(scp244);
                });
            }

            yield return Timing.WaitForSeconds(SmokeDuration);
        }

        protected override void OnDropping(DroppingItemEventArgs ev)
        {
            // منع رمي العنصر المخصص
            ev.IsAllowed = false;
            base.OnDropping(ev);
        }
    }
}