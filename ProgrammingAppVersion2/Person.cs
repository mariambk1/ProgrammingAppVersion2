﻿using System;
using System.Drawing;
using System.IO;

namespace ProgrammingApp
{
    public class Person
    {
        public int PlaceX { get; private set; }
        public int PlaceY { get; private set; }
        public Direction Direction { get; private set; }

        public Person()
        {
            PlaceX = 0;
            PlaceY = 0;
            Direction = Direction.East;
        }

        public void Turn(string turnDirection)
        {
            if (turnDirection == "Left")
            {
                Direction = Direction switch
                {
                    Direction.North => Direction.West,
                    Direction.West => Direction.South,
                    Direction.South => Direction.East,
                    Direction.East => Direction.North,
                    _ => Direction
                };
            }
            else if (turnDirection == "Right")
            {
                Direction = Direction switch
                {
                    Direction.North => Direction.East,
                    Direction.East => Direction.South,
                    Direction.South => Direction.West,
                    Direction.West => Direction.North,
                    _ => Direction
                };
            }
            else
            {
                throw new ArgumentException("Turn must be 'Left' or 'Right'");
            }
        }

        public void Move(int steps)
        {
            switch (Direction)
            {
                case Direction.North:
                    PlaceY += steps;
                    break;
                case Direction.East:
                    PlaceX += steps;
                    break;
                case Direction.South:
                    PlaceY -= steps;
                    break;
                case Direction.West:
                    PlaceX -= steps;
                    break;
            }
        }

        public string GetPosition()
        {
            return $"({PlaceX}, {PlaceY})";
        }
        public string GetDirection()
        {
            return $"{Direction}";
        }

        public bool WallAhead()
        {
            return false;
        }

        public bool AtGridEdge()
        {
            return false;
        }

        public void ResetPosition()
        {
            PlaceX = 0;
            PlaceY = 0;
            Direction = Direction.East; 
        }

        public Bitmap GetCharacterImage()
        {
            string projectRoot = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

            switch (Direction)
            {
                case Direction.North:
                    return new Bitmap(Path.Combine(projectRoot, "characterImages", "CharacterNorth.png"));
                case Direction.South:
                    return new Bitmap(Path.Combine(projectRoot, "characterImages", "CharacterSouth.png"));
                case Direction.West:
                    return new Bitmap(Path.Combine(projectRoot, "characterImages", "CharacterWest.png"));
                case Direction.East:
                default:
                    return new Bitmap(Path.Combine(projectRoot, "characterImages", "CharacterEast.png"));
            }
        }
    }
}
