using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DataLayer.Enums
{
    public enum OrderDirection
    {
        [EnumMember(Value = "asc")]
        Ascending,

        [EnumMember(Value = "desc")]
        Descending
    }
}
