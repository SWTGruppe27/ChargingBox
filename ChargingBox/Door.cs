using System;
using System.Collections.Generic;
using System.Text;

namespace ChargingBox
{
    public class Door : IDoor
    {
        public event EventHandler<DoorChangedStateEventArgs> DoorChangedStateEvent;

        public bool _doorLocked { get; private set; }

        public Door()
        {
            _doorLocked = false;
        }

        public void LockDoor()
        {
            _doorLocked = true;
            Console.WriteLine("Lock door");
        }

        public void UnlockDoor()
        {
            _doorLocked = false;
            Console.WriteLine("Unlock door");
        }

        public void ChangeDoorState(bool doorState)
        {
            OpenDoor(new DoorChangedStateEventArgs { _doorOpen = doorState });
        }

        protected virtual void OpenDoor(DoorChangedStateEventArgs e)
        {
            DoorChangedStateEvent?.Invoke(this, e);
        }
    }
}
