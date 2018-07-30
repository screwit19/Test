using Pacman_Movement_Service.Enums;
using Pacman_Movement_Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pacman_Movement_Service.Implementations
{
   public  class PacManMovementCommands: IPacManMovementCommands
    {
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public CardinalDirectionTypesEnum CurrentDirection { get; set; }
        public string StatusMessage { get; set; }
    

      
        private const int MaxX = 5; //Grid X limit
        private const int MaxY = 5; //Grid Y limit
        private const string COMMAND_SUCCESSFULL = "Command Successfull";
        private const string INVALID_POSITION = "Invalid Position.";
        private const string INVALID_MOVE = "Invalid Move.";
        private const string UNKNOWN_ERROR = "Unknown Error.";



        public bool Left()
        {
            try
            {
                CurrentDirection = (Convert.ToInt16(CurrentDirection) > 1) ? CurrentDirection - 1 : CurrentDirection + 3;
            }
            catch (Exception ex)
            {
                StatusMessage = UNKNOWN_ERROR;
                return false;
            }
            return true;
        }

        public bool Right()
        {
            try
            {
                CurrentDirection = (Convert.ToInt16(CurrentDirection) < 4) ? CurrentDirection + 1 : CurrentDirection - 3;
            }
            catch (Exception ex)
            {
                StatusMessage = UNKNOWN_ERROR;
                return false;
            }
            return true;
        }

        public string Report()
        {
            try
            {
                return "Output: " + XPosition + "," + YPosition + "," + CurrentDirection.ToString();
            }
            catch (Exception ex)
            {
                //TODO : Exception to be logged
                return UNKNOWN_ERROR;
            }
        }

        public bool Place(int X, int Y, CardinalDirectionTypesEnum defaultDirection)
        {
            try
            {

                if (!IsPlacementValid(X, Y, defaultDirection))
                {
                    return false;
                }

                XPosition = X;
                YPosition = Y;
                CurrentDirection = defaultDirection;
                StatusMessage = COMMAND_SUCCESSFULL;
            }
            catch (Exception ex)
            {
                StatusMessage = UNKNOWN_ERROR;
                return false;
            }
            return true;

        }

        public bool Move()
        {
            try
            {
                if (IsMoveValid(CurrentDirection))
                {

                    switch (CurrentDirection)
                    {
                        case CardinalDirectionTypesEnum.NORTH:
                            YPosition++;
                            break;
                        case CardinalDirectionTypesEnum.EAST:
                            XPosition++;
                            break;
                        case CardinalDirectionTypesEnum.SOUTH:
                            YPosition--;
                            break;
                        case CardinalDirectionTypesEnum.WEST:
                            XPosition--;
                            break;
                    }
                    return true;
                }
                else
                {
                    StatusMessage = INVALID_MOVE;
                    return false;
                }
            }
            catch (Exception)
            {
                StatusMessage = UNKNOWN_ERROR;
                return false;
            }
        }

        private bool IsPlacementValid(int X, int Y, CardinalDirectionTypesEnum defaultDirection)
        {
            try
            {
                //Should be within the grid from 0-5
                if (X < 0 || Y < 0 || X > MaxX || Y > MaxY)
                {
                    StatusMessage = INVALID_POSITION;
                    return false;
                }
            }
            catch (Exception ex)
            {
              StatusMessage = UNKNOWN_ERROR;
                return false;
            }

            return true;
        }

        private bool IsMoveValid(CardinalDirectionTypesEnum movingDirection)
        {
            try
            {
                switch (movingDirection)
                {
                    case CardinalDirectionTypesEnum.NORTH:
                        if (YPosition == 5) return false;
                        break;
                    case CardinalDirectionTypesEnum.EAST:
                        if (XPosition == 5) return false;
                        break;
                    case CardinalDirectionTypesEnum.SOUTH:
                        if (YPosition == 0) return false;
                        break;
                    case CardinalDirectionTypesEnum.WEST:
                        if (XPosition == 0) return false;
                        break;
                }
            }
            catch (Exception ex)
            {
                StatusMessage = UNKNOWN_ERROR;
                return false;
            }

            return true;
        }

    }
}
