using System;
using System.Text;
using System.Threading;

namespace RaspberryPi.PiGPIO.SLR
{
    class Program
    {
        static void Main(string[] args)
        {
            //Config
            int gpioNum = 24;
            string piAddress = "10.0.0.117";
            int port = 8888; //Default port
            int msDelay = 500;
            byte[] bytes;
            int buflen = 240;
            int res;

            //Create a client
            using (PigsClient client = new PigsClient(piAddress, port))
            {
                //Connect
                if (!client.IsConnected)
                    client.ConnectAsync().Wait();

                Console.Write("connected");
                try
                {
                    res = client.SLRO(gpioNum, 9600, 8);
                } catch (PiGPIOException e)
                {
                    if (e.ErrorCode != PiGPIOException.PI_GPIO_IN_USE) //GPIO already in use
                        throw (e);
                }
                //While the user has not pressed Ctrl+C
                bool run = true;
                Console.TreatControlCAsInput = true;
                while (run)
                {
                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey(true);
                        if (key.Key == ConsoleKey.C && key.Modifiers == ConsoleModifiers.Control)
                        {
                            run = false;
                            Console.TreatControlCAsInput = false;
                        }
                    }
                    res = client.SLR(gpioNum, buflen, out bytes);
                    Console.WriteLine( $"SLR = {res}, Read bytes = '{Encoding.UTF8.GetString(bytes, 0, res)}'" );
                    Thread.Sleep(msDelay);
                }
                client.SLRC(gpioNum);
            }
        }
    }
}
