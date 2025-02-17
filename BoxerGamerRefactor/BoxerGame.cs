﻿using System;
using System.Text;
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
        private bool _matchIsDone = false;

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
            Renderer.Clear();
            _player.Name = Input.ReadStringWithText("Enter your name", 0, 0);
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
                }

                for (int attack = 1; attack <= MAX_ATTACKS; attack++)
                {
                    RenderBoxerStats(round, attack);

                    GameController.CheckIfBoxerIsKnockedOut(_player, _computer);
                    GameController.CheckIfBoxerIsKnockedOut(_computer, _player);

                    if (GameController.HasRoundEnded(_player, _computer)) { break; }

                    GameController.Attack(_playersTurn, _attacks, _player, _computer);
                    Thread.Sleep(SECONDS_BETWEEN_ATTACK * 1000);
                }

                RenderRoundWinner();
                Thread.Sleep(SECONDS_BETWEEN_ROUND * 1000);
                GameController.RegenBoxers(_player, _computer);
            }

            RenderMatchWinner();
            AskUserForRematch();
        }

        private void AskUserForRematch()
        {
            if(_matchIsDone)
            {
                var input = Input.ReadStringWithText("Want to take a rematch (Y/n)?", 2, 20);
                Thread.Sleep(500);
                if(input.ToLower() == "y")
                {
                    _matchIsDone = false;
                    Start();
                }
                else
                {
                    Exit();
                }
            }
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
            _matchIsDone = true;
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
                StartStamina = 75,
                Strength = 5
            };
            _player.Health = _player.StartHealth;
            _player.Stamina = _player.StartStamina;

            _computer = new Boxer
            {
                Name = "Computer",
                StartHealth = 110,
                StartStamina = 125,
                Strength = 15
            };
            _computer.Health = _computer.StartHealth;
            _computer.Stamina = _computer.StartStamina;
        }
    }
}