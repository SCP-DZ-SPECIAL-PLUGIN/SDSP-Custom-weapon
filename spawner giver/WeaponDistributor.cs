using Exiled.API.Features;
using Exiled.Events.EventArgs.Server;
using Exiled.API.Enums;
using Exiled.CustomItems.API.Features;
using System.Linq;
using PlayerRoles;

namespace CustomWeapons
{
    public class WeaponDistributor
    {
        public void RegisterEvents()
        {
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStarted;
        }

        public void UnregisterEvents()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStarted;
        }

        private void OnRoundStarted()
        {
            Log.Info("🔫Distribution of weapons allocated to the NTF and Chaos teams...");

            foreach (var player in Player.List)
            {
                if (player.Role.Team == Team.FoundationForces) // ✅ فريق MTF
                {
                    GiveWeapon(player, 3001); // 🎯 M-98 Sniper Rifle
                    Log.Info($"🎯 {player.Nickname} He got the M-98 sniper weapon!");
                }
                else if (player.Role.Team == Team.ChaosInsurgency) // ✅ فريق Chaos
                {
                    GiveWeapon(player, 3002); // 💉 Tranquilizer Gun
                    Log.Info($"💉 {player.Nickname} He got the Tranquilizer weapon!");
                }
            }
        }

        private void GiveWeapon(Player player, uint weaponId)
        {
            CustomItem customWeapon = CustomItem.Get(weaponId);
            if (customWeapon != null)
            {
                customWeapon.Give(player);
            }
            else
            {
                Log.Warn($"⚠️ السلاح بمعرف {weaponId} غير موجود في قائمة الأسلحة المخصصة!");
            }
        }
    }
}