
using System;
using System.CodeDom.Compiler;

namespace JPFMS_BankKeyboard.Model
{
    public static class KeyGenerator
    {
        public static Keys[] Generate()
        {
            Random random = new Random();

            var shuffledNumbers = Enumerable.Range(0, 10)
            .OrderBy(_ => random.Next())
            .ToList();

            var pairs = new List<Keys>();

            for (int i = 0; i < shuffledNumbers.Count; i += 2)
            {
                if (i + 1 < shuffledNumbers.Count) // Garantindo que há um par
                {
                    int a = shuffledNumbers[i];
                    int b = shuffledNumbers[i + 1];

                    pairs.Add(new Keys { N1 = Math.Min(a, b), N2 = Math.Max(a, b) });
                }
            }

            return pairs.ToArray();
        }
    }
}
