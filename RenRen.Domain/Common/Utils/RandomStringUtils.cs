using System;

namespace RenRen.Domain.Common.Utils
{
    public class RandomStringUtils
    {
        private static readonly Random RANDOM = new Random();

        public RandomStringUtils()
        {

        }

        public static string Random(int count)
        {
            return Random(count, false, false);
        }

        public static string RandomAscii(int count)
        {
            return Random(count, 32, 127, false, false);
        }

        public static string RandomAlphabetic(int count)
        {
            return Random(count, true, false);
        }

        public static string RandomAlphanumeric(int count)
        {
            return Random(count, true, true);
        }

        public static string RandomNumeric(int count)
        {
            return Random(count, false, true);
        }

        public static string Random(int count, bool letters, bool numbers)
        {
            return Random(count, 0, 0, letters, numbers);
        }

        public static string Random(int count, int start, int end, bool letters, bool numbers)
        {
            return Random(count, start, end, letters, numbers, null, RANDOM);
        }

        public static string Random(int count, int start, int end, bool letters, bool numbers, char[] chars)
        {
            return Random(count, start, end, letters, numbers, chars, RANDOM);
        }

        public static string Random(int count, int start, int end, bool letters, bool numbers, char[] chars, Random random)
        {
            if (count == 0)
            {
                return "";
            }
            else if (count < 0)
            {
                throw new Exception("Requested random string length " + count + " is less than 0.");
            }
            else
            {
                if (start == 0 && end == 0)
                {
                    end = 123;
                    start = 32;
                    if (!letters && !numbers)
                    {
                        start = 0;
                        end = 2147483647;
                    }
                }

                char[] buffer = new char[count];
                int gap = end - start;

                while (true)
                {
                    while (true)
                    {
                        while (count-- != 0)
                        {
                            char ch;
                            if (chars == null)
                            {
                                ch = (char)(random.Next(0, gap) + start);
                            }
                            else
                            {
                                ch = chars[random.Next(0, gap) + start];
                            }

                            if (letters && char.IsLetter(ch) || numbers && char.IsDigit(ch) || !letters && !numbers)
                            {
                                if (ch >= '\udc00' && ch <= '\udfff')
                                {
                                    if (count == 0)
                                    {
                                        ++count;
                                    }
                                    else
                                    {
                                        buffer[count] = ch;
                                        --count;
                                        buffer[count] = (char)('\ud800' + random.Next(0, 128));
                                    }
                                }
                                else if (ch >= '\ud800' && ch <= '\udb7f')
                                {
                                    if (count == 0)
                                    {
                                        ++count;
                                    }
                                    else
                                    {
                                        buffer[count] = (char)('\udc00' + random.Next(0,128));
                                        --count;
                                        buffer[count] = ch;
                                    }
                                }
                                else if (ch >= '\udb80' && ch <= '\udbff')
                                {
                                    ++count;
                                }
                                else
                                {
                                    buffer[count] = ch;
                                }
                            }
                            else
                            {
                                ++count;
                            }
                        }

                        return new string(buffer);
                    }
                }
            }
        }

        public static string Random(int count, string chars)
        {
            return chars == null ? Random(count, 0, 0, false, false, null, RANDOM) : Random(count, chars.ToCharArray());
        }

        public static string Random(int count, char[] chars)
        {
            return chars == null ? Random(count, 0, 0, false, false, null, RANDOM) : Random(count, 0, chars.Length, false, false, chars, RANDOM);
        }
    }
}
