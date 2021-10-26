namespace BoxerGamerRefactor
{
    public interface IBoxerGameController
    {
        bool HasRoundEnded(Boxer player, Boxer computer);

        void AttackBoxer(Boxer player1, Boxer player2, BoxerAttack boxerAttack);
        void RegenBoxer(Boxer boxer);
    }

    public class BoxerGameController : IBoxerGameController
    {
        private IBoxerGameRenderer Renderer { get; }

        public BoxerGameController(IBoxerGameRenderer renderer)
        {
            Renderer = renderer;
        }

        public bool HasRoundEnded(Boxer player, Boxer computer)
        {
            CheckIfBoxerIsKnockedOut(player, computer);
            CheckIfBoxerIsKnockedOut(computer, player);
            return player.Knockedout && computer.Knockedout;
        }

        public void AttackBoxer(Boxer player1, Boxer player2, BoxerAttack boxerAttack)
        {
            var player1DamageResult = player1.Damage(boxerAttack.ComputerModifier);
            var player2DamageResult = player2.Damage(boxerAttack.PlayerModifier);

            ScreenViews.ShowBoxerHitMessage(player1, player2, player2DamageResult);
            ScreenViews.ShowBoxerHitMessage(player2, player1, player1DamageResult);

            //RegenBoxer(player1);
            //RegenBoxer(player2);
        }

        public void RegenBoxer(Boxer boxer)
        {
            var boxerRegen = boxer.Regen();
            System.Console.WriteLine($"{boxer.Name} Regains some of his stamina back and recives {boxerRegen.RegenAmount} health, now {boxer.Name} has {boxerRegen.NewHealth} health");
        }

        private void CheckIfBoxerIsKnockedOut(Boxer attacker, Boxer victim)
        {
            if (victim.Health <= 0)
            {
                attacker.Victories++;
                Renderer.RenderText($"{attacker.Name} knocked down {victim.Name}. Number of knock downs {attacker.Victories}", 2, 20);
            }
        }
    }
}