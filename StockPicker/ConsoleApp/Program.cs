List<int> result = StockPicker.BestTradingDays([17,3,6,9,15,8,6,1,10]);
Console.WriteLine("Best Trading Pair ==> [{0}, {1}]", result[0], result[1]);


static class StockPicker
{
    public static List<int> BestTradingDays(List<int> list) {
        List<int> bestDays  = [0,1];
        int orginalValue = list[bestDays[1]] - list[bestDays[0]];
        int end = list.Count;
        for (int i = 1; i < end - 1 ; i++)
        {
            for (int j = i+1; j < end; j++)
            {
                int value = list[j] - list[i];
                if (orginalValue < value) {
                    bestDays = [i, j];
                    orginalValue = value;
                }
            }
        }
    return bestDays;
    }
}
