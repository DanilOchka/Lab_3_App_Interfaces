using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3_App.Convert
{
    public class CurrencyConverter
    {
        private Dictionary<string, double> exchangeRates = new Dictionary<string, double>
    {
        { "Гривні ₴", 1 },  // Курси до гривні
        { "Долари $", 0.0251 },
        { "Євро €", 0.0238 }
        // додайте інші валюти тут з їх курсами до гривні
    };

        public double Convert(double amount, string fromCurrency, string toCurrency)
        {
            if (exchangeRates.ContainsKey(fromCurrency) && exchangeRates.ContainsKey(toCurrency))
            {
                double fromRate = exchangeRates[fromCurrency];
                double toRate = exchangeRates[toCurrency];
                double amountInUah = amount / fromRate; // конвертуємо вхідну суму до гривні
                double result = amountInUah * toRate; // конвертуємо суму з гривні до вихідної валюти
                return Math.Round(result, 2); // Округлюємо результат до двох знаків після коми
            }
            else
            {
                throw new ArgumentException("Невідома валюта");
            }
        }
    }
}
