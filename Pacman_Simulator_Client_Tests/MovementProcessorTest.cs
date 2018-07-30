
using NUnit.Framework;
using Pacman_Movement_Service.Implementations;
using Pacman_Movement_Service.Interfaces;
using Pacman_Movement_Service.Enums;

namespace Pacman_Simulator_Client_Tests
{

    [TestFixture]
    public class MovementProcessorTest
    {
            IPacManMovementCommands movementProcessor;

        [OneTimeSetUp]
        public void Setup()
        {
            movementProcessor = new PacManMovementCommands();
        }

        [TestCase(0, 4, CardinalDirectionTypesEnum.NORTH)]
        [TestCase(1, 0, CardinalDirectionTypesEnum.EAST)]
        [TestCase(2, 5, CardinalDirectionTypesEnum.SOUTH)]
        [TestCase(3, 0, CardinalDirectionTypesEnum.WEST)]
        public void ProcessMovementTest_ShouldReturnTrue_WhenValidArgumentsArepassed(int postionX, int positionY, CardinalDirectionTypesEnum directionType)
        {
            //Act
            var result = movementProcessor.Place(postionX, positionY, directionType);

            //Assert
            Assert.That(result, Is.True);
        }
        //Edge Cases.
        [TestCase(-1, 4, CardinalDirectionTypesEnum.NORTH)]
        [TestCase(6, 0, CardinalDirectionTypesEnum.EAST)]
        [TestCase(2, 8, CardinalDirectionTypesEnum.SOUTH)]
        [TestCase(3, -1, CardinalDirectionTypesEnum.WEST)]
        public void ProcessMovementTest_ShouldReturnFalse_WhenValidArgumentsArepassed(int postionX, int positionY, CardinalDirectionTypesEnum directionType)
        {
            //Act
            var result = movementProcessor.Place(postionX, positionY, directionType);

            //Assert
            Assert.That(result, Is.False);
        }

        [TestCase(0, 4, CardinalDirectionTypesEnum.NORTH)]
        [TestCase(1, 0, CardinalDirectionTypesEnum.EAST)]
        [TestCase(2, 5, CardinalDirectionTypesEnum.SOUTH)]
        [TestCase(5, 5, CardinalDirectionTypesEnum.WEST)]
        public void MoveTest_ShouldReturnTrueWhenPositionIsvalid(int postionX, int positionY, CardinalDirectionTypesEnum cuurrentDirection)
        {
            //Arrange
            movementProcessor.XPosition = postionX;
            movementProcessor.YPosition = positionY;
            movementProcessor.CurrentDirection = cuurrentDirection;

            //Act
            var result = movementProcessor.Move();

            //Assert
            Assert.That(result, Is.True);
        }

        //Edge cases.
        [TestCase(0, 0, CardinalDirectionTypesEnum.SOUTH)]
        [TestCase(0, 0, CardinalDirectionTypesEnum.WEST)]
        [TestCase(5, 5, CardinalDirectionTypesEnum.NORTH)]
        [TestCase(5, 5, CardinalDirectionTypesEnum.EAST)]
        public void MoveTest_ShouldReturnFalseWhenPositionToInvalid(int postionX, int positionY, CardinalDirectionTypesEnum cuurrentDirection)
        {
            //Arrange
            movementProcessor.XPosition = postionX;
            movementProcessor.YPosition = positionY;
            movementProcessor.CurrentDirection = cuurrentDirection;

            //Act
            var result = movementProcessor.Move();

            //Assert
            Assert.That(result, Is.False);
        }

        [TestCase(CardinalDirectionTypesEnum.SOUTH, CardinalDirectionTypesEnum.WEST)]
        [TestCase(CardinalDirectionTypesEnum.EAST, CardinalDirectionTypesEnum.SOUTH)]
        [TestCase(CardinalDirectionTypesEnum.WEST, CardinalDirectionTypesEnum.NORTH)]
        [TestCase(CardinalDirectionTypesEnum.NORTH, CardinalDirectionTypesEnum.EAST)]
        public void RightTest_ShouldReturnChangedDirection_WhenCorrectComamndIsGiven(CardinalDirectionTypesEnum currentDirection, CardinalDirectionTypesEnum newDirection)
        {
            //Arrange
            movementProcessor.CurrentDirection = currentDirection;

            //Act
            var result = movementProcessor.Right();

            //Assert
            Assert.That(movementProcessor.CurrentDirection, Is.EqualTo(newDirection));
        }

        [TestCase(CardinalDirectionTypesEnum.SOUTH, CardinalDirectionTypesEnum.WEST)]
        [TestCase(CardinalDirectionTypesEnum.EAST, CardinalDirectionTypesEnum.SOUTH)]
        [TestCase(CardinalDirectionTypesEnum.WEST, CardinalDirectionTypesEnum.NORTH)]
        [TestCase(CardinalDirectionTypesEnum.NORTH, CardinalDirectionTypesEnum.EAST)]
        public void LeftTest_ShouldReturnChangedDirection_WhenCorrectComamndIsGiven(CardinalDirectionTypesEnum currentDirection, CardinalDirectionTypesEnum newDirection)
        {
            //Arrange
            movementProcessor.CurrentDirection = currentDirection;

            //Act
            var result = movementProcessor.Right();

            //Assert
            Assert.That(movementProcessor.CurrentDirection, Is.EqualTo(newDirection));
        }

        //Edge Cases
        [TestCase(0, 0, CardinalDirectionTypesEnum.SOUTH, "Output: 0,0,SOUTH")]
        [TestCase(0, 5, CardinalDirectionTypesEnum.EAST, "Output: 0,5,EAST")]
        [TestCase(5, 0, CardinalDirectionTypesEnum.WEST, "Output: 5,0,WEST")]
        [TestCase(5, 5, CardinalDirectionTypesEnum.NORTH, "Output: 5,5,NORTH")]
        public void ReportTest_ShouldPrintOutput_WhenCorrectCommandIsGiven(int postionX, int positionY, CardinalDirectionTypesEnum directionType, string output)
        {
            //Arrange
            movementProcessor.XPosition = postionX;
            movementProcessor.YPosition = positionY;
            movementProcessor.CurrentDirection = directionType;

            //Act
            var result = movementProcessor.Report();

            //Assert
            Assert.That(result, Is.EqualTo(output));
        }
    }
}