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
using System.IO;
using System.Reflection;
using System.Text;
using PrimevalRL.Game;
using PrimevalRL.Views.Controls;
using ZRLib.Core;
using ZRLib.Engine;

namespace PrimevalRL.Views
{
    public sealed class StartupView : SubView, IProgressController
    {
        private const int SV_COL = 0x008080;

        private string[] fTitle;
        private readonly ChoicesArea fChoicesArea;
        private bool fMenuMode;
        private int fGenStages = 50;
        private int fGenCompleted = 5;
        private string fGenLabel = null;

        public StartupView(BaseView ownerView, Terminal terminal)
            : base(ownerView, terminal)
        {
            ParseTitle();

            fChoicesArea = new ChoicesArea(terminal, 40, Colors.DarkGreen);
            fChoicesArea.AddChoice('N', Locale.GetStr(RS.Rs_NewGame));
            fChoicesArea.AddChoice('Q', Locale.GetStr(RS.Rs_Quit));

            fMenuMode = true;
        }

        private void ParseTitle()
        {
            fTitle = new string[42];

            try {
                Assembly assembly = typeof(StartupView).Assembly;
                int i = 0;
                using (Stream stm = assembly.GetManifestResourceStream("resources.startup.txt")) {
                    using (StreamReader reader = new StreamReader(stm, Encoding.GetEncoding(866))) {
                        while (reader.Peek() != -1) {
                            fTitle[i] = reader.ReadLine().Trim();
                            i++;
                        }
                    }
                }
            } catch (Exception ex) {
                Logger.Write("StartupView.parseTitle(): " + ex.Message);
            }
        }

        internal override void UpdateView()
        {
            fTerminal.TextBackground = Colors.Black;
            fTerminal.TextForeground = SV_COL;
            fTerminal.Clear();
            fTerminal.DrawBox(0, 0, 159, 79, false);

            int maxw = 0;
            for (int i = 0; i < fTitle.Length; i++) {
                maxw = Math.Max(maxw, fTitle[i].Length);
            }
            int offset = (fTerminal.TermWidth - maxw) / 2;

            for (int i = 0; i < fTitle.Length; i++) {
                fTerminal.Write(offset, 2 + i, fTitle[i]);
            }

            fTerminal.TextForeground = Colors.AliceBlue;
            int top = fTitle.Length;
            fTerminal.WriteCenter(1, 158, top + 4, MRLData.MRL_VER);
            fTerminal.WriteCenter(1, 158, top + 6, MRLData.MRL_COPYRIGHT);

            if (fMenuMode) {
                fChoicesArea.Draw();
            }

            DrawProgress();
        }

        public override void KeyPressed(KeyEventArgs e)
        {
        }

        public override void KeyTyped(KeyPressEventArgs e)
        {
            switch (e.Key) {
                case 'q':
                case 'Q':
                    fTerminal.System.Quit();
                    break;

                case 'n':
                case 'N':
                    fMenuMode = false;
                    GameSpace.InitBaseRealm(this);
                    MainView.View = ViewType.vtPlayerChoice;
                    break;
            }
        }

        public override void MouseClicked(MouseEventArgs e)
        {
        }

        public override void MouseMoved(MouseMoveEventArgs e)
        {
        }

        public override void Show()
        {
        }

        public override void Tick()
        {
        }

        private void DrawProgress()
        {
            if (fGenLabel != null) {
                fTerminal.TextForeground = Colors.White;
                fTerminal.Write(20, 70, fGenLabel);
                fTerminal.DrawProgress(20, 159 - 20, 71, fGenCompleted, fGenStages);
            }
        }

        public void Complete(int stage)
        {
            fGenCompleted++;

            MainView.RepaintImmediately();
        }

        public void SetStage(string label, int size)
        {
            fGenLabel = label;
            fGenStages = size;
            fGenCompleted = 0;

            MainView.RepaintImmediately();
        }
    }
}
