using System;
using System.Collections.Generic;
using System.Text;

namespace ChargingBox
{
    public class Door : IDoor
    {
        public void LockDoor()
        {

            Console.WriteLine("Ladeskab er optaget");
        }

        public void UnlockDoor()
        {
            Console.WriteLine("Indlæs RFID");

        }
    }
}
