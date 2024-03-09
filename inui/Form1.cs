using SoftCircuits.IniFileParser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

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

        Funcs f = new Funcs();

        private void Form1_Load(object sender, EventArgs e)
        {
            IniFile file = new IniFile();
            file.Load(Properties.Settings.Default.Default_Ini_Filepath);

            Text = file.GetSetting(IniFile.DefaultSectionName, "window.title", "Window");
            Width = file.GetSetting(IniFile.DefaultSectionName, "window.width", 500);
            Height = file.GetSetting(IniFile.DefaultSectionName, "window.height", 500);
            TopMost = file.GetSetting(IniFile.DefaultSectionName, "window.topmost", false);

            IEnumerable<string> elements = file.GetSections();

            foreach (string element in elements)
            {
                if (element.Split(':')[0] == "Button")
                {
                    System.Windows.Forms.Button button = new System.Windows.Forms.Button();
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
                    button.Location = new Point(file.GetSetting(element, "x", 10), file.GetSetting(element, "y", 10));
                    button.Size = new Size(file.GetSetting(element, "width", 75), file.GetSetting(element, "height", 23));

                    button.Click += (_, __) => f.runAll(file.GetSetting(element, "onClicked.lua", ""), file.GetSetting(element, "onClicked.cmd", ""));

                    controls_string.Add(element.Split(':')[1]);
                    controls_object.Add(button);

                    f.setBackColor(file, element, button);
                    f.setForeColor(file, element, button);
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
                        label.Size = new Size(file.GetSetting(element, "width", 50), file.GetSetting(element, "height", 10));
                    }

                    label.Text = file.GetSetting(element, "text", "Label");
                    label.Location = new Point(file.GetSetting(element, "x", 10), file.GetSetting(element, "y", 10));
                    label.Size = new Size(label.Text.Length * 5, label.Height);

                    controls_string.Add(element.Split(':')[1]);
                    controls_object.Add(label);

                    f.setBackColor(file, element, label);
                    f.setForeColor(file, element, label);
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
                    chk.Location = new Point(file.GetSetting(element, "x", 10), file.GetSetting(element, "y", 10));
                    chk.Checked = file.GetSetting(IniFile.DefaultSectionName, "checked", false);

                    chk.CheckedChanged += (_, __) => f.runCheckBox(chk.Checked, file, element); // checkbox is special, it has its own function

                    controls_string.Add(element.Split(':')[1]);
                    controls_object.Add(chk);

                    f.setBackColor(file, element, chk);
                    f.setForeColor(file, element, chk);
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
                    button.Location = new Point(file.GetSetting(element, "x", 10), file.GetSetting(element, "y", 10));
                            
                    button.Click += (_, __) => f.runAll(file.GetSetting(element, "onClicked.lua", ""), file.GetSetting(element, "onClicked.cmd", "")); // this was a pain at first

                    controls_string.Add(element.Split(':')[1]);
                    controls_object.Add(button);

                    f.setBackColor(file, element, button);
                    f.setForeColor(file, element, button);
                }
                if (element.Split(':')[0] == "GroupBox")
                {
                    GroupBox group = new GroupBox();
                    group.Name = element.Split(':')[1];
                    group.Parent = this;

                    group.Text = file.GetSetting(element, "text", "GroupBox");
                    group.Location = new Point(file.GetSetting(element, "x", 10), file.GetSetting(element, "y", 10));
                    group.Size = new Size(file.GetSetting(element, "width", 100), file.GetSetting(element, "height", 100));

                    controls_string.Add(element.Split(':')[1]); // parenting system
                    controls_object.Add(group); // actual parenting system

                    f.setBackColor(file, element, group);
                    f.setForeColor(file, element, group);
                }
                if (element.Split(':')[0] == "PictureBox")
                {
                    PictureBox pic = new PictureBox();
                    pic.Name = element.Split(':')[1];
                    pic.Parent = this;

                    pic.Location = new Point(file.GetSetting(element, "x", 10), file.GetSetting(element, "y", 10));
                    pic.Size = new Size(file.GetSetting(element, "width", 100), file.GetSetting(element, "height", 100));

                    pic.BackgroundImage = Image.FromFile(file.GetSetting(element, "path", "")); // needs to be background image because that has sizing

                    if (file.GetSetting(element, "sizing", Properties.Settings.Default.Default_Image_Sizing_Mode) == "tile") // sizing because getting the size perfectly right wth width/y is a pain
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

                    f.setBackColor(file, element, pic);
                    f.setForeColor(file, element, pic);
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

                    list.Location = new Point(file.GetSetting(element, "x", 10), file.GetSetting(element, "y", 10));
                    list.Size = new Size(file.GetSetting(element, "width", 100), file.GetSetting(element, "height", 100));

                    string[] strings = file.GetSetting(element, "items", "Item 1,Item 2").Split(',');

                    foreach (string s in strings)
                    {
                        list.Items.Add(s);
                    }

                    list.SelectedIndexChanged += (_, __) => f.runAll(file.GetSetting(element, "onSelected.lua", ""), file.GetSetting(element, "onSelected.cmd", "")); // idk how im gonna transfer vals to the lua side

                    controls_string.Add(element.Split(':')[1]);
                    controls_object.Add(list);

                    f.setBackColor(file, element, list);
                    f.setForeColor(file, element, list);
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

                    tabs.Location = new Point(file.GetSetting(element, "x", 10), file.GetSetting(element, "y", 10));
                    tabs.Size = new Size(file.GetSetting(element, "width", 100), file.GetSetting(element, "height", 100));

                    controls_string.Add(element.Split(':')[1]);
                    controls_object.Add(tabs);

                    f.setBackColor(file, element, tabs);
                    f.setForeColor(file, element, tabs);
                }
                if (element.Split(':')[0] == "TabPage")
                {
                    TabPage tab = new TabPage();
                    tab.Text = file.GetSetting(element, "title", "Tab");

                    if (file.GetSetting(element, "parent", "") == "")
                    {
                        MessageBox.Show("TabPage:" + element.Split(':')[1] + " needs to be parented to a tab!", "inui - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Exit();
                    }
                    else
                    {
                        if (controls_object[controls_string.IndexOf(file.GetSetting(element, "parent", ""))] is TabControl)
                        {
                            TabControl ctrl = (TabControl)controls_object[controls_string.IndexOf(file.GetSetting(element, "parent", ""))];
                            ctrl.TabPages.Add(tab);
                        }
                        else
                        {
                            MessageBox.Show("TabPage:" + element.Split(':')[1] + " can only be parented to a tab!", "inui - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Application.Exit();
                        }

                        controls_string.Add(controls_object[controls_string.IndexOf(file.GetSetting(element, "parent", ""))].Name + "." + element.Split(':')[1]);
                        controls_object.Add(tab);

                        f.setBackColor(file, element, tab);
                        f.setForeColor(file, element, tab);
                    }
                }
            }
        }
    }
}