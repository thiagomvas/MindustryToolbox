using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindustryToolbox.Core.ValueTypes;
public readonly record struct LiquidBuff
{
    public readonly Resource Liquid;
    public readonly double Multiplier;
    public readonly double Rate;
}
