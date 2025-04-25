/// this file is secure with MIT licens dont copy this project or get this project
/// created by SDSP company > dzarenafixers > Moncef50g

using System;
using CustomItems;
using CustomWeapons;
using Exiled.API.Features;
using Exiled.CustomItems;
using Exiled.CustomItems.API;
using Exiled.CustomItems.API.Features;
using HarmonyLib;
using InventorySystem.Items.Firearms;
using PluginAPI.Core.Attributes;
using Sniper_NTF.item;
using PluginPriority = Exiled.API.Enums.PluginPriority;

namespace Sniper_NTF
{
    /// this file is secure with MIT licens dont copy this project or get this project
    /// created by SDSP company > dzarenafixers > Moncef50g
    public class Plugin : Plugin<Config.Config>
    {
        public override string Author { get; } = "MONCEF50G";
        public override string Name { get; } = "Custom Weapons system";
        public override string Prefix { get; } = "customsniper";
        public override Version Version { get; } = new Version(2, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(9, 5, 1);
        public override PluginPriority Priority { get; }
        
         public Harmony Harmony;
         
        /// this file is secure with MIT licens dont copy this project or get this project
        /// created by SDSP company > dzarenafixers > Moncef50g
        public static Plugin Instance { get; private set; }
        
        /// this file is secure with MIT licens dont copy this project or get this project
        /// created by SDSP company > dzarenafixers > Moncef50g
        public override void OnEnabled()
        {
            Instance = this;
            
            // تسجيل السلاح المخصص
            CustomItem sniper = new SniperRifle();
            CustomItem HealingGun = new HealingGun();
            CustomItem CAD = new CustomSpeedAndHealingSyringe();
            CustomItem HackingCard = new HackingCard();
            CustomItem TelepoetCoin = new TeleportCoin();
            CustomWeapon.RegisterItems();
            CustomWeapon SCP127 = new SCP127();
            CustomItem SmokeGrenade = new SmokeGrenade();
            CustomItem tranquilizer = new TranquilizerGun();
             
            
             item.SniperRifle.RegisterItems();
             item.CustomSpeedAndHealingSyringe.RegisterItems();
             item.SmokeGrenade.RegisterItems();
             item.CustomSpeedAndHealingSyringe.RegisterItems();
             item.HackingCard.RegisterItems();
             sniper.Register();
             SCP127.Register();
             item.SCP127.RegisterItems();
             SniperRifle.RegisterItems();
             item.TeleportCoin.RegisterItems();
             HealingGun.Register();
             TeleportCoin.RegisterItems();
             TelepoetCoin.Register();
             HackingCard.Register();
             Harmony.PatchAll();
             item.HackingCard.RegisterItems();
             SmokeGrenade.Register();
             item.SmokeGrenade.RegisterItems();
             CustomWeapon.RegisterItems();
             
             SCP127.Register();
             item.SCP127.RegisterItems();
             TranquilizerGun.RegisterItems();
            tranquilizer.Register();
            sniper.Register();
            base.OnEnabled();
        }
        /// this file is secure with MIT licens dont copy this project or get this project
        /// created by SDSP company > dzarenafixers > Moncef50g
        public override void OnDisabled()
        {
            CustomItem sniper = CustomItem.Get(3001);
            CustomItem cad = CustomItem.Get(56);
            CustomItem hackingCard = CustomItem.Get(54);
            CustomItem TeleportPlayer = CustomItem.Get(53);
            CustomItem HP = CustomItem.Get(30);
            CustomItem HealingGun = CustomItem.Get(50);
            CustomItem SmokeGrenade = CustomItem.Get(51);
            CustomItem Overchargegrenade = CustomItem.Get(201);
            CustomItem ParticleDisruptor = CustomItem.Get(128);
            CustomItem nightGun = CustomItem.Get(2525);
            CustomItem explisiveShotgun = CustomItem.Get(3600);
            CustomItem scp127 = CustomItem.Get(127);
            CustomItem magnet = CustomItem.Get(4205);
            CustomItem hHomingGl = CustomItem.Get(5002);
            CustomItem tranquilizer = CustomItem.Get(4001);
            tranquilizer.Unregister(); 
            HP.Unregister();
            TeleportPlayer.Unregister();
            hHomingGl.Unregister();
            nightGun.Unregister();
            ParticleDisruptor.Unregister();
            Overchargegrenade.Unregister();
            scp127.Unregister();
            explisiveShotgun.Unregister();   
            magnet.Unregister();
            hHomingGl.Unregister();
            sniper?.Unregister();

            Instance = null;
            base.OnDisabled();
            /// this file is secure with MIT licens dont copy this project or get this project
            /// created by SDSP company > dzarenafixers > Moncef50g
        }
    }
}