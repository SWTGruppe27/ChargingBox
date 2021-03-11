using System;
using System.Collections.Generic;
using System.Text;

namespace ChargingBox
{
    public class ChargeControl :IChargeControl
    {
        private UsbChargerSimulator _usbChargerSimulator;
        public bool IsConnected()
        {
            if (_usbChargerSimulator.Connected)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void StartCharge()
        {
            _usbChargerSimulator.StartCharge();
        }

        public void StopCharge()
        {
            _usbChargerSimulator.StopCharge();
        }
    }
}
