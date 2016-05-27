using System;
using System.Collections.Generic;
using System.Text;

namespace DevelopersSite.Helpers
{
    /* 
     * Please compare against the latest Java version at http://www.DaveKoelle.com
     * to see the most recent modifications 
     */
    public class AlphanumComparator : IComparer<string>
    {
        private enum ChunkType { Alphanumeric, Numeric };
        private bool InChunk(char ch, char otherCh)
        {
            ChunkType type = ChunkType.Alphanumeric;

            if (char.IsDigit(otherCh))
            {
                type = ChunkType.Numeric;
            }

            if ((type == ChunkType.Alphanumeric && char.IsDigit(ch))
                || (type == ChunkType.Numeric && !char.IsDigit(ch)))
            {
                return false;
            }

            return true;
        }

        public int Compare(string x, string y)
        {
            int thisMarker = 0, thisNumericChunk = 0;
            int thatMarker = 0, thatNumericChunk = 0;

            while ((thisMarker < x.Length) || (thatMarker < y.Length))
            {
                if (thisMarker >= x.Length)
                {
                    return -1;
                }
                else if (thatMarker >= y.Length)
                {
                    return 1;
                }
                char thisCh = x[thisMarker];
                char thatCh = y[thatMarker];

                StringBuilder thisChunk = new StringBuilder();
                StringBuilder thatChunk = new StringBuilder();

                while ((thisMarker < x.Length) && (thisChunk.Length == 0 || InChunk(thisCh, thisChunk[0])))
                {
                    thisChunk.Append(thisCh);
                    thisMarker++;

                    if (thisMarker < x.Length)
                    {
                        thisCh = x[thisMarker];
                    }
                }

                while ((thatMarker < y.Length) && (thatChunk.Length == 0 || InChunk(thatCh, thatChunk[0])))
                {
                    thatChunk.Append(thatCh);
                    thatMarker++;

                    if (thatMarker < y.Length)
                    {
                        thatCh = y[thatMarker];
                    }
                }

                int result = 0;
                // If both chunks contain numeric characters, sort them numerically
                if (char.IsDigit(thisChunk[0]) && char.IsDigit(thatChunk[0]))
                {
                    thisNumericChunk = Convert.ToInt32(thisChunk.ToString());
                    thatNumericChunk = Convert.ToInt32(thatChunk.ToString());

                    if (thisNumericChunk < thatNumericChunk)
                    {
                        result = -1;
                    }

                    if (thisNumericChunk > thatNumericChunk)
                    {
                        result = 1;
                    }
                }
                else
                {
                    result = string.Compare(thisChunk.ToString(), thatChunk.ToString(), StringComparison.CurrentCulture);
                }

                if (result != 0)
                {
                    return result;
                }
            }

            return 0;
        }
    }
}
