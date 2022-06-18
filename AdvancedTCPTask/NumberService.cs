namespace AdvancedTCPTask
{
    public static class NumberService
    {
        public static int GetNumber(int number)
        {
            bool gotValue = false;

            do
            {
                try
                {
                    var response = Tcp.Request($"{KeyModel.Key}|{number}\n");

                    if (response.Contains("Key has"))
                    {
                        KeyModel.RenewKey();
                        continue;
                    }
                    else
                    {
                        KeyModel.Changed = false;
                    }

                    var receivedNumber = Regex.Match(response, @"\d+").Value;

                    int result = 0;
                    if (int.TryParse(receivedNumber, out result) && response.EndsWith("\n"))
                    {
                        gotValue = true;
                        Console.WriteLine($"{number}: {receivedNumber} : {KeyModel.Key}");
                        return result;
                    }
                }
                catch (Exception e)
                {
                    KeyModel.Changed = false;
                    Console.WriteLine(e.Message);
                }
            } while (!gotValue);
            return 0;
        }

        public static double GetMedianNumbers(ConcurrentBag<double> receivedNumbers)
        {
            var sortedNumbers = receivedNumbers.OrderBy(p => p).ToList();

            int numberCount = sortedNumbers.Count();
            int halfIndex = sortedNumbers.Count() / 2;

            double median;
            if ((numberCount % 2) == 0)
            {
                median = ((sortedNumbers.ElementAt(halfIndex) +
                    sortedNumbers.ElementAt((halfIndex - 1))) / 2);
            }
            else
            {
                median = sortedNumbers.ElementAt(halfIndex);
            }

            Console.WriteLine($"Median: {median}");
            using (StreamWriter st = new StreamWriter("median.txt"))
            {
                st.WriteLine($"Median: {median}");
            }

            return median;
        }

        public static void CheckMedian(double median)
        {
            try
            {
                var response = Tcp.Request($"Check_Advanced {median}\n");

                Console.WriteLine($"Received Response: ");
                Console.WriteLine(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
