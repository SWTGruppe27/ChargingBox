﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ChargingBox
{
    public interface IChargeControl
    {
        bool IsConnected();
        void StartCharge();
        void StopCharge();
    }
}
