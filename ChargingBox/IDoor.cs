using System;
using System.Collections.Generic;
using System.Text;

namespace ChargingBox
{
    public class DoorChangedStateEventArgs : EventArgs
    {
        public bool _doorOpen { get; set; }
    }

    public interface IDoor
    {
        void LockDoor();
        void UnlockDoor();

        void ChangeDoorState(bool doorState);

        event EventHandler<DoorChangedStateEventArgs> DoorChangedStateEvent;
    }
}
