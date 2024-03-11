using System;
using System.Drawing;
using SoftCircuits.IniFileParser;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Collections;
using System.IO;

namespace inui
{
    internal class Funcs
    {
        public List<string> controls_string = new List<string>();
        public List<Control> controls_object = new List<Control>();

        public void setBackColor(IniFile file, string element, Control control)
        {
            if (file.GetSetting(element, "backColor", "inherit") == "inherit")
            {
                control.BackColor = control.Parent.BackColor;
            }
            else
            {
                string[] vals = file.GetSetting(element, "backColor", "inherit").Split(','); // e.g: backColor=255,255,255 
                int red = int.Parse(vals[0]);
                int green = int.Parse(vals[1]);
                int blue = int.Parse(vals[2]);

                control.BackColor = Color.FromArgb(red, green, blue);
            }
        }

        public void setForeColor(IniFile file, string element, Control control)
        {
            if (file.GetSetting(element, "foreColor", "inherit") == "inherit")
            {
                control.ForeColor = control.Parent.ForeColor;
            }
            else
            {
                string[] vals = file.GetSetting(element, "foreColor", "inherit").Split(',');
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

        public void runLua(string file, string[] args)
        {
            string command = Environment.CurrentDirectory + "\\lua\\lua.exe " + file;
            foreach (string arg in args)
            {
                command = command + " " + arg;
            }

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

        public void runAll(string lua, string cmd, string[] luaArgs) // func nesting or smh lol
        {
            runCmd(cmd);
            runLua(lua, luaArgs);
        }

        public void runCheckBox(bool state, IniFile file, string element)
        {
            CheckBox chk = (CheckBox)controls_object[controls_string.IndexOf(element)];
            string[] args = { chk.Checked.ToString() };
            string[] nullargs = { "" };

            runAll(file.GetSetting(element, "onClicked.lua", ""), file.GetSetting(element, "onClicked.cmd", ""), args);
            if (state)
            {
                runAll(file.GetSetting(element, "onChecked.lua", ""), file.GetSetting(element, "onChecked.cmd", ""), nullargs);
            }
            else
            {
                runAll(file.GetSetting(element, "onUnchecked.lua", ""), file.GetSetting(element, "onUnchecked.cmd", ""), nullargs);
            }
        }

        public void runListBox(IniFile file, string element, ListBox list)
        {
            string[] args = { list.SelectedItem.ToString(), list.SelectedIndex.ToString() };
            runAll(file.GetSetting(element, "onSelected.lua", ""), file.GetSetting(element, "onSelected.cmd", ""), args);
        }

        public void runTabControl(IniFile file, string element, TabControl tabs)
        {
            string[] args = { tabs.SelectedTab.Text, tabs.SelectedIndex.ToString() };
            runAll(file.GetSetting(element, "onTabChanged.lua", ""), file.GetSetting(element, "onTabChanged.cmd", ""), args);
        }
    }
}
