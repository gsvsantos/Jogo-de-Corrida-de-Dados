using Jogo_de_Corrida_de_Dados.Entities;
using Jogo_de_Corrida_de_Dados.Entities.Utils;

namespace Jogo_de_Corrida_de_Dados
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool menu = true;
            do
            {
                Console.Clear();
                ViewUtils.Header("Boas Vindas ao Jogo de Corrida com Dados");
                ViewUtils.Paint("\n1 - Iniciar Jogo", ConsoleColor.White);
                ViewUtils.Paint("\nS - Fechar Jogo", ConsoleColor.White);
                ViewUtils.Paint("\n\nOpção: ", ConsoleColor.White);
                string option = Console.ReadLine();
                if (option == null)
                {
                    ViewUtils.Paint($"Acho que isso não é uma opção...", ConsoleColor.Red);
                    ViewUtils.PressEnter("TENTAR-NOVAMENTE");
                    continue;
                }
                option = option.Replace("s", "S");
                switch (option)
                {
                    case "1":
                        Console.Clear();
                        RacingGame.MainMenu();
                        break;
                    case "S":
                        menu = false;
                        Console.Clear();
                        ViewUtils.Paint("Adeus (T_T)/\n\n", ConsoleColor.White);
                        break;
                    default:
                        ViewUtils.Paint($"Acho que '{option}' não é uma opção...", ConsoleColor.Red);
                        ViewUtils.PressEnter("TENTAR-NOVAMENTE");
                        break;
                }
            } while (menu);
        }
    }
}