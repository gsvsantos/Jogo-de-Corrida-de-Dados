namespace Jogo_de_Corrida_de_Dados.Entities.Utils
{
    internal class ViewUtils
    {
        public static void Header(string message, ConsoleColor color = ConsoleColor.Green)
        {
            Paint($"------- {message} -------\n", color);
        }
        public static void Paint(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }
        public static void PressEnter(string type)
        {
            switch (type)
            {
                case "VOLTAR-MENU":
                    Paint("\n>> Pressione [enter] para voltar ao menu.", ConsoleColor.DarkYellow);
                    Console.ReadKey();
                    break;
                case "CONTINUAR":
                    Paint("\n>> Pressione [enter] para continuar.", ConsoleColor.DarkYellow);
                    Console.ReadKey();
                    break;
                case "TENTAR-NOVAMENTE":
                    Paint("\n>> Pressione [enter] para tentar novamente.", ConsoleColor.DarkYellow);
                    Console.ReadKey();
                    break;
                case "JOGAR-DADOS":
                    Paint("\n>> Pressione [enter] para jogar os dados.", ConsoleColor.White);
                    Console.ReadKey();
                    break;
                case "PROXIMO-ROUND":
                    Paint("\n>> Pressione [enter] para ir a proxima rodada.", ConsoleColor.DarkYellow);
                    Console.ReadKey();
                    break;
                case "PROXIMO-JOGADOR":
                    Paint("\n>> Pressione [enter] para o proximo jogador.", ConsoleColor.DarkYellow);
                    Console.ReadKey();
                    break;
                case "ENCERRAR-PARTIDA":
                    Paint("\n>> Pressione [enter] para encerrar partida.", ConsoleColor.DarkYellow);
                    Console.ReadKey();
                    break;
            }
        }
    }
}
