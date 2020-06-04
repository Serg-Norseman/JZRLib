﻿/*
 *  "ZRLib", Roguelike games development Library.
 *  Copyright (C) 2015,2020 by Serg V. Zhdanovskih.
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
using BSLib;

namespace ZRLib.Core
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILocated
    {
        int PosX { get; }
        int PosY { get; }

        bool InRect(ExtRect rt);
        void SetPos(int posX, int posY);
    }
}
