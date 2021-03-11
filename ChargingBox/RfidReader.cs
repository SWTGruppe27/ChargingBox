using System;
using System.Collections.Generic;
using System.Text;

namespace ChargingBox
{
    public class RfidReader : IRfidReader
    {
        private int _id;
        public event EventHandler<ReadIdEventArgs> ReadIdEvent;

        public void SetId(int id)
        {
            _id = id;
            ReadId(new ReadIdEventArgs {Id = _id});
        }
        protected virtual void ReadId(ReadIdEventArgs e)
        {
            ReadIdEvent?.Invoke(this,e);
        }
    }
}
