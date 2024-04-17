
namespace SolutionJson
{
    public static class CheckData
    {
        public static int CheckNumber()
        {
            int number;
            bool isValidInput = false;

            do
            {
                Console.WriteLine("Введите число от 1 до 8:");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out number))
                {
                    if (number >= 1 && number <= 8)
                    {
                        isValidInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Число не находится в диапазоне от 1 до 8");
                    }
                }
                else
                {
                    Console.WriteLine("Введенная строка не является числом");
                }
            }
            while (!isValidInput);

            return number;
        }

        public static string CheckDigits()
        {
            int number;
            bool isValidInput = false;

            do
            {
                Console.WriteLine("Введите число от 1 до 2:");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out number))
                {
                    if (number >= 1 && number <= 2)
                    {
                        isValidInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Число не находится в диапазоне от 1 до 2");
                    }
                }
                else
                {
                    Console.WriteLine("Введенная строка не является числом");
                }
            }
            while (!isValidInput);

            
            return number.ToString();
        }
    }
}
