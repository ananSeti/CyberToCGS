using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace DbOnlinetest
{
    [TestClass]
    public class DateTimeTEST
    {
        [TestMethod]
        public void TestMethod1()
        {
            string dateString, format;
            DateTime result;
            CultureInfo provider = CultureInfo.InvariantCulture;

            // Parse date-only value with invariant culture.
            dateString = "06/15/2008";
            format = "d";
            try
            {
                result = DateTime.ParseExact (dateString, format, provider);
                Console.WriteLine("{0} converts to {1}.", dateString, result.ToString());
            }
            catch (FormatException)
            {
                Console.WriteLine("{0} is not in the correct format.", dateString);
            }

            dateString = "2560-06-18";
            format = "YYYY-mm-dd";
            try
            {
                result = DateTime.ParseExact(dateString, format, provider);
                Console.WriteLine("{0} converts to {1}.", dateString, result.ToString());
            }
            catch (FormatException)
            {
                Console.WriteLine("{0} is not in the correct format.", dateString);
            }

        }
    }
}
