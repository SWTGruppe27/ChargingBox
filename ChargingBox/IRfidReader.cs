using System;
using System.Collections.Generic;
using System.Text;

namespace ChargingBox
{
    public class ReadIdEventArgs : EventArgs
    {
        public int Id { get; set; }
    }

    public interface IRfidReader
    {
        event EventHandler<ReadIdEventArgs> ReadIdEvent;

        void SetId(int id);
    }
}
