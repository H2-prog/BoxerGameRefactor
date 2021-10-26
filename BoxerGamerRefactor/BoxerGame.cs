using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;

namespace BoxerGamerRefactor
{
    public class BoxerGame : ConsoleGame
    {
        private IBoxerGameController BoxerGameController { get; set; }

        private Boxer _player1;
        private Boxer _player2;
        int _boxingSimmatches = 0;
        private int _rounds;

        const int MAX_ROUNDS = 3;
        const int MAX_ATTACKS = 10;

        private List<BoxerAttack> _attacks = new List<BoxerAttack>
        {
            new BoxerAttack("normal Attack", ConsoleKey.W, 4, 1),
            new BoxerAttack("power Attack", ConsoleKey.D, 12, 1)
        };

        public override void Setup()
        {
            BoxerGameController = ServiceProvider.GetService<IBoxerGameController>();

            _player1 = new Boxer
            {
                Health = 200,
                Stamina = 10,
                Strength = 5
            };
            _player2 = new Boxer
            {
                Health = 110,
                Stamina = 9,
                Strength = 15
            };
        }

        public override void Start()
        {
            _player1.Name = ScreenViews.ReadString("Enter your name player 1");
            _player2.Name = ScreenViews.ReadString("Enter your name player 2");
            _rounds = ScreenViews.ReadInt("How many rounds do you want to fight");
        }

        public override void Update()
        {
            ScreenViews.AddEmptyLine();
            ScreenViews.AddSeperator();
            ScreenViews.AddText("The begining for a new round");
            ScreenViews.AddEmptyLine();

            int currentRound = 0;
            if (_player1.Dead || _player2.Dead)
            {
                _player1.Health = 200;
                _player2.Health = 110;

                // Wait for 10 seconds...
                ScreenViews.GetReadyForNextRound(currentRound);
                Thread.Sleep(10 * 1000);
            }

            while (_boxingSimmatches <= _rounds && !(_player1.Dead || _player2.Dead))
            {
                for (currentRound = 0; currentRound < MAX_ROUNDS; currentRound++)
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
                                default: Console.WriteLine("It was not the you were told to press!"); break;
                            }
                            continue;
                        }
                        BoxerGameController.AttackBoxer(_player1, _player2, attack);
                    }

                    if (_player2.Dead)
                    {
                        ScreenViews.AddEmptyLine();
                        Console.WriteLine($"{_player1.Name} knocked down {_player2.Name} number of knock downs {++_player1.Victories}");
                    }
                    else if (_player1.Dead)
                    {
                        ScreenViews.AddEmptyLine();
                        Console.WriteLine($"{_player2.Name} knocked down {_player1.Name} number of knock downs {++_player2.Victories}");
                    }

                    if (_player1.Dead || _player2.Dead)
                    {
                        ScreenViews.AddEmptyLine();
                        Console.WriteLine($"{_player1.Name} Has won {_player1.Victories} Times");
                        Console.WriteLine($"{_player2.Name} Has won {_player2.Victories} Times");
                        ScreenViews.AddEmptyLine();
                        Console.WriteLine("############################## We have a Winner ##################################");
                        _rounds++;
                    }

                    if (_player1.Victories > _player2.Victories || _player1.Health > _player2.Health)
                    {
                        Console.WriteLine($"{_player1.Name} is the winner of the round!");
                    }
                    else
                    {
                        Console.WriteLine($"{_player2.Name} is the winner of the round!");
                    }
                }
            }
            ScreenViews.AddSeperator();
        }
    }
}