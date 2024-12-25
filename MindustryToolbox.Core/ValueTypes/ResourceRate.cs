using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindustryToolbox.Core.ValueTypes;
public readonly record struct ResourceRate
{
    public Resource Resource { get; init; }
    public float Rate { get; init; }
}
