using System;
using System.Collections.Generic;
using System.Text;

namespace ChargingBox
{
    public class Door : IDoor
    {
        public event EventHandler<DoorChangedStateEventArgs> DoorChangedStateEvent;

        public void LockDoor()
        {
            Console.WriteLine("Lock door");
        }

        public void UnlockDoor()
        {
            Console.WriteLine("Unlock door");
        }

        public void ChangeDoorState(bool doorState)
        {
            OpenDoor(new DoorChangedStateEventArgs {_doorOpen = doorState});
        }

        protected virtual void OpenDoor(DoorChangedStateEventArgs e)
        {
            DoorChangedStateEvent?.Invoke(this, e);
        }
    }
}
