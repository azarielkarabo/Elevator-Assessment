

using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    enum Direction
    {
        Up,
        Down,
        None
    }

    class Elevator
    {
        public int CurrentFloor { get; set; }
        public Direction CurrentDirection { get; set; }
        public int NumberOfPeople { get; set; }
        public int WeightLimit { get; set; }

        public void MoveToFloor(int floor)
        {
            if (floor > CurrentFloor)
                CurrentDirection = Direction.Up;
            else if (floor < CurrentFloor)
                CurrentDirection = Direction.Down;

            Console.WriteLine($"Elevator is moving {CurrentDirection.ToString().ToLower()} from floor {CurrentFloor} to floor {floor}.");

            CurrentFloor = floor;
            CurrentDirection = Direction.None;
        }

        public void AddPeople(int numberOfPeople)
        {
            NumberOfPeople += numberOfPeople;
        }

        public void RemovePeople(int numberOfPeople)
        {
            NumberOfPeople -= numberOfPeople;
        }

        public bool IsOverWeightLimit(int numberOfPeople)
        {
            int totalWeight = (NumberOfPeople + numberOfPeople) * 70; // Assuming average weight per person as 70 kg
            return totalWeight > WeightLimit;
        }

        public void ShowStatus()
        {
            Console.WriteLine($"Elevator is on floor {CurrentFloor}. People on board: {NumberOfPeople}. Direction: {CurrentDirection.ToString()}");
        }
    }

    class ElevatorController
    {
        private List<Elevator> elevators;

        public ElevatorController(int numberOfElevators, int weightLimit)
        {
            elevators = new List<Elevator>();

            for (int i = 0; i < numberOfElevators; i++)
            {
                Elevator elevator = new Elevator
                {
                    WeightLimit = weightLimit
                };
                elevators.Add(elevator);
            }
        }

        public void CallElevator(int floor, int numberOfPeople)
        {
            Elevator elevator = GetNearestAvailableElevator(floor, numberOfPeople);

            if (elevator != null)
            {
                elevator.MoveToFloor(floor);
                elevator.AddPeople(numberOfPeople);
            }
            else
            {
                Console.WriteLine("No available elevator or weight limit exceeded.");
            }
        }

        private Elevator GetNearestAvailableElevator(int floor, int numberOfPeople)
        {
            Elevator nearestElevator = null;
            int minDistance = int.MaxValue;

            foreach (Elevator elevator in elevators)
            {
                int distance = Math.Abs(elevator.CurrentFloor - floor);

                if (distance < minDistance && elevator.NumberOfPeople == 0 && !elevator.IsOverWeightLimit(numberOfPeople))
                {
                    minDistance = distance;
                    nearestElevator = elevator;
                }
            }

            return nearestElevator;
        }

        public void ShowAllElevatorStatus()
        {
            foreach (Elevator elevator in elevators)
            {
                elevator.ShowStatus();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ElevatorController controller = new ElevatorController(3, 500); // Set weight limit for elevators as 500 kg

            // Call elevator to floor 3 with 2 people
            controller.CallElevator(3, 2);

            // Call elevator to floor 1 with 3 people
            controller.CallElevator(1, 3);

            // Show status of all elevators
            controller.ShowAllElevatorStatus();
        }
    }
}