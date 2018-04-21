using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CSGSI;
using CSGSI.Nodes;

namespace CsgoGameState
{
    class Program
    {


        public static readonly int CTFullBuy = 5500;
        public static readonly int TFullBuy = 4700;
        public static readonly int Loss1 = 1400;
        public static readonly int Loss2 = 1900;
        public static readonly int Loss3 = 2400;
        //public static int counter = 40; //bombe tid



        static void Main(string[] args)
        {
            PrintTable();
            GameStateListener gsl = new GameStateListener(3000); //http://localhost:3000/  
            gsl.NewGameState += new NewGameStateHandler(OnNewGameState);

            //tester om den er startet
            if (!gsl.Start())
            {
                Environment.Exit(0);
            }

            Console.WriteLine("Listening...");

        }


        private static void mittable(GameState gs)
        {

            int _playermoney = gs.Player.State.Money;
            string _current = myifloop(_playermoney);
            string _loss1 = myifloop(_playermoney + Loss1);
            string _loss2 = myifloop(_playermoney + Loss1 + Loss2);
            string _loss3 = myifloop(_playermoney + Loss1 + Loss2 + Loss3);

            string _fullbuy = myifloop(Fullbuy(gs)); // en metode kalder en metode no go


            Console.WriteLine(@"╔════════════════════════╗
                ║ Nuværende: {0}       ║
                ╠════════╦═══════╦═══════╣
                ║ Round: ║ Loss  ║ Win   ║
                ╠════════╬═══════╬═══════╣
                ║ 1.     ║  {1}║ 16000 ║
                ╠════════╬═══════╬═══════╣
                ║ 2.     ║  {2}║       ║
                ╠════════╬═══════╬═══════╣
                ║ 3.     ║  {3}║       ║
                ╠════════╩═══════╩═══════╣
                ║ Full buy: {4}        ║
                ╚════════════════════════╝",_current,_loss1,_loss2,_loss3, _fullbuy);
        }
        static void OnNewGameState(GameState gs)
        {
            int _playerMoney = gs.Player.State.Money;

            if (gs.Round.Phase == RoundPhase.FreezeTime) //tjek runder samme som sidst?
            {

                Console.Clear();
                PrintTable();


                Console.WriteLine("Nuværende:" + _playerMoney);
                _playerMoney += Loss1;
                Console.WriteLine("\nOm antal tabte");
                Console.WriteLine("1.Runde:" + _playerMoney);
                _playerMoney += Loss2;
                Console.WriteLine("2.Runde:" + _playerMoney);
                _playerMoney += Loss3;
                Console.WriteLine("3.Runde:" + _playerMoney);

                if (gs.Player.Team == PlayerTeam.CT)
                {
                    Console.WriteLine("Full buy: 5500");
                }
                else
                {
                    Console.WriteLine("Full buy: 4700");
                    Thread.Sleep(100);
                }






            }

            #region commented

            //while (gs.Round.Bomb == BombState.Planted)
            //{
            //    Console.WriteLine(counter);
            //    counter = -1;
            //    Thread.Sleep(1000);
            //    if (counter < 1)
            //    {
            //        counter = 40;
            //        break;
            //    }
            //}

            #endregion

        }

        private static void PrintTable()
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
            {
                return istring + " ";
            }
            else if (istring.Length == 3)
            {
                return istring + "  ";
            }
            else if (istring.Length == 2)
            {
                return istring + "   ";
            }
            else if (istring.Length == 1)
            {
                return istring + "     ";
            }
            else
            {
                return istring;
            }
        }

        private static int Fullbuy(GameState gs)
        {
            if (gs.Player.Team == PlayerTeam.T)
            {
                return 4700;
            }
            else
            {
                return 5500;
            }
        }
    }
}
