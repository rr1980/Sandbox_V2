using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Extensions
{
    public static class AssertH
    {
        public static void DoesNotThrow(Func<object> p)
        {
            try
            {
                p();
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }

        public static void DoesNotThrow(Action p)
        {
            try
            {
                p();
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }
    }
}
