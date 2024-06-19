using Deli.Entities;
using Deli.Interface;

namespace Deli.Utils;

public class RandomNumberGeneratorNumber
{
    private static readonly HashSet<long> generatedNumbers = new();
    private static readonly Random random = new();

    public static long GenerateUnique6DigitNumber(IRepositoryWrapper repositoryWrapper)
    {
        long min = 1000;
        long max = 9999;

        long fourDigitNumber;
        long uniqueNumber;
        Order order;
        do
        {
            fourDigitNumber = (long)(random.NextDouble() * (max - min) + min);
            int dayOfYear = DateTime.Now.DayOfYear;
            uniqueNumber = long.Parse($"{fourDigitNumber}{dayOfYear}");

            order = repositoryWrapper.Order.Get(t => t.OrderNumber == uniqueNumber).Result;
        } while (generatedNumbers.Contains(fourDigitNumber) || order != null);

        generatedNumbers.Add(fourDigitNumber);

        return uniqueNumber;
    }
}