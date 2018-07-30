using Pacman_Movement_Service.Enums;

namespace Pacman_Movement_Service.Interfaces
{
 public   interface IPacManMovementCommands
    {
        CardinalDirectionTypesEnum CurrentDirection { get; set; }
        int XPosition { get; set; }
        int YPosition { get; set; }
        string StatusMessage { get; set; }
        bool Left();
        bool Move();
        bool Place(int positionX, int positionY, CardinalDirectionTypesEnum defaultDirection);
        string Report();
        bool Right();
    }
}
