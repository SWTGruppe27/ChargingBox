using System;
using System.Collections.Generic;
using System.Text;

namespace ChargingBox
{
    interface IChargeControl
    {
        bool IsConnected();
        void StartCharge();
        void StopCharge();
    }
}
