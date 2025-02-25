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
                ViewUtils.Header("Boas vindas ao Jogo de Corrida com Dados!");
                ViewUtils.Paint("\n1 - Iniciar jogo", ConsoleColor.White);
                ViewUtils.Paint("\nS - Sair", ConsoleColor.White);
                ViewUtils.Paint("\n\nOpção: ", ConsoleColor.White);
                string option = Console.ReadLine().Replace("s", "S");
                switch (option)
                {
                    case "1":
                        Console.Clear();
                        RacingGame.MainMenu();
                        break;
                    case "S":
                        menu = false;
                        Console.Clear();
                        ViewUtils.Paint("Adeus (T_T)/\n", ConsoleColor.White);
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
