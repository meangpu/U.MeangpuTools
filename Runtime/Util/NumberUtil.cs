namespace Meangpu.Util
{
    public static class NumberUtil
    {
        public static string ConvertToAbbreviateNumber(int number)
        {
            switch (number)
            {
                case < 1000:
                    return number.ToString();
                case < 10000:
                    return (number / 1000f).ToString("0.#") + "K";
                case < 1000000:
                    return (number / 1000).ToString() + "K";
                case < 10000000:
                    return (number / 1000000f).ToString("0.#") + "M";
                case >= 10000000:
                    return (number / 1000000).ToString() + "M";
            }
        }
    }
}
