using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day4
    {
        public static void Execute()
        {
            List<Dictionary<string, string>> passports = new List<Dictionary<string, string>>();
            var passport = new Dictionary<string, string>();

            var lines = File.ReadAllLines("inputs/Day 4/input.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if (line == string.Empty)
                {
                    passports.Add(passport);
                    passport = new Dictionary<string, string>();
                    continue;
                }

                string[] elements = line.Split(' ');
                foreach (var item in elements)
                {
                    passport[item.Split(':')[0]] = item.Split(':')[1];
                }

                // what about the end of the array?
                if (i == lines.Length - 1)
                    passports.Add(passport);
            }

            Console.WriteLine("Executing Day 4.");

            Part1(passports);
            Part2(passports);

            Console.WriteLine("Finished Day 4.");
        }

        private static void Part1(IEnumerable<Dictionary<string, string>> passports)
        {
            int valid = passports.Count(IsValidPassport);
            Console.WriteLine($"{valid} valid passports");
        }

        private static void Part2(IEnumerable<Dictionary<string, string>> passports)
        {
            int valid = passports.Count(IsValidPassportPart2);
            Console.WriteLine($"{valid} valid passports");
        }

        private static bool IsValidPassport(Dictionary<string, string> passport)
        {
            return
                passport.Count >= 7 &&
                passport.ContainsKey("byr") &&
                passport.ContainsKey("iyr") &&
                passport.ContainsKey("eyr") &&
                passport.ContainsKey("hgt") &&
                passport.ContainsKey("hcl") &&
                passport.ContainsKey("ecl") &&
                passport.ContainsKey("pid");
        }

        private static bool IsValidPassportPart2(Dictionary<string, string> passport)
        {
            if (passport.Count >= 7 &&
                passport.ContainsKey("byr") &&
                passport.ContainsKey("iyr") &&
                passport.ContainsKey("eyr") &&
                passport.ContainsKey("hgt") &&
                passport.ContainsKey("hcl") &&
                passport.ContainsKey("ecl") &&
                passport.ContainsKey("pid"))
            {

                if (!ValidBirthYear()) return false;
                if (!ValidIssueYear()) return false;
                if (!ValidExpirationYear()) return false;
                if (!ValidHeight()) return false;
                if (!ValidHairColour()) return false;
                if (!ValidEyeColour()) return false;
                if (!ValidPassportId()) return false;

                return true;
            }

            return false;

            bool ValidBirthYear()
            {
                if (!int.TryParse(passport["byr"], out int byr))
                    return false;
                return byr >= 1920 && byr <= 2002;
            }

            bool ValidIssueYear()
            {
                if (!int.TryParse(passport["iyr"], out int byr))
                    return false;
                return byr >= 2010 && byr <= 2020;
            }

            bool ValidExpirationYear()
            {
                if (!int.TryParse(passport["eyr"], out int byr))
                    return false;
                return byr >= 2020 && byr <= 2030;
            }

            bool ValidHeight()
            {
                Regex hgtValidation = new Regex("^(?<height>[0-9]{2,3})(?<unit>cm|in)$", RegexOptions.Compiled);

                var match = hgtValidation.Match(passport["hgt"]);
                if (match.Success)
                {
                    string unit = match.Groups["unit"].Value;
                    int height = int.Parse(match.Groups["height"].Value);

                    if (unit == "in" && height >= 59 && height <= 76)
                        return true;
                    if (unit == "cm" && height >= 150 && height <= 193)
                        return true;
                }
                return false;
            }

            bool ValidHairColour()
            {
                Regex hclValidation = new Regex("^#[0-9a-f]{6}$", RegexOptions.Compiled);

                return hclValidation.IsMatch(passport["hcl"]);
            }

            bool ValidEyeColour()
            {
                Regex eclValidation = new Regex("^(amb|blu|brn|gry|grn|hzl|oth)$", RegexOptions.Compiled);

                return eclValidation.IsMatch(passport["ecl"]);
            }

            bool ValidPassportId()
            {
                Regex pidValidation = new Regex("^[0-9]{9}$", RegexOptions.Compiled);
                return pidValidation.IsMatch(passport["pid"]);
            }
        }
    }
}
