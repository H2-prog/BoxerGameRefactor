using System;

namespace BoxerGamerRefactor
{
    public class Game
    {
        private Boxer _player1;
        private Boxer _player2;
        private int _rounds;

        private readonly Random random;

        public Game()
        {
            random = new Random();
            _player1 = new Boxer(random)
            {
                Health = 200,
                Stamina = 10,
                Strength = 5
            };
            _player2 = new Boxer(random)
            {
                Health = 110,
                Stamina = 9,
                Strength = 15
            };
        }

        public void Setup()
        {
            Console.WriteLine("Enter your name player 1");
            _player1.Name = ScreenViews.GetInputAsString();

            Console.WriteLine("Enter your name player 2");
            _player2.Name = ScreenViews.GetInputAsString();

            Console.WriteLine("How many rounds do you want to fight");
            _rounds = ScreenViews.GetInputAsInt();
        }

        public void Loop()
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

                        var key = ScreenViews.ChooseAttack();
                        switch(key.Key)
                        {
                            case ConsoleKey.W: Attack(1, 4); break;
                            case ConsoleKey.D: Attack(1, 12); break;
                            case ConsoleKey.Escape: Environment.Exit(0); break;
                            default: Console.WriteLine("It was not the you were told to press!"); break; 
                        }
                    }

                    if (_player2.Health <= 0)
                    {
                        Console.WriteLine("");
                        Console.WriteLine($"{_player1.Name} knocked down {_player2.Name} number of knock downs {_player1.Victories++}");
                    }
                    else if (_player1.Health <= 0)
                    {
                        Console.WriteLine("");
                        Console.WriteLine($"{_player2.Name} knocked down {_player1.Name} number of knock downs {_player2.Victories++}");
                    }

                    EndGameIfThereIsAWinner();
                }
            }
        }

        private void Attack(int player1Modifier, int player2Modifier)
        {
            var player2DamageResult = _player2.Damage(player2Modifier);
            var player1DamageResult = _player1.Damage(player1Modifier);

            Console.WriteLine("{0} Hits {1} for {2}, {1} takes {2} Damage, {1} now has {3} health left", _player1.Name, _player2.Name, player1DamageResult.Damage, player2DamageResult.NewHealth);
            Console.WriteLine("{0} Hits {1} for {2}, {1} takes {2} Damage, {1} now has {3} health left", _player2.Name, _player1.Name, player2DamageResult.Damage, player1DamageResult.NewHealth);
            Console.WriteLine("");

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
                Console.WriteLine("");
                Console.WriteLine($"{_player1.Name} Has won {_player1.Victories} Times");
                Console.WriteLine($"{_player2.Name} Has won {_player2.Victories} Times");
                Console.WriteLine("");
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
