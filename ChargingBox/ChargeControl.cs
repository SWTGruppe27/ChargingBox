using System;
using System.Collections.Generic;
using System.Text;

namespace ChargingBox
{
    public class ChargeControl :IChargeControl
    {
        private IUsbCharger _usbChargerSimulator;

        public ChargeControl(IUsbCharger usbCharger)
        {
            _usbChargerSimulator = usbCharger;
        }

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
