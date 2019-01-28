using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAnalysisSoftware_ASE_B_FirstAssignment
{
    public class FileConvertor
    {
        /// <summary>
        /// Split text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string[] SplitString(string text)
        {
            var splitString = GetParams();

            var splittedText = text.Split(splitString, StringSplitOptions.RemoveEmptyEntries);

            return splittedText;
        }

        /// <summary>
        /// Splitting data by enter
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string[] SplitStringByEnter(string text)
        {
            return text.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Split text by space
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string[] SplitStringBySpace(string text)
        {
            var formattedText = string.Join(" ", text.Split().Where(x => x != ""));
            return formattedText.Split(' ');
        }

        public string[] GetParams()
        {
            return new string[] { "[Params]", "[Note]", "[IntTimes]", "[IntNotes]",
                "[ExtraData]", "[LapNames]", "[Summary-123]",
                "[Summary-TH]", "[HRZones]", "[SwapTimes]", "[Trip]", "[HRData]"};
        }
    }
}
