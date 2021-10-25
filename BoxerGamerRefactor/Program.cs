using System;

namespace BoxerGamerRefactor
{
    class BoxingGame
    {

        static void Main(string[] args)
        {
            TempGame();
        }

        public static void TempGame()
        {
            var player1 = new Boxer();
            var player2 = new Boxer();

            player1.Health = 200;
            player1.Stamina = 10;
            player1.Strength = 5;

            player2.Health = 110;
            player2.Stamina = 9;
            player2.Strength = 15;

            Random rnd = new Random();

            string player1Name;

            Console.WriteLine("Enter your name player 1");
            player1Name = Console.ReadLine();

            player1.Name = player1Name;

            string player2Name;

            Console.WriteLine("Enter your name player 2");
            player2Name = Console.ReadLine();

            player2.Name = player2Name;

            Console.WriteLine("How many rounds do you want to fight");

            int matches;
            matches = Convert.ToInt32(Console.ReadLine());

            for (int BoxingSimmatches = 0; BoxingSimmatches <= matches; BoxingSimmatches++)
            {

                for (int round = 0; round < 10; round++)
                {
                    if (player1.Health <= 0 || player2.Health <= 0)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("================= Round" + round + " has ended get ready for the next round =================");
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

                        var random = new Random();

                        var regen1 = 0;
                        var regen2 = 0;

                        switch (input)
                        {
                            case "w":

                                var dmg1 = rnd.Next(player1.Strength + 4);
                                var dmg2 = rnd.Next(player2.Strength + 1);

                                Console.WriteLine("{0} Hits {1} for {2}, {1} takes {2} Damage, {1} now has {3} health left", player1.Name, player2.Name, dmg1, player2.Health -= dmg1);
                                Console.WriteLine("{0} Hits {1} for {2}, {1} takes {2} Damage, {1} now has {3} health left", player2.Name, player1.Name, dmg2, player1.Health -= dmg2);
                                Console.WriteLine("");

                                regen1 = random.Next(0, player1.Stamina);
                                regen2 = random.Next(0, player2.Stamina);

                                player1.Health = regen1 + player1.Health;
                                player2.Health = regen2 + player2.Health;

                                Console.WriteLine("{0} Regains some of his stamina back and recives {1} health, now {0} has {2} health", player1.Name, regen1, player1.Health);
                                Console.WriteLine("{0} Regains some of his stamina back and recives {1} health, now {0} has {2} health", player2.Name, regen2, player2.Health);

                                break;

                            case "d":
                                dmg1 = rnd.Next(player1.Strength + 12);
                                dmg2 = rnd.Next(player2.Strength + 1);

                                Console.WriteLine("{0} Hits {1} for {2}, {1} takes {2} Damage, {1} now has {3} health left", player1.Name, player2.Name, dmg1, player2.Health -= dmg1);
                                Console.WriteLine("{0} Hits {1} for {2}, {1} takes {2} Damage, {1} now has {3} health left", player2.Name, player1.Name, dmg2, player1.Health -= dmg2);
                                Console.WriteLine("");

                                regen1 = random.Next(0, player1.Stamina);
                                regen2 = random.Next(0, player2.Stamina);

                                player1.Health = regen1 + player1.Health;
                                player2.Health = regen2 + player2.Health;

                                Console.WriteLine("{0} Regains some of his stamina back and recives {1} health, now {0} has {2} health", player1.Name, regen1, player1.Health);
                                Console.WriteLine("{0} Regains some of his stamina back and recives {1} health, now {0} has {2} health", player2.Name, regen2, player2.Health);

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
    }
}
