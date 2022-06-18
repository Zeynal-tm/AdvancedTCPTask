namespace AdvancedTCPTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConcurrentBag<double> receivedNumbers = new ConcurrentBag<double>();
            KeyModel.Key = KeyModel.GetKey();

            Parallel.For(1, 2019, number =>
            {
                receivedNumbers.Add(NumberService.GetNumber(number));
            });

            var medianNumbers = NumberService.GetMedianNumbers(receivedNumbers);

            NumberService.CheckMedian(medianNumbers);

            Console.ReadKey();
        }
    }
}