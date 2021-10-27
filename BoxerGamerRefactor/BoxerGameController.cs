using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace BoxerGamerRefactor
{
    public interface IBoxerGameController
    {
        bool HasRoundEnded(Boxer player, Boxer computer);
        bool CheckIfBoxerIsKnockedOut(Boxer attacker, Boxer victim);
        void RegenBoxers(Boxer player, Boxer computer);
        void Attack(bool playersTurn, IEnumerable<BoxerAttack> attacks, Boxer player, Boxer computer);
    }

    public class BoxerGameController : IBoxerGameController
    {
        private IBoxerGameInputHandler InputHandler { get; }
        private IBoxerGameAIHandler AIHandler { get; }
        private IBoxerGameRenderer Renderer { get; }

        public BoxerGameController(IBoxerGameInputHandler inputHandler, IBoxerGameAIHandler aiHandler, IBoxerGameRenderer renderer)
        {
            InputHandler = inputHandler;
            AIHandler = aiHandler;
            Renderer = renderer;
        }

        public bool HasRoundEnded(Boxer player, Boxer computer)
        {
            return player.Knockedout || computer.Knockedout;
        }

        public void RegenBoxers(Boxer player, Boxer computer)
        {
            var d = player.Regen();
            d = computer.Regen();
        }

        public void Attack(bool playersTurn, IEnumerable<BoxerAttack> attacks, Boxer player, Boxer computer)
        {
            var message = "";
            if (playersTurn)
            {
                var choosenAttack = InputHandler.ChooseAttack(attacks);
                if (choosenAttack == null)
                {
                    message = "It was not the you were told to press!";
                }
                else 
                {
                    var damageResult = computer.Damage(choosenAttack.PlayerModifier);
                    message = $"{player.Name} Hits {computer.Name} for {damageResult.Damage}, {computer.Name} takes {damageResult.Damage} Damage, {computer.Name} now has {damageResult.NewHealth} health left";
                }
            }
            else
            {
                Thread.Sleep(500);
                var choosenAttack = AIHandler.ChooseRandomAttackIn(attacks);
                var damageResult = player.Damage(choosenAttack.ComputerModifier);
                message = $"{computer.Name} Hits {player.Name} for {damageResult.Damage}, {player.Name} takes {damageResult.Damage} Damage, {player.Name} now has {damageResult.NewHealth} health left";
            }
            Renderer.RenderText("====== Log ======", 2, 17);
            Renderer.RenderText(message, 2, 18);
        }

        public bool CheckIfBoxerIsKnockedOut(Boxer attacker, Boxer victim)
        {
            if (victim.Knockedout)
            {
                attacker.Victories++;
                Renderer.RenderText($"{attacker.Name} knocked down {victim.Name}. Number of knock downs {attacker.Victories}", 2, 20);
            }
            return victim.Knockedout;
        }
    }
}