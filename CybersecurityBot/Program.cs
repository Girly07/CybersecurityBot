using System;
using System.Media; // ✅ for your voice files
using System.Threading;

namespace CybersecurityBot
{
    interface IResponder
    {
        string GetResponse(string userInput);
    }

    abstract class BaseResponder : IResponder
    {
        public abstract string GetResponse(string userInput);

        protected string NormalizeInput(string input)
        {
            if (input == null)
                return "";

            return input.Trim().ToLower();
        }
    }

    class CyberSecurityResponder : BaseResponder
    {
        public override string GetResponse(string userInput)
        {
            string input = NormalizeInput(userInput);

            if (string.IsNullOrWhiteSpace(input))
                return "You entered nothing. Please type something.";

            if (input.Contains("how are you"))
                return "I'm doing great! I'm here to help you stay safe online.";

            else if (input.Contains("purpose"))
                return "My purpose is to educate you about cybersecurity risks.";

            else if (input.Contains("what can i ask"))
                return "You can ask about passwords, phishing, safe browsing, privacy and hackers.";

            else if (input.Contains("password"))
                return "Use strong passwords with letters, numbers, and symbols. Never share them.";

            else if (input.Contains("phishing"))
                return "Phishing is when scammers trick you. Never click suspicious links.";

            else if (input.Contains("safe browsing") || input.Contains("browse"))
                return "Always check websites and avoid unsafe downloads.";

            else if (input.Contains("hack"))
                return "Hackers try to access accounts. Keep your info private.";

            else if (input.Contains("virus"))
                return "Viruses harm devices. Avoid unknown files and use antivirus.";

            else
                return "Try asking about passwords, phishing, or safe browsing.";
        }
    }

    class User
    {
        public string Name { get; set; }

        public bool IsValidName()
        {
            return !string.IsNullOrWhiteSpace(Name);
        }
    }

    static class Utils
    {
        // 🎤 PLAY YOUR OWN VOICE FILE
        public static void PlayVoice(string file)
        {
            try
            {
                SoundPlayer player = new SoundPlayer(file);
                player.PlaySync();
            }
            catch
            {
                Console.WriteLine("Voice file not found: " + file);
            }
        }

        public static void ShowAsciiAnimated()
        {
            string[] frames =
            {
@"
         CYBER BOT 

         .-""""-.
        / -   -  \
       |  o   o  |
       |    ^    |
        \  ---  /
         '-___-'
",
@"
         CYBER BOT 

         .-""""-.
        / -   -  \
       |  -   -  |
       |    ^    |
        \  ---  /
         '-___-'
"
            };

            for (int i = 0; i < 4; i++)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.Cyan;

                Console.Clear();
                Console.WriteLine(frames[i % 2]);

                Thread.Sleep(400);
            }
        }

        public static void ShowHeader()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine(@"
========================================
   CYBERSECURITY AWARENESS CHATBOT
========================================
");
        }

        public static void TypeEffect(string text)
        {
            Console.ForegroundColor = ConsoleColor.White;

            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(20);
            }
            Console.WriteLine();
        }

        public static void ShowLoading()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.Write("Bot is thinking");
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(400);
                Console.Write(".");
            }
            Console.WriteLine();
        }

        public static void ShowMenu()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("\n1. Password Safety");
            Console.WriteLine("2. Phishing");
            Console.WriteLine("3. Safe Browsing");
            Console.WriteLine("4. What can I ask?");
            Console.WriteLine("5. Ask your own question");
            Console.WriteLine("6. Exit");
        }
    }

    class ChatBot
    {
        private IResponder responder;
        private User user;

        public ChatBot(IResponder responder)
        {
            this.responder = responder;
            user = new User();
        }

        public void Start()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

            Utils.ShowAsciiAnimated();
            Utils.ShowHeader();

            // 🎤 YOUR VOICE GREETING
            Utils.PlayVoice("greeting.wav");

            Console.Write("Enter your name: ");
            user.Name = Console.ReadLine();

            if (!user.IsValidName())
                user.Name = "Guest";

            string welcome = "Welcome " + user.Name;
            Utils.TypeEffect(welcome);

            RunChat();
        }

        private void RunChat()
        {
            while (true)
            {
                Utils.ShowMenu();

                Console.Write("\nChoice: ");
                string choice = Console.ReadLine();

                string input = "";

                switch (choice)
                {
                    case "1": input = "password"; break;
                    case "2": input = "phishing"; break;
                    case "3": input = "safe browsing"; break;
                    case "4": input = "what can i ask"; break;

                    case "5":
                        Console.Write("Ask: ");
                        input = Console.ReadLine();
                        break;

                    case "6":
                        Utils.PlayVoice("goodbye.wav"); // 🎤 YOUR VOICE
                        Console.WriteLine("Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid option.");
                        continue;
                }

                Utils.ShowLoading();

                string response = responder.GetResponse(input);

                Utils.TypeEffect("Bot: " + response);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ChatBot bot = new ChatBot(new CyberSecurityResponder());
            bot.Start();
        }
    }
}