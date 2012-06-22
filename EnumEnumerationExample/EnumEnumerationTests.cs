using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnumEnumerationExample
{
    public enum TestEnum
    {
        Value1,
        Value2,
        Value3,
        Value4
    };

    [TestClass]
    public class EnumEnumerationTests
    {
        public TestContext TestContext { get; set; }

        private static readonly IDictionary<TestEnum, string> EnumToString =
            new Dictionary<TestEnum, string>
                {
                    {TestEnum.Value1, "Value one"},
                    {TestEnum.Value2, "Value two"},
                    {TestEnum.Value3, "Value three"},
                    {TestEnum.Value4, "Value four"}
                };

        static EnumEnumerationTests()
        {
            if (!Enum.GetValues(typeof (TestEnum))
                     .Cast<TestEnum>()
                     .All(type => EnumToString.ContainsKey(type)))
                throw new InvalidOperationException(
                    "Enum value missing from enumToString dictionary");
        }

        [TestMethod]
        public void ShowEnumValues()
        {
            Enum.GetValues(typeof (TestEnum))
                .Cast<TestEnum>()
                .ToList()
                .ForEach(e => Console.WriteLine(e));
        }

        [TestMethod]
        public void ShowEnumStrings()
        {
            Enum.GetValues(typeof(TestEnum))
                .Cast<TestEnum>()
                .ToList()
                .ForEach(e => Console.WriteLine(EnumToString[e]));
        }

        [TestMethod]
        public void EnsureAllEnumValuesAreInDictionary()
        {
            Assert.IsTrue(
                Enum.GetValues(typeof(TestEnum))
                    .Cast<TestEnum>()
                    .All(type => EnumToString.ContainsKey(type)),
                "Enum value missing from enumToString dictionary");
        }

        [TestMethod]
        public void EnsureAllEnumValuesAreInDictionary1()
        {
            Assert.IsTrue(
                GetEnumValues<TestEnum>().All(type => EnumToString.ContainsKey(type)),
                "Enum value missing from enumToString dictionary");
        }

        private static IEnumerable<T> GetEnumValues<T>()
        {
            return Enum.GetValues(typeof (T)).Cast<T>();
        }
    }
}
