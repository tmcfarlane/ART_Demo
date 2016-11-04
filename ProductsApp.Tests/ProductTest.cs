using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductsApp.Models;
using ProductsApp.Resources;

namespace ProductsApp.Tests
{
    [TestClass]
    public class ProductTest
    {
        [TestMethod]
        public void PrintTest()
        {
            PrintProperties(typeof(Product));
        }

        [TestMethod]
        public void GenerateDataTest()
        {
            var someData = SampleData.GeneratePropertyData(typeof(Product));
            PrintGeneratedData(typeof(Product), someData);
        }

        private void PrintGeneratedData(Type baseType, IEnumerable<KeyValuePair<Type, object>> someData)
        {
            Console.WriteLine("Base Type: {0}\n---------------\n", baseType);
            foreach (var data in someData)
            {
                var value =  data.Value as IEnumerable<KeyValuePair<Type, object>>;
                if (value != null)
                {
                    Console.WriteLine();
                    PrintGeneratedData(data.Key, value);
                }
                else
                {
                    Console.WriteLine(data);
                }
            }
        }

        private void PrintProperties(Type type)
        {
            var propertyInfo = type.GetProperties();
            StringBuilder buffer = new StringBuilder();
            buffer.AppendFormat("Model: {0}\n", type.Name);
            foreach (var propType in propertyInfo.Select(x => x.PropertyType))
            {
                buffer.AppendFormat("Type: {0}\n", propType);
                if (propType.GetInterfaces().Contains(typeof(IResource)))
                {
                    PrintProperties(propType);
                }
            }
            buffer.AppendLine("========================");
            Console.WriteLine(buffer.ToString());
        }

        

        public class SampleData
        {
            private static Random random = new Random();

            private const int numchars = 50;

            public static string RandomString()
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 ";
                return new string(Enumerable.Repeat(chars, numchars)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
            }

            public static int RandomInt()
            {
                return random.Next(int.MinValue, int.MaxValue);
            }

            public static decimal RandomDecimal()
            {
                return new decimal(RandomDouble());
            }

            public static double RandomDouble()
            {
                return random.NextDouble();
            }

            private static object GetPropertyData(Type propType)
            {
                if (propType.GetInterfaces().Contains(typeof(IResource)))
                {
                    return GeneratePropertyData(propType);
                }
                else if (propType == typeof(int))
                {
                    return RandomInt();
                }
                else if (propType == typeof(string))
                {
                    return RandomString();
                }
                else if (propType == typeof(decimal))
                {
                    return RandomDecimal();
                }
                else if (propType == typeof(double))
                {
                    return RandomDouble();
                }
                else
                {
                    throw new Exception($"Unexpected type found: {propType}");
                }
            }

            public static IEnumerable<KeyValuePair<Type, object>> GeneratePropertyData(Type type)
            {
                /* todo
                 * This is a hardcoded sample function to mimic the action of generating data.
                 * Here we should use a framework like https://github.com/garethdown44/nbuilder/ to generate test data.
                 * This will require a framework that supports defining test data for custom object types (including a Resource data type)
                 */

                var propTypeAndData = new List<KeyValuePair<Type, object>>();

                foreach (var propType in type.GetProperties().Select(x => x.PropertyType))
                {
                    propTypeAndData.Add(new KeyValuePair<Type, object>(propType, GetPropertyData(propType)));
                }

                return propTypeAndData;
            }
        }
    }
}