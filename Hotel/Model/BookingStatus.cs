﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model
{
    public enum BookingStatus
    {
        Available,
        Reserved,
        Cancelled,
        CheckedIn,
        CheckedOut,
        NoShow
    }
}