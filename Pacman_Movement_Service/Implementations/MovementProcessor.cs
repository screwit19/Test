using Pacman_Movement_Service.Enums;
using Pacman_Movement_Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pacman_Movement_Service.Implementations
{
   public  class MovementProcessor: IMovementProcessor
    {

        private const string INVALID_COMMAND = "Invalid Command.";
        private const string UNKNOWN_ERROR = "Unknown Error.";
        private IPacManMovementCommands pacManMovementCommands { get; set; }


        public MovementProcessor(IPacManMovementCommands _pacManMovementCommands)
        {
            this.pacManMovementCommands = _pacManMovementCommands;
        }

        public string ProcessMovement(string command)
        {
            string commandString = command.ToUpper();
            string commandTrimmed = "";
            bool commandResult = false;

            try
            {

                if (ValidateCommand(commandString))
                {
                    commandTrimmed = GetActualCommand(commandString);
                }
                else
                {
                    return pacManMovementCommands.StatusMessage;
                }

                switch ((MovementCommandsEnum)Enum.Parse(typeof(MovementCommandsEnum), commandTrimmed, true))
                {
                    case MovementCommandsEnum.PLACE:
                        string arguments = commandString.Substring(commandString.IndexOf(" "));

                        string[] argArray = arguments.Split(',');

                        if (argArray.Length < 3) return INVALID_COMMAND;

                        int posX = Convert.ToInt16(argArray[0]);
                        int posY = Convert.ToInt16(argArray[1]);
                        string direction = argArray[2];

                        if (!Enum.IsDefined(typeof(CardinalDirectionTypesEnum), direction))
                        {
                            return INVALID_COMMAND;
                        }

                        commandResult = pacManMovementCommands.Place(posX, posY, (CardinalDirectionTypesEnum)Enum.Parse(typeof(CardinalDirectionTypesEnum), direction, true));
                        return PrintOutput(commandResult);
                    case MovementCommandsEnum.MOVE:
                        commandResult = pacManMovementCommands.Move();
                        return PrintOutput(commandResult);
                    case MovementCommandsEnum.LEFT:
                        commandResult = pacManMovementCommands.Left();
                        return PrintOutput(commandResult);
                    case MovementCommandsEnum.RIGHT:
                        commandResult = pacManMovementCommands.Right();
                        return PrintOutput(commandResult);
                    case MovementCommandsEnum.REPORT:
                        return pacManMovementCommands.Report();
                    default:
                        return INVALID_COMMAND;

                }
            }
            catch (Exception ex)
            {
                //TODO : Exception to be logged
                return UNKNOWN_ERROR;
            }
        }

        private bool ValidateCommand(string commandString)
        {
            try
            {

                if (Enum.IsDefined(typeof(MovementCommandsEnum), commandString))
                {
                    if ((MovementCommandsEnum)Enum.Parse(typeof(MovementCommandsEnum), commandString, true) != MovementCommandsEnum.PLACE)
                    {
                        if (ValidInitialCommand(commandString))
                        {
                            return true;
                        }
                        else
                        {
                            pacManMovementCommands.StatusMessage = INVALID_COMMAND;
                            return false;
                        }
                    }
                    else
                    {
                        pacManMovementCommands.StatusMessage = INVALID_COMMAND;
                        return false;
                    }
                }
                else
                {
                    if (commandString.IndexOf(" ") == -1)
                    {
                        pacManMovementCommands.StatusMessage = INVALID_COMMAND;
                        return false;
                    }
                    else
                    {
                        string str = commandString.Substring(0, commandString.IndexOf(" "));
                        if (ValidInitialCommand(str))
                        {
                            return true;
                        }
                        else
                        {
                            pacManMovementCommands.StatusMessage = INVALID_COMMAND;
                            return false;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                //TODO : Exception to be logged
                pacManMovementCommands.StatusMessage = UNKNOWN_ERROR;
                return false;
            }
        }

        //Inital Command Should be a Place Command. 
        private bool ValidInitialCommand(string commandString)
        {
            try
            {
                if (Enum.IsDefined(typeof(MovementCommandsEnum), commandString))
                {
                    if (pacManMovementCommands.CurrentDirection == 0 && (MovementCommandsEnum)Enum.Parse(typeof(MovementCommandsEnum), commandString, true) != MovementCommandsEnum.PLACE)
                    {
                        pacManMovementCommands.StatusMessage = INVALID_COMMAND;
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    pacManMovementCommands.StatusMessage = INVALID_COMMAND;
                    return false;
                }
            }
            catch (Exception ex)
            {
                //TODO : Exception to be logged
                pacManMovementCommands.StatusMessage = UNKNOWN_ERROR;
                return false;
            }
        }

        private string GetActualCommand(string commandString)
        {
            string command = "";
            try
            {
                if (Enum.IsDefined(typeof(MovementCommandsEnum), commandString))
                {
                    command = commandString;
                }
                else
                {
                    command = commandString.Substring(0, commandString.IndexOf(" "));
                }
            }
            catch (Exception ex)
            {
                //TODO : Exception to be logged
                return "";
            }

            return command;
        }

        private string PrintOutput(bool commandResult)
        {
            try
            {
                if (commandResult)
                {
                    return "";
                }
                else
                {
                    return pacManMovementCommands.StatusMessage;
                }
            }
            catch (Exception ex)
            {
                //TODO : Exception to be logged
                return UNKNOWN_ERROR;
            }
        }
    }
}
