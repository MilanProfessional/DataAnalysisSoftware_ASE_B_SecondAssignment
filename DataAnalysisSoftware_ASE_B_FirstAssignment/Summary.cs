﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAnalysisSoftware_ASE_B_FirstAssignment
{
    class Summary
    {
        public static int FindMax(List<string> value)
        {
            int maxValue = 0;

            for (int i = 0; i < value.Count; i++)
            {
                maxValue = (maxValue > Convert.ToInt16(value.ElementAt(i))) ? maxValue : Convert.ToInt16(value.ElementAt(i));
            }

            return maxValue;
        }

        public static int FindMin(List<string> value)
        {
            int minValue = Convert.ToInt16(value.ElementAt(0));

            for (int i = 0; i < value.Count; i++)
            {
                minValue = (minValue > Convert.ToInt16(value.ElementAt(i))) ? Convert.ToInt16(value.ElementAt(i)) : minValue;
            }

            return minValue;
        }

        public static double FindAverage(List<string> value)
        {
            int average = 0;

            foreach (var data in value)
            {
                average += Convert.ToInt16(data);
            }

            return average / value.Count;
        }

        public static int FindSum(List<string> list)
        {
            int sum = 0;

            foreach (var data in list)
            {
                sum += Convert.ToInt16(data);
            }

            return sum;
        }
    }
}
