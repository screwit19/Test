using System;
using System.Collections.Generic;
using System.Text;

namespace Pacman_Movement_Service.Interfaces
{
    public interface IMovementProcessor
    {
        string ProcessMovement(string command);
    }
}
