using System;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace TrouvezMoi
{
    class Program
    {
        static string title = Application.ProductName;
        static int windowX = Console.WindowWidth;
        static int windowY = Console.WindowHeight;
        static int bufferY = Console.BufferHeight;
        static string name = Application.ProductName;
        static string ver = Application.ProductVersion;
        static int looses = 0;
        static int winnes = 0;
        public static void Main(string[] args)
        {
            try
            {
                try
                {
                    Console.CancelKeyPress += Console_CancelKeyPress;
                }
                catch (InvalidOperationException) { }
                Console.Title = title;
                Console.SetWindowSize(64, 20);
                Console.SetBufferSize(64, 100);
                Application.EnableVisualStyles();
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
        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Console.Write("\n"); // fix bug with gameplay, needs line break
            e.Cancel = true;
        }
        public static void menu()
        {
            bool quit = false;
            while (!quit)
            {
                printMainMenu();
                try
                {
                    string choix = Console.ReadLine();
                    int choix2 = int.Parse(choix);
                    switch (choix2)
                    {
                        case 1:
                            game(0, 1000, 10, 1);
                            break;
                        case 2:
                            game(0, 100, 5, 2);
                            break;
                        case 3:
                            game(0, 10, 1, 3);
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
                        case 42:
                            Console.Clear();
                            printUniverse();
                            break;
                        default:
                            throw new FormatException();
                    }
                }
                catch (FormatException)
                {
                    printInvalid();
                }
                catch (OverflowException)
                {
                    printOverflow();
                }
                catch (ArgumentNullException)
                {
                    DialogResult c = MessageBox.Show("Quitter le jeu?", "Quitter", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    switch (c)
                    {
                        case DialogResult.Yes:
                            quit = true;
                            exit();
                            break;
                        case DialogResult.No:
                        default:
                            break;
                    }
                }
            }
        }
        private static void printMainMenu()
        {
            string maintitle = name.ToUpper() + " version " + ver + "";
            int maintitle_length = maintitle.Length;
            for (int i = 0; i < (64 - 1 - maintitle_length); i++)
            {
                maintitle += " ";
            }
            Console.Title = title + " - Menu principal";
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(maintitle);
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            printLine("=");
            Console.Write("" +
                "Vous avez un nombre de coups limité pour trouver un nombre     \n" +
                "entier généré aléatoirement compris entre deux bornes.         \n" +
                "Réussirez-vous?                                                \n");
            printLine("=");
            Console.Write(
                "1: FACILE          :   Jouer de 0 à 1000, 10 tentatives        \n" +
                "2: INTERMÉDIAIRE   :   Jouer de 0 à 100, 5 tentatives          \n" +
                "3: DIFFICILE       :   Jouer de 0 à 10, une tentative          \n" +
                "4: PERSONNALISÉ    :   Jouer une partie personnalisée          \n" +
                "5: ALÉATOIRE       :   Jouer une partie aléatoire              \n" +
                "0: Quitter         :   Quitter le jeu                          \n");
            printLine("=");
            Console.Write("Entrez votre choix: (1,2,3,4,5,9,0) > ");
        }
        private static void randomGame()
        {
            string cs = "";
            int c = 0, nbtm = 0, max = 0, min = 0;
            int[] level = { };
            int[] nbt_level = { };
            int[] easy = { 0, 200 };
            int[] medium = { 0, 500 };
            int[] hard = { 0, 1000 };
            int[] nbts_easy = { 7, 10 }, nbts_medium = { 4, 6 }, nbts_hard = { 1, 3 };
            int[][] nbts_tab = { nbts_easy, nbts_medium, nbts_hard };
            int[][] level_tab = { easy, medium, hard };
            bool returnMenu = false;
            while (!returnMenu)
            {
                bool good = false;
                while (!good)
                {
                    good = true;
                    try
                    {
                        printRandomMenu();
                        cs = Console.ReadLine();
                        c = int.Parse(cs);
                        switch (c)
                        {
                            case 1:
                                level = easy;
                                nbt_level = nbts_easy;
                                break;
                            case 2:
                                level = medium;
                                nbt_level = nbts_medium;
                                break;
                            case 3:
                                level = hard;
                                nbt_level = nbts_hard;
                                break;
                            case 4:
                                int rnd_i = new Random().Next(0, 2);
                                level = level_tab[rnd_i];
                                nbt_level = nbts_tab[rnd_i];
                                break;
                            case 0:
                                good = false;
                                returnMenu = true;
                                throw new ArgumentNullException();
                            default:
                                good = false;
                                throw new FormatException();
                        }
                        bool good_range = false;
                        while (!good_range)
                        {
                            try
                            {
                                min = new Random().Next(level[0], level[1]);
                                max = new Random().Next(level[0], level[1]);
                                nbtm = new Random().Next(nbt_level[0], nbt_level[1]);
                                if ((max - min <= level[1]) && min < max)
                                {
                                    good_range = true;
                                }
                            }
                            catch (IndexOutOfRangeException) { }
                        }
                        game(min, max, nbtm, 5);
                    }
                    catch (FormatException)
                    {
                        printInvalid();
                        good = false;
                    }
                    catch (ArgumentNullException)
                    {
                        DialogResult c2 = MessageBox.Show("Retourner au menu principal?", "Retour", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        switch (c2)
                        {
                            case DialogResult.Yes:

                                good = true; // just return to menu
                                returnMenu = true;
                                break;
                            case DialogResult.No:
                            default:
                                good = false;
                                break;
                        }
                    }
                    catch (OverflowException)
                    {
                        printOverflow();
                        good = false;
                    }
                }
            }
        }

        private static void printRandomMenu()
        {
            Console.Title = title + " - Mode aléatoire";
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Clear();
            Console.Write("" +
                "Mode aléatoire\n");
            printLine("-");
            Console.Write("" +
                "Le mode aléatoire choisit pour vous le nombre minumum, le\n" +
                "nombre maximum  et le nombre de tentatives dépendamment du \n" +
                "critère que vous aurez choisi.\n"
                );
            printLine("-");
            Console.Write(""+
                "1:     Partie aléatoire facile\n" +
                "2:     Partie aléatoire intermédiaire\n" +
                "3:     Partie aléatoire difficile\n" +
                "4:     Partie aléatoire à difficulté aléatoire\n" +
                "0:     Retour au menu principal\n" +
                "");
            printLine("-");
            Console.Write("Entrez un choix (1,2,3,4,0): > ");
        }

        private static void printLine(string s)
        {
            for (int i = 0; i < 63; i++)
            {
                Console.Write(s);
            }
            Console.Write("\n");
        }
        private static void clearLine()
        {
           
            Console.SetCursorPosition(0, Console.CursorTop -1);
            printLine(" ");
            Console.SetCursorPosition(0, Console.CursorTop -1);
            
        }
        public static void customGame()
        {
            int min = 0, max = 0, nbtm = 0;
            string choix = "";
            bool good = false, returnMenu = false;
            int[] val = { min, max, nbtm };
            int nbgames = 1;
            string[] msg = { "Entrez un nombre minimum: > ", "Entrez un nombre maximum: > ", "Entrez un nombre de tentatives: > " };
            string[] value_msg = { "minimum", "maximum", "de tentatives" };
            while (!returnMenu)
            {
                for (int i = 0; i < val.Length; i++)
                {
                    val[i] = 0;
                }
                bool canplay = false;
                while (!canplay && !returnMenu)
                {
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    Console.Clear();
                    printCustomMenu(nbgames);
                    try
                    {
                        int i = 0;
                        while (i < val.Length && !returnMenu)
                        {
                            good = false;
                            while (!good && !returnMenu)
                            {
                                try
                                {
                                    Console.Write(msg[i]);
                                    choix = Console.ReadLine();
                                    try
                                    {
                                        if (choix.ToLower().Equals("exit"))
                                        {
                                            throw new ArgumentNullException();
                                        }
                                        else if (choix.ToLower().Equals("random"))
                                        {
                                            bool is_good_random = false;
                                            switch (i)
                                            {
                                                case 0:
                                                    while (!is_good_random)
                                                    {
                                                        try
                                                        {
                                                            val[i] = getRandomValue(new Random().Next(-250, 500), new Random().Next(-100, 1000));
                                                            is_good_random = true;
                                                        }
                                                        catch (Exception) { }
                                                    }
                                                    break;
                                                case 1:
                                                    is_good_random = false;
                                                    while (!is_good_random)
                                                    {
                                                        try
                                                        {
                                                            val[i] = getRandomValue(val[0], val[0] + new Random().Next(2, 1000));
                                                            if ((val[i] - val[0] > 1) && val[1] > val[0])
                                                            {
                                                                is_good_random = true;
                                                            }
                                                        }
                                                        catch (OverflowException) { }
                                                    }
                                                    break;
                                                case 2:
                                                    val[i] = getRandomValue(2, 10);
                                                    break;
                                                default:
                                                    break;
                                           }
                                            clearLine();
                                            string random_message = "Le nombre " + value_msg[i] + " sera " + val[i] + " .";
                                            Console.Write(random_message);
                                            Console.Write("\n");
                                        }
                                        else
                                        {
                                            val[i] = int.Parse(choix);
                                        }
                                    }
                                    catch (NullReferenceException)
                                    {
                                        val[i] = int.Parse(choix);
                                    }
                                    good = true;
                                }
                                catch (FormatException)
                                {
                                    printInvalid();
                                }
                                catch (OverflowException)
                                {
                                    printOverflow();
                                }
                                catch (ArgumentNullException)
                                {
                                    DialogResult c = MessageBox.Show("Retourner au menu principal?", "Retour", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    switch (c)
                                    {
                                        case DialogResult.Yes:
                                            returnMenu = true;// just return to menu
                                            good = true;
                                            nbtm = -1;
                                            break;
                                        case DialogResult.No:
                                        default:
                                            clearLine();
                                            break;
                                    }
                                }
                            }
                            i++;
                        }
                        bool isValidGame = validateCustomGame(val[0], val[1], val[2]);
                        if (nbtm == -1)
                        {
                            return;
                        }
                        else if (isValidGame)
                        {
                            DialogResult c3 = MessageBox.Show("" +
                                "Le nombre minimum sera " + val[0] + ".\n" +
                                "Le nombre maximum sera " + val[1] + ".\n" +
                                "Vous aurez " + val[2] + " tentatives pour trouver la solution.\n" +
                                "\n" +
                                "Confirmer ces choix?", "Choix des options", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            switch (c3)
                            {
                                case DialogResult.Yes:
                                    canplay = true;
                                    game(val[0], val[1], val[2], 4);
                                    nbgames++;
                                    break;
                                case DialogResult.No:
                                default:
                                    canplay = true;
                                    break;
                            }
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                        DialogResult c2 = MessageBox.Show("Rejouer une nouvelle partie personnalisée?", "Partie personnalisée", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        switch (c2)
                        {
                            case DialogResult.Yes:
                                break;
                            case DialogResult.No:
                            default:
                                returnMenu = true;
                                break;
                        }
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        MessageBox.Show("Valeurs invalides.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private static void printCustomMenu(int nbgames)
        {
            Console.Clear();
            Console.Title = title + " - Choix des options";
            Console.WriteLine("NOUVELLE PARTIE PERSONNALISÉE (n' " + nbgames + ")");
            printLine("-");
            Console.WriteLine("" +
                "Choisissez vos options de jeu à votre goût et jouez une partie\n" +
                "personnalisée.\n" +
                "Les valeurs négatives sont acceptées dans ce mode.\n\n" +
                "Pour une valeur aléatoire, tapez \"random\".");
            printLine("-");
        }

        private static bool validateCustomGame(int min, int max, int nbtm)
        {
            if (nbtm > 0)
            {
                if (min < max)
                {
                    if ((max - min) > 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public static void game(int min, int max, int nbtm, int from)
        {
            int magic_rnd = 0, rnd = 0;
            int magic = askForMagicNumber();
            if (magic != -1)
            {
                bool quit = false;
                while (!quit)
                {
                    bool trouve = false;
                    int nbt = 0;
                    int[] rnds = getRandomNumbers(magic, min, max);
                    rnd = rnds[0];
                    magic_rnd = rnds[1];
                    printCountdown((max - min));
                    while (!trouve && nbt < nbtm)
                    {
                        int nbi = 0;
                        string etat = "";
                        try
                        {
                            printTentative(min, max, nbtm, nbt);
                            string nb = Console.ReadLine();
                            nbi = int.Parse(nb);
                            if (nbi >= min && nbi <= max)
                            {
                                if (nbi == rnd)
                                {
                                    trouve = true;
                                    winnes++;
                                    printWinnerMessage(rnd, nbt);
                                    continue;
                                }
                                else if (nbi == magic_rnd)
                                {
                                    winnes++;
                                    trouve = true;
                                    printMagicWinnerMessage(magic, rnd, magic_rnd);
                                    continue;
                                }
                                else
                                {
                                    if (!trouve)
                                    {
                                        if (nbt + 1 != nbtm)
                                        {
                                            printError(rnd, nbi, etat, nbt, nbtm);
                                        }
                                        else
                                        {
                                            looses++;
                                            printLoserMessage(rnd);
                                        }
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
                            printInvalid();
                        }
                        catch (OverflowException)
                        {
                            printOverflow();
                        }
                        catch (ArgumentNullException)
                        {
                            DialogResult c3 = MessageBox.Show("" +
                                "Abandonner?\n" +
                                "Il vous reste " + (nbtm - nbt) + " tentatives pour trouver la solution.", "Abandon", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                            switch (c3)
                            {
                                case DialogResult.Yes:
                                    nbt = nbtm; // allow to break game
                                    break;
                                case DialogResult.No:
                                default:
                                    clearLine();
                                    break;
                            }
                        }
                    }
                    bool quiterr = true;
                    while (quiterr)
                    {
                        quiterr = false;
                        DialogResult c = MessageBox.Show("Rejouer?", "Rejouer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        switch (c)
                        {
                            case DialogResult.Yes:
                                Console.Clear();
                                break;
                            case DialogResult.No:
                                quit = true;
                                break;
                            default:
                                quiterr = true;
                                break;
                        }
                    }
                }
            }
        }

        private static void printTentative(int min, int max, int nbtm, int nbt)
        {
            string tentative_title = title + " - " + (nbtm - nbt) + " ";
            if (nbtm - nbt > 1)
            {
                tentative_title += "tentatives restantes";
            }
            else
            {
                tentative_title += "tentative restante";
            }
            Console.Title = tentative_title;
            Console.Write("Entrez un nombre entre " + min + " et " + max + " ("+(nbt+1)+"/"+nbtm+"): > ");
        }

        private static void printOverflow()
        {
            MessageBox.Show("La valeur tapée dépasse celle autorisée par le programme.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            clearLine();
        }

        private static void printInvalid()
        {
            MessageBox.Show("Valeur invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            clearLine();
        }

        private static void printError(int rnd, int nbi, string etat, int nbt, int nbtm)
        {
            if (nbi < rnd)
            // plus petit
            {
                etat = "plus grand";
            }
            else
            // plus grand
            {
                etat = "plus petit";
            }
            clearLine();
            Console.WriteLine(""+(nbt+1)+"/"+nbtm+" : Le nombre recherché est " + etat + " que " + nbi + ".");
        }

        private static void printLoserMessage(int rnd)
        {
            Console.Title = title + " - Vous avez perdu!";
            MessageBox.Show("" +
                "Meilleure chance la prochaine fois!\n" +
                "\n" +
                "Le nombre était " + rnd + ".\n" +
                "", "Vous avez perdu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void printMagicWinnerMessage(int magic, int rnd, int magic_rnd)
        {
            if (magic == 1)
            {
                MessageBox.Show("" +
                    "Bravo, vous avez trouvé le nombre magique!\n" +
                    "\n" +
                    "Le nombre était " + rnd + ".\n" +
                    "Le nombre magique était " + magic_rnd + "." +
                    "", "Nombre magique trouvé", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Le nombre est " + rnd + ". Tricheur!!!");
            }
        }

        private static void printUniverse()
        {
            Console.Title = "42";
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    Console.Write("42");
                }
                Console.WriteLine();
            }
        }

        private static void printWinnerMessage(int rnd, int nbt)
        {
            string message = "";
            Console.Title = title + " - Vous avez gagné!";
            message += "" +
                "Félicitations! Vous l'avez trouvé après " + (nbt + 1) + " ";
            if (nbt + 1 > 1)
            {
                message += "tentatives!!!";
            }
            else
            {
                message += "tentative!!!";
            }
            MessageBox.Show(message + "\n\n" +
                "Le nombre était " + rnd + ".", "Vous avez gagné!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static int[] getRandomNumbers(int magic, int min, int max)
        {
            int rnd = 0, magic_rnd = 0;
            if (magic == 1)
            {
                do
                {
                    rnd = getRandomValue(min, max);
                    magic_rnd = getRandomValue(min, max);
                } while (magic_rnd == rnd);
            }
            else
            {
                rnd = getRandomValue(min, max);
                magic_rnd = -999999;
            }
            int[] rnds = { rnd, magic_rnd };
            return rnds;
        }

        private static int askForMagicNumber()
        {
            DialogResult isMagic = MessageBox.Show("" +
                "Jouer avec un nombre magique?\n" +
                "Le nombre magique est un entier supplémentaire généré aléatoirement par le jeu. Il vous donne une chance supplémentaire de gagner; cependant, vous n'avez aucun indice pour le trouver." +
                "", "Nombre magique", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            switch (isMagic)
            {
                case DialogResult.Yes:
                    return 1;
                case DialogResult.No:
                default:
                    return 0;
                case DialogResult.Cancel:
                    return -1;
            }
        }

        private static void printCountdown(int nb)
        {
            Console.Clear();
            Console.SetWindowSize(64,4);
            Console.Title = title;
            Console.Write("Chargement...\n\n");
            printLine("░");
            for (int i = 0; i <= 62; ++i)
            {
                Console.SetCursorPosition(i, 2);
                Console.Write("█");
                Thread.Sleep(getRandomValue(0,200));
            }
            Console.Write("\n");
            Console.SetBufferSize(64, 100);
            Console.SetWindowSize(64, 20);
            Console.Clear();
        }
        private static int getRandomValue(int min, int max)
        {
            int rnd = 0;
            int total_d = 0;
            double total = 0;
            int[] rnds = new int[new Random().Next(3, 10)];
            for (int i = 0; i < rnds.Length; i++)
            {
                rnds[i] = new Random().Next(min, max);
                total += rnds[i];
            }
            total_d = (int)Math.Round(total);
            rnd = total_d / rnds.Length;
            return rnd;
        }
        private static void exit()
        {
            Console.ResetColor();
            Console.SetWindowSize(windowX, windowY);
            Console.SetBufferSize(windowX, bufferY);
            Console.Clear();
            Environment.Exit(0);
        }
    }
}