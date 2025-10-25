using System;
using System.Drawing;
using WindowsInput;
using System;
using System.Linq;
using WindowsInput;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using ProcSpector.API;
using ProcSpector.Core;
using ProcSpector.Impl.Net.Data;
using ProcSpector.Impl.Net.Tools;
using ProcSpector.Impl.Win.Data;
using ProcSpector.Impl.Win.Memory;
using xj;
using static ProcSpector.Core.EnumTool;

// ReSharper disable UnusedMember.Global

#pragma warning disable CA1416

namespace ProcSpector.Impl.Win.Inputs
{
    internal sealed class InpSim
    {
        private static readonly Lazy<InputSimulator> Sim = new(() => new InputSimulator());

        public static void TextEntry(string? text)
        {
            var kb = Sim.Value.Keyboard;
            var txt = text ?? string.Empty;
            switch (txt.Length)
            {
                case 0: return;
                case 1: kb.TextEntry(txt[0]); break;
                default: kb.TextEntry(txt); break;
            }
        }

        public static void SendKey(string mode, string arg)
        {
            var pms = ParseArg<KeyMode>(mode);
            var pc = ParseArg<VirtualKeyCode>(arg);
            var kb = Sim.Value.Keyboard;
            foreach (var pm in pms)
            {
                switch (pm)
                {
                    case KeyMode.Down: kb.KeyDown(pc); break;
                    case KeyMode.Up: kb.KeyUp(pc); break;
                    case KeyMode.Press: kb.KeyPress(pc); break;
                    default: throw new ArgumentOutOfRangeException($"{pm} ?!");
                }
            }
        }

        public static void SendModKey(string mod, string arg)
        {
            var kb = Sim.Value.Keyboard;
            var pMod = ParseArg<VirtualKeyCode>(mod);
            var pCode = ParseArg<VirtualKeyCode>(arg);
            kb.ModifiedKeyStroke(pMod, pCode);
        }

        public static void Sleep(string mode, double arg)
        {
            var pms = ParseArg<TimeMode>(mode);
            var kb = Sim.Value.Keyboard;
            foreach (var pm in pms)
            {
                switch (pm)
                {
                    case TimeMode.Ms: kb.Sleep(TimeSpan.FromMilliseconds(arg)); break;
                    case TimeMode.S: kb.Sleep(TimeSpan.FromSeconds(arg)); break;
                    case TimeMode.M: kb.Sleep(TimeSpan.FromMinutes(arg)); break;
                    default: throw new ArgumentOutOfRangeException($"{pm} ?!");
                }
            }
        }

        public static void SendBtn(string mode, string arg)
        {
            var m = Sim.Value.Mouse;
            var pms = ParseArg<MouseMode>(mode);
            var pbs = ParseArg<MouseBtn>(arg);
            foreach (var pm in pms)
            {
                foreach (var pb in pbs)
                {
                    switch (pm, pb)
                    {
                        case (MouseMode.Down, MouseBtn.Left): m.LeftButtonDown(); break;
                        case (MouseMode.Up, MouseBtn.Left): m.LeftButtonUp(); break;
                        case (MouseMode.Click, MouseBtn.Left): m.LeftButtonClick(); break;
                        case (MouseMode.Double, MouseBtn.Left): m.LeftButtonDoubleClick(); break;
                        case (MouseMode.Down, MouseBtn.Middle): m.MiddleButtonDown(); break;
                        case (MouseMode.Up, MouseBtn.Middle): m.MiddleButtonUp(); break;
                        case (MouseMode.Click, MouseBtn.Middle): m.MiddleButtonClick(); break;
                        case (MouseMode.Double, MouseBtn.Middle): m.MiddleButtonDoubleClick(); break;
                        case (MouseMode.Down, MouseBtn.Right): m.RightButtonDown(); break;
                        case (MouseMode.Up, MouseBtn.Right): m.RightButtonUp(); break;
                        case (MouseMode.Click, MouseBtn.Right): m.RightButtonClick(); break;
                        case (MouseMode.Double, MouseBtn.Right): m.RightButtonDoubleClick(); break;
                        default: throw new ArgumentOutOfRangeException($"{pm} {pb} ?!");
                    }
                }
            }
        }

        public static void Scroll(string mode, int arg)
        {
            var m = Sim.Value.Mouse;
            var pms = ParseArg<MouseScroll>(mode);
            var amount = arg == 0 ? m.MouseWheelClickSize : arg;
            foreach (var pm in pms)
            {
                switch (pm)
                {
                    case MouseScroll.Vertical: m.VerticalScroll(amount); break;
                    case MouseScroll.Horizontal: m.HorizontalScroll(amount); break;
                    default: throw new ArgumentOutOfRangeException($"{pm} ?");
                }
            }
        }
    }
}