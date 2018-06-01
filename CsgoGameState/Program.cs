using System;
using System.Threading;
using CSGSI;
using CSGSI.Nodes;

namespace CsgoGameState
{
    internal class Program
    {
        public static readonly int CTFullBuy = 5500; //m4
        public static readonly int TFullBuy = 4700; //ak
        public static readonly int Loss1 = 1400;
        public static readonly int Loss2 = 1900;
        public static readonly int Loss3 = 2400;
        public static readonly int Loss4 = 2900;
        public static readonly int Loss5 = 3400;
        public static readonly int Win = 3250;

        public static string Roundwin = "";
        public static int TotalRoundsLostRow = 0;
        public static int NextRoundMoney;

        public static string round1;
        public static string round2;
        public static string round3;
        public static string round4;

        private static void Main(string[] args)
        {

                printMik();
                GameStateListener gsl = new GameStateListener(3000); //http://localhost:3000/  
                gsl.NewGameState += mittable;
                if (!gsl.Start()) Environment.Exit(0);

                Console.WriteLine("Waiting for game to launch");


        }


        private static void mittable(GameState gs)
        {
            int _playermoney = gs.Player.State.Money;
            string _current = myifloop(_playermoney);
            string _loss1 = myifloop(_playermoney + Loss1);
            string _loss2 = myifloop(_playermoney + Loss1 + Loss2);//loss1 +2 = you save
            string _loss3 = myifloop(_playermoney + Loss1 + Loss2 + Loss3); //loss1 +2 = you save

            string _fullbuy = myifloop(Fullbuy(gs));

            //win
            string _win1 = myifloop(_playermoney + Win);
            string _win2 = myifloop(_playermoney + Win * 2);
            string _win3 = myifloop(_playermoney + Win * 3);


            if (gs.Previously.Player.State.Money != gs.Player.State.Money)
            {
                Console.Clear();
                LostRoundCounter(gs);

                switch (TotalRoundsLostRow)
                {
                    case 0:
                        NextRoundMoney = _playermoney + Loss1;
                        ClearAllRounds();
                        round1 = "<-";
                        break;
                    case 1:
                        NextRoundMoney = _playermoney + Loss2;
                        ClearAllRounds();
                        round2 = "<-";
                        break;
                    case 2:
                        NextRoundMoney = _playermoney + Loss3;
                        ClearAllRounds();
                        round3 = "<-";
                        break;
                    case 3:
                        NextRoundMoney = _playermoney + Loss4;
                        ClearAllRounds();
                        round4 = "->";
                        break;
                    case 4:
                        NextRoundMoney = _playermoney + Loss5;
                        ClearAllRounds();
                        round4 = "->";
                        break;
                    default:
                        NextRoundMoney = _playermoney + Loss5;
                        ClearAllRounds();
                        round4 = "->";
                        break;
                }
                // printMik();
                Console.WriteLine(@"
╔════════════════════════╗
║ Current: {0}         ║
╠════════╦═══════╦═══════╣
║ Round: ║ Loss  ║ Win   ║
╠════════╬═══════╬═══════╣
║ 1.{9}   ║  {1}║ {5} ║
╠════════╬═══════╬═══════╣
║ 2.{10}   ║  {2}║ {6} ║
╠════════╬═══════╬═══════╣
║ 3.{11}   ║  {3}║ {7} ║
╠════════╩═══════╩═══════╣
║ Full buy: {4}        ║
╚════════════════════════╝
 {12}Next Round loss: {8}", _current, _loss1, _loss2, _loss3, _fullbuy, _win1, _win2, _win3, NextRoundMoney, round1, round2, round3, round4);
                Thread.Sleep(1000);
            }
        }


        private static void printMik()
        {
            Console.WriteLine(@"                  _          ___                              _             
  /\/\   __ _  __| | ___    / __\_   _  /\   /\___   ___   __| | ___   ___  
 /    \ / _` |/ _` |/ _ \  /__\// | | | \ \ / / _ \ / _ \ / _` |/ _ \ / _ \ 
/ /\/\ \ (_| | (_| |  __/ / \/  \ |_| |  \ V / (_) | (_) | (_| | (_) | (_) |
\/    \/\__,_|\__,_|\___| \_____/\__, |   \_/ \___/ \___/ \__,_|\___/ \___/ 
                                 |___/                                      ");
        }

        private static string myifloop(int i)
        {
            string istring = i.ToString();

            if (istring.Length == 4)
                return istring + " ";
            if (istring.Length == 3)
                return istring + "  ";
            if (istring.Length == 2)
                return istring + "   ";
            if (istring.Length == 1)
                return istring + "    ";
            return istring;
        }

        private static int Fullbuy(GameState gs)
        {
            if (gs.Player.Team == PlayerTeam.T)
                return 4700;
            return 5500;
        }

        private static void LostRoundCounter(GameState gs)
        {
            if (gs.Previously.Round.WinTeam.ToString().ToLower() != "undefined")
            {
                Roundwin = gs.Previously.Round.WinTeam.ToString();
                if (Roundwin == gs.Player.Team.ToString())
                {
                    TotalRoundsLostRow = 0;
                }
                else TotalRoundsLostRow++;
            }
        }

        private static void ClearAllRounds()
        {
            round1 = "  ";
            round2 = "  ";
            round3 = "  ";
            round4 = "  ";
        }

    }
}