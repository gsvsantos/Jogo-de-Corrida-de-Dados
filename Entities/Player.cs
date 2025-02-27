﻿using Jogo_de_Corrida_de_Dados.Entities.Utils;

namespace Jogo_de_Corrida_de_Dados.Entities
{
    internal class Player : Entity
    {
        public override void RollDice()
        {
            base.RollDice();
            ViewUtils.Paint($"Dado caiu em: {Value}\n", ConsoleColor.White);
            ViewUtils.Paint($"Posição anterior: {Position}\n", ConsoleColor.White);
            Position += Value;
            ViewUtils.Paint($"Posição atual: {Position}", ConsoleColor.White);
        }
    }
}
