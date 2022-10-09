using System.Collections.Generic;
using System.Text;

namespace InviteService.Services
{
    public class TranslitService : ITranslitService
    {
        private const string translitDictionary = "а:a,б:b,в:v,г:g,д:d,е:e,ё:jo,ж:zh,з:z,и:i,й:jj,к:k,л:l,м:m,н:n,о:o,п:p,р:r,с:s,т:t,у:u,ф:f,х:kh,ц:c,ч:ch,ш:sh,щ:shh,ъ:\",ы:y,ь:',э:eh,ю:ju,я:ja";

        public string Translit(string message)
        {
            var translitSymbols = new Dictionary<string, string>();
            foreach (string item in translitDictionary.Split(","))
            {
                string[] symbols = item.Split(":");
                translitSymbols.Add(symbols[0].ToLower(), symbols[1].ToLower());
                translitSymbols.Add(symbols[0].ToUpper(), symbols[1].ToUpper());
            }
            var result = new StringBuilder();
            foreach (var symbol in message)
            {
                var symbolString = symbol.ToString();
                string translitedSymbol;
                if (translitSymbols.TryGetValue(symbolString, out translitedSymbol))
                {
                    result.Append(translitedSymbol);
                }
                else
                {
                    result.Append(symbolString);
                }

            }
            return result.ToString();
        }
    }
}
