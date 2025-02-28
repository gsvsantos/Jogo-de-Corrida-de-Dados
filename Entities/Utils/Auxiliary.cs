using Jogo_de_Corrida_de_Dados.Entities.Exceptions;
using System.Globalization;

namespace Jogo_de_Corrida_de_Dados.Entities.Utils
{
    internal class Auxiliary
    {
        public static int IntVerify(string prompt, string inputError, string belowMinValue, string aboveMaxValue, int minValue, int maxValue)
        {
            do
            {
                ViewUtils.Paint(prompt, ConsoleColor.White);
                string input = Console.ReadLine();

                if (input == null)
                {
                    ViewUtils.Paint($"\nAcho que isso não é um número...\n", ConsoleColor.Red);
                    continue;
                }
                try
                {

                    if (input.Trim().Contains(',') || input.Trim().Contains('.') || input.Trim().Contains(' '))
                    {
                        throw new AuxiliaryException("\nO valor não pode conter espaço ' ', ponto (.), ou vírgula (,)!\n");
                    }
                    int n = Convert.ToInt32(input, CultureInfo.InvariantCulture);

                    if (n < minValue)
                    {
                        throw new AuxiliaryException(belowMinValue);
                    }
                    if (n > maxValue)
                    {
                        throw new AuxiliaryException(aboveMaxValue);
                    }
                    return n;
                }
                catch (AuxiliaryException e)
                {
                    ViewUtils.Paint(e.Message, ConsoleColor.Red);
                    continue;
                }
                catch (OverflowException)
                {
                    ViewUtils.Paint(aboveMaxValue, ConsoleColor.Red);
                    continue;
                }
                catch (FormatException)
                {
                    ViewUtils.Paint(inputError, ConsoleColor.Red);
                    continue;
                }
                catch (Exception e)
                {
                    ViewUtils.Paint("Algo deu errado...", ConsoleColor.Red);
                    ViewUtils.Paint(e.Message, ConsoleColor.Red);
                    continue;
                }
            } while (true);
        }
    }
}