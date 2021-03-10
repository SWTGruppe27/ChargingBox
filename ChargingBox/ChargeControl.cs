using System;
using System.Collections.Generic;
using System.Text;

namespace ChargingBox
{
    public class ChargeControl :IChargeControl
    {
        public bool IsConnected()
        {
            return true;
        }

        public void StartCharge()
        {

        }

        public void StopCharge()
        {

        }
    }
}
