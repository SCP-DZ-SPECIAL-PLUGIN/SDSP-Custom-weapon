using System;
using System.Timers;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.Firearms.ShotEvents;
/// this file is secure with MIT licens dont copy this project or get this project
/// created by SDSP company > dzarenafixers > MONCEF50G
namespace Sniper_NTF.item
{
    public class NightGun : CustomWeapon
    {
        public override uint Id { get; set; } = 2525; // رقم فريد للسلاح
        public override string Name { get; set; } = "Drug Gun";
        public override string Description { get; set; } = "Shoot to player and see 6s for end!!!!";
        public override float Weight { get; set; }
        public override SpawnProperties SpawnProperties { get; set; }
        public override float Damage { get; set; } = 5f; // ضرر خفيف
        public uint Ammo { get; set; } = 6; // عدد الطلقات
        public bool UnlimitedAmmo { get; set; } = false; // لا يملك ذخيرة لا نهائية
        public bool CanBePickedUp { get; set; } = true;
        public float Recoil { get; set; } = 1.5f; // ارتداد بسيط
        public override ItemType Type { get; set; } = ItemType.GunCOM15; // استخدام COM-15 كأساس للسلاح
        /// this file is secure with MIT licens dont copy this project or get this project
        /// created by SDSP company > dzarenafixers > MONCEF50G
        public NightGun()
        {
            ShootingEventArgs += OnShot; // تسجيل الحدث عند إطلاق النار
        }
        public void OnPlayerJoin(JoinedEventArgs ev)
        {
            if (ev.Player.Role.Type != PlayerRoles.RoleTypeId.Spectator)
            {
                Exiled.CustomItems.API.Features.CustomItem.TryGive(ev.Player, 2525);
                ev.Player.Broadcast(5, "You take a Drug gun");
            }
        }
            
        public Action<ShotEventArgs> ShootingEventArgs { get; set; }
        /// this file is secure with MIT licens dont copy this project or get this project
        /// created by SDSP company > dzarenafixers > MONCEF50G
        private void OnShot(ShotEventArgs ev)
        {
            if (ev.Target == null || ev.Target.Role.Type == PlayerRoles.RoleTypeId.Spectator) return;

            Log.Info($"{ev.Player.Nickname} fire to {ev.Target.Nickname}with Drug gun!");

            ev.Target.EnableEffect(EffectType.Ensnared, 6f); // يجعل اللاعب غير قادر على الحركة
            ev.Target.EnableEffect(EffectType.Blinded, 6f); // يعمي اللاعب لمدة 6 ثواني
            ev.Target.EnableEffect(EffectType.Concussed, 6f); // يعطي تأثير دوار

            Timer timer = new Timer(6000);
            timer.Elapsed += (sender, args) =>
            {
                ev.Target.DisableAllEffects(); // إزالة جميع التأثيرات بعد 6 ثواني
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }
    }
}
/// this file is secure with MIT licens dont copy this project or get this project
/// created by SDSP company > dzarenafixers > MONCEF50G