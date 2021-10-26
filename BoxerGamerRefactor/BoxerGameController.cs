namespace BoxerGamerRefactor
{
    public interface IBoxerGameController
    {
        void AttackBoxer(Boxer player1, Boxer player2, BoxerAttack boxerAttack);
        void RegenBoxer(Boxer boxer);
    }

    public class BoxerGameController : IBoxerGameController
    {
        public BoxerGameController()
        {
        }

        public void AttackBoxer(Boxer player1, Boxer player2, BoxerAttack boxerAttack)
        {
            var player1DamageResult = player1.Damage(boxerAttack.Player2Modifier);
            var player2DamageResult = player2.Damage(boxerAttack.Player1Modifier);

            ScreenViews.ShowBoxerHitMessage(player1, player2, player2DamageResult);
            ScreenViews.ShowBoxerHitMessage(player2, player1, player1DamageResult);
            ScreenViews.AddEmptyLine();

            RegenBoxer(player1);
            RegenBoxer(player2);
        }

        public void RegenBoxer(Boxer boxer)
        {
            var boxerRegen = boxer.Regen();
            System.Console.WriteLine($"{boxer.Name} Regains some of his stamina back and recives {boxerRegen.RegenAmount} health, now {boxer.Name} has {boxerRegen.NewHealth} health");
        }
    }
}