using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ShoppingCartApi.Models.Strata
{
    public enum CustomerType
    {
        [Description("Default")] Default = 0,
        [Description("Silver")] Silver = 1,
        [Description("Gold")]  Gold = 2,
    }
}