var result = BubbleSort.Sort([4,3,78,2,0,2]);
PrintList(result);
result = BubbleSort.Sort([3, 2, 9, 6, 5]);
PrintList(result);


static void PrintList<T>(List<T> list)
{
  Console.Write("[ ");
  bool first = true;

  foreach (T item in list)
  {
    if (!first)
    {
      Console.Write(", ");
    }
    Console.Write(item);
    first = false;
  }
  Console.WriteLine(" ]");
}
