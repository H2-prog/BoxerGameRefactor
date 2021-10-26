using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Diagnostics;

namespace BoxerGamerRefactor
{
    public class BoxerGame : ConsoleGame
    {
        const int SECONDS_BETWEEN_ATTACK = 2;
        const int SECONDS_BETWEEN_ROUND = 5;
        const int MAX_ATTACKS = 10;

        private IBoxerGameController GameController { get; set; }
        private IBoxerGameRenderer Renderer { get; set; }
        private IBoxerGameInputHandler Input { get; set; }

        private Boxer _player;
        private Boxer _computer;
        private int _rounds;
        private bool _playersTurn = true;

        private List<BoxerAttack> _attacks = new List<BoxerAttack>
        {
            new BoxerAttack("normal Attack", ConsoleKey.W, ConsoleColor.Red, 4, 1),
            new BoxerAttack("power Attack", ConsoleKey.D, ConsoleColor.Yellow, 12, 1),
            new BoxerAttack("super duper power killing Attack", ConsoleKey.F, ConsoleColor.Green, 60, 250),
        };

        public override void Setup()
        {
            GameController = ServiceProvider.GetService<IBoxerGameController>();
            Renderer = ServiceProvider.GetService<IBoxerGameRenderer>();
            Input = ServiceProvider.GetService<IBoxerGameInputHandler>();
        }

        public override void Start()
        {
            InitializeBoxers();
            _player.Name = Input.ReadStringWithText("Enter your name");
            _rounds = Input.ReadIntWithText("How many rounds do you want to fight");
        }

        public override void Update()
        {
            for (int round = 1; round <= _rounds; round++)
            {
                if (GameController.HasRoundEnded(_player, _computer))
                {
                    _player.Health = _player.StartHealth;
                    _computer.Health = _computer.StartHealth;

                    // Wait for 10 seconds...
                    Renderer.RenderText($"================= Round {round} has ended get ready for the next round =================", 2, 26);
                    Thread.Sleep(10 * 1000);
                }
                Renderer.AddEmptyLine();

                int attack1 = 0;
                while (attack1 < MAX_ATTACKS && !GameController.HasRoundEnded(_player, _computer))
                {
                    //for (int attack = 0; attack < MAX_ATTACKS; attack++)
                    //{
                        RenderBoxerStats(round, attack1);

                        if (GameController.HasRoundEnded(_player, _computer))
                        {
                            break;
                        }

                        GameController.Attack(_playersTurn, _attacks, _player, _computer);
                        Thread.Sleep(SECONDS_BETWEEN_ATTACK * 1000);
                    //}
                    attack1++;
                }

                RenderRoundWinner();
                Thread.Sleep(SECONDS_BETWEEN_ROUND * 1000);
            }

            RenderMatchWinner();
        }

        private void RenderRoundWinner()
        {
            Renderer.AddEmptyLine();
            Renderer.RenderText("Round over!", 2, 12);
            Renderer.RenderText($"{_player.Name} has {_player.Health} left", 2, 13);
            Renderer.RenderText($"{_computer.Name} has {_computer.Health} left", 2, 14);
            Renderer.ClearLine(15);

            if (_player.Health > _computer.Health || _computer.Knockedout)
            {
                Renderer.RenderText($"{_player.Name} wins the round!", 2, 16);
                Renderer.RenderWinnerCharacter(11, 6, ConsoleColor.Blue);
            }
            else if (_computer.Health > _player.Health || _player.Knockedout)
            {
                Renderer.RenderText($"{_computer.Name} wins the round!", 2, 16);
                Renderer.RenderWinnerCharacter(19, 6, ConsoleColor.Red);
            }
            else
            {
                Renderer.RenderText("The round is a draw!", 2, 16);
            }
        }

        private void RenderMatchWinner()
        {
            Renderer.AddEmptyLine();
            Renderer.RenderText("Match over!", 2, 12);
            Renderer.RenderText($"{_player.Name} Has won {_player.Victories} Times", 2, 13);
            Renderer.RenderText($"{_computer.Name} Has won {_computer.Victories} Times", 2, 14);
            Renderer.ClearLine(15);

            if (_player.Victories > _computer.Victories)
            {
                Renderer.RenderText($"{_player.Name} is the winner!", 2, 16);
                Renderer.RenderWinnerCharacter(15, 6, ConsoleColor.Blue);
            }
            else if (_computer.Victories > _player.Victories)
            {
                Renderer.RenderText($"{_computer.Name} is the winner!", 2, 16);
                Renderer.RenderWinnerCharacter(15, 6, ConsoleColor.Red);
            }
            else
            {
                Renderer.RenderText("The match is a draw!", 2, 16);
            }
        }

        private void RenderBoxerStats(int round, int attack)
        {
            Renderer.Clear();
            Renderer.RenderBoxerStats(_player, 2, 1);
            Renderer.RenderBoxerCharacter(13, 6);
            Renderer.RenderBoxerStats(_computer, 18, 1);
            Renderer.RenderBoxerCharacter(17, 6);

            RenderRoundAndAttack(round, attack);
            RenderTurnByAttack(attack);
        }

        private void RenderRoundAndAttack(int round, int attack)
        {
            Renderer.RenderText($"Round {round}, attack {attack}", 2, 9);
        }

        private void RenderTurnByAttack(int attack)
        {
            _playersTurn = attack % 2 == 0;
            Boxer attacker = _playersTurn ? _player : _computer;
            Renderer.RenderText($"{attacker.Name}s turn", 2, 10);
        }

        private void InitializeBoxers()
        {
            _player = new Boxer
            {
                StartHealth = 200,
                StartStamina = 10,
                Strength = 5
            };
            _player.Health = _player.StartHealth;
            _player.Stamina = _player.StartStamina;

            _computer = new Boxer
            {
                Name = "Computer",
                StartHealth = 110,
                StartStamina = 9,
                Strength = 15
            };
            _computer.Health = _computer.StartHealth;
            _computer.Stamina = _computer.StartStamina;
        }
    }
}