using Jogo_de_Corrida_de_Dados.Entities.Utils;
using System.ComponentModel.Design;

namespace Jogo_de_Corrida_de_Dados.Entities
{
    internal class RacingGame
    {
        public static List<CPU> cpu = new List<CPU>();
        public static List<CPU> winners = new List<CPU>();
        public static List<MatchHistory> matchHistory = new List<MatchHistory>();
        public static int trackLimit = 30;
        public static int turnCount;

        public static void MainMenu()
        {
            bool menu = true;
            do
            {
                Console.Clear();
                ViewUtils.Header("Menu Principal");
                ViewUtils.Paint("\n1 - Iniciar partida", ConsoleColor.White);
                ViewUtils.Paint("\n2 - Histórico de partidas", ConsoleColor.White);
                ViewUtils.Paint("\n3 - Configurações", ConsoleColor.White);
                ViewUtils.Paint("\nS - Voltar", ConsoleColor.White);
                ViewUtils.Paint("\n\nOpção: ", ConsoleColor.White);
                string option = Console.ReadLine().Replace("s", "S");
                switch (option)
                {
                    case "1":
                        Console.Clear();
                        GameStart();
                        break;
                    case "2":
                        MatchHistory();
                        break;
                    case "3":
                        Console.Clear();
                        GameConfiguration();
                        break;
                    case "S":
                        menu = false;
                        Console.Clear();
                        break;
                    default:
                        ViewUtils.Paint($"Acho que '{option}' não é uma opção...", ConsoleColor.Red);
                        ViewUtils.PressEnter("TENTAR-NOVAMENTE");
                        break;
                }
            } while (menu);
        }
        public static void GameConfiguration()
        {
            bool menu = true;
            do
            {
                Console.Clear();
                ViewUtils.Header("Configurações");
                ViewUtils.Paint("\n1 - Adicionar CPUs", ConsoleColor.White);
                ViewUtils.Paint("\n2 - Definir limite da pista de corrida", ConsoleColor.White);
                ViewUtils.Paint("\nS - Voltar", ConsoleColor.White);
                ViewUtils.Paint("\n\nOpção: ", ConsoleColor.White);
                string option = Console.ReadLine().Replace("s", "S");
                switch (option)
                {
                    case "1":
                        Console.Clear();
                        ViewUtils.Header("Adicionar CPU (padrão 1)");
                        int quantity = Auxiliary.IntVerify("\nDeseja jogar contra quantos CPUs? ", "Isso não é um número..", "Precisa ser maior que 0!");
                        cpu.Clear();
                        for (int i = 1; i <= quantity; i++)
                        {
                            cpu.Add(new CPU(i));
                        }
                        cpu.Capacity = quantity;
                        break;
                    case "2":
                        Console.Clear();
                        ViewUtils.Header("Definir limite da pista de corrida (padrão 30)");
                        trackLimit = Auxiliary.IntVerify("\nQual o novo limite? ", "O limite precisa de um número...", "O valor precisa ser positivo!");
                        break;
                    case "S":
                        menu = false;
                        Console.Clear();
                        break;
                    default:
                        ViewUtils.Paint($"Acho que '{option}' não é uma opção...", ConsoleColor.Red);
                        ViewUtils.PressEnter("TENTAR-NOVAMENTE");
                        break;
                }
            } while (menu);
        }
        public static void GameStart()
        {
            bool ongoing = true;
            bool playerTurn = true;
            bool cpuTurn = true;
            bool isADraw = false;
            int cpuCount = 0;
            int turnCount = 0;
            string winner = "";
            Player player = new Player();

            do
            {
                turnCount++;
                Console.SetCursorPosition(0, 0);
                Console.Clear();
                do
                {
                    Console.Clear();
                    ViewUtils.Header($"RODADA NÚMERO #{turnCount}");
                    Console.WriteLine();
                    ViewUtils.Header("Vez do Jogador", ConsoleColor.Blue);
                    player.RollDice();
                    PlayerLuckyTest(player.Pos, player.Value, player);
                    Console.WriteLine();
                    ViewUtils.PressEnter("PROXIMO-JOGADOR");
                    playerTurn = false;
                } while (playerTurn);
                do
                {
                    if (cpu.Count == 0)
                    {
                        cpu.Add(new CPU(1));
                        cpu.Capacity = 1;
                    }
                    for (int i = 1; i <= cpu.Capacity; i++)
                    {
                        Console.Clear();
                        ViewUtils.Header($"RODADA NÚMERO #{turnCount}");
                        Console.WriteLine();
                        ViewUtils.Header($"Vez do CPU #{i}", ConsoleColor.Magenta);
                        cpu[--i].RollDice();
                        CPULuckyTest(cpu[i].Pos, cpu[i].Value, cpu[i]);
                        Console.WriteLine();
                        ++i;
                        if (i < cpu.Capacity)
                        {
                            ViewUtils.PressEnter("PROXIMO-JOGADOR");
                            continue;
                        }
                        for (int n = 1; n <= cpu.Capacity; n++)
                        {

                            if (cpu[--n].Pos >= trackLimit || player.Pos >= trackLimit)
                            {
                                ++n;
                                ViewUtils.PressEnter("ENCERRAR-PARTIDA");
                                break;
                            }
                            else
                            { 
                                ViewUtils.PressEnter("PROXIMO-ROUND");
                                break;
                            }
                        }
                        cpuCount = i;
                    }
                    cpuTurn = false;
                } while (cpuTurn);
                MatchResult(ref ongoing, isADraw, turnCount, ref winner, player);
            } while (ongoing);
            cpu.Clear();
            winners.Clear();
            trackLimit = 30;
            matchHistory.Add(new MatchHistory(winner, turnCount, cpuCount));
        }
        public static void MatchResult(ref bool ongoing, bool isADraw, int turnCount, ref string winner, Player player)
        {
            foreach (CPU c in cpu)
            {
                if (c.Pos >= trackLimit)
                {
                    winners.Add(c);
                }
                if (winners.Count >= 1 && player.Pos >= trackLimit)
                {
                    isADraw = true;
                }
                else if (winners.Count > 1)
                {
                    isADraw = true;
                }
            }
            if (isADraw == true)
            {
                winner = "Empate entre > ";
                Console.Clear();
                ViewUtils.Header("RESULTADO");
                ViewUtils.Paint($"\nApós longas {turnCount} rodadas...\nA partida EMPATOU!!!\n", ConsoleColor.Cyan);
                ViewUtils.Paint($"\nVencedores: ", ConsoleColor.Cyan);
                if (player.Pos >= trackLimit)
                {
                    ViewUtils.Paint($"Jogador", ConsoleColor.White);
                    winner += "Jogador";
                }
                foreach (CPU w in winners)
                {
                    ViewUtils.Paint($" - CPU #{w.cpuID}", ConsoleColor.White);
                    winner += $" - CPU #{w.cpuID}";
                }
                Console.WriteLine();
                ViewUtils.PressEnter("VOLTAR-MENU");
                ongoing = false;
            }
            if (isADraw == false)
            {
                do
                {
                    foreach (CPU c in cpu)
                    {
                        if (c.Pos >= trackLimit)
                        {
                            Console.Clear();
                            ViewUtils.Header("RESULTADO");
                            ViewUtils.Paint($"\nApós longas {turnCount} rodadas...\nO vencedor é o CPU #{c.cpuID}!!!\n", ConsoleColor.Cyan);
                            winner = $"CPU #{c.cpuID}";
                            ViewUtils.PressEnter("VOLTAR-MENU");
                            ongoing = false;
                            break;
                        }
                    }
                    if (player.Pos >= trackLimit)
                    {
                        Console.Clear();
                        ViewUtils.Header("RESULTADO");
                        ViewUtils.Paint($"\nApós longas {turnCount} rodadas...\nO vencedor é o Jogador!!!\n", ConsoleColor.Cyan);
                        winner = "Jogador";
                        ViewUtils.PressEnter("VOLTAR-MENU");
                        player.Pos = 0;
                        ongoing = false;
                        break;
                    }
                } while (isADraw);
            }
        }
        public static void MatchHistory()
        {
            Console.Clear();
            ViewUtils.Header("Histórico de Partidas");
            if (matchHistory.Count == 0)
            {
                ViewUtils.Paint("\nNenhuma partida encontrada.\n", ConsoleColor.Red);
                ViewUtils.PressEnter("VOLTAR-MENU");
                return;
            }
            foreach (MatchHistory m in matchHistory)
            {
                Console.WriteLine();
                ViewUtils.Paint($"\nPartida #{m.MatchId}", ConsoleColor.White);
                ViewUtils.Paint($"\nNúmero de jogadores: {++m.CPUCount}", ConsoleColor.White);
                ViewUtils.Paint($"\nNúmero de rodadas: {m.Matches}", ConsoleColor.White);
                ViewUtils.Paint($"\nVencedor: {m.Winner}", ConsoleColor.White);
            }
            Console.WriteLine();
            ViewUtils.PressEnter("VOLTAR-MENU");
        }
        public static void PlayerLuckyTest(int pos, int value, Player player)
        {
            if (value == 6)
            {
                ViewUtils.Paint("\n\nSorte grande! Rodada extra:\n", ConsoleColor.DarkGreen);
                player.RollDice();
            }
            switch (player.Pos)
            {
                case 4:
                    ViewUtils.Paint("\n\nAcho um skate e andou nele por +2 casas!\n", ConsoleColor.DarkGreen);
                    player.Pos += 2;
                    ViewUtils.Paint($"Pulou para a posição: {player.Pos}", ConsoleColor.White);
                    break;
                case 11:
                    ViewUtils.Paint("\n\nPegou carona e avançou +4 casas =D\n", ConsoleColor.DarkGreen);
                    player.Pos += 4;
                    ViewUtils.Paint($"Pulou para a posição: {player.Pos}", ConsoleColor.White);
                    break;
                case 18:
                    ViewUtils.Paint("\n\nCarregou um checkpoint sorteado e avançou +3 casas =)\n", ConsoleColor.DarkGreen);
                    player.Pos += 3;
                    ViewUtils.Paint($"Pulou para a posição: {player.Pos}", ConsoleColor.White);
                    break;
            }
            switch (player.Pos)
            {
                case 7:
                    ViewUtils.Paint("\n\nQue azar, você escorregou na banana! Volte 2 casas =/\n", ConsoleColor.DarkRed);
                    player.Pos -= 2;
                    ViewUtils.Paint($"Voltou para a posição: {player.Pos}", ConsoleColor.White);
                    break;
                case 13:
                    ViewUtils.Paint("\n\nVoce caiu em um buraco e voltou 4 casas =/\n", ConsoleColor.DarkRed);
                    player.Pos -= 4;
                    ViewUtils.Paint($"Voltou para a posição: {player.Pos}", ConsoleColor.White);
                    break;
                case 27:
                    ViewUtils.Paint("\n\nCarregou o checkpoint sem querer e voltou 7 casas =(\n", ConsoleColor.DarkRed);
                    player.Pos -= 7;
                    ViewUtils.Paint($"Voltou para a posição: {player.Pos}", ConsoleColor.White);
                    break;
            }
        }
        public static void CPULuckyTest(int pos, int value, CPU cpu)
        {
            if (value == 6)
            {
                ViewUtils.Paint("\n\nSorte grande! Rodada extra:\n", ConsoleColor.DarkGreen);
                cpu.RollDice();
            }
            switch (cpu.Pos)
            {
                case 4:
                    ViewUtils.Paint("\n\nAcho um skate e andou nele por +2 casas!\n", ConsoleColor.DarkGreen);
                    cpu.Pos += 2;
                    ViewUtils.Paint($"Pulou para a posição: {cpu.Pos}", ConsoleColor.White);
                    break;
                case 11:
                    ViewUtils.Paint("\n\nPegou carona e avançou +4 casas =D\n", ConsoleColor.DarkGreen);
                    cpu.Pos += 4;
                    ViewUtils.Paint($"Pulou para a posição: {cpu.Pos}", ConsoleColor.White);
                    break;
                case 18:
                    ViewUtils.Paint("\n\nCarregou um checkpoint sorteado e avançou +3 casas =)\n", ConsoleColor.DarkGreen);
                    cpu.Pos += 3;
                    ViewUtils.Paint($"Pulou para a posição: {cpu.Pos}", ConsoleColor.White);
                    break;
            }
            switch (cpu.Pos)
            {
                case 7:
                    ViewUtils.Paint("\n\nQue azar, você escorregou na banana! Volte 2 casas =/\n", ConsoleColor.DarkRed);
                    cpu.Pos -= 2;
                    ViewUtils.Paint($"Voltou para a posição: {cpu.Pos}", ConsoleColor.White);
                    break;
                case 13:
                    ViewUtils.Paint("\n\nVoce caiu em um buraco e voltou 4 casas =/\n", ConsoleColor.DarkRed);
                    cpu.Pos -= 4;
                    ViewUtils.Paint($"Voltou para a posição: {cpu.Pos}", ConsoleColor.White);
                    break;
                case 27:
                    ViewUtils.Paint("\n\nCarregou o checkpoint sem querer e voltou 7 casas =(\n", ConsoleColor.DarkRed);
                    cpu.Pos -= 7;
                    ViewUtils.Paint($"Voltou para a posição: {cpu.Pos}", ConsoleColor.White);
                    break;
            }
        }
    }
}
