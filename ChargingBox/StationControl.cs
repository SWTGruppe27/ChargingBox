using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingBox
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum ChargingBoxState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        private ChargingBoxState _state;
        private IChargeControl _charger;
        private int _oldId;
        private IDoor _door;
        private IRfidReader _rfidReader;
        private IDisplay _display;

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        private void AttachEvents()
        {
            _rfidReader.ReadIdEvent += RfidDetected;
            _door.DoorChangedStateEvent += DoorChangedState;
        }
        public IDoor Door
        {
            get
            {
                return _door;
            }
        }
        public IRfidReader RfidReader
        {
            get
            {
                return _rfidReader;
            }
        }

        public StationControl()
        {
            _rfidReader = new RfidReader();
            _door = new Door();
            _display = new Display();
            _charger = new ChargeControl(new UsbChargerSimulator());

            AttachEvents();
        }

        public StationControl(IDoor door, IRfidReader rfidReader, IChargeControl chargeControl, IDisplay display, IChargeControl charger)
        {
            _rfidReader = rfidReader;
            _door = door;
            _display = display;
            _charger = charger;

            AttachEvents();
        }

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(object sender, ReadIdEventArgs e)
        {
            switch (_state)
            {
                case ChargingBoxState.Available:
                    // Check for ladeforbindelse
                    if (_charger.IsConnected())
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = e.Id;
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", e.Id);
                        }

                        _display.ChargingBoxLocked();
                        _state = ChargingBoxState.Locked;
                    }
                    else
                    {
                        _display.ConnectionToChargerFailed();
                    }

                    break;

                case ChargingBoxState.DoorOpen:
                    // Ignore
                    break;

                case ChargingBoxState.Locked:
                    // Check for correct ID
                    if (e.Id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", e.Id);
                        }

                        _display.RemovePhone();
                        _state = ChargingBoxState.Available;
                    }
                    else
                    {
                        _display.RfidError();
                    }

                    break;
            }
        }

        // Her mangler de andre trigger handlere
        private void DoorChangedState(object sender, DoorChangedStateEventArgs e)
        {
            if (_state != ChargingBoxState.Locked)
            {
                if (e._doorOpen == true)
                {
                    _state = ChargingBoxState.DoorOpen;
                    _display.ConnectPhone();
                }
                else
                {
                    _state = ChargingBoxState.Available;
                    _display.ScanRfid();
                }
            }
            else
            {
                _display.ChargeStationLockedNotAvalible();
            }
        }
    }
}
