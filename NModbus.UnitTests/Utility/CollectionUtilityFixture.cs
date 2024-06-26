﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NModbus.Data;
using NModbus.UnitTests.Message;
using NModbus.Unme.Common;


namespace NModbus.UnitTests.Utility
{
    public class CollectionUtilityFixture
    {
        [Test()]
        public void SliceMiddle()
        {
            byte[] test = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Assert.AreEqual(new byte[] { 3, 4, 5, 6, 7 }, test.Slice(2, 5).ToArray());
        }

        [Test()]
        public void SliceBeginning()
        {
            byte[] test = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Assert.AreEqual(new byte[] { 1, 2 }, test.Slice(0, 2).ToArray());
        }

        [Test()]
        public void SliceEnd()
        {
            byte[] test = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Assert.AreEqual(new byte[] { 9, 10 }, test.Slice(8, 2).ToArray());
        }

        [Test()]
        public void SliceCollection()
        {
            Collection<bool> col = new Collection<bool>(new bool[] { true, false, false, false, true, true });
            Assert.AreEqual(new bool[] { false, false, true }, col.Slice(2, 3).ToArray());
        }

        [Test()]
        public void SliceReadOnlyCollection()
        {
            ReadOnlyCollection<bool> col =
                new ReadOnlyCollection<bool>(new bool[] { true, false, false, false, true, true });
            Assert.AreEqual(new bool[] { false, false, true }, col.Slice(2, 3).ToArray());
        }

        [Test()]
        public void SliceNullICollection()
        {
            ICollection<bool> col = null;
            Assert.Throws<ArgumentNullException>(() => col.Slice(1, 1).ToArray());
        }

        [Test()]
        public void SliceNullArray()
        {
            bool[] array = null;
            Assert.Throws<ArgumentNullException>(() => array.Slice(1, 1).ToArray());
        }

        [Test()]
        public void CreateDefaultCollectionNegativeSize()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => MessageUtility.CreateDefaultCollection<RegisterCollection, ushort>(0, -1));
        }

        [Test()]
        public void CreateDefaultCollection()
        {
            RegisterCollection col = MessageUtility.CreateDefaultCollection<RegisterCollection, ushort>(3, 5);
            Assert.AreEqual(5, col.Count);
            Assert.AreEqual(new ushort[] { 3, 3, 3, 3, 3 }, col.ToArray());
        }
    }
}