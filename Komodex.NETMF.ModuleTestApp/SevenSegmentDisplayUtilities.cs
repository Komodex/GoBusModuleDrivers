using System;
using Microsoft.SPOT;

namespace Komodex.NETMF
{
    // This class contains some sample code for use with the Seven Segment Display Module.
    // Feel free to modify this code or include it in your own programs.

    public static class SevenSegmentDisplayUtilities
    {
        #region Temperature Display

        public static void SetTemperatureDisplay(this SevenSegmentDisplay display, float temperature, Digit unit, bool showDecimal = true)
        {
            // Check input
            if (temperature > 999 || temperature < -99)
                throw new ArgumentOutOfRangeException("temperature");

            bool isNegative = (temperature < 0);

            // Determine if we should show a decimal place
            bool isDecimal = false;
            if (showDecimal && temperature > -10 && temperature < 100)
            {
                temperature *= 10;
                isDecimal = true;
            }

            // Get the value to be displayed
            int value = System.Math.Abs((int)temperature);

            // Get the Digit values for each number
            Digit d1, d2, d3;
            d3 = SevenSegmentDisplay.GetDigit(value % 10);
            value /= 10;
            d2 = SevenSegmentDisplay.GetDigit(value % 10);
            value /= 10;
            d1 = SevenSegmentDisplay.GetDigit(value % 10);

            // Add the decimal point if necessary
            if (isDecimal)
                d2 |= Digit.Decimal;

            // Add the negative sign if necessary and clear leading zeros
            if (isNegative)
            {
                d1 = Digit.Dash;
                if (d2 == Digit.D0)
                    d2 = Digit.Blank;
            }
            else if (d1 == Digit.D0)
            {
                d1 = Digit.Blank;
                if (d2 == Digit.D0)
                    d2 = Digit.Blank;
            }

            // Send the value to the display
            display.SetValue(d1, d2, d3, unit);

            // Turn the apostrophe on (so it looks like a degree symbol) and make sure the colon is turned off
            display.SetApostrophe(true);
            display.SetColon(false);
        }

        #endregion

    }
}
