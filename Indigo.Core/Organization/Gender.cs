using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Indigo.Organization
{
    /// <summary>
    /// 性别 参考ISO-5218
    /// </summary>
    public enum Gender
    {
        [Description("未知")]
        Unknown = 0,

        [Description("男")]
        Male = 1,

        [Description("女")]
        Female = 2
    }
}
