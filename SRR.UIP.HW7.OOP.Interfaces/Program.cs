﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SRR.UIP.HW7.OOP.Interfaces
{
    struct Point
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Logger logger = new Logger();
            logger.LogStorages.Add(new ConsoleLogStorage());
            logger.LogStorages.Add(new FileLogStorage());
            logger.LogLevel = 1;


            List<Point> points = GetPoints(5);
            Console.WriteLine($"{CalculateArea(points, true)} = {CalculateArea(points, false)}");
            GetPoint(1);


            Console.ReadKey();
        }
        static List<Point> GetPointsTest()
        {
            List<Point> setOfPoints = new List<Point>
            {
                new Point(0,0),
                new Point(1,1),
                new Point(2,4),
                new Point(3,1),
                new Point(4,0)
            };
            return setOfPoints;
        }
        static List<Point> GetPoints(int PointsNumber)
        {
            if (PointsNumber <= 0 || PointsNumber > 20)
            {
                return null;
            }
            Point tempPoint;
            List<Point> tempListOfPoints = new List<Point>();
            int pointsCounter = 0;
            bool isWrongInput = false;
            while(pointsCounter < PointsNumber)
            {
                tempPoint = GetPoint(pointsCounter);
                if (pointsCounter > 0)
                {
                    foreach (var point in tempListOfPoints)
                    {
                        if (tempPoint.Equals(point))
                        {
                            isWrongInput = true;
                            break;
                        } 
                    }
                    if (isWrongInput)
                    {
                        continue;
                    }
                }
                tempListOfPoints.Add(tempPoint);
                pointsCounter++;
            }
            
            return tempListOfPoints;
        }

        static long CalculateArea(List<Point> points, bool isAltAppr)
        {
            long landArea = 0;
            for (int i = 0; i < points.Count; i++)
            {
                int nextIndex = (i == points.Count - 1) ? 0 : i + 1;
                int prevIndex = (i == 0) ? points.Count - 1 : i - 1;
                long par1 = isAltAppr ? points[i].X : points[i].Y;
                long par2_1 = isAltAppr ? points[nextIndex].Y : points[prevIndex].X;
                long par2_2 = isAltAppr ? points[prevIndex].Y : points[nextIndex].X;
                long tempLandArea = landArea;
                landArea += par1 * (par2_1 - par2_2);
            }
            long result = (long)Math.Abs(landArea / 2);
            return result;
        }

        static Point GetPoint(int pointNumber)
        {
            string enteredStringNumber;
            int xCoord;
            int yCoord;
            bool isParsedOK = false;
            Console.WriteLine($"Enter coordinates of {pointNumber} point");
            do
            {
                Console.Write("X => ");
                enteredStringNumber = Console.ReadLine();
                isParsedOK = int.TryParse(enteredStringNumber, out xCoord);
            } while (!isParsedOK);

            do
            {
                Console.Write("Y => ");
                enteredStringNumber = Console.ReadLine();
                isParsedOK = int.TryParse(enteredStringNumber, out yCoord);
            } while (!isParsedOK);

            return new Point(xCoord, yCoord);

        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetMethodName()
        {
            StackFrame sf = new StackTrace().GetFrame(1);
            return sf.GetMethod().Name;
        }

    }
}
