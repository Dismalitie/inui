using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using SoftCircuits.IniFileParser;
using System.Windows.Forms;
using System.Diagnostics;

namespace inui
{
    internal class Funcs
    {
        public void setBackColor(IniFile file, string element, Control control)
        {
            if (file.GetSetting(element, "backcolor", "inherit") == "inherit")
            {
                control.BackColor = control.Parent.BackColor;
            }
            else
            {
                string[] vals = file.GetSetting(element, "backcolor", "inherit").Split(','); // e.g: backcolor=255,255,255 
                int red = int.Parse(vals[0]);
                int green = int.Parse(vals[1]);
                int blue = int.Parse(vals[2]);

                control.BackColor = Color.FromArgb(red, green, blue);
            }
        }

        public void setForeColor(IniFile file, string element, Control control)
        {
            if (file.GetSetting(element, "forecolor", "inherit") == "inherit")
            {
                control.ForeColor = control.Parent.ForeColor;
            }
            else
            {
                string[] vals = file.GetSetting(element, "forecolor", "inherit").Split(',');
                int red = int.Parse(vals[0]);
                int green = int.Parse(vals[1]);
                int blue = int.Parse(vals[2]);

                control.ForeColor = Color.FromArgb(red, green, blue);
            }
        }

        public void runCmd(string path)
        {
            string command = path;

            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.CreateNoWindow = true;
            processStartInfo.FileName = "cmd.exe";
            processStartInfo.Arguments = "/c " + command;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.UseShellExecute = false;
            Process process = new Process();
            process.StartInfo = processStartInfo;
            process.Start();
        }

        public void runLua(string file)
        {
            string command = Environment.CurrentDirectory + "\\lua\\lua.exe " + file;

            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.CreateNoWindow = true;
            processStartInfo.FileName = "cmd.exe";
            processStartInfo.Arguments = "/c " + command;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.UseShellExecute = false;
            Process process = new Process();
            process.StartInfo = processStartInfo;
            process.Start();
        }

        public void runAll(string lua, string cmd) // func nesting or smh lol
        {
            runCmd(cmd);
            runLua(lua);
        }

        public void runCheckBox(bool state, IniFile file, string element)
        {
            runAll(file.GetSetting(element, "onClicked.lua", ""), file.GetSetting(element, "onClicked.cmd", ""));
            if (state)
            {
                runAll(file.GetSetting(element, "onChecked.lua", ""), file.GetSetting(element, "onChecked.cmd", ""));
            }
            else
            {
                runAll(file.GetSetting(element, "onUnchecked.lua", ""), file.GetSetting(element, "onUnchecked.cmd", ""));
            }
        }
    }
}
