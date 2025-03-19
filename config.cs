/// this file is secure with MIT licens dont copy this project or get this project
/// created by SDSP company > dzarenafixers > Moncef50g

using System.ComponentModel;
using Exiled.API.Interfaces;

namespace Sniper_NTF
{
    /// this file is secure with MIT licens don't copy this project or get this project
    /// created by SDSP company > dzarenafixers > Moncef50g
    public class Config : IConfig
    {
        
        [Description("تفعيل أو تعطيل البلجن.")]
        public bool IsEnabled { get; set; } = true;

        [Description("تفعيل وضع التصحيح (Debug Mode).")]
        public bool Debug { get; set; } = false;

        [Description("كمية الضرر التي يحدثها السلاح.")]
        public float SniperDamage { get; set; } = 150f;
        
        [Description("كمية الضرر التي يحدثها السلاح.")]
        public float ExplosiveShotgunDamage { get; set; } = 8f;

        [Description("حجم السكوب عند التكبير.")]
        public float ScopeSize { get; set; } = 8f;
    

        [Description("الفرق التي يمكنها الحصول على السلاح.")]
        public string[] AllowedTeams { get; set; } = { "NtfSergeant" };
        
        [Description("مدة الشلل بعد الإصابة برصاصة التخدير.")]
        public float TranquilizerStunTime { get; set; } = 5f;
        
        [Description("كم ثانية تحتاج الذخيرة لتجديد رصاصة واحدة.")] // SCP127
        public float ReloadTime { get; set; } = 10f;
        
        [Description("المسافة التي يتم نقل اللاعب بها عشوائيًا.")] // ParticleDisruptorGun
        public float TeleportRange { get; set; } = 30f;

    }
}
 /// this file is secure with MIT licens dont copy this project or get this project
 /// created by SDSP company > dzarenafixers > Moncef50g