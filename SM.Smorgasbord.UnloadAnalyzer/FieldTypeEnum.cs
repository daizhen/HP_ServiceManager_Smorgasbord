using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;

namespace SM. Smorgasbord. UnloadAnalyzer
{
    [Flags]
    public enum FieldType
    {
        Number= 1,
        Character = 2,
        DateTime = 3,
        Logical = 4,
        Array = 8,
        Structure = 9,
        Expression = 11
    }
}
