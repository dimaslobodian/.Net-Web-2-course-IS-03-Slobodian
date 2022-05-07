
namespace Lab1
{
    public class Program
    {
        public class Node<T>
        {
            public T Data { get; set; }
            public Node<T> Next { get; set; }

            public Node(T data)
            {
                Data = data;
                Next = null;
            }
            public override string ToString()
            {
                return Data.ToString();
            }
        }

        public class LinkedList<T>
        {
            public event EventHandler AddNewElementToList;
            public event EventHandler RemoveElementFromList;
            private Node<T> Tail { get; set; }
            public int Count { get; set; }  
            public LinkedList()
            {
                Tail = null;
                Count = 0;
            }

            public void AddNew(T data)
            {
                try
                {
                    Node<T> node = new(data);
                    if (Count == 0)
                    {
                        Tail = node;
                        Tail.Next = Tail;
                    }
                    else
                    {
                        node.Next = Tail.Next;
                        Tail.Next = node;
                        Tail = node;
                    }
                    Count++;
                    AddNewElementToList?.Invoke(node, EventArgs.Empty);
                }
                catch (NullReferenceException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            public void RemoveFromTheBeginning()
            {
                try
                {
                    if(Count == 0)
                    {
                        throw new Exception("There is an empty list!");
                    }
                    var temp = Tail.Next;
                    Tail.Next = Tail.Next.Next;
                    Count--;
                    RemoveElementFromList?.Invoke(temp, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            public Node<T> FindElementByIndex(int index)
            {
                Node<T> runner;
                _ = Count == 0 ? runner = null : runner = Tail.Next;
                int runningIndex = 0;
                try
                {
                    if (index < 0 || index + 1 > Count)
                        throw new ArgumentException("Invalid index!", index.ToString());
                    else if(Count == 0)
                        throw new Exception("There is no data in that list!");
                    else if (index+1 == Count)
                        return Tail;
                    do
                    {
                        if(runningIndex == index)
                            return runner;
                        runningIndex++;
                        runner = runner.Next;
                    } while (runner != Tail.Next);
                    return runner;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
            public void GetAll()
            {
                Node<T> runner;
                _ = Count == 0 ? runner = null : runner = Tail.Next;
                try
                {
                    if (Count == 0)
                        throw new Exception("There is an empty list!");
                    Console.WriteLine("________");
                    do
                    {
                        Console.WriteLine(runner.Data);
                        runner = runner.Next;
                    }
                    while (runner != Tail.Next);
                    Console.WriteLine("________\n");

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public static void AddedNewElementToList(object sender, EventArgs e)
        {
            Console.WriteLine("Element {0} was created", sender.ToString());
        }
        public static void RemovedElementFromList(object sender, EventArgs e)
        {
            Console.WriteLine("Element {0} was removed", sender.ToString());
        }

        public static void Main()
        {
            LinkedList<int> list = new();

            int[] insertData = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

            list.AddNewElementToList += AddedNewElementToList; // subscribe on event of adding
            list.RemoveElementFromList += RemovedElementFromList; // subscribe on event of removing

            list.RemoveFromTheBeginning(); // should throw exception, `cause its an empty list

            for (int i = 0; i < 3; i++)
            {
                list.AddNew(i + 1); // add new data
            }

            list.GetAll(); // show that data

            list.RemoveFromTheBeginning();  //remove one node  
            list.RemoveFromTheBeginning();  //remove second node
            list.RemoveFromTheBeginning();  //remove last node


            list.GetAll();  // should throw exception, `cause its an empty list


            insertData.ToList().ForEach(x => list.AddNew(x)); // insert more data

            list.GetAll();
            Console.WriteLine();
            Console.WriteLine(list.FindElementByIndex(5).Data); // find element by index 5, should print '6' 


        }
    }
}