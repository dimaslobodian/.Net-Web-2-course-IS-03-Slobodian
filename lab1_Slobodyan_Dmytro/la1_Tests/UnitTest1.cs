using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.IO;
using static Lab1.Program;

namespace la1_Tests
{
    public class Tests
    {
        public LinkedList<int> list_int;
        public LinkedList<string> list_string;
        public Node<int> node;

        [SetUp]
        public void Setup()
        {
            list_int = new LinkedList<int>();
            list_string = new LinkedList<string>();
            node = new Node<int>(1);
        }

        [Test]
        public void NodeToString_ReturnsNodeData()
        {
            NUnit.Framework.Assert.AreEqual("1",node.ToString());
        }

        [Test]
        public void AddNewElementToBlankIntList_ShouldAddNewElementToList()
        {
            int newElementData = 1;
            list_int.AddNew(newElementData);
            NUnit.Framework.Assert.AreEqual(1, list_int.Count);
        }

        [Test]
        public void AddNewElementToBlankIntList_ShouldInvokeEvent()
        {
            int newElementData = 1;
            var wasCalled = false;

            list_int.AddNewElementToList += (o,e) => wasCalled = true;

            list_int.AddNew(newElementData);

            NUnit.Framework.Assert.IsTrue(wasCalled);
        }

        [Test]
        public void AddNewElementTotListHandler_ShouldWriteToConsole()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            AddedNewElementToList(1, null);


            NUnit.Framework.Assert.AreEqual(stringWriter.ToString(), "Element 1 was created\r\n");
        }


        [Test]
        public void AddNewElement_ThrowsNullReferenceException()
        {
            list_int = null;
            var testData = 1;
            NUnit.Framework.Assert.Throws<NullReferenceException>(() => list_int.AddNew(testData));
        }

        [Test]
        public void AddNewElement_ThrowsArgumentException()
        {
            list_int = null;
            var testData = 1;
            NUnit.Framework.Assert.Throws<NullReferenceException>(() => list_int.AddNew(testData));
        }



        [Test]
        public void FindElementByIndex_ShouldReturnValue()
        {
            int newElementData = 1;
            int index = 0;
            list_int.AddNew(newElementData);

            var result = list_int.FindElementByIndex(index);

            NUnit.Framework.Assert.AreEqual(newElementData, result.Data);
        }

        [Test]
        public void FindElementByIndex_ShouldReturnValueOfTail()
        {
            int index = 2;
            int[] insertData = new int[] { 1, 2, 3 };
            foreach (int item in insertData)
            {
                list_int.AddNew(item);
            }

            var result = list_int.FindElementByIndex(index);

            NUnit.Framework.Assert.AreEqual(3, result.Data);
        }

        [Test]
        public void FindElementByIndex_ShouldReturnNull()
        {
            int index = -1;

            var result = list_int.FindElementByIndex(index);

            NUnit.Framework.Assert.IsTrue(result is null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void FindElementByIndex_ShouldThrowArgumentException()
        {
            int index = -1;

            var result = list_int.FindElementByIndex(index);
        }

        [Test]
        [ExpectedException(typeof(Exception),
            "There is no data in that list!")]
        public void FindElementByIndex_ShouldThrowException()
        {
            int index = 0;

            var result = list_int.FindElementByIndex(index);
        }

        [Test]
        public void GetAll_ShouldPrintAllElementsData()
        {
            int[] insertData = new int[] { 1, 2, 3 };
            foreach (int item in insertData)
            {
                list_int.AddNew(item);
            }
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);


            list_int.GetAll();

            NUnit.Framework.Assert.AreEqual(stringWriter.ToString(), "________\r\n1\r\n2\r\n3\r\n________\n\r\n");
        }

        [Test]
        public void GetAll_ShouldThrowExceptionAndWriteToConsole()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            list_int.GetAll();

            NUnit.Framework.Assert.AreEqual(stringWriter.ToString(), "There is an empty list!\r\n");
        }

        [Test]
        public void RemoveElementFrom_CountShouldBeReducedByOne()
        {
            int newElementData = 1;
            int expectedCount = 0;
            list_int.AddNew(newElementData);

            list_int.RemoveFromTheBeginning();

            NUnit.Framework.Assert.AreEqual(expectedCount, list_int.Count);
        }

        [Test]
        public void RemoveElementFrom_ShouldInvokeEvent()
        {
            int newElementData = 1;
            var wasCalled = false;
            list_int.AddNew(newElementData);

            list_int.RemoveElementFromList += (o, e) => wasCalled = true;

            list_int.RemoveFromTheBeginning();

            NUnit.Framework.Assert.IsTrue(wasCalled);
        }

        [Test]
        public void RemoveElemntFromListHandler_ShouldWriteToConsole()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            RemovedElementFromList(1, null);

            NUnit.Framework.Assert.AreEqual(stringWriter.ToString(), "Element 1 was removed\r\n");
        }

        [Test]
        [ExpectedException(typeof(Exception),
    "There is an empty list!")]
        public void RemoveElemntFromListHandler_ShouldThrowException()
        {
            list_int.RemoveFromTheBeginning();
        }

        [Test]
        public void AddNewElementToBlankStringList_ShouldAddNewElementToList()
        {
            string newElementData = "some line";
            list_string.AddNew(newElementData);
            NUnit.Framework.Assert.AreEqual(1, list_string.Count);
        }

    }
}