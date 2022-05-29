using KaizenCaseStudyOne.Business.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaizenCaseStudyOne.Business.Utils
{
    internal static class Extensions
    {
        public static char GetCalculatedLastCharacter(this string codeExceptLastCharacter)
        {
            var allowedCharacters = DefaultValues.AllowedCharacters;
            var calculatedSecretSum = codeExceptLastCharacter
               .Select(I => I + codeExceptLastCharacter.IndexOf(I) + Array.IndexOf(allowedCharacters, I))
               .Sum();

            var lastCharacter = calculatedSecretSum % allowedCharacters.Length;
            
            return char.Parse(allowedCharacters[lastCharacter]);
        }
    }
}
