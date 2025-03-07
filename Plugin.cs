using System;
using Exiled.API.Features;
using Exiled.CustomItems.API;
using CustomWeapons;
using Exiled.CustomItems.API.Features;
using PlayerRoles;

namespace CustomSniperPlugin
{
    public class Plugin : Plugin<Config>
    {
        public override string Author { get; } = "MONCEF50G";
        public override string Name { get; } = "Custom Sniper";
        public override string Prefix { get; } = "customsniper";
        public override Version Version { get; } = new Version(1, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(9, 6, 0);

        public static Plugin Instance { get; private set; }

        public override void OnEnabled()
        {
            Instance = this;
            
            // تسجيل السلاح المخصص
            CustomItem sniper = new SniperRifle();
            CustomItem tranquilizer = new TranquilizerGun();
            tranquilizer.Register();

            sniper.Register();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            CustomItem sniper = CustomItem.Get(3001);
            CustomItem tranquilizer = CustomItem.Get(4001);
            tranquilizer.Unregister();

            sniper?.Unregister();

            Instance = null;
            base.OnDisabled();
            
        }
    }
}