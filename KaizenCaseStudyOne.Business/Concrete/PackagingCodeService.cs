using KaizenCaseStudyOne.Business.Abstract;
using KaizenCaseStudyOne.Business.Constants;
using KaizenCaseStudyOne.Business.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaizenCaseStudyOne.Business.Concrete
{
    public class PackagingCodeService : IPackagingCodeService
    {
        public string GeneratePackagingCode()
        {
            const int packagingCodeLength = 8;
            var allowedCharacters = DefaultValues.AllowedCharacters;

            var random = new Random();
            var codeBuilder = new StringBuilder();

            // Generate 6 characters randomly
            for (var i = 0; i < packagingCodeLength - 1; i++)
            {
                codeBuilder.Append(allowedCharacters[random.Next(0, allowedCharacters.Length)]);
            }

            var codeExceptLastCharacter = codeBuilder.ToString();

            // This variable is used to prevent collisions while deciding last character caused by order of characters in codeExceptLastCharacter.
            // The calculation involves allowedCharacters to provide a secret that belongs to server.
            // The uncompleted packaging code is used to provide a secret that belongs to each packaging code dynamically.
            // Modulus operation is used to map last character

            var calculatedSecretSum = codeExceptLastCharacter
                .Select(I => I + codeExceptLastCharacter.IndexOf(I) + Array.IndexOf(allowedCharacters, I))
                .Sum();

            var lastCharacter = codeExceptLastCharacter.GetCalculatedLastCharacter();
            
            // Last character added to packaging code that will be used in verification process.

            codeBuilder.Append(lastCharacter);

            return codeBuilder.ToString();
        }

        public bool ValidatePackagingCode(string packagingCode)
        {
            if (string.IsNullOrEmpty(packagingCode))
            {
                return false;
            }

            var lastCharacter = packagingCode.Last();
            var packagingCodeExceptLastCharacter = packagingCode.Substring(0, packagingCode.Length - 1);

            var calculatedLastCharacter = packagingCodeExceptLastCharacter.GetCalculatedLastCharacter();
            return calculatedLastCharacter.Equals(lastCharacter);

        }
    }
}
