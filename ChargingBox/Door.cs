using System;
using System.Collections.Generic;
using System.Text;

namespace ChargingBox
{
    public class Door : IDoor
    {
        public event EventHandler<DoorChangedStateEventArgs> DoorChangedStateEvent;

        public bool DoorLocked { get; private set; }

        public Door()
        {
            DoorLocked = false;
        }

        public void LockDoor()
        {
            DoorLocked = true;
            Console.WriteLine("Lock door");
        }

        public void UnlockDoor()
        {
            DoorLocked = false;
            Console.WriteLine("Unlock door");
        }

        public void ChangeDoorState(bool doorState)
        {
            OpenDoor(new DoorChangedStateEventArgs { DoorOpen = doorState });
        }

        protected virtual void OpenDoor(DoorChangedStateEventArgs e)
        {
            DoorChangedStateEvent?.Invoke(this, e);
        }
    }
}
