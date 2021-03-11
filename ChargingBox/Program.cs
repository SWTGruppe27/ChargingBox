using System;

namespace ChargingBox
{
    class Program
    {
        static void Main(string[] args)
        {
            // Assemble your system here from all the classes
            StationControl _stationControl = new StationControl();

            bool finish = false;
            do
            {
                string input;
                System.Console.WriteLine("Indtast E, O, C, R: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    //End program
                    case 'E':
                        finish = true;
                        break;

                    case 'O':
                        _stationControl.Door.();
                        break;

                    case 'C':
                        _stationControl.Door.LockDoor();
                        break;

                    case 'R':
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        rfidReader.OnRfidRead(id);
                        break;

                    default:
                        break;
                }

            } while (!finish);
        }
    }
}
