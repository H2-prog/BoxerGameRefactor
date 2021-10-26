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
        private IBoxerGameAIHandler AIHandler { get; set; }

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
            AIHandler = ServiceProvider.GetService<IBoxerGameAIHandler>();
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

                for (int attack = 0; attack < MAX_ATTACKS; attack++)
                {
                    RenderBoxerStats(round, attack);

                    if (GameController.HasRoundEnded(_player, _computer))
                    {
                        break;
                    }

                    GameController.Attack(_playersTurn, _attacks, _player, _computer);
                    Thread.Sleep(SECONDS_BETWEEN_ATTACK * 1000);
                }

                RenderRoundWinner();
                Thread.Sleep(SECONDS_BETWEEN_ATTACK * 1000);
            }

            RenderMatchWinner();

            /*
            if (GameController.HasRoundEnded(_boxingSimmatches, _rounds))
            {
                var res = ScreenViews.ReadString("Do you want to restart the game? (Y/n)");
                if(res.ToLower() == "y")
                {
                    ScreenViews.Clear();
                    Start();
                }
                else
                {
                    ScreenViews.AddText("Game done! Waiting 2 seconds to quit the game...");
                    Thread.Sleep(2 * 1000);
                    Exit();
                }
            }

            while (!GameController.HasRoundEnded(_boxingSimmatches, _rounds) && !(_player1.Dead || _player2.Dead))
            {
                ScreenViews.AddEmptyLine();
                ScreenViews.AddSeperator();

                if (_player1.Dead || _player2.Dead)
                {
                    _player1.Health = 200;
                    _player2.Health = 110;

                    // Wait for 10 seconds...
                    ScreenViews.GetReadyForNextRound(_boxingSimmatches);
                    Thread.Sleep(10 * 1000);
                }
                ScreenViews.AddEmptyLine();

                for (_currentRound = 0; _currentRound < MAX_ROUNDS; _currentRound++)
                {
                    if (_player1.Dead || _player2.Dead)
                    {
                        break;
                    }

                    for (int i = 0; i < MAX_ATTACKS; i++)
                    {
                        if (_player1.Dead || _player2.Dead)
                        {
                            break;
                        }

                        var consoleInput = ScreenViews.ShowAndChooseAttack(_attacks);
                        var attack = (_attacks.Where(x => x.Key == consoleInput.Key)).FirstOrDefault();
                        if (attack == null)
                        {
                            switch (consoleInput.Key)
                            {
                                case ConsoleKey.Escape: Exit(); break;
                                default: ScreenViews.AddText("It was not the you were told to press!"); break;
                            }
                            continue;
                        }
                        GameController.AttackBoxer(_player1, _player2, attack);
                    }

                    if (_player2.Dead)
                    {
                        ScreenViews.AddEmptyLine();
                        ScreenViews.AddText($"{_player1.Name} knocked down {_player2.Name} number of knock downs {++_player1.Victories}");
                    }
                    else if (_player1.Dead)
                    {
                        ScreenViews.AddEmptyLine();
                        ScreenViews.AddText($"{_player2.Name} knocked down {_player1.Name} number of knock downs {++_player2.Victories}");
                    }

                    if (_player1.Dead || _player2.Dead)
                    {
                        ScreenViews.AddEmptyLine();
                        ScreenViews.AddText($"{_player1.Name} Has won {_player1.Victories} Times");
                        ScreenViews.AddText($"{_player2.Name} Has won {_player2.Victories} Times");
                        ScreenViews.AddEmptyLine();
                        ScreenViews.AddText("############################## We have a Winner ##################################");
                        _currentRound++;
                    }

                    if (_player1.Victories > _player2.Victories || _player1.Health > _player2.Health)
                    {
                        ScreenViews.AddText($"{_player1.Name} is the winner of the round!");
                    }
                    else
                    {
                        ScreenViews.AddText($"{_player2.Name} is the winner of the round!");
                    }
                }
                _boxingSimmatches++;
                ScreenViews.AddSeperator();
            }
            */
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