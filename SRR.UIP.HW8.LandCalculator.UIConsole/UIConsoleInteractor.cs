using SRR.UIP.HW8.LandCalculator.BLL.Services;
using SRR.UIP.HW8.LandCalculator.Shared;
using SRR.UIP.HW8.LandCalculator.Shared.Interfaces;
using SRR.UIP.HW8.LandCalculator.Shared.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRR.UIP.HW8.LandCalculator.UIConsole
{
    public class UIConsoleInteractor
    {
        private ILandCalculator LandAreaCalculator { get; }
        private IPointsValidator PointsValidator { get; set; }

        public UIConsoleInteractor(ILandCalculator landAreaCalculator)
        {
            LandAreaCalculator = landAreaCalculator;
            this.PointsValidator = new PointsValidator();

        }

        internal void Start()
        {
            List<Point> points = GetPointsTest(3);
            PointsValidationResult validationResult = PointsValidator.GetValidationResult(points);
            Console.WriteLine($"Result validation: {validationResult.Message}; ");
            long landArea = LandAreaCalculator.CalculateLandArea(points);
            Console.WriteLine($"Result = {landArea}");
        }

        private List<Point> GetPointsTest(int choice)
        {
            List<Point> setOfPoints;

            switch (choice)
            {
                case 0:
                    setOfPoints = new List<Point>
                    {
                        new Point(0,0),
                        new Point(1,1),
                        new Point(2,2),
                        new Point(3,3),
                        new Point(4,4),
                        new Point(0,0)
                    };
                    break;
                case 1:
                    setOfPoints = new List<Point>
                    {
                        new Point(0,0),
                        new Point(1,1)
                    };
                    break;
                case 2://crossed
                    setOfPoints = new List<Point>
                    {
                        new Point(0,0),
                        new Point(1,1),
                        new Point(2,4),
                        new Point(3,1),
                        new Point(0,4),
                        new Point(0,0)
                    };
                    break;
                case 3://same point
                    setOfPoints = new List<Point>
                    {
                        new Point(0,0),
                        new Point(1,1),
                        new Point(1,1),
                        new Point(3,1),
                        new Point(4,0),
                        new Point(0,0)
                    };
                    break;
                default:
                    setOfPoints = new List<Point>
                    {
                        new Point(0,0),
                        new Point(0,1),
                        new Point(1,1),
                        new Point(1,0),
                        new Point(0,0)
                    };
                    break;
            }
            return setOfPoints;
        }

        private List<Point> GetPoints(int PointsNumber)
        {
            if (PointsNumber <= 0 || PointsNumber > 20)
            {
                StaticInjector.Logger.Warn("Number of points must be  0 < x <=20");
                return null;
            }
            Point tempPoint;
            List<Point> tempListOfPoints = new List<Point>();
            int pointsCounter = 0;
            while (pointsCounter < PointsNumber)
            {
                tempPoint = GetPoint(pointsCounter);
                if (pointsCounter != PointsNumber - 1)
                {
                    bool isInputValid = IsPointValid(tempListOfPoints, tempPoint);
                    if (!isInputValid)
                    {
                        continue;
                    }

                }
                tempListOfPoints.Add(tempPoint);
                StaticInjector.Logger.Info($"Point {pointsCounter} with value ({tempPoint.X},{tempPoint.Y}) added");
                pointsCounter++;

            }

            return tempListOfPoints;
        }

        private bool IsPointValid(List<Point> pointsCollection, Point takenPoint)
        {
            if (pointsCollection.Count == 0)
            {
                return true;
            }
            else
            {
                foreach (var point in pointsCollection)
                {
                    if (takenPoint.Equals(point))
                    {
                        StaticInjector.Logger.Warn("Points can not be the same");
                        return false;
                    }
                }
                return true;
            }

        }

        static Point GetPoint(int pointNumber)
        {
            int xCoord;
            int yCoord;

            Console.WriteLine($"Enter coordinates of {pointNumber} point");
            InputCoordinate("X", out xCoord);
            InputCoordinate("Y", out yCoord);
            StaticInjector.Logger.Info($"Point {pointNumber} created, ({xCoord},{yCoord})");

            return new Point(xCoord, yCoord);
        }

        private static void InputCoordinate(string coordinateName, out int coordinate)
        {
            bool isParsedOK = false;
            do
            {
                Console.Write($"{coordinateName} => ");
                string enteredStringNumber = Console.ReadLine();
                isParsedOK = int.TryParse(enteredStringNumber, out coordinate);
                if (!isParsedOK)
                {
                    StaticInjector.Logger.Error($"Not valid value of {coordinateName}");
                }
            } while (!isParsedOK);
        }
    }
}
