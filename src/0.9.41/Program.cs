using System;
using System.Windows.Forms;

namespace LaLoterieAltern2ative
{
    class Program
    {
        static string title = "La loterie alternative";
        static int windowX = Console.WindowWidth;
        static int windowY = Console.WindowHeight;
        static int bufferY = Console.BufferHeight;
        public static void Main(string[] args)
        {
            try
            {
                Console.Title = title;
                Console.SetWindowSize(64, 20);
                Console.SetBufferSize(64, 100);
                menu();
            }
            catch (FormatException)
            {
                Console.Error.Write("Argument incorrect.");
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.Error.Write("Argument incorrect.");
            }
            catch (TypeInitializationException)
            {
                Console.Error.Write("Redirection non supportée.");
            }
        }
        public static void menu()
        {
            bool quit = false;
            while (!quit)
            {
                Console.Title = title + " - Menu principal";
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("" +
                    "                    LA LOTERIE ALTERNATIVE      version 0.9.41 ");
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("" +
                    "===============================================================\n" +
                    "Vous avez un nombre de coups limité pour trouver un nombre     \n" +
                    "entier généré aléatoirement compris entre deux bornes.         \n" +
                    "Réussirez-vous?                                                \n" +
                    "===============================================================\n" +
                    "Plus votre score est élevé, meilleur est votre résultat!       \n" +
                    "===============================================================\n" +
                    "1: FACILE          :   Jouer de 0 à 1000, 10 tentatives        \n" +
                    "2: INTERMÉDIAIRE   :   Jouer de 0 à 100, 5 tentatives          \n" +
                    "3: DIFFICILE       :   Jouer de 0 à 10, une tentative          \n" +
                    "4: PERSONNALISÉ    :   Jouer une partie personnalisée          \n" +
                    "5: ALÉATOIRE       :   Jouer une partie aléatoire              \n"+
                    "0: Quitter         :   Quitter le jeu                          \n" +
                    "===============================================================\n" +
                    "Entrez votre choix: (1,2,3,4,5,0) > ");
                try
                {
                    string choix = Console.ReadLine();
                    int choix2 = int.Parse(choix);
                    switch (choix2)
                    {
                        case 1:
                            game(0, 1000, 10);
                            break;
                        case 2:
                            game(0, 100, 5);
                            break;
                        case 3:
                            game(0, 10, 1);
                            break;
                        case 4:
                            customGame();
                            break;
                        case 5:
                            randomGame();
                            break;
                        case 0:
                            quit = true;
                            exit();
                            break;
                        default:
                            throw new FormatException();
                    }
                }
                catch (FormatException)
                {
                    Console.Error.Write("     Choix invalide.");
                    System.Threading.Thread.Sleep(1000);
                }
                catch (OverflowException)
                {
                    Console.Error.Write("     La valeur tapée dépasse celle supportée par le programme.");
                    System.Threading.Thread.Sleep(1000);
                }
                catch (ArgumentNullException)
                {
                    Console.Error.Write("     Utilisez l'option 5 pour terminer le processus.");
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }

        private static void randomGame()
        {
            Console.Title = title + " - Mode aléatoire";
            string cs = "";
            int c = 0, nbtm = 0, max = 0;
            int[] easy = { 0, 100 };
            int[] medium = { 101, 500 };
            int[] hard = { 501, 1000 };
            int[] nbts_easy = { 7, 10 }, nbts_medium = { 4, 6 }, nbts_hard = { 1, 3 };
            bool good = false;
            while (!good)
            {
                try
                {
                    Console.Write("Niv. difficulté: 1=Facile, 2=Intermédiaire, 3=Difficile ? > ");
                    cs = Console.ReadLine();
                    c = int.Parse(cs);
                    switch (c)
                    {
                        case 1:
                            max = new Random().Next(easy[0], easy[1]);
                            nbtm = new Random().Next(nbts_easy[0], nbts_easy[1]);
                            game(easy[0], max, nbtm);
                            good = true;
                            break;
                        case 2:
                            max = new Random().Next(medium[0], medium[1]);
                            nbtm = new Random().Next(nbts_medium[0], nbts_medium[1]);
                            game(medium[0], max, nbtm);
                            good = true;
                            break;
                        case 3:
                            max = new Random().Next(hard[0], hard[1]);
                            nbtm = new Random().Next(nbts_hard[0], nbts_hard[1]);
                            game(hard[0], max, nbtm);
                            good = true;
                            break;
                        default:
                            throw new FormatException();
                    }
                }
                catch (FormatException)
                {
                    Console.Error.WriteLine("     Valeur invalide.");
                }
                catch (ArgumentNullException)
                {
                    Console.Error.WriteLine("     Opération annulée, retour au menu...");
                    System.Threading.Thread.Sleep(1000);
                    good = true; // just return to menu
                }
                catch (OverflowException)
                {
                    Console.Error.WriteLine("     La valeur tapée dépasse celle supportée par le programme.");
                }
            }
        }
        public static void customGame()
        {
            Console.Title = title + " - Choix des options";
            int min = 0, max = 0, nbtm = 0;
            string choix = "";
            bool good = false, returnMenu = false;
            int[] val = { min, max, nbtm };
            string[] msg = { "Entrez un nombre minimum: > ", "Entrez un nombre maximum: > ", "Entrez un nombre de tentatives: > " };
            int i = 0;
            while (i < val.Length)
            {
                good = false;
                while (!good)
                {
                    try
                    {
                        Console.Write(msg[i]);
                        choix = Console.ReadLine();
                        val[i] = int.Parse(choix);
                        good = true;
                    }
                    catch (FormatException)
                    {
                        Console.Error.WriteLine("Nombre incorrect. ");
                    }
                    catch (OverflowException)
                    {
                        Console.Error.WriteLine("Nombre hors des bornes autorisées. ");
                    }
                    catch (ArgumentNullException)
                    {
                        returnMenu = true;
                        Console.Error.WriteLine("Opération annulée, retour au menu...");
                        System.Threading.Thread.Sleep(1000);
                        menu();
                    }
                }
                i++;
            }
            if (!returnMenu)
            {
                game(val[0], val[1], val[2]);
            }
        }
        public static void game(int min, int max, int nbtm)
        {
            try
            {
                bool quit = false;
                string car = "";
                if (min != max && nbtm > 0)
                {
                    while (!quit)
                    {
                        if (nbtm != 9999)
                        {
                            Console.Title = title;
                        }
                        int valeur = new Random().Next(min, max);
                        Console.Clear();
                        bool trouve = false;
                        int nbt = 0;
                        if (nbtm != 9999)
                        {
                            Console.Write("Prêt!...");
                            System.Threading.Thread.Sleep(1000);
                            string[] timer = { "3", ".", ".", ".", "2", ".", ".", ".", "1", ".", ".", ".", " GO!" };
                            for (int i = 0; i < timer.Length; i++)
                            {
                                Console.Write(timer[i]);
                                System.Threading.Thread.Sleep(300);
                            }
                            Console.WriteLine();
                        }
                        while (!trouve && nbt < nbtm)
                        {
                            int nbi = 0;
                            string etat = "";
                            try
                            {
                                if (nbtm != 9999)
                                {
                                    Console.Title = title + " - Tentative " + (nbt + 1) + " sur " + nbtm;
                                }
                                Console.Write("Entrez un nombre entre " + min + " et " + max + ": > ");
                                string nb = Console.ReadLine();
                                nbi = int.Parse(nb);
                                if (nbi >= min && nbi <= max)
                                {
                                    if (nbi == valeur)
                                    {
                                        trouve = true;
                                        if (nbtm != 9999)
                                        {
                                            Console.Title = title + " - VOUS AVEZ GAGNÉ!";
                                        }
                                        Console.WriteLine("\n" +
                                        "===================================\n" +
                                        "Félicitations! Vous l'avez trouvé \n" +
                                        "après " + (nbt + 1) + " tentative(s)!\n" +
                                        "===================================");
                                        continue;
                                    }
                                    else if (nbi == -999)
                                    {
                                        trouve = true;
                                        Console.WriteLine("Le nombre était " + valeur + ".");
                                        continue;
                                    }
                                    else
                                    {
                                        if (!trouve)
                                        {
                                            if (nbi < valeur)
                                            // plus petit
                                            {
                                                etat = "plus grand";
                                            }
                                            else
                                            //plus grand
                                            {
                                                etat = "plus petit";
                                            }
                                            Console.WriteLine("Erreur! Le nombre que l'on cherche est " + etat + " que " + nbi + ".");
                                        }
                                    }
                                }
                                else
                                {
                                    throw new FormatException();
                                }
                                nbt++;
                            }
                            catch (FormatException)
                            {
                                Console.Error.WriteLine("Nombre non valide.");
                            }
                            catch (OverflowException)
                            {
                                Console.Error.WriteLine("Le nombre dépasse les bornes autorisées par le programme.");
                            }
                            catch (ArgumentNullException)
                            {
                                Console.Error.WriteLine("Impossible de terminer le processus.");
                            }
                        }
                        if (!trouve && nbtm != 9999)
                        {
                            Console.Title = title + " - Vous avez perdu!";
                            Console.WriteLine("\n" +
                                "===================================\n" +
                                "Meilleure chance la prochaine fois!\n" +
                                "===================================");
                        }
                        Console.WriteLine("" +
                                "Le nombre était        :       " + valeur + "\n" +
                                "===================================\n");
                        bool quiterr = true;
                        if (nbtm != 9999)
                        {
                            while (quiterr)
                            {
                                try
                                {
                                    quiterr = false;
                                    Console.Write("Rejouer? (O/N) > ");
                                    car = Console.ReadLine();
                                    switch (car[0])
                                    {
                                        case 'N':
                                        case 'n':
                                            quit = true;
                                            break;
                                        case 'O':
                                        case 'o':
                                            Console.Clear();
                                            break;
                                        default:
                                            quiterr = true;
                                            break;
                                    }
                                }
                                catch (NullReferenceException)
                                {
                                    Console.Error.WriteLine("Impossible de terminer le processus.");
                                    quiterr = true;
                                }
                                catch (IndexOutOfRangeException)
                                {
                                    quiterr = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.Error.Write("" +
                    "        Valeurs invalides.\n");
                customGame();
            }
        }
        public static void exit()
        {
            Console.ResetColor();
            Console.SetWindowSize(windowX, windowY);
            Console.SetBufferSize(windowX, bufferY);
            Console.Clear();
            Environment.Exit(0);
        }
    }
}
