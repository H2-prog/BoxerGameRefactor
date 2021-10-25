using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

namespace BoxerGamerRefactor
{
    public class BoxerGame : ConsoleGame
    {
        private Boxer _player1;
        private Boxer _player2;
        private int _rounds;

        private List<BoxerAttack> _attacks = new List<BoxerAttack>
        {
            new BoxerAttack("normal Attack", ConsoleKey.W, 4, 1),
            new BoxerAttack("power Attack", ConsoleKey.D, 12, 1)
        };

        private Random _random;

        public BoxerGame()
        {
            _random = new Random();
            _player1 = new Boxer(_random)
            {
                Health = 200,
                Stamina = 10,
                Strength = 5
            };
            _player2 = new Boxer(_random)
            {
                Health = 110,
                Stamina = 9,
                Strength = 15
            };
        }

        public override void Start()
        {
            Console.WriteLine("Enter your name player 1");
            _player1.Name = ScreenViews.GetInputAsString();

            Console.WriteLine("Enter your name player 2");
            _player2.Name = ScreenViews.GetInputAsString();

            Console.WriteLine("How many rounds do you want to fight");
            _rounds = ScreenViews.GetInputAsInt();
        }

        public override void Update()
        {
            for (int BoxingSimmatches = 0; BoxingSimmatches <= _rounds; BoxingSimmatches++)
            {
                for (int round = 0; round < 3; round++)
                {
                    if (_player1.Health <= 0 || _player2.Health <= 0)
                    {
                        ScreenViews.GetReadyForNextRound(round);
                        break;
                    }

                    for (int i = 0; i < 10; i++)
                    {
                        if (_player1.Health <= 0 || _player2.Health <= 0)
                        {
                            break;
                        }

                        var consoleInput = ScreenViews.ShowAndChooseAttack(_attacks);
                        var attack = (_attacks.Where(x => x.Key == consoleInput.Key)).FirstOrDefault();
                        if(attack == null)
                        {
                            switch (consoleInput.Key)
                            {
                                case ConsoleKey.Escape: Exit(); break;
                                default: Console.WriteLine("It was not the you were told to press!"); break;
                            }
                            continue;
                        }
                        Attack(attack.Player1Modifier, attack.Player2Modifier);
                    }

                    if (_player2.Health <= 0)
                    {
                        ScreenViews.AddEmptyLine();
                        Console.WriteLine($"{_player1.Name} knocked down {_player2.Name} number of knock downs {_player1.Victories++}");
                    }
                    else if (_player1.Health <= 0)
                    {
                        ScreenViews.AddEmptyLine();
                        Console.WriteLine($"{_player2.Name} knocked down {_player1.Name} number of knock downs {_player2.Victories++}");
                    }

                    EndGameIfThereIsAWinner();
                }
            }
        }

        private void Attack(int player1Modifier, int player2Modifier)
        {
            var player2DamageResult = _player2.Damage(player1Modifier);
            var player1DamageResult = _player1.Damage(player2Modifier);

            ScreenViews.ShowBoxerHitMessage(_player1, _player2, player2DamageResult);
            ScreenViews.ShowBoxerHitMessage(_player2, _player1, player1DamageResult);
            ScreenViews.AddEmptyLine();

            RegenBoxer(_player1);
            RegenBoxer(_player2);
        }

        private void RegenBoxer(Boxer boxer)
        {
            var boxerRegen = boxer.Regen();
            Console.WriteLine($"{boxer.Name} Regains some of his stamina back and recives {boxerRegen.RegenAmount} health, now {boxer.Name} has {boxerRegen.NewHealth} health");
        }

        private void EndGameIfThereIsAWinner()
        {
            if (_player1.Health <= 0 || _player2.Health <= 0)
            {
                ScreenViews.AddEmptyLine();
                Console.WriteLine($"{_player1.Name} Has won {_player1.Victories} Times");
                Console.WriteLine($"{_player2.Name} Has won {_player2.Victories} Times");
                ScreenViews.AddEmptyLine();
                Console.WriteLine("############################## We have a Winner ##################################");
                _rounds++;
            }


            if (_player1.Victories > _player2.Victories)
            {
                Console.WriteLine($"{_player1.Name} is the winner");
            }
            else
            {
                Console.WriteLine($"{_player2.Name} is the winner");
            }
        }
    }
}