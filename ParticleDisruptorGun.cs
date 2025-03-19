/// this file is secure with MIT licens dont copy this project or get this project
/// created by SDSP company > dzarenafixers > Moncef50g

using Exiled.API.Features;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerRoles.FirstPersonControl;
using UnityEngine;

namespace Sniper_NTF.item
{
    public class ParticleDisruptor : CustomWeapon
    {
        /// this file is secure with MIT licens dont copy this project or get this project
        /// created by SDSP company > dzarenafixers > Moncef50g
        public override uint Id { get; set; } = 200; // معرف السلاح
        public override string Name { get; set; } = "Particle Disruptor";
        public override string Description { get; set; } = "its strong gun for particle disruptor its gun puched you longer";
        public override float Weight { get; set; }
        public override SpawnProperties SpawnProperties { get; set; }
        public override float Damage { get; set; } = 0f; // ضرر شبه معدوم
        public override byte ClipSize { get; set; } = 5; // عدد الطلقات
        public override ItemType Type { get; set; } = ItemType.ParticleDisruptor; // يعتمد على مسدس ParticleDisruptor
        /// this file is secure with MIT licens dont copy this project or get this project
        /// created by SDSP company > dzarenafixers > Moncef50g
        public float PushForce { get; set; } = 15f; // قوة الدفع عند الإصابة

        protected override void OnShooting(ShootingEventArgs ev)
        {
            base.OnShooting(ev);

            if (ev.Player != null && ev.Player != ev.Player) // التأكد أن الهدف موجود وليس نفس الشخص
            {
                PushPlayer(ev.Player);
            }
        }
        /// this file is secure with MIT licens dont copy this project or get this project
        /// created by SDSP company > dzarenafixers > Moncef50g
        private void PushPlayer(Player target)
        {
            if (target.IsAlive)
            {
                target.Position += Vector3.forward; // تحريك اللاعب للأمام
                Vector3 pushDirection = target.GameObject.transform.forward * PushForce;
                float minMoveDistance = target.GameObject.GetComponent<CharacterControllerSettingsPreset>().MinMoveDistance;
                target.ShowHint("<color=red>💥 you touched by Particle Disruptor!</color>", 2f);
            }
        }
    }
}
/// this file is secure with MIT licens dont copy this project or get this project
/// created by SDSP company > dzarenafixers > Moncef50g