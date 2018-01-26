using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hacking
{
    public static class LineHelper
    {
        /// <summary>
        /// Gets a line that is eased in one direction and lerped in the other
        /// </summary>
        /// <param name="start">Starting point of the line</param>
        /// <param name="end">End point of the line</param>
        /// <param name="iterations">How many times to run the algorithm. More = smoother line but laggier</param>
        /// <param name="easePow">How much to ease the line. 0 = not eased</param>
        /// <param name="down">True if the line will be used downwards, else false. Defaults to false</param>
        /// <returns>An array containing all the points in the line</returns>
        public static Vector3[] GetEasedLine(Vector2 start, Vector2 end, int iterations, float easePow, bool down = true)
        {
            List<Vector3> output = new List<Vector3>();

            for(int i = 0; i < iterations; i++)
            {
                float ix = (float)i / (iterations-1);
                float x = Lerp(start.x, end.x, down ? Ease(ix, easePow + 1) : ix); // Position on x axis
                float y = Lerp(start.y, end.y, !down ? Ease(ix, easePow + 1) : ix); // Position on y axis
                output.Add(new Vector3(x, y));
            }

            return output.ToArray();
        }

        /// <summary>
        /// Gets a straight line from start to end
        /// </summary>
        /// <param name="start">The starting point of the line</param>
        /// <param name="end">The end point of the line</param>
        /// <param name="iterations">How many segments to create. Defaults to 2</param>
        /// <returns>An array containing all the points in the line</returns>
        public static Vector3[] GetStraightLine(Vector2 start, Vector2 end, int iterations = 2)
        {
            List<Vector3> output = new List<Vector3>();

            for (int i = 0; i < iterations; i++)
            {
                output.Add(Vector3.Lerp(start, end, (float)i / (iterations-1)));
            }

            return output.ToArray();
        }

        static float Ease(float x, float a)
        {
            return Mathf.Pow(x, a) / (Mathf.Pow(x, a) + Mathf.Pow(1 - x, a));
        }

        static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }
    }
}