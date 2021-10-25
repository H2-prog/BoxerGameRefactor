using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxerGamerRefactor
{
    public class Game
    {
        private Boxer player1;
        private Boxer player2;
        private int matches;

        private Random random = new Random();

        public Game(string player1Name, string player2Name, int matchesToFight)
        {
            player1 = new Boxer(random)
            {
                Name = player1Name,
                Health = 200,
                Stamina = 10,
                Strength = 5
            };
            player2 = new Boxer(random)
            {
                Name = player2Name, 
                Health = 110,
                Stamina = 9,
                Strength = 15
            };
            matches = matchesToFight;
        }

        public void Loop()
        {
            for (int BoxingSimmatches = 0; BoxingSimmatches <= matches; BoxingSimmatches++)
            {
                for (int round = 0; round < 10; round++)
                {
                    if (player1.Health <= 0 || player2.Health <= 0)
                    {
                        Console.WriteLine("");
                        Console.WriteLine($"================= Round {round} has ended get ready for the next round =================");
                        Console.WriteLine("");
                        break;
                    }

                    for (int i = 0; i < 10; i++)
                    {

                        if (player1.Health <= 0 || player2.Health <= 0)
                        {
                            break;
                        }

                        Console.WriteLine();

                        Console.WriteLine("Press = W = for a normal Attack");
                        Console.WriteLine("Press = D = for a normal Attack");
                        Console.WriteLine("Which attack do you want to perform");

                        var input = Console.ReadLine();

                        switch (input)
                        {
                            case "w":
                                {
                                    var player2DamageResult = player2.Damage(4);
                                    var player1DamageResult = player1.Damage(1);
                                    Console.WriteLine("{0} Hits {1} for {2}, {1} takes {2} Damage, {1} now has {3} health left", player1.Name, player2.Name, player1DamageResult.Damage, player2DamageResult.NewHealth);
                                    Console.WriteLine("{0} Hits {1} for {2}, {1} takes {2} Damage, {1} now has {3} health left", player2.Name, player1.Name, player2DamageResult.Damage, player1DamageResult.NewHealth);
                                    Console.WriteLine("");

                                    RegenBoxer(player1);
                                    RegenBoxer(player2);
                                }
                                break;
                            case "d":
                                {
                                    var player2DamageResult = player2.Damage(12);
                                    var player1DamageResult = player1.Damage(1);

                                    Console.WriteLine("{0} Hits {1} for {2}, {1} takes {2} Damage, {1} now has {3} health left", player1.Name, player2.Name, player1DamageResult.Damage, player2DamageResult.NewHealth);
                                    Console.WriteLine("{0} Hits {1} for {2}, {1} takes {2} Damage, {1} now has {3} health left", player2.Name, player1.Name, player2DamageResult.Damage, player1DamageResult.NewHealth);
                                    Console.WriteLine("");

                                    RegenBoxer(player1);
                                    RegenBoxer(player2);
                                }
                                break;
                            case "color":
                                Console.BackgroundColor = ConsoleColor.Cyan;
                                Console.ForegroundColor = ConsoleColor.Black;
                                break;

                            default:
                                Console.WriteLine("It was not the keys you where told to press");
                                break;
                        }
                    }

                    if (player2.Health <= 0)
                    {
                        Console.WriteLine("");
                        Console.WriteLine(player1.Name + " knocked down " + player2.Name + " number of knock downs " + player1.Victories++);
                        player1.Health = 100;
                        player2.Health = 200;
                    }
                    else if (player1.Health <= 0)
                    {
                        Console.WriteLine("");
                        Console.WriteLine(player2.Name + " knocked down " + player1.Name + " number of knock downs " + player2.Victories++);
                        player1.Health = 100;
                        player2.Health = 200;
                    }

                    if (player1.Health <= 0 || player2.Health <= 0)
                    {
                        Console.WriteLine("");
                        Console.WriteLine(player1.Name + " Has won " + player1.Victories + " Times");
                        Console.WriteLine(player2.Name + " Has won " + player2.Victories + " Times");
                        Console.WriteLine("");
                        Console.WriteLine("############################## We have a Winner ##################################");
                        matches++;

                    }


                    if (player1.Victories > player2.Victories)
                    {
                        Console.WriteLine("{0} is the winner", player2.Name);
                    }
                    else
                    {
                        Console.WriteLine("{0} is the winner", player1.Name);
                    }
                }
            }
        }

        private void RegenBoxer(Boxer boxer)
        {
            var boxerRegen = boxer.Regen();
            Console.WriteLine($"{boxer.Name} Regains some of his stamina back and recives {boxerRegen.RegenAmount} health, now {boxer.Name} has {boxerRegen.NewHealth} health");
        }
    }
}
