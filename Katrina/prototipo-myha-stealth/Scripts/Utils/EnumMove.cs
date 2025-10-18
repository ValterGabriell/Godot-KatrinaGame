using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototipoMyha
{
    public enum EnumMove
    {
        RIGHT,
        LEFT,
        IDLE
    }

    public enum EnumGuardMove
    {
        end_warning,
        roaming,
        shoot,
        start_warning,
        walk_alert
    }
}
