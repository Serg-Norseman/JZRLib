/*
 *  "PrimevalRL", roguelike game.
 *  Copyright (C) 2015, 2017 by Serg V. Zhdanovskih.
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using BSLib;
using ZRLib.Core;

namespace PrimevalRL.Generators
{
    public sealed class NameGenerator
    {
        private static readonly IList<string> fFemaleNames = new List<string>(1024);
        private static readonly IList<string> fMaleNames = new List<string>(1024);
        private static readonly IList<string> fSurnames = new List<string>(1024);

        static NameGenerator()
        {
            ParseNames(fFemaleNames, "female");
            ParseNames(fMaleNames, "male");
            ParseSurnames();
        }

        private readonly Random fRandom;

        public NameGenerator()
        {
            fRandom = new Random();
        }

        private static void ParseSurnames()
        {
            try {
                Assembly assembly = typeof(NameGenerator).Assembly;
                using (Stream stream = assembly.GetManifestResourceStream("resources.namegen.surnames.csv")) {
                    using (StreamReader strd = new StreamReader(stream, Encoding.GetEncoding(1251)))
                    {
                        while (strd.Peek() != -1)
                        {
                            string ns = strd.ReadLine().Trim();
                            string[] line = ns.Replace("\"", "").Split(',');
                            fSurnames.Add(line[0]);
                        }
                    }
                }
            } catch (Exception ex) {
                Logger.Write("NameGenerator.parseSurnames(): " + ex.Message);
            }
        }

        private static void ParseNames(IList<string> names, string sex)
        {
            try {
                Assembly assembly = typeof(NameGenerator).Assembly;
                using (Stream stream = assembly.GetManifestResourceStream("resources.namegen." + sex + ".txt")) {
                    using (StreamReader strd = new StreamReader(stream, Encoding.GetEncoding(1251)))
                    {
                        while (strd.Peek() != -1)
                        {
                            string ns = strd.ReadLine().Trim();
                            string[] line = ns.Split(' ');
                            names.Add(line[0]);
                        }
                    }
                }
            } catch (Exception ex) {
                Logger.Write("NameGenerator.parseNames(): " + ex.Message);
            }
        }

        public string GenerateFullName(bool isMale)
        {
            string name = GenerateName(isMale);
            string surname = GenerateSurname();

            return name + " " + surname;
        }

        public string GenerateName(bool isMale)
        {
            string name = "";
            if (isMale) {
                name = fMaleNames[fRandom.Next(fMaleNames.Count)];
            } else {
                name = fFemaleNames[fRandom.Next(fFemaleNames.Count)];
            }

            return ConvertHelper.UniformName(name);
        }

        public string GenerateSurname()
        {
            return ConvertHelper.UniformName(fSurnames[fRandom.Next(fSurnames.Count)]);
        }
    }
}
