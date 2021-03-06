﻿using System;
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
                    "                    LA LOTERIE ALTERNATIVE      version 0.9.50 ");
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
                    "5: ALÉATOIRE       :   Jouer une partie aléatoire              \n" +
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
                    MessageBox.Show("Choix invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                catch (OverflowException)
                {
                    MessageBox.Show("La valeur tapée dépasse celle supportée par le programme.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        case 0:
                            throw new ArgumentNullException();
                        default:
                            throw new FormatException();
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("" +
                        "Choix invalide.\n" +
                        "\n" +
                        "Choix possibles:\n" +
                        "   1: Facile\n" +
                        "   2: Intermédiaire\n" +
                        "   3: Difficile\n" +
                        "   0: Retour au menu principal\n" +
                        "", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                catch (ArgumentNullException)
                {
                    DialogResult c2 = MessageBox.Show("Retourner au menu principal?", "Retour", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    switch (c2)
                    {
                        case DialogResult.Yes:
                            good = true; // just return to menu
                            break;
                        case DialogResult.No:
                        default:
                            break;
                    }
                }
                catch (OverflowException)
                {
                    MessageBox.Show("La valeur tapée dépasse celle autorisée par le programme.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        MessageBox.Show("Valeur invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    catch (OverflowException)
                    {
                        MessageBox.Show("La valeur tapée dépasse celle autorisée par le programme.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (ArgumentNullException)
                    {
                        DialogResult c = MessageBox.Show("Retourner au menu principal?", "Retour", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        switch (c)
                        {
                            case DialogResult.Yes:
                                returnMenu = true;// just return to menu
                                break;
                            case DialogResult.No:
                            default:
                                break;
                        }
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
                                        MessageBox.Show("" +
                                            "Félicitations! Vous l'avez trouvé après " + (nbt + 1) + " tentative(s)!!!\n" +
                                            "\n" +
                                            "Le nombre était " + valeur + ".", "Vous avez gagné!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        continue;
                                    }
                                    else if (nbi == -999)
                                    {
                                        trouve = true;
                                        MessageBox.Show("Le nombre est " + valeur + ".");
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
                                MessageBox.Show("Valeur invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            catch (OverflowException)
                            {
                                MessageBox.Show("La valeur tapée dépasse celle autorisée par le programme.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            catch (ArgumentNullException)
                            {
                                DialogResult c3 = MessageBox.Show("Abandonner?","Abandon",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                                switch (c3)
                                {
                                    case DialogResult.Yes:
                                        nbt = nbtm; // allow to break game
                                        break;
                                    case DialogResult.No:
                                    default:
                                        break;
                                }
                            }
                        }
                        if (!trouve && nbtm != 9999)
                        {
                            Console.Title = title + " - Vous avez perdu!";
                            MessageBox.Show("" +
                                "Meilleure chance la prochaine fois!\n" +
                                "\n" +
                                "Le nombre était " + valeur + "." +
                                "", "Vous avez perdu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        bool quiterr = true;
                        if (nbtm != 9999)
                        {
                            while (quiterr)
                            {
                                try
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
                                catch (NullReferenceException)
                                {
                                    DialogResult c2 = MessageBox.Show("Abandonner?", "Abandon", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    switch (c2)
                                    {
                                        case DialogResult.Yes:
                                            break;
                                        case DialogResult.No:
                                        default:
                                            quiterr = true;
                                            break;
                                    }
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
                MessageBox.Show("Valeurs invalides.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
