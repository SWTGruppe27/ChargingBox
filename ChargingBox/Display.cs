using System;
using System.Collections.Generic;
using System.Text;

namespace ChargingBox
{
    public class Display : IDisplay
    {
        public void ConnectPhone()
        {
            Console.WriteLine("Tilslut din telefon");
        }

        public void ScanRfid()
        {
            Console.WriteLine("Indlæs RFID chip");
        }

        public void ConnectionToChargerFailed()
        {
            Console.WriteLine("Laderen er ikke tilsluttet korrekt tilsluttet");
        }

        public void ChargingBoxLocked()
        {
            Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
        }

        public void ChargeStationLockedNotAvalible()
        {
            Console.WriteLine("Ladeskab er optaget");
        }

        public void RfidError()
        {
            Console.WriteLine("Forkert RFID tag");
        }

        public void RemovePhone()
        {
            Console.WriteLine("Fjern tilsluttet telefon");
        }

        public void DoorIsOpen()
        {
            Console.WriteLine("Døren er åben");
        }
    }
}