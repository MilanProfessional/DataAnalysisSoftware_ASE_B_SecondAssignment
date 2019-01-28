﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAnalysisSoftware_ASE_B_FirstAssignment
{
    public class Summary
    {

        /// <summary>
        /// to find maximum value from array
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int FindMax(List<string> value)
        {
            int maxValue = 0;

            for (int i = 0; i < value.Count; i++)
            {
                maxValue = (maxValue > Convert.ToInt16(value.ElementAt(i))) ? maxValue : Convert.ToInt16(value.ElementAt(i));
            }

            return maxValue;
        }

        /// <summary>
        /// to find minimum value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int FindMin(List<string> value)
        {
            int minValue = Convert.ToInt16(value.ElementAt(0));

            for (int i = 0; i < value.Count; i++)
            {
                minValue = (minValue > Convert.ToInt16(value.ElementAt(i))) ? Convert.ToInt16(value.ElementAt(i)) : minValue;
            }

            return minValue;
        }


        /// <summary>
        /// to find avarage value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double FindAverage(List<string> value)
        {
            int average = 0;

            foreach (var data in value)
            {
                average += Convert.ToInt16(data);
            }

            return average / value.Count;
        }

        /// <summary>
        /// To find sum
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static double FindSum(List<string> list)
        {
            double sum = 0;

            foreach (var data in list)
            {
                sum += Convert.ToDouble(data);
            }

            return sum;
        }

        /// <summary>
        /// to make date in a proper format
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ConvertToDate(string date)
        {
            string year = "";
            string month = "";
            string day = "";
            for (int i = 0; i < 4; i++)
            {
                year = year + date[i];
            };
            for (int i = 4; i < 6; i++)
            {
                month = month + date[i];
            };
            for (int i = 6; i < 8; i++)
            {
                day = day + date[i];
            };
            string convertedDate = year + "-" + month + "-" + day;
            return convertedDate;
        }

        /// <summary>
        /// to round up into required decimal
        /// </summary>
        /// <param name="input"></param>
        /// <param name="places"></param>
        /// <returns></returns>
        public static double RoundUp(double input, int places)
        {
            double multiplier = Math.Pow(10, Convert.ToDouble(places));
            return Math.Ceiling(input * multiplier) / multiplier;
        }
    }
}

