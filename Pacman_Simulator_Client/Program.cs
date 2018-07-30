using Pacman_Movement_Service.Implementations;
using Pacman_Movement_Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman_Simulator_Client
{
   public  class Program
    {
        private const string WELCOME_MESSAGE = "*****Pacman Simulator*****";
        private const string COMMAND_INIT_MESSAGE = "Please enter the command.";
        private const string UNKNOWN_ERROR = "Unknown Error.";

        static void Main(string[] args)
        {
            try
            {
                System.Console.WriteLine(WELCOME_MESSAGE);
                System.Console.WriteLine(COMMAND_INIT_MESSAGE);

                IPacManMovementCommands movementCommand = new PacManMovementCommands();
                IMovementProcessor commandProcessor = new MovementProcessor(movementCommand);

               string output = commandProcessor.ProcessMovement(System.Console.ReadLine());
                if (output != string.Empty)
                {
                    System.Console.WriteLine(output);
                }
               
            }
            catch (Exception ex)
            {
             System.Console.WriteLine(UNKNOWN_ERROR);
            }
        }
    }
}
