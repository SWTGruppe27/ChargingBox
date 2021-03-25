using System;
using System.Collections.Generic;
using System.Text;

namespace ChargingBox
{
    public class ChargeControl :IChargeControl
    {
        private IUsbCharger _usbChargerSimulator;
        private IDisplay _display;

        public ChargeControl(IUsbCharger usbCharger, IDisplay display)
        {
            _usbChargerSimulator = usbCharger;
            _display = display;
            _usbChargerSimulator.CurrentValueEvent += CurrentValueEventHandler;
        }

        private void CurrentValueEventHandler(object sender, CurrentEventArgs e)
        {
            if(e.Current > 0 && e.Current <= 5)
            {
                _display.PhoneFullCharge();
            }
            else if (e.Current > 5 && e.Current <= 500)
            {
                _display.PhoneCharging();
            }
            else if(e.Current > 500)
            {
                _display.PhoneChargingError();
            }
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
