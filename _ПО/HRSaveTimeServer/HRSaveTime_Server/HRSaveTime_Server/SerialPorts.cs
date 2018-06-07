using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;

namespace HRSaveTime_Server
{
    class SerialPorts
    {
        public String[] SearchPorts()
        {
            string[] ports = SerialPort.GetPortNames();
            return ports;
        }
    }
}
