using System;
using System.Collections.Generic;

namespace PlayerDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database();

            database.WorkProgram();
        }

        class Database
        {
            private List<Player> _players = new List<Player>();
            private Random _random = new Random();

            public void WorkProgram()
            {
                const string CommandAddPlayer = "1";
                const string CommandPlayerBan = "2";
                const string CommandUnbanPlayer = "3";
                const string CommandDeletePlayer = "4";
                const string CommandShowPlayer = "5";
                const string CommandExit = "6";

                string userInput;
                bool isWork = true;

                while (isWork)
                {
                    Console.WriteLine();
                    Console.WriteLine($"{CommandAddPlayer} - добавить игрока\n" +
                                      $"{CommandPlayerBan} - забанить игрока\n" +
                                      $"{CommandUnbanPlayer} - разбанить игрока\n" +
                                      $"{CommandDeletePlayer} - удалить игрока\n" +
                                      $"{CommandShowPlayer} - показать игроков\n" +
                                      $"{CommandExit} - выйти");

                    userInput = Console.ReadLine();
                    
                    Console.Clear();

                    switch (userInput)
                    {
                        case CommandAddPlayer:
                            AddPlayer();
                            break;

                        case CommandPlayerBan:
                            Ban();
                            break;

                        case CommandUnbanPlayer:
                            Unban();
                            break;

                        case CommandDeletePlayer:
                            DeletePlayer();
                            break;

                        case CommandShowPlayer:
                            ShowInfo();
                            break;

                        case CommandExit:
                            isWork = false;
                            break;

                        default:
                            break;
                    } 
                }
            }

            private void AddPlayer()
            {
                Console.WriteLine("Введите имя:");
                string name = Console.ReadLine();

                Console.WriteLine("Введите уровень: ");
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out int level))
                {
                    int idNumber = CreateIdNumber();

                    _players.Add(new Player(name, level, idNumber));
                }
                else
                {
                    Console.WriteLine("Не удалось распознать число!");
                }
            }

            private void DeletePlayer()
            {
                ShowInfo();

                Console.WriteLine("Введите идентификатор: ");
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out int idNumber))
                {
                    if (SearchMatch(idNumber, out int index))
                    {
                        _players.RemoveAt(index);
                    }
                    else
                    {
                        Console.WriteLine("Игрока с таким идентификатором нет!");
                    }
                }
                else
                {
                    Console.WriteLine("Не удалось распознать число!");
                }
            }

            private void ShowInfo()
            {
                for (int i = 0; i < _players.Count; i++)
                {
                    _players[i].StatsDatabase();
                }
            }

            private void Ban()
            {
                string status = "Игрок уже забанен!";

                ChangePlayerStatus(false, true, status);
            }

            private void Unban()
            {
                string status = "Игрок уже разбанен!";

                ChangePlayerStatus(true, false, status);
            }

            private void ChangePlayerStatus(bool canChange, bool isBanned, string status)
            {
                ShowInfo();

                Console.WriteLine("Введите идентификатор: ");
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out int idNumber))
                {
                    if (SearchMatch(idNumber, out int index))
                    {
                        if (_players[index].IsBanned == canChange)
                        {
                            _players[index].ChangeBooleanValue(isBanned);
                        }
                        else
                        {
                            Console.WriteLine(status);
                        }
                    }
                }
            }

            private int CreateIdNumber()
            {
                bool isWork = true;
                int IdNumber = 0;

                while (isWork)
                {
                    int minIndex = 0;
                    int maxIndex = 100;
                    IdNumber = _random.Next(minIndex, maxIndex);

                    if (SearchMatch(IdNumber,out int index) == false)
                    {
                        isWork = false;
                    }
                }

                return IdNumber;
            }

            private bool SearchMatch(int idNumber, out int index)
            {
                for (int i = 0; i < _players.Count; i++)
                {
                    if (_players[i].IdNumber == idNumber)
                    {
                        index = i;
                        return true;
                    }
                }

                index = 0;
                return false;
            }
        }
    }

    class Player
    {
        private string _name;
        private int _level;

        public Player(string name, int level, int idNumber)
        {
            _name = name;
            _level = level;
            IdNumber = idNumber;
            IsBanned = false;
        }

        public int IdNumber { get; private set; }
        public bool IsBanned { get; private set; }

        public void StatsDatabase()
        {
            Console.WriteLine($"{IdNumber}: имя - {_name}, уровень - {_level}, бан - {IsBanned}");
        }

        public void ChangeBooleanValue(bool isBanned)
        {
            IsBanned = isBanned;
        }
    }
}
