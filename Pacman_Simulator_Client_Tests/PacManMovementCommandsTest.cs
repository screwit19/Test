using System;
using NUnit.Framework;
using Pacman_Movement_Service.Implementations;
using Pacman_Movement_Service.Interfaces;

namespace Pacman_Simulator_Client_Tests
{
    [TestFixture]
    public class PacManMovementCommandsTest
    {
        IMovementProcessor movementProcessor;

        [OneTimeSetUp]
        public void Setup()
        { 
            IPacManMovementCommands pacmanMovementCommands = new PacManMovementCommands();
            movementProcessor = new Pacman_Movement_Service.Implementations.MovementProcessor(pacmanMovementCommands);
        }

        [TestCase("PLACE 0,0,NORTH", "")]
        [TestCase("PLACE 5,5,SOUTH", "")]
        [TestCase("PLACE", "Invalid Command.")]
        [TestCase("PLACE -1,0,NORTH", "Invalid Position.")]
        [TestCase("PLACE 0,-1,NORTH", "Invalid Position.")]
        [TestCase("PLACE 6, 5,NORTH", "Invalid Position.")]
        [TestCase("place 5, 5,NORTH", "")]
        [TestCase("MOVE", "")]
        [TestCase("MOVE", "Invalid Move.")]
        [TestCase("RIGHT", "")]
        [TestCase("LEFT", "")]
        [TestCase("REPORT", "Output: 5,4,EAST")]

        public void PacManMovementCommandsTest_ShouldReturnCorrectOutput_WhenCommandIsGiven(string command, string report)
        {

            //Act
            var result = movementProcessor.ProcessMovement(command);

            //Assert
            Assert.AreEqual(report,result);
        }
    }
}
