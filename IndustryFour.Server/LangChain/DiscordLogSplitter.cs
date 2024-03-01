using LangChain.Splitters.Text;
using System.Text.RegularExpressions;

namespace IndustryFour.Server.LangChain
{
    internal class DiscordLogSplitter : TextSplitter
    {
        /*
         * Mark O'Donovan — 02/15/2024 10:28 AM
         * I think that approach at least gives a definition.
         * I think this group should be able to simply and consistently answer what is a UNS.
         * RickBullotta — 02/15/2024 10:28 AM
         * You're telling me you didn't get yours yet?
         */
        public override IReadOnlyList<string> SplitText(string text)
        {
            string pattern = @"^.* \u2014 \d{2}/\d{2}/\d{4} \d{1,2}:\d{2} [AP]M\s*$";
            string[] chunks = Regex.Split(text, pattern, RegexOptions.Multiline);

            foreach (var chunk in chunks)
            {
                Console.WriteLine($"--->>>>>\r\n{chunk}");
            }

            return chunks;
        }
    }
}