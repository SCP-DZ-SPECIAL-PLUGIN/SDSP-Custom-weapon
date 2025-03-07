using Exiled.API.Interfaces;
using System.ComponentModel;

namespace CustomSniperPlugin
{
    public class Config : IConfig
    {
        [Description("تفعيل أو تعطيل البلجن.")]
        public bool IsEnabled { get; set; } = true;

        [Description("تفعيل وضع التصحيح (Debug Mode).")]
        public bool Debug { get; set; } = false;

        [Description("كمية الضرر التي يحدثها السلاح.")]
        public float SniperDamage { get; set; } = 150f;

        [Description("حجم السكوب عند التكبير.")]
        public float ScopeSize { get; set; } = 8f;

        [Description("الفرق التي يمكنها الحصول على السلاح.")]
        public string[] AllowedTeams { get; set; } = { "NtfSergeant" };
        
        
        [Description("مدة الشلل بعد الإصابة برصاصة التخدير.")]
        public float TranquilizerStunTime { get; set; } = 5f;
    }
}