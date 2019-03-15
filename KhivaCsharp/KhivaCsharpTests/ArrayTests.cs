﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using khiva.array;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace khiva.array.Tests
{
    [TestClass()]
    public class ArrayTests
    {
        [TestMethod()]
        public void TestDoubleNull()
        {
            double[] tss = null;
            long[] dims = { 1, 1, 1, 1 };
            try
            {
                new Array(tss, dims);
            }catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Null elems object provided");
            }
        }

        [TestMethod()]
        public void TestDoubleMismatchingDims()
        {
            double[] tss = {1, 2};
            long[] dims = { 1, 1, 1, 1 };
            try
            {
                new Array(tss, dims);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Mismatching dims and array size");
            }
        }

        [TestMethod()]
        public unsafe void TestDoubleOkDims()
        {
            double[] tss = { 1, 2 };
            long[] dims = { 1, 2 };
            try
            {
                Array arr = new Array(tss, dims);
                void* data = arr.GetData();
                IntPtr dataPtr = new IntPtr(data); // Search how to get double from IntPtr
                Assert.AreEqual(tss, dataPtr);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Mismatching dims and array size");
            }
        }
    }
}