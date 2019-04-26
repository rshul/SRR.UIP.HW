using SRR.UIP.HW8.LandCalculator.Shared.Interfaces;
using SRR.UIP.HW8.LandCalculator.Shared.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRR.UIP.HW8.LandCalculator.BLL.Services
{
    public class PointsValidator : IPointsValidator
    {
        public PointsValidationResult GetValidationResult(List<Point> points)
        {
            PointsValidationResult validationResult = new PointsValidationResult();
            bool notEnoughUniquePoints = !IsPointsCountMoreThanTwo(points) || GetNumberOfDublicates(points) != 0;
            validationResult.Mistakes = PointsMistakes.Nothing;
            validationResult.Mistakes = notEnoughUniquePoints ?
                validationResult.Mistakes | PointsMistakes.NotEnoughUniquePoints : validationResult.Mistakes;
            validationResult.Mistakes = IsClosedContour(points) ? 
                validationResult.Mistakes : validationResult.Mistakes | PointsMistakes.NotClosedShape;
            validationResult.Mistakes = IsCrossedContour(points) ?
                validationResult.Mistakes | PointsMistakes.CrossedShape : validationResult.Mistakes;

            validationResult.ArePointsValid = validationResult.Mistakes == PointsMistakes.Nothing ? true : false;
            validationResult.Message = $"Passed validation {validationResult.ArePointsValid}; Problems: {validationResult.Mistakes}";

            return validationResult;
        }

        private bool IsPointsCountMoreThanTwo(List<Point> points)
        {
            return points.Count > 2;
        }

        private bool IsClosedContour(List<Point> points)
        {

            if (points == null || points.Count <= 2)
            {
                return false;
            }
 
            int pointsCount = points.Count;
            bool isLastAndFirstPointsConnected =  points[0] == points[pointsCount - 1];
            bool isNotOneLineOfEdges = IsNotOneLineOfEdges(points); 
            
            return isLastAndFirstPointsConnected && isNotOneLineOfEdges;
        }

        private bool IsNotOneLineOfEdges(List<Point> points)
        {
            Point pointStart, pointMiddle, pointEnd;
            for (int i = 2; i < points.Count-1; i++)
            {
                pointStart = points[i - 2];
                pointMiddle = points[i - 1];
                pointEnd = points[i];
                bool isNotOneLineWithThreePoints = !float.IsNaN(FindIntersection(pointStart, pointMiddle, pointMiddle, pointEnd).X);
                if (isNotOneLineWithThreePoints)
                {
                    return true;
                }

            }
            return false;
        }

        private int GetNumberOfDublicates(List<Point> points)
        {
            if (points ==  null || points.Count == 0)
            {
                return 0;
            }
            List<Point> tempPoints = points.GetRange(0, points.Count-1);
            return tempPoints.GroupBy(i => i).Where(g => g.Count() > 1).Select(g => g.Key).ToArray().Length;
        }

        private bool IsCrossedContour(List<Point> points)
        {
            List<Point> tempPoints = points.Distinct().ToList();
            if (tempPoints == null || tempPoints.Count <= 2)
            {
                return false;
            }
            int pointsCount = tempPoints.Count;
            Point sectionAPoint1, sectionAPoint2, sectionBPoint1, sectionBPoint2;
            for (int i = 2; i < pointsCount - 1; i++)
            {
                sectionAPoint1 = tempPoints[i];
                sectionAPoint2 = tempPoints[i + 1];
                for (int j = 0; j < i - 1; j++)
                {
                    if (i == pointsCount - 2 && j == 0)
                    {
                        continue;
                    }
                    sectionBPoint1 = tempPoints[j];
                    sectionBPoint2 = tempPoints[j + 1];
                    if (IsIntersection(sectionAPoint1, sectionAPoint2, sectionBPoint1, sectionBPoint2))
                    {
                        return true;
                    }

                }

            }
            return false;
        }
        
        public static PointF FindIntersection(Point s1, Point e1, Point s2, Point e2)
        {
            
            float a1 = e1.Y - s1.Y;
            float b1 = s1.X - e1.X;
            float c1 = a1 * s1.X + b1 * s1.Y;

            float a2 = e2.Y - s2.Y;
            float b2 = s2.X - e2.X;
            float c2 = a2 * s2.X + b2 * s2.Y;

            float delta = a1 * b2 - a2 * b1;
            //If lines are parallel, the result will be (NaN, NaN).
            return delta == 0 ? new PointF(float.NaN, float.NaN)
                : new PointF((b2 * c1 - b1 * c2) / delta, (a1 * c2 - a2 * c1) / delta);
        }

        public static bool IsIntersection(Point p1, Point p2, Point p3, Point p4)
        {
            float dx12 = p2.X - p1.X;
            float dy12 = p2.Y - p1.Y;
            float dx34 = p4.X - p3.X;
            float dy34 = p4.Y - p3.Y;
            float denominator = (dy12 * dx34 - dx12 * dy34);
            float t1 = ((p1.X - p3.X) * dy34 + (p3.Y - p1.Y) * dx34) / denominator;
            if (float.IsInfinity(t1))
            {
                // The lines are parallel (or close enough to it).
                return false;
            }
            float t2 = ((p3.X - p1.X) * dy12 + (p1.Y - p3.Y) * dx12) / -denominator;
            return ((t1 >= 0) && (t1 <= 1) && (t2 >= 0) && (t2 <= 1));
        }
    }
}
