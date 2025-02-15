﻿using System;
/// <summary>
/// Check if a given point is inside a polygon or not.
/// Given a point, check if a horizontal right line from the point to infinity intersects the sides of the polygon
/// If the line intersects odd number of times, the point is INSIDE
/// If there is NO intersection or EVEN number of intersections, the point is OUTSIDE.
/// </summary>
namespace PolygonTestApp
{
    class Program
    {
        //In order to overcome overflow problem
        static int INF = 10000;

        class Point
        {
            public int x;
            public int y;

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        };
        //To check q lies on pr
        static bool onSegment(Point p, Point q, Point r)
        {
            if (q.x <= Math.Max(p.x, r.x) &&
                q.x >= Math.Min(p.x, r.x) &&
                q.y <= Math.Max(p.y, r.y) &&
                q.y >= Math.Min(p.y, r.y))
            {
                return true;
            }
            return false;
        }
        static int orientation(Point p, Point q, Point r)
        {
            int val = (q.y - p.y) * (r.x - q.x) -
                    (q.x - p.x) * (r.y - q.y);

            if (val == 0)
            {
                return 0; // colinear 
            }
            return (val > 0) ? 1 : 2; // clock or counterclock wise 
        }
        static bool doIntersect(Point p1, Point q1,
                                   Point p2, Point q2)
        {
            // Find the four orientations needed for 
            // general and special cases 
            int o1 = orientation(p1, q1, p2);
            int o2 = orientation(p1, q1, q2);
            int o3 = orientation(p2, q2, p1);
            int o4 = orientation(p2, q2, q1);

            // General case 
            if (o1 != o2 && o3 != o4)
            {
                return true;
            }

            // Special Cases 
            // p1, q1 and p2 are colinear and 
            // p2 lies on segment p1q1 
            if (o1 == 0 && onSegment(p1, p2, q1))
            {
                return true;
            }

            // p1, q1 and p2 are colinear and 
            // q2 lies on segment p1q1 
            if (o2 == 0 && onSegment(p1, q2, q1))
            {
                return true;
            }

            // p2, q2 and p1 are colinear and 
            // p1 lies on segment p2q2 
            if (o3 == 0 && onSegment(p2, p1, q2))
            {
                return true;
            }

            // p2, q2 and q1 are colinear and 
            // q1 lies on segment p2q2 
            if (o4 == 0 && onSegment(p2, q1, q2))
            {
                return true;
            }

            // Doesn't fall in any of the above cases 
            return false;
        }
        //check whether point lies inside or outside of polygon
        static bool isInside(Point[] polygon, int n, Point p)
        {
            // There must be at least 3 vertices in polygon[] 
            if (n < 3)
            {
                return false;
            }

            // Create a point for line segment from p to infinite 
            Point extreme = new Point(INF, p.y);

            // Count intersections of the above line 
            // with sides of polygon 
            int count = 0, i = 0;
            do
            {
                int next = (i + 1) % n;

                // Check if the line segment from 'p' to 
                // 'extreme' intersects with the line 
                // segment from 'polygon[i]' to 'polygon[next]' 
                if (doIntersect(polygon[i],
                                polygon[next], p, extreme))
                {
                    // If the point 'p' is colinear with line 
                    // segment 'i-next', then check if it lies 
                    // on segment. If it lies, return true, otherwise false 
                    if (orientation(polygon[i], p, polygon[next]) == 0)
                    {
                        return onSegment(polygon[i], p,
                                        polygon[next]);
                    }
                    count++;
                }
                i = next;
            } while (i != 0);

            // Return true if count is odd, false otherwise 
            return (count % 2 == 1); // Same as (count%2 == 1) 
        }
        static void Main(string[] args)
        {                   
                 
      
   
            Point[] polygon1 = {new Point(3, 4),
                            new Point(5, 3),
                            new Point(4, 2),
                            new Point(5, 1),
                            new Point(1, 2) };
            Point p = new Point(3, 3);
            int n = polygon1.Length;
            if (isInside(polygon1, n, p))
            {
                Console.WriteLine("True");
            }
            else
            {
                Console.WriteLine("False");
            }
        }
    }
    }

