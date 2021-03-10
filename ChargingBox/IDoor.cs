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

        event EventHandler<DoorChangedStateEventArgs> DoorChangedStateEvent;
    }
}
