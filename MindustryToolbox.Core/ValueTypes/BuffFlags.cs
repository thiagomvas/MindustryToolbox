using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindustryToolbox.Core.ValueTypes;
[Flags]
public enum BuffFlags
{
    NoLiquidOrOverdrive = NoLiquid | NoOverdrive, 
    NoLiquid = 1 << 0,        
    NoOverdrive = 1 << 1,     
    Water = 1 << 2,          
    Cryofluid = 1 << 3,       
    OverdriveDome = 1 << 4,   
    OverdriveProjector = 1 << 5 
}

