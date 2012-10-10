//-----------------------------------------------------------------------
// <copyright file="BitmapCompare.cs" company="eggfly">
//     Copyright (c) eggfly. All rights reserved.
// </copyright>
// <author>eggfly</author>
//-----------------------------------------------------------------------

namespace RemoteCapture.Common
{
    using System;
    using System.Net;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Ink;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;

    /// <summary>
    /// BitmapCompare Class
    /// </summary>
    public class BitmapCompare
    {
        /// <summary>
        /// Compares the specified Y buffer A.
        /// </summary>
        /// <param name="bufferA">The buffer A.</param>
        /// <param name="bufferB">The buffer B.</param>
        /// <returns>
        /// The match precent.
        /// </returns>
        public static double Compare(byte[] bufferA, byte[] bufferB)
        {
            bool different = false;
            for (int i = 0; i < bufferA.Length; i++)
            {
                if (bufferA[i] != bufferB[i])
                {
                    different = true;
                    break;
                }
            }

            different = !different;

            var hisogramA = GetHisogram(bufferA);
            var hisogramB = GetHisogram(bufferB);
            return CompareHisogram(hisogramA, hisogramB);
        }

        /// <summary>
        /// Compares the hisogram.
        /// </summary>
        /// <param name="hisogramA">The hisogram A.</param>
        /// <param name="hisogramB">The hisogram B.</param>
        /// <returns>The match precent.</returns>
        private static double CompareHisogram(int[] hisogramA, int[] hisogramB)
        {
            if (hisogramA.Length != hisogramB.Length)
            {
                return 0.0;
            }

            int len = hisogramA.Length;
            double result = 0;
            for (int i = 0; i < len; i++)
            {
                double match;
                int a = hisogramA[i];
                int b = hisogramB[i];
                if (a != b)
                {
                    int ddd = 0;
                    ddd++;
                }

                if (a == 0 && b == 0)
                {
                    match = 1.0;
                }
                else
                {
                    match = 1.0 - ((double)Math.Abs(a - b) / Math.Max(a, b));
                }

                result += match;
            }

            return result / len;
        }

        /// <summary>
        /// Gets the hisogram.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <returns>The hisogram.</returns>
        private static int[] GetHisogram(byte[] buffer)
        {
            int[] hisogram = new int[byte.MaxValue + 1];
            foreach (byte b in buffer)
            {
                hisogram[b]++;
            }

            return hisogram;
        }
    }
}
