using Jogo_de_Corrida_de_Dados.Entities.Utils;

namespace Jogo_de_Corrida_de_Dados.Entities
{
    internal class RacingGame
    {
        public static Player player = new Player();
        public static List<CPU> cpu = new List<CPU>();
        public static List<CPU> winners = new List<CPU>();
        public static List<MatchHistory> matchHistory = new List<MatchHistory>();
        public static int trackLimit = 30;

        public static void MainMenu()
        {
            bool menu = true;
            do
            {
                if (cpu.Count == 0)
                {
                    cpu.Add(new CPU(1));
                }

                Console.Clear();
                ViewUtils.Header("Menu Principal");
                ViewUtils.Paint("\n1 - Iniciar Partida", ConsoleColor.White);
                ViewUtils.Paint("\n2 - Histórico de Partidas", ConsoleColor.White);
                ViewUtils.Paint("\n3 - Configurações", ConsoleColor.White);
                ViewUtils.Paint("\n4 - Como Jogar", ConsoleColor.White);
                ViewUtils.Paint("\nS - Voltar à Tela Inicial", ConsoleColor.White);
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
                        GameStart();
                        break;
                    case "2":
                        MatchHistory();
                        break;
                    case "3":
                        Console.Clear();
                        GameConfiguration();
                        break;
                    case "4":
                        Console.Clear();
                        GameAbout();
                        break;
                    case "S":
                        matchHistory.Clear();
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
            bool hasAWinner = false;
            int cpuCount = 0;
            int turnCount = 0;
            string winner = "";

            do
            {
                Console.Clear();
                turnCount++;
                do
                {
                    Console.Clear();
                    ViewUtils.Header($"RODADA NÚMERO #{turnCount}");
                    Console.WriteLine();
                    ViewUtils.Header("Vez do Jogador", ConsoleColor.Blue);
                    ViewUtils.PressEnter("JOGAR-DADO");
                    player.RollDice();
                    PlayerLuckyTest(player.Position, player.Value, player);
                    Console.WriteLine();
                    ViewUtils.PressEnter("PROXIMO-JOGADOR");
                    playerTurn = false;
                } while (playerTurn);
                do
                {
                    for (int i = 0; i < cpu.Count; i++)
                    {
                        Console.Clear();
                        ViewUtils.Header($"RODADA NÚMERO #{turnCount}");
                        Console.WriteLine();
                        ViewUtils.Header($"Vez do CPU #{cpu[i].cpuID}", ConsoleColor.Magenta);
                        cpu[i].RollDice();
                        CPULuckyTest(cpu[i].Position, cpu[i].Value, cpu[i]);
                        Console.WriteLine();

                        if (cpu[i].Position >= trackLimit || player.Position >= trackLimit)
                        {
                            hasAWinner = true;
                        }
                        if (i < cpu.Count - 1)
                        {
                            ViewUtils.PressEnter("PROXIMO-JOGADOR");
                            cpuCount++;
                            continue;
                        }
                    }
                    if (hasAWinner == true)
                    {
                        ViewUtils.PressEnter("ENCERRAR-PARTIDA");
                        MatchResult(ref ongoing, turnCount, ref winner);
                        cpuTurn = false;
                        break;
                    }
                    else
                    {
                        ViewUtils.PressEnter("PROXIMO-ROUND");
                    }
                    cpuTurn = false;
                } while (cpuTurn);
            } while (ongoing);

            cpu.Clear();
            winners.Clear();
            trackLimit = 30;
            player.Position = 0;
            matchHistory.Add(new MatchHistory(winner, turnCount, cpuCount));
        }
        public static void PlayerLuckyTest(int pos, int value, Player player)
        {
            if (value == 6 && player.Position < trackLimit)
            {
                    ViewUtils.Paint("\n\nSorte grande! Rodada extra:\n", ConsoleColor.DarkGreen);
                    player.RollDice();
            }
            switch (player.Position)
            {
                case 4:
                    ViewUtils.Paint("\n\nAcho um skate e andou nele por +2 casas!\n", ConsoleColor.DarkGreen);
                    player.Position += 2;
                    ViewUtils.Paint($"Pulou para a posição: {player.Position}", ConsoleColor.White);
                    break;
                case 11:
                    ViewUtils.Paint("\n\nPegou carona e avançou +4 casas =D\n", ConsoleColor.DarkGreen);
                    player.Position += 4;
                    ViewUtils.Paint($"Pulou para a posição: {player.Position}", ConsoleColor.White);
                    break;
                case 18:
                    ViewUtils.Paint("\n\nCarregou um checkpoint sorteado e avançou +3 casas =)\n", ConsoleColor.DarkGreen);
                    player.Position += 3;
                    ViewUtils.Paint($"Pulou para a posição: {player.Position}", ConsoleColor.White);
                    break;
            }
            switch (player.Position)
            {
                case 7:
                    ViewUtils.Paint("\n\nQue azar, você escorregou na banana! Volte 2 casas =/\n", ConsoleColor.DarkRed);
                    player.Position -= 2;
                    ViewUtils.Paint($"Voltou para a posição: {player.Position}", ConsoleColor.White);
                    break;
                case 13:
                    ViewUtils.Paint("\n\nVoce caiu em um buraco e voltou 4 casas =/\n", ConsoleColor.DarkRed);
                    player.Position -= 4;
                    ViewUtils.Paint($"Voltou para a posição: {player.Position}", ConsoleColor.White);
                    break;
                case 27:
                    ViewUtils.Paint("\n\nCarregou o checkpoint sem querer e voltou 7 casas =(\n", ConsoleColor.DarkRed);
                    player.Position -= 7;
                    ViewUtils.Paint($"Voltou para a posição: {player.Position}", ConsoleColor.White);
                    break;
            }
        }
        public static void CPULuckyTest(int pos, int value, CPU cpu)
        {
            if (value == 6&& cpu.Position < trackLimit)
            {
                ViewUtils.Paint("\n\nSorte grande! Rodada extra:\n", ConsoleColor.DarkGreen);
                cpu.RollDice();
            }
            switch (cpu.Position)
            {
                case 4:
                    ViewUtils.Paint("\n\nAcho um skate e andou nele por +2 casas!\n", ConsoleColor.DarkGreen);
                    cpu.Position += 2;
                    ViewUtils.Paint($"Pulou para a posição: {cpu.Position}", ConsoleColor.White);
                    break;
                case 11:
                    ViewUtils.Paint("\n\nPegou carona e avançou +4 casas =D\n", ConsoleColor.DarkGreen);
                    cpu.Position += 4;
                    ViewUtils.Paint($"Pulou para a posição: {cpu.Position}", ConsoleColor.White);
                    break;
                case 18:
                    ViewUtils.Paint("\n\nCarregou um checkpoint sorteado e avançou +3 casas =)\n", ConsoleColor.DarkGreen);
                    cpu.Position += 3;
                    ViewUtils.Paint($"Pulou para a posição: {cpu.Position}", ConsoleColor.White);
                    break;
            }
            switch (cpu.Position)
            {
                case 7:
                    ViewUtils.Paint("\n\nQue azar, você escorregou na banana! Volte 2 casas =/\n", ConsoleColor.DarkRed);
                    cpu.Position -= 2;
                    ViewUtils.Paint($"Voltou para a posição: {cpu.Position}", ConsoleColor.White);
                    break;
                case 13:
                    ViewUtils.Paint("\n\nVoce caiu em um buraco e voltou 4 casas =/\n", ConsoleColor.DarkRed);
                    cpu.Position -= 4;
                    ViewUtils.Paint($"Voltou para a posição: {cpu.Position}", ConsoleColor.White);
                    break;
                case 27:
                    ViewUtils.Paint("\n\nCarregou o checkpoint sem querer e voltou 7 casas =(\n", ConsoleColor.DarkRed);
                    cpu.Position -= 7;
                    ViewUtils.Paint($"Voltou para a posição: {cpu.Position}", ConsoleColor.White);
                    break;
            }
        }
        public static void MatchResult(ref bool ongoing, int turnCount, ref string winner)
        {
            bool drawVerify;
            bool wasADraw = false;
            bool loop = true;
            int maxPosition = 0;

            do
            {
                if (cpu.Count == 1)
                {
                    foreach (CPU c in cpu)
                    {
                        maxPosition = Math.Max(maxPosition, c.Position);
                        maxPosition = Math.Max(maxPosition, player.Position);

                        if (player.Position > c.Position && player.Position >= trackLimit)
                        {
                            Console.Clear();
                            ViewUtils.Header("RESULTADO");
                            ViewUtils.Paint($"\nApós longas {turnCount} rodadas e com a maior posição final ({player.Position})...\nO vencedor é o Jogador!!!\n", ConsoleColor.Cyan);
                            winner = "Jogador";
                            ongoing = false;
                            ViewUtils.PressEnter("VOLTAR-MENU");
                            break;
                        }
                        if (c.Position > player.Position && c.Position >= trackLimit)
                        {
                            Console.Clear();
                            ViewUtils.Header("RESULTADO");
                            ViewUtils.Paint($"\nApós longas {turnCount} rodadas e com a maior posição final ({c.Position})...\nO vencedor é o CPU #{c.cpuID}!!!\n", ConsoleColor.Cyan);
                            winner = $"CPU #{c.cpuID}";
                            ongoing = false;
                            ViewUtils.PressEnter("VOLTAR-MENU");
                            break;
                        }
                        if (c.Position == player.Position && c.Position >= trackLimit && player.Position >= trackLimit)
                        {
                            winners.Add(c);
                            wasADraw = true;
                        }
                    }
                }
                if (cpu.Count > 1)
                {
                    do
                    {
                        foreach (CPU c in cpu)
                        {
                            maxPosition = Math.Max(maxPosition, c.Position);
                        }
                        maxPosition = Math.Max(maxPosition, player.Position);

                        for (int i = 0; i < cpu.Count; i++)
                        {
                            for (int j = i + 1; j < cpu.Count; j++)
                            {
                                if (cpu[i].Position == cpu[j].Position && cpu[i].Position == maxPosition)
                                {
                                    if (!winners.Contains(cpu[i]))
                                    {
                                        winners.Add(cpu[i]);
                                    }
                                    if (!winners.Contains(cpu[j]))
                                    {
                                        winners.Add(cpu[j]);
                                    }
                                    if (maxPosition >= trackLimit)
                                    {
                                        wasADraw = true;
                                    }
                                }
                            }
                            if (player.Position == cpu[i].Position && player.Position == maxPosition)
                            {
                                if (!winners.Contains(cpu[i]))
                                {
                                    winners.Add(cpu[i]);
                                }
                                if (maxPosition >= trackLimit)
                                {
                                    wasADraw = true;
                                }
                            }
                        }
                        drawVerify = false;
                    } while (drawVerify);
                    if (wasADraw == false)
                    {
                        do
                        {
                            for (int i = 0; i < cpu.Count; i++)
                            {
                                for (int j = i + 1; j < cpu.Count; j++)
                                {
                                    if (cpu[i].Position > cpu[j].Position && cpu[i].Position > player.Position && cpu[i].Position == maxPosition && maxPosition >= trackLimit)
                                    {
                                        Console.Clear();
                                        ViewUtils.Header("RESULTADO");
                                        ViewUtils.Paint($"\nApós longas {turnCount} rodadas e com a maior posição final ({cpu[i].Position})...\nO vencedor é o CPU #{cpu[i].cpuID}!!!", ConsoleColor.Cyan);
                                        winner = $"CPU #{cpu[i].cpuID}";
                                        ongoing = false;
                                        ViewUtils.PressEnter("VOLTAR-MENU");
                                        return;
                                    }
                                    if (cpu[j].Position > cpu[i].Position && cpu[j].Position > player.Position && cpu[j].Position == maxPosition && maxPosition >= trackLimit)
                                    {
                                        Console.Clear();
                                        ViewUtils.Header("RESULTADO");
                                        ViewUtils.Paint($"\nApós longas {turnCount} rodadas e com a maior posição final ({cpu[j].Position})...\nO vencedor é o CPU #{cpu[j].cpuID}!!!\n", ConsoleColor.Cyan);
                                        winner = $"CPU #{cpu[j].cpuID}";
                                        ongoing = false;
                                        ViewUtils.PressEnter("VOLTAR-MENU");
                                        return;
                                    }
                                    if (player.Position > cpu[i].Position && player.Position >= maxPosition && maxPosition >= trackLimit)
                                    {
                                        Console.Clear();
                                        ViewUtils.Header("RESULTADO");
                                        ViewUtils.Paint($"\nApós longas {turnCount} rodadas e com a maior posição final ({player.Position})...\nO vencedor é o Jogador!!!\n", ConsoleColor.Cyan);
                                        winner = "Jogador";
                                        ongoing = false;
                                        ViewUtils.PressEnter("VOLTAR-MENU");
                                        return;
                                    }
                                }
                            }
                        } while (loop);
                    }
                }
                if (wasADraw == true)
                {
                    winner = "Empate entre: ";
                    Console.Clear();
                    ViewUtils.Header("RESULTADO");
                    ViewUtils.Paint($"\nApós {turnCount} rodadas intensas...\nA partida EMPATOU!!!\n", ConsoleColor.Cyan);
                    ViewUtils.Paint($"\nVencedores com a maior distância ({maxPosition}):\n", ConsoleColor.Cyan);
                    if (player.Position == maxPosition)
                    {
                        ViewUtils.Paint($"| Jogador ", ConsoleColor.White);
                        winner += "| Jogador ";
                    }
                    foreach (CPU w in winners)
                    {
                        ViewUtils.Paint($"| CPU #{w.cpuID} |", ConsoleColor.White);
                        winner += $"| CPU #{w.cpuID} |";
                    }
                    Console.WriteLine();
                    ViewUtils.PressEnter("VOLTAR-MENU");
                    ongoing = false;
                }
                break;
            } while (true);
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
                ViewUtils.Paint($"\nVencedor: {m.Winner}\n", ConsoleColor.White);
            }

            ViewUtils.PressEnter("VOLTAR-MENU");
        }
        public static void GameConfiguration()
        {
            bool menu = true;
            do
            {
                Console.Clear();
                ViewUtils.Header("Configurações");
                ViewUtils.Paint("\n1 - Ajustar Número de CPUs", ConsoleColor.White);
                ViewUtils.Paint("\n2 - Alterar o Tamanho da Pista", ConsoleColor.White);
                ViewUtils.Paint("\nS - Voltar ao Menu Principal", ConsoleColor.White);
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
                        ViewUtils.Header($"Configuração de CPUs");
                        ViewUtils.Paint($"\nAtualmente: {cpu.Count} CPUs", ConsoleColor.White);
                        int quantity = Auxiliary.IntVerify("\nEscolha quantos CPUs deseja enfrentar (1 a 10): ", "\nIsso não é um valor válido...\n", "\nPrecisa ser um valor positivo...!\n", $"\nEsse número é muito grande... (Máximo aceitável: 10)\n", 1, 10);
                        cpu.Clear();
                        for (int i = 1; i <= quantity; i++)
                        {
                            cpu.Add(new CPU(i));
                        }
                        if (quantity > 1)
                        {
                            ViewUtils.Paint($"\nAgora você irá jogar contra {quantity} CPUs, boa sorte!\n", ConsoleColor.Yellow);
                        }
                        ViewUtils.PressEnter("CONTINUAR");
                        break;
                    case "2":
                        Console.Clear();
                        ViewUtils.Header($"Configuração da Pista");
                        ViewUtils.Paint($"\nTamanho atual da pista: {trackLimit} casas", ConsoleColor.White);
                        trackLimit = Auxiliary.IntVerify("\nEscolha um novo tamanho entre 10 e 250: ", "\nO limite precisa ser um número...\n", "\nO valor precisa ser maior que 10!\n", $"\nEsse número é muito grande... (Máximo aceitável: 250)\n", 10, 250);
                        if (trackLimit > 30)
                        {
                            ViewUtils.Paint("\nAtenção: As casas especiais só funcionam até a posição 30!\n", ConsoleColor.Yellow);
                        }
                        ViewUtils.PressEnter("CONTINUAR");
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
        public static void GameAbout()
        {
            Console.Clear();
            ViewUtils.Header("Como Jogar", ConsoleColor.DarkYellow);
            ViewUtils.Paint("\n1. Escolha iniciar uma nova partida no menu principal.", ConsoleColor.White);
            ViewUtils.Paint("\n2. O jogo começa com você e os CPUs (ajustável) na linha de partida.", ConsoleColor.White);
            ViewUtils.Paint("\n3. Na sua vez, o dado será jogado automaticamente para determinar quantas casas você avançará.", ConsoleColor.White);
            ViewUtils.Paint("\n4. As CPUs também jogam automaticamente.", ConsoleColor.White);
            ViewUtils.Paint("\n5. Algumas casas possuem eventos especiais.", ConsoleColor.White);
            ViewUtils.Paint("\n6. Continue através das rodadas até que alguém ultrapasse a linha de chegada.", ConsoleColor.White);
            ViewUtils.Paint("\n7. Se houver empate, ganhará quem estiver na maior distância, podendo acabar com um ou mais vencedores.\n", ConsoleColor.White);
            Console.WriteLine();
            ViewUtils.Header("Regras Especiais", ConsoleColor.DarkYellow);
            ViewUtils.Paint("\n- Se tirar 6 no dado, o dado irá rodar novamente.", ConsoleColor.White);
            ViewUtils.Paint("\n- Casas especiais podem ajudar (skate, carona, checkpoint) ou atrapalhar (buraco, banana, checkpoint).", ConsoleColor.White);
            ViewUtils.Paint("\n- O histórico das partidas é salvo, registrando vencedor(es) e número de rodadas.\n", ConsoleColor.White);

            ViewUtils.PressEnter("VOLTAR-MENU");
        }
    }
}