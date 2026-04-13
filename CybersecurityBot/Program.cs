using System;
using System.Media; // for audio greeting
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
                return "I'm doing great! I'm here to help you stay safe online and protect your personal information.";

            else if (input.Contains("purpose"))
                return "My purpose is to educate you about cybersecurity risks like hacking, phishing, and unsafe browsing habits.";

            else if (input.Contains("what can i ask"))
                return "You can ask about:\n- Password safety\n- Phishing scams\n- Safe browsing\n- Online privacy\n- Hackers and viruses";

            else if (input.Contains("password"))
                return "PASSWORD SAFETY:\n\n  Use 8–12 characters\n✔ Mix uppercase, lowercase, numbers, symbols\n Avoid names/birthdays\n Do NOT share passwords\n Use different passwords\n Change passwords regularly";

            else if (input.Contains("phishing"))
                return "PHISHING:\n\nPhishing is when scammers trick you.\n\n Signs:\n- Fake emails\n- Urgent messages\n- Suspicious links\n\n Verify sender\n Don't click unknown links\n Never share passwords";

            else if (input.Contains("safe browsing") || input.Contains("browse"))
                return "SAFE BROWSING:\n\n Check URLs (https://)\n Avoid downloads\n Ignore pop-ups\n Use antivirus";

            else if (input.Contains("hack"))
                return "Hackers try to access your accounts. Use strong passwords and never share info.";

            else if (input.Contains("virus"))
                return "Viruses can damage your device. Avoid unknown downloads and use antivirus.";

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
        public static void PlayGreeting()
        {
            try
            {
                SoundPlayer player = new SoundPlayer("greeting.wav");
                player.PlaySync();
            }
            catch
            {
                Console.WriteLine("Audio file not found.");
            }
        }

        public static void ShowAsciiAnimated()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

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
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Blue; 
                Console.ForegroundColor = ConsoleColor.Cyan;

                Console.WriteLine(frames[i % 2]);
                Thread.Sleep(400);
            }
        }

        public static void ShowHeader()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine(@"
====================================================
      CYBERSECURITY AWARENESS CHATBOT
====================================================
         Stay Safe | Stay Smart 
====================================================
");
        }

        public static void TypeEffect(string text)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(20);
            }
            Console.WriteLine();
        }

        public static void ShowLoading()
        {
            Console.Write("Bot is thinking");

            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(400);
                Console.Write(".");
            }

            Console.WriteLine();
        }
    }
}
