﻿/*
 *  "MysteriesRL", roguelike game.
 *  Copyright (C) 2018 by Serg V. Zhdanovskih.
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
using MysteriesRL.Game.Types;

namespace MysteriesRL.Data
{
    public sealed class CreatureRec
    {
        public string Name;

        public int Agility;
        public int Constitution;
        public int Sight;
        public int Strength;
        public int Skin;

        public Sprite Sprite;

        public HabitatType Habitat;
        public CreatureType Type;

        public string[] Area { get; set; }
        public string[] Loot { get; set; }
    }
}
