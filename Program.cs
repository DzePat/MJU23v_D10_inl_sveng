using System.Runtime.ExceptionServices;
using System.IO;

namespace MJU23v_D10_inl_sveng
{
    internal class Program
    {
        static List<SweEngGloss> dictionary;
        class SweEngGloss
        {
            public string word_swe, word_eng;
            public SweEngGloss(string word_swe, string word_eng)
            {
                this.word_swe = word_swe; this.word_eng = word_eng;
            }
            public SweEngGloss(string line)
            {
                string[] words = line.Split('|');
                this.word_swe = words[0]; this.word_eng = words[1];
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the dictionary app!\nType 'help' for help!");
            do
            {
                
                Console.Write("> ");
                string[] argument = Console.ReadLine().Split();
                string command = argument[0];
                if (command == "quit")
                {
                    Console.WriteLine("Goodbye!");
                }
                else if (command == "help")
                {
                    help();
                }
                else if (command == "load")
                {
                    LoadFile(argument);
                }

                else if (command == "list")
                {
                    dictlist();

                }
                // FIXME: catch if the file is not loaded
                else if (command == "new")
                {
                    addWords(argument);
                }


                else if (command == "delete")
                {
                    deleteWord(argument);

                }
                // FIXME: catch error if the file is not loaded
                else if (command == "translate")
                {
                    translate(argument);
                }

                else
                {
                    Console.WriteLine($"Unknown command: '{command}'");
                }
            }
            while (true);
        }

        private static void LoadFile(string[] argument)
        {
            string defaultFile = "..\\..\\..\\dict\\sweeng.lis";
            if (argument.Length == 2)
            {
                string dictpath = "..\\..\\..\\dict\\";
                string fullpath = dictpath + argument[1];
                getFile(fullpath);
            }
            else if (argument.Length == 1)
                getFile(defaultFile);
        }

        private static void deleteWord(string[] argument)
        {
            try
            {
                if (argument.Length == 3)
                {
                    int index = -1;
                    for (int i = 0; i < dictionary.Count; i++)
                    {
                        SweEngGloss gloss = dictionary[i];
                        if (gloss.word_swe == argument[1] && gloss.word_eng == argument[2])
                            index = i;
                    }
                    dictionary.RemoveAt(index);
                }
                else if (argument.Length == 1)
                {
                    Console.WriteLine("Write word in Swedish: ");
                    string sweWord = Console.ReadLine();
                    Console.Write("Write word in English: ");
                    string engWord = Console.ReadLine();
                    int index = -1;
                    for (int i = 0; i < dictionary.Count; i++)
                    {
                        SweEngGloss gloss = dictionary[i];
                        if (gloss.word_swe == sweWord && gloss.word_eng == engWord)
                            index = i;
                    }
                    dictionary.RemoveAt(index);
                }
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("please load a file first");
            }
            catch (System.ArgumentOutOfRangeException)
            {
                Console.WriteLine("selected words are not in the dictionary");
            }
        }

        private static void translate(string[] argument)
        {
            if (argument.Length == 2)
            {
                transword(argument[1]);
            }
            else if (argument.Length == 1)
            {
                Console.WriteLine("Write word to be translated: ");
                string wordInput = Console.ReadLine();
                transword(wordInput);
            }
        }

        private static void addWords(string[] argument)
        {
            try
            {
                if (argument.Length == 3)
                {
                    dictionary.Add(new SweEngGloss(argument[1], argument[2]));
                }
                else if (argument.Length == 1)
                {
                    Console.WriteLine("Write word in Swedish: ");
                    string sweWord = Console.ReadLine();
                    Console.Write("Write word in English: ");
                    string engWord = Console.ReadLine();
                    dictionary.Add(new SweEngGloss(sweWord, engWord));
                }
                else { Console.WriteLine("Type 'new' or 'new /s/ /e/' to add a word to the dictionary"); }
            }
            catch { Console.WriteLine("please load a file first"); }
        }

        private static void dictlist()
        {
            try
            {
                foreach (SweEngGloss gloss in dictionary)
                {
                    Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}");
                }
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("please load a file first");
            }
        }

        private static void getFile(string FilePath)
        {
            try
            {
                using (StreamReader sr = new StreamReader(FilePath))
                {
                    dictionary = new List<SweEngGloss>(); // Empty it!
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        SweEngGloss gloss = new SweEngGloss(line);
                        dictionary.Add(gloss);
                        line = sr.ReadLine();
                    }
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("could not find the file");
            }
        }

        private static void transword(string word)
        {
            foreach (SweEngGloss gloss in dictionary)
            {
                if (gloss.word_swe == word)
                {
                    Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                    break;
                }
                if (gloss.word_eng == word)
                {
                    Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                    break;
                }
            }
        }

        public static void help()
        {
            Console.WriteLine("Available Commands: ");
            Console.WriteLine("  load           - load a list from the file sweeng.lis");
            Console.WriteLine("  load /file/    - load a list from the /file/");
            Console.WriteLine("  list           - list all the words in the dictionary");
            Console.WriteLine("  new            - Create new translation");
            Console.WriteLine("  new /s/ /e/    - Create new translation /swedish/ /english/ ");
            Console.WriteLine("  translate      - translate a word from swedish to english or english to swedish");
            Console.WriteLine("  translate /w/  - translate selected word to english or swedish depending on the word");
            Console.WriteLine("  delete         - delete a selected words from the dictionary");
            Console.WriteLine("  delete /s/ /e/ - delete swedish english word from the dictionary");
            Console.WriteLine("  quit           - quit the program");
            Console.WriteLine();
        }
    }
}