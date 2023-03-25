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
        // FIXME: if two arguments occur and the file can not be found catch exception
        // TODO: add remove doubles in load to make the code look cleaner
        static void Main(string[] args)
        {
            string defaultFile = "..\\..\\..\\dict\\sweeng.lis";
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
                else if(command == "help")
                {
                    help();
                }
                else if (command == "load")
                {
                    if(argument.Length == 2)
                    {
                        using (StreamReader sr = new StreamReader(argument[1]))
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
                    else if(argument.Length == 1)
                    {
                        using (StreamReader sr = new StreamReader(defaultFile))
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
                }
                // FIXME: if dictionary is not loaded catch null exception
                else if (command == "list")
                {
                    foreach(SweEngGloss gloss in dictionary)
                    {
                        Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}");
                    }
                }
                // TODO: if arguments only 2 give a proper message
                // FIXME: catch if the file is not loaded
                else if (command == "new")
                {
                    if (argument.Length == 3)
                    {
                        dictionary.Add(new SweEngGloss(argument[1], argument[2]));
                    }
                    else if(argument.Length == 1)
                    {
                        Console.WriteLine("Write word in Swedish: ");
                        string s = Console.ReadLine();
                        Console.Write("Write word in English: ");
                        string e = Console.ReadLine();
                        dictionary.Add(new SweEngGloss(s, e));
                    }
                    else { Console.WriteLine("Type 'new' or 'new /s/ /e/' to add a word to the dictionary"); }
                }
                // FIXME: if the word does not exist try catch  null exception exception
                else if (command == "delete")
                {
                    if (argument.Length == 3)
                    {
                        int index = -1;
                        for (int i = 0; i < dictionary.Count; i++) {
                            SweEngGloss gloss = dictionary[i];
                            if (gloss.word_swe == argument[1] && gloss.word_eng == argument[2])
                                index = i;
                        }
                        dictionary.RemoveAt(index);
                    }
                    else if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word in Swedish: ");
                        string s = Console.ReadLine();
                        Console.Write("Write word in English: ");
                        string e = Console.ReadLine();
                        int index = -1;
                        for (int i = 0; i < dictionary.Count; i++)
                        {
                            SweEngGloss gloss = dictionary[i];
                            if (gloss.word_swe == s && gloss.word_eng == e)
                                index = i;
                        }
                        dictionary.RemoveAt(index);
                    }
                }
                // FIXME: catch error if the file is not loaded
                else if (command == "translate")
                {
                    if (argument.Length == 2)
                    {
                            transword(argument[1]);
                    }
                    else if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word to be translated: ");
                        string s = Console.ReadLine();
                        transword(s);
                    }
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{command}'");
                }
            }
            while (true);
        }

        private static void transword(string s)
        {
            foreach (SweEngGloss gloss in dictionary)
            {
                if (gloss.word_swe == s)
                {
                    Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                    break;
                }
                if (gloss.word_eng == s)
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