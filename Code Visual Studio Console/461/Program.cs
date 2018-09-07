using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace _461
{
    class Program
    {
        /*
           ** mic postion er chehera:
           ----8--------7----
           ----6--------5----
           ----4--------3----
           ----2--------1----
           */
        int mic_pos_1 = 1;
        int mic_pos_2 = 2;
        int mic_pos_3 = 3;
        int mic_pos_4 = 4;
        int mic_pos_5 = 5;
        int mic_pos_6 = 6;
        int mic_pos_7 = 7;

        static int current_bot_position = 0;
        static  int user_position = 0;

        static SerialPort port;

        static void Main(string[] args)
        {
            string[] ports = SerialPort.GetPortNames();
            for (int i = 0; i < ports.Length; i++)
            {
                Console.WriteLine(ports[i]);
            }
            String portName = Console.ReadLine();

            port = new SerialPort(portName, 9600);
            port.Open();
            port.Write("0");//0 means the pc ready now to communicate with arduino
            Console.WriteLine("please press esc to terminate");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.Escape)
            {

                Console.WriteLine("Good bye");
                System.Threading.Thread.Sleep(1000);

            }
            else {

                Console.ReadLine();
            }

            bug(user_position, current_bot_position);

        }

        static void bug(int destination, int source)
        {
            //bot movement director
             
            if (source % 2 == 0 && destination % 2 != 0)
            {
                //right
                movement('r');
            }
            else if (source % 2 != 0 && destination % 2 == 0)
            {
                //left
                movement('l');
            }
            else if ((source % 2 == 0 && destination % 2 == 0) || (source % 2 != 0 && destination % 2 != 0)) {
                // back or forward
                if (source > destination){
                    //down
                    movement('d');
                }
                else if (source < destination) {
                    //up
                    movement('u');
                }
            }
            bot_position();
            if (user_position != current_bot_position)
            {
                bug(user_position, current_bot_position);
            }
            
        }

        static void bot_position() {
            //from arduino of microphone over usb
            String temp = port.ReadLine();
            while (temp != null) {
                temp = port.ReadLine();
            }
            current_bot_position = Convert.ToInt32(temp);
        }

        static void movement(char dir) {
            Console.WriteLine("Moving: " + dir);
            //send the char dir over bluetooth
            //best of luck asrshad

        }
    }
}
