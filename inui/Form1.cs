using SoftCircuits.IniFileParser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace inui
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<string> controls_string = new List<string>();
        List<Control> controls_object = new List<Control>();

        void runCmd(string path)
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
        void runLua(string file)
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
        void runAll(string lua, string cmd) // func nesting or smh lol
        {
            runCmd(cmd);
            runLua(lua);
        }
        void runCheckBox(bool state, IniFile file, string element)
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

        private void Form1_Load(object sender, EventArgs e)
        {
            IniFile file = new IniFile();
            file.Load(Properties.Settings.Default.Default_Ini_Filepath);

            MessageBox.Show(" ", "luna - Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            Text = file.GetSetting(IniFile.DefaultSectionName, "window.title", "Window");
            Width = file.GetSetting(IniFile.DefaultSectionName, "window.sz.x", 500);
            Height = file.GetSetting(IniFile.DefaultSectionName, "window.sz.y", 500);
            TopMost = file.GetSetting(IniFile.DefaultSectionName, "window.topmost", false);

            IEnumerable<string> elements = file.GetSections();

            foreach (string element in elements)
            {
                if (element.Split(':')[0] == "Button")
                {
                    Button button = new Button();
                    button.Name = element.Split(':')[1];
                    if (file.GetSetting(element, "parent", "") == "")
                    {
                        button.Parent = this;
                    }
                    else
                    {
                        button.Parent = controls_object[controls_string.IndexOf(file.GetSetting(element, "parent", ""))];
                    }

                    button.Text = file.GetSetting(element, "text", "Button");
                    button.Location = new Point(file.GetSetting(element, "pos.x", 10), file.GetSetting(element, "pos.y", 10));
                    button.Size = new Size(file.GetSetting(element, "sz.x", 75), file.GetSetting(element, "sz.y", 23));

                    button.Click += (_, __) => runAll(file.GetSetting(element, "onClicked.lua", ""), file.GetSetting(element, "onClicked.cmd", ""));

                    controls_string.Add(element.Split(':')[1]);
                    controls_object.Add(button);
                }
                if (element.Split(':')[0] == "Label")
                {
                    Label label = new Label();
                    label.Name = element.Split(':')[1];
                    if (file.GetSetting(element, "parent", "") == "")
                    {
                        label.Parent = this;
                    }
                    else
                    {
                        label.Parent = controls_object[controls_string.IndexOf(file.GetSetting(element, "parent", ""))];
                    }

                    if (Properties.Settings.Default.Custom_Label_Size)
                    {
                        label.Size = new Size(file.GetSetting(element, "sz.x", 50), file.GetSetting(element, "sz.y", 10));
                    }

                    label.Text = file.GetSetting(element, "text", "Label");
                    label.Location = new Point(file.GetSetting(element, "pos.x", 10), file.GetSetting(element, "pos.y", 10));
                    label.Size = new Size(label.Text.Length * 5, label.Height);

                    controls_string.Add(element.Split(':')[1]);
                    controls_object.Add(label);
                }
                if (element.Split(':')[0] == "CheckBox")
                {
                    CheckBox chk = new CheckBox();
                    chk.Name = element.Split(':')[1];
                    if (file.GetSetting(element, "parent", "") == "")
                    {
                        chk.Parent = this;
                    }
                    else
                    {
                        chk.Parent = controls_object[controls_string.IndexOf(file.GetSetting(element, "parent", ""))];
                    }

                    chk.Text = file.GetSetting(element, "text", "CheckBox");
                    chk.Location = new Point(file.GetSetting(element, "pos.x", 10), file.GetSetting(element, "pos.y", 10));
                    chk.Checked = file.GetSetting(IniFile.DefaultSectionName, "checked", false);

                    chk.CheckedChanged += (_, __) => runCheckBox(chk.Checked, file, element); // checkbox is special, it has its own function
                }
                if (element.Split(':')[0] == "RadioButton")
                {
                    RadioButton button = new RadioButton();
                    button.Name = element.Split(':')[1];
                    if (file.GetSetting(element, "parent", "") == "")
                    {
                        button.Parent = this;
                    }
                    else
                    {
                        button.Parent = controls_object[controls_string.IndexOf(file.GetSetting(element, "parent", ""))];
                    }

                    button.Text = file.GetSetting(element, "text", "RadioButton");
                    button.Location = new Point(file.GetSetting(element, "pos.x", 10), file.GetSetting(element, "pos.y", 10));
                            
                    button.Click += (_, __) => runAll(file.GetSetting(element, "onClicked.lua", ""), file.GetSetting(element, "onClicked.cmd", "")); // this was a pain at first

                    controls_string.Add(element.Split(':')[1]);
                    controls_object.Add(button);
                }
                if (element.Split(':')[0] == "GroupBox")
                {
                    GroupBox group = new GroupBox();
                    group.Name = element.Split(':')[1];
                    group.Parent = this;

                    group.Text = file.GetSetting(element, "text", "GroupBox");
                    group.Location = new Point(file.GetSetting(element, "pos.x", 10), file.GetSetting(element, "pos.y", 10));
                    group.Size = new Size(file.GetSetting(element, "sz.x", 100), file.GetSetting(element, "sz.y", 100));

                    controls_string.Add(element.Split(':')[1]); // parenting system
                    controls_object.Add(group); // actual parenting system
                }
                if (element.Split(':')[0] == "PictureBox")
                {
                    PictureBox pic = new PictureBox();
                    pic.Name = element.Split(':')[1];
                    pic.Parent = this;

                    pic.Location = new Point(file.GetSetting(element, "pos.x", 10), file.GetSetting(element, "pos.y", 10));
                    pic.Size = new Size(file.GetSetting(element, "sz.x", 100), file.GetSetting(element, "sz.y", 100));

                    pic.BackgroundImage = Image.FromFile(file.GetSetting(element, "path", "")); // needs to be background image because that has sizing

                    if (file.GetSetting(element, "sizing", Properties.Settings.Default.Default_Image_Sizing_Mode) == "tile") // sizing because getting the size perfectly right wth sz.x/y is a pain
                    {
                        pic.BackgroundImageLayout = ImageLayout.Tile;
                    }
                    else if (file.GetSetting(element, "sizing", Properties.Settings.Default.Default_Image_Sizing_Mode) == "stretch")
                    {
                        pic.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    else if (file.GetSetting(element, "sizing", Properties.Settings.Default.Default_Image_Sizing_Mode) == "zoom")
                    {
                        pic.BackgroundImageLayout = ImageLayout.Zoom;
                    }
                    else if (file.GetSetting(element, "sizing", Properties.Settings.Default.Default_Image_Sizing_Mode) == "center")
                    {
                        pic.BackgroundImageLayout = ImageLayout.Zoom;
                    }

                    controls_string.Add(element.Split(':')[1]);
                    controls_object.Add(pic);
                }
                if (element.Split(':')[0] == "ListBox")
                {
                    ListBox list = new ListBox();
                    list.Name = element.Split(':')[1];
                    if (file.GetSetting(element, "parent", "") == "")
                    {
                        list.Parent = this;
                    }
                    else
                    {
                        list.Parent = controls_object[controls_string.IndexOf(file.GetSetting(element, "parent", ""))];
                    }

                    list.Location = new Point(file.GetSetting(element, "pos.x", 10), file.GetSetting(element, "pos.y", 10));
                    list.Size = new Size(file.GetSetting(element, "sz.x", 100), file.GetSetting(element, "sz.y", 100));

                    string[] strings = file.GetSetting(element, "items", "Item 1,Item 2").Split(',');

                    foreach (string s in strings)
                    {
                        list.Items.Add(s);
                    }

                    list.SelectedIndexChanged += (_, __) => runAll(file.GetSetting(element, "onSelected.lua", ""), file.GetSetting(element, "onSelected.cmd", "")); // idk how im gonna transfer vals to the lua side

                    controls_string.Add(element.Split(':')[1]);
                    controls_object.Add(list);
                }
                if (element.Split(':')[0] == "TabControl")
                {
                    TabControl tabs = new TabControl();
                    tabs.Name = element.Split(':')[1];

                    if (file.GetSetting(element, "parent", "") == "")
                    {
                        tabs.Parent = this;
                    }
                    else
                    {
                        tabs.Parent = controls_object[controls_string.IndexOf(file.GetSetting(element, "parent", ""))];
                    }

                    tabs.Location = new Point(file.GetSetting(element, "pos.x", 10), file.GetSetting(element, "pos.y", 10));
                    tabs.Size = new Size(file.GetSetting(element, "sz.x", 100), file.GetSetting(element, "sz.y", 100));

                    string[] strings = file.GetSetting(element, "tabs", "Tab 1,Tab 2").Split(',');

                    foreach (string s in strings)
                    {
                        TabPage tab = new TabPage();
                        tab.Name = s;
                        tab.Text = s;

                        tabs.TabPages.Add(tab);

                        controls_string.Add(element.Split(':')[1] + "." + s);
                        controls_object.Add(tab);
                    }
                }
            }
        }
    }
}