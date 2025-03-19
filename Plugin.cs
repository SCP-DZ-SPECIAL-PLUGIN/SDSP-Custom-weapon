/// this file is secure with MIT licens dont copy this project or get this project
/// created by SDSP company > dzarenafixers > Moncef50g

using System;
using CustomItems;
using CustomWeapons;
using Exiled.API.Features;
using Exiled.CustomItems;
using Exiled.CustomItems.API;
using Exiled.CustomItems.API.Features;
using PluginAPI.Core.Attributes;
using Sniper_NTF.item;
using PluginPriority = Exiled.API.Enums.PluginPriority;

namespace Sniper_NTF
{
    /// this file is secure with MIT licens dont copy this project or get this project
    /// created by SDSP company > dzarenafixers > Moncef50g
    public class Plugin : Plugin<Config>
    {
        public override string Author { get; } = "MONCEF50G";
        public override string Name { get; } = "Custom Weapons system";
        public override string Prefix { get; } = "customsniper";
        public override Version Version { get; } = new Version(2, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(9, 6, 0);
        public override PluginPriority Priority { get; }
        

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
            CustomItem HP = new HealingPistol();
            CustomItem nightGun = new NightGun();
            CustomItem ParticleDisruptor = new ParticleDisruptor();
            CustomItem tranquilizer = new TranquilizerGun();
            CustomItem overchargegrenade = new OverchargeGrenade();
             OverchargeGrenade.RegisterItems();
             overchargegrenade.Register();
             HP.Register();
             
             nightGun.Register();
             HealingPistol.RegisterItems();
             NightGun.RegisterItems();
             ParticleDisruptor.Register();
             item.ParticleDisruptor.RegisterItems();
            tranquilizer.Register();
            sniper.Register();
            base.OnEnabled();
        }
        /// this file is secure with MIT licens dont copy this project or get this project
        /// created by SDSP company > dzarenafixers > Moncef50g
        public override void OnDisabled()
        {
            CustomItem sniper = CustomItem.Get(3001);
            CustomItem HP = CustomItem.Get(30);
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
            hHomingGl.Unregister();
            NightGun.UnregisterItems();
            nightGun.Unregister();
            item.ParticleDisruptor.UnregisterItems();
            ParticleDisruptor.Unregister();
            Overchargegrenade.Unregister();
            scp127.Unregister();
            OverchargeGrenade.UnregisterItems();
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