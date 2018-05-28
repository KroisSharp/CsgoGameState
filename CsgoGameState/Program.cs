using System;
using System.Threading;
using CSGSI;
using CSGSI.Nodes;

namespace CsgoGameState
{
    internal class Program
    {
        public static readonly int CTFullBuy = 5500;
        public static readonly int TFullBuy = 4700;
        public static readonly int Loss1 = 1400;
        public static readonly int Loss2 = 1900;
        public static readonly int Loss3 = 2400;
        public static readonly int Win = 3250;


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
            string _loss2 = myifloop(_playermoney + Loss1 + Loss2);
            string _loss3 = myifloop(_playermoney + Loss1 + Loss2 + Loss3);

            string _fullbuy = myifloop(Fullbuy(gs)); // en metode kalder en metode no go

            //win
            string _win1 = myifloop(_playermoney + Win);
            string _win2 = myifloop(_playermoney + Win * 2);
            string _win3 = myifloop(_playermoney + Win * 3);

            if (gs.Round.Phase == RoundPhase.FreezeTime)
            {
                Console.Clear();
                printMik();
                Console.WriteLine(@"
╔════════════════════════╗
║ Current: {0}         ║
╠════════╦═══════╦═══════╣
║ Round: ║ Loss  ║ Win   ║
╠════════╬═══════╬═══════╣
║ 1.     ║  {1}║ {5} ║
╠════════╬═══════╬═══════╣
║ 2.     ║  {2}║ {6} ║
╠════════╬═══════╬═══════╣
║ 3.     ║  {3}║ {7} ║
╠════════╩═══════╩═══════╣
║ Full buy: {4}        ║
╚════════════════════════╝", _current, _loss1, _loss2, _loss3, _fullbuy, _win1, _win2, _win3);
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
                return istring + "     ";
            return istring;
        }

        private static int Fullbuy(GameState gs)
        {
            if (gs.Player.Team == PlayerTeam.T)
                return 4700;
            return 5500;
        }
    }
}