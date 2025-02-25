namespace Jogo_de_Corrida_de_Dados.Entities.Utils
{
    internal class Auxiliary
    {
        public static int IntVerify(string prompt, string inputError, string valueError, int minValue = 1, int maxValue = int.MaxValue)
        {
            int n;
            do
            {
                ViewUtils.Paint(prompt, ConsoleColor.White);
                if (!int.TryParse(Console.ReadLine(), out n))
                {
                    ViewUtils.Paint(inputError, ConsoleColor.White);
                    continue;
                }
                if (n < minValue || n > maxValue)
                {
                    ViewUtils.Paint(valueError, ConsoleColor.White);
                    continue;
                }
                return n;
            } while (true);
        }
    }
}
