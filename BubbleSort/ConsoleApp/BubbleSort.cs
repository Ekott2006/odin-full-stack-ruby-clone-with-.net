// # puts "#{bubble_sort([4,3,78,2,0,2])}"


internal static class BubbleSort
{
    public static List<T> Sort<T>(List<T> array) where T: IComparable
    {
        for (int i = 0; i < array.Count - 1; i++)
        {
            for (int j = i + 1; j < array.Count; j++)
            {
                if (array[j].CompareTo(array[i]) >= 0)
                {
                    continue;
                }

                (array[i], array[j]) = (array[j], array[i]);
            }
        }
        return array;
    }
}

// # puts "#{bubble_sort([4,3,78,2,0,2])}"
// # puts "#{bubble_sort([3, 2, 9, 6, 5])}"
