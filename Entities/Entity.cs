namespace Jogo_de_Corrida_de_Dados.Entities
{
    internal class Entity : Random
    {

        public int Position;
        public int Value { get; set; }

        public virtual void RollDice()
        {
            Value = Next(1, 7);
        }
    }
}
