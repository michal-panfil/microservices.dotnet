﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseService.Core.Enums
{
    public enum OrderStatus
    {
        New,
        InProcess,
        Paid,
        Sent,
        Cancelled,
        Completed
    }
}
