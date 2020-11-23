using System;
using System.Collections.Generic;
using System.Text;

namespace GameConsole
{
    class IronBonesDefence : SpecialDefence
    {
        public override int CalculateDamageReduction(int totalDamage)
        {
            return 5;
        }
    }
}
