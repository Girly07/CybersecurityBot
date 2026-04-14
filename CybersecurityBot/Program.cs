using System;
using System.Media; // Used to play .wav audio files
using System.Threading; // Used for delays (animation & typing effect)

namespace CybersecurityBot
{
    // Interface that defines chatbot response behavior
    interface IResponder
    {
        string GetResponse(string userInput);
    }

    // Base class that provides shared functionality
    abstract class BaseResponder : IResponder
    {
        // Abstract method to be implemented by child classes
        public abstract string GetResponse(string userInput);

        // Normalizes user input (removes spaces & converts to lowercase)
        protected string NormalizeInput(string input)
        {
            if (input == null)
                return "";

            return input.Trim().ToLower();
        }
    }

    // Main chatbot logic class (handles cybersecurity responses)
    class CyberSecurityResponder : BaseResponder
    {
        public override string GetResponse(string userInput)
        {
            string input = NormalizeInput(userInput);

            // Handle empty input
            if (string.IsNullOrWhiteSpace(input))
                return "You entered nothing. Please type something.";

            // Respond to different cybersecurity-related keywords
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

            // Default response if no keyword is matched
            else
                return "Try asking about passwords, phishing, or safe browsing.";
        }
    }

    // Represents the user interacting with the chatbot
    class User
    {
        public string Name { get; set; }

        // Validates that the user entered a name
        public bool IsValidName()
        {
            return !string.IsNullOrWhiteSpace(Name);
        }
    }

    // Utility class for reusable helper methods
    static class Utils
    {
        // Plays a .wav voice file
        public static void PlayVoice(string file)
        {
            try
            {
                SoundPlayer player = new SoundPlayer(file);
                player.PlaySync(); // Plays audio synchronously (waits until finished)
            }
            catch
            {
                // Error handling if file is missing
                Console.WriteLine("Voice file not found: " + file);
            }
        }

        // Displays animated ASCII chatbot
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

            // Loop through frames to create animation effect
            for (int i = 0; i < 4; i++)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.Cyan;

                Console.Clear();
                Console.WriteLine(frames[i % 2]);

                Thread.Sleep(400); // Pause for animation timing
            }
        }

        // Displays application header
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

        // Simulates typing effect for chatbot responses
        public static void TypeEffect(string text)
        {
            Console.ForegroundColor = ConsoleColor.White;

            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(20); // Delay between each character
            }
            Console.WriteLine();
        }

        // Displays "thinking" loading animation
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

        // Displays menu options for the user
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

    // Main chatbot controller class
    class ChatBot
    {
        private IResponder responder; // Handles responses
        private User user; // Stores user info

        public ChatBot(IResponder responder)
        {
            this.responder = responder;
            user = new User();
        }

        // Starts the chatbot application
        public void Start()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

            Utils.ShowAsciiAnimated(); // Show animated bot
            Utils.ShowHeader(); // Show title

            Utils.PlayVoice("greeting.wav"); // Play greeting audio

            // Ask user for their name
            Console.Write("Enter your name: ");
            user.Name = Console.ReadLine();

            // Default to "Guest" if name is empty
            if (!user.IsValidName())
                user.Name = "Guest";

            // Welcome message
            string welcome = "Welcome " + user.Name;
            Utils.TypeEffect(welcome);

            RunChat(); // Start interaction loop
        }

        // Handles the main chatbot loop
        private void RunChat()
        {
            while (true)
            {
                Utils.ShowMenu();

                Console.Write("\nChoice: ");
                string choice = Console.ReadLine();

                string input = "";

                // Menu selection handling
                switch (choice)
                {
                    case "1": input = "password"; break;
                    case "2": input = "phishing"; break;
                    case "3": input = "safe browsing"; break;
                    case "4": input = "what can i ask"; break;

                    case "5":
                        Console.Write("Ask: ");
                        input = Console.ReadLine(); // User custom question
                        break;

                    case "6":
                        Utils.PlayVoice("goodbye.wav"); // Play exit audio
                        Console.WriteLine("Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid option.");
                        continue;
                }

                Utils.ShowLoading(); // Show thinking animation

                // Get chatbot response
                string response = responder.GetResponse(input);

                // Display response with typing effect
                Utils.TypeEffect("Bot: " + response);
            }
        }
    }

    // Program entry point
    class Program
    {
        static void Main(string[] args)
        {
            // Create chatbot instance and start it
            ChatBot bot = new ChatBot(new CyberSecurityResponder());
            bot.Start();
        }
    }
}