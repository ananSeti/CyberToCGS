using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DbOnlinetest
{
    [TestClass]
    public class StringFormat :ICustomFormatter
    {
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            //throw new NotImplementedException();
            return string.Format("->{0} {1,-10}<-", "Hello", "World");
        }

        public class StringPadderFormatProvider : IFormatProvider
        {
            public object GetFormat(Type formatType)
            {
                //throw new NotImplementedException();
                if (formatType == typeof(ICustomFormatter))
                    return new StringPadder();

                return null;
            }
            public static readonly IFormatProvider Default =
            new StringPadderFormatProvider();
        }
        [TestMethod]
        public void ShouldPadLeftThenRight()
        {
            //string result = string.Format(new PaddedStringFormatInfo(), "->{0:10:L} {1:-10:R}<-", "Hello", "World");
            //string expected = "->LLLLLHello WorldRRRRR<-";
            //Assert.AreEqual(result, expected);
            // string result =  string.Format(StringPadderFormatProvider.Default, "->{0:x20}<-", "Hello");
            char pad = '0';
            string a = "4";
            string b = a.PadLeft(3,pad);
        }
        [TestMethod]
        public void subString() {

            string s = "25650512";
            string y = s.Substring(0,4);
            int c = Convert.ToInt32(y)-543;
            string final = s.Replace(y, c.ToString());
        }
    }

    internal class StringPadder : ICustomFormatter
    {
        public StringPadder()
        {
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {

            //throw new NotImplementedException();
            return string.Format("->{0} {1,-10}<-", "Hello", "World");
        }
}


}
