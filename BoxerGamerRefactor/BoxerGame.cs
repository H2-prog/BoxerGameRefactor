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
        private IBoxerGameController GameController { get; set; }
        private IBoxerGameRenderer Renderer { get; set; }
        private IBoxerGameInputHandler Input { get; set; }
        private IBoxerGameAIHandler AIHandler { get; set; }

        private Boxer _player;
        private Boxer _computer;
        private int _rounds;
        private bool _playersTurn = true;
        //private int _boxingSimmatches = 0;4
        //private int _currentRound = 0;

        const int SECONDS_BETWEEN_ATTACK = 1;
        const int MAX_ATTACKS = 10;

        private List<BoxerAttack> _attacks = new List<BoxerAttack>
        {
            new BoxerAttack("normal Attack", ConsoleKey.W, 4, 1),
            new BoxerAttack("power Attack", ConsoleKey.D, 12, 1),
            new BoxerAttack("super duper power killing Attack", ConsoleKey.F, 60, 250),
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
            _player.Name = Input.ReadString("Enter your name");
            _rounds = Input.ReadInt("How many rounds do you want to fight");
        }

        public override void Update()
        {
            for (int round = 0; round < _rounds; round++)
            {
                if (_player.Knockedout || _computer.Knockedout)
                {
                    _player.Health = 200;
                    _computer.Health = 110;

                    // Wait for 10 seconds...
                    ScreenViews.GetReadyForNextRound(round);
                    Thread.Sleep(10 * 1000);
                }
                ScreenViews.AddEmptyLine();

                for (int attack = 0; attack < MAX_ATTACKS; attack++)
                {
                    RenderBoxerStats(round, attack);

                    if(GameController.HasRoundEnded(_player, _computer, Renderer))
                    {
                        break;
                    }

                    if (_playersTurn)
                    {
                        var choosenAttack = Input.ChooseAttack(_attacks, out ConsoleKey key);
                        if(key == ConsoleKey.Escape)
                        {
                            Exit();
                            return;
                        }
                        _computer.Damage(choosenAttack.PlayerModifier);
                        //GameController.AttackBoxer(_player, _computer, choosenAttack);
                        Trace.WriteLine($"Player: {_player.Health}/{_player.StartHealth} ({Utils.CalculatePercent(_player.Health, _player.StartHealth)}%)");
                        Trace.WriteLine($"Computer: {_computer.Health}/{_computer.StartHealth} ({Utils.CalculatePercent(_computer.Health, _computer.StartHealth)}%)");
                        Thread.Sleep(SECONDS_BETWEEN_ATTACK * 1000);
                    }
                    else
                    {
                        var choosenAttack = AIHandler.ChooseRandomAttackIn(_attacks);
                        _player.Damage(choosenAttack.ComputerModifier);
                        Thread.Sleep(SECONDS_BETWEEN_ATTACK * 1000);
                    }
                }

                if (_computer.Knockedout)
                {
                    Renderer.AddEmptyLine();
                    Renderer.RenderText($"{_player.Name} knocked down {_computer.Name} number of knock downs {++_player.Victories}", 2, 20);
                }
                else if (_player.Knockedout)
                {
                    Renderer.AddEmptyLine();
                    Renderer.RenderText($"{_player.Name} knocked down {_computer.Name} number of knock downs {++_player.Victories}", 2, 20);
                }
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

        private void RenderMatchWinner()
        {
            Renderer.AddEmptyLine();
            Console.WriteLine("Match over!");
            Console.WriteLine(_player.Name + " Has won " + _player.Victories + " Times");
            Console.WriteLine(_computer.Name + " Has won " + _computer.Victories + " Times");

            Renderer.AddEmptyLine();
            if (_player.Victories > _computer.Victories)
            {
                Console.WriteLine($"{_player.Name} is the winner!");
            }
            else if (_computer.Victories > _player.Victories)
            {
                Console.WriteLine($"{_computer.Name} is the winner!");
            }
            else
            {
                Console.WriteLine("The match is a draw!");
            }

            Console.ReadKey();
        }

        private void RenderBoxerStats(int round, int attack)
        {
            Renderer.Clear();
            Renderer.RenderBoxerStats(_player, 2, 1);
            Renderer.RenderBoxerCharacters(13, 6);
            Renderer.RenderBoxerStats(_computer, 18, 1);
            Renderer.RenderBoxerCharacters(17, 6);

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
            Boxer opponent = _playersTurn ? _computer : _player;
            Renderer.RenderText($"Turn {attacker.Name}", 2, 10);
        }

        private void InitializeBoxers()
        {
            _player = new Boxer
            {
                Health = 200,
                StartHealth = 200,
                Stamina = 10,
                StartStamina = 10,
                Strength = 5
            };
            _computer = new Boxer
            {
                Name = "Computer",
                Health = 110,
                StartHealth = 110,
                Stamina = 9,
                StartStamina = 9,
                Strength = 15
            };
        }
    }
}