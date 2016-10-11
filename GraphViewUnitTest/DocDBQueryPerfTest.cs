﻿using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphView;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GraphViewUnitTest
{
    [TestClass]
    public class DocDBQueryPerfTest
    {
        [TestMethod]
        public void query1()
        {
            GraphViewConnection connection = new GraphViewConnection("https://graphview.documents.azure.com:443/",
"MqQnw4xFu7zEiPSD+4lLKRBQEaQHZcKsjlHxXn2b96pE/XlJ8oePGhjnOofj1eLpUdsfYgEhzhejk2rjH/+EKA==",
"GroupMatch", "IOTTest");
            var expTimes = 100;
            var sumTime = 0.0;
            var result = new List<Double>();

            for (int i = 0; i < expTimes; i++)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                // Note: update as the random number
                string DeviceID = "25015";
                GraphTraversal g = new GraphTraversal(connection);
                var q = g.V()
                        .has("label", "DeviceModel")
                        .has("id", DeviceID)
                        .As("DeviceModel")
                        .@select("DeviceModel");
                
                foreach (var x in q)
                {

                }

                sw.Stop();
                sumTime += sw.Elapsed.TotalMilliseconds;
                result.Add(sw.Elapsed.TotalMilliseconds);
                Console.WriteLine("query{0} time is:{1}", i, sw.Elapsed.TotalMilliseconds);
            }

            Console.WriteLine("max query time is: {0}", result.Max());
            Console.WriteLine("min query time is: {0}", result.Min());
            Console.WriteLine("avg query time is: {0}", result.Average());
            Console.WriteLine("stdDev query time is: {0}", stdDev(result));
            Console.WriteLine("avg,max,min,stdDev");
            Console.WriteLine("{0}, {1}, {2}, {3}", result.Average(), result.Max(), result.Min(), stdDev(result));

        }
        [TestMethod]
        public void query2()
        {
            GraphViewConnection connection = new GraphViewConnection("https://graphview.documents.azure.com:443/",
"MqQnw4xFu7zEiPSD+4lLKRBQEaQHZcKsjlHxXn2b96pE/XlJ8oePGhjnOofj1eLpUdsfYgEhzhejk2rjH/+EKA==",
"GroupMatch", "IOTTest");
            var expTimes = 100;
            var sumTime = 0.0;
            var result = new List<Double>();

            for (int i = 0; i < expTimes; i++)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                // Note: update as the random number
                string DeviceID = "25015";
                GraphTraversal g = new GraphTraversal(connection);
                var q = g.V()
                    .has("label", "DeviceModel")
                    .has("id", DeviceID)
                    .As("DeviceModel")
                    .Out("type_of")
                    .has("label", "DeviceTwin")
                    .As("device")
                    .@select("device", "DeviceModel");
                
                foreach (var x in q)
                {

                }

                sw.Stop();
                sumTime += sw.Elapsed.TotalMilliseconds;
                result.Add(sw.Elapsed.TotalMilliseconds);
                Console.WriteLine("query{0} time is:{1}", i, sw.Elapsed.TotalMilliseconds);
            }

            var avgTime = sumTime / expTimes;
            Console.WriteLine("max query time is: {0}", result.Max());
            Console.WriteLine("min query time is: {0}", result.Min());
            Console.WriteLine("avg query time is: {0}", result.Average());
            Console.WriteLine("stdDev query time is: {0}", stdDev(result));
            Console.WriteLine("avg,max,min,stdDev");
            Console.WriteLine("{0}, {1}, {2}, {3}", result.Average(), result.Max(), result.Min(), stdDev(result));

        }

        public double stdDev(List<double> values)
        {
            double ret = 0;
            if (values.Count() > 0)
            {
                //Compute the Average      
                double avg = values.Average();
                //Perform the Sum of (value-avg)_2_2      
                double sum = values.Sum(d => Math.Pow(d - avg, 2));
                //Put it all together      
                ret = Math.Sqrt((sum) / (values.Count() - 1));
            }
            return ret;
        }
    }
}
