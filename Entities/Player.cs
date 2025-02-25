using Jogo_de_Corrida_de_Dados.Entities.Utils;

namespace Jogo_de_Corrida_de_Dados.Entities
{
    internal class Player : Entity
    {
        public override void RollDice()
        {
            base.RollDice();
            ViewUtils.Paint($"Dado caiu em: {Value}\n", ConsoleColor.White);
            ViewUtils.Paint($"Posição anterior: {Pos}\n", ConsoleColor.White);
            Pos += Value;
            ViewUtils.Paint($"Posição atual: {Pos}", ConsoleColor.White);
        }
    }
}
