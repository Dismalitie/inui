using SoftCircuits.IniFileParser;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace inui
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Funcs f = new Funcs();
        string[] nullargs = { "" };

        private void Form1_Load(object sender, EventArgs e)
        {
            IniFile file = new IniFile();
            file.Load(Properties.Settings.Default.Default_Ini_Filepath);

            Text = file.GetSetting(IniFile.DefaultSectionName, "window.title", "Window");
            Width = file.GetSetting(IniFile.DefaultSectionName, "window.width", 500);
            Height = file.GetSetting(IniFile.DefaultSectionName, "window.height", 500);
            TopMost = file.GetSetting(IniFile.DefaultSectionName, "window.topmost", false);
            Icon = new Icon(file.GetSetting(IniFile.DefaultSectionName, "window.icon", ".\\inui.ico"));

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
                        button.Parent = f.controls_object[f.controls_string.IndexOf(file.GetSetting(element, "parent", ""))];
                    }

                    button.Text = file.GetSetting(element, "text", "Button");
                    button.Location = new Point(file.GetSetting(element, "x", 10), file.GetSetting(element, "y", 10));
                    button.Size = new Size(file.GetSetting(element, "width", 75), file.GetSetting(element, "height", 23));

                    button.Click += (_, __) => f.runAll(file.GetSetting(element, "onClicked.lua", ""), file.GetSetting(element, "onClicked.cmd", ""), nullargs);

                    f.controls_string.Add(element.Split(':')[1]);
                    f.controls_object.Add(button);

                    f.setBackColor(file, element, button);
                    f.setForeColor(file, element, button);
                }
                else if (element.Split(':')[0] == "Label")
                {
                    Label label = new Label();
                    label.Name = element.Split(':')[1];
                    if (file.GetSetting(element, "parent", "") == "")
                    {
                        label.Parent = this;
                    }
                    else
                    {
                        label.Parent = f.controls_object[f.controls_string.IndexOf(file.GetSetting(element, "parent", ""))];
                    }

                    if (Properties.Settings.Default.Custom_Label_Size)
                    {
                        label.Size = new Size(file.GetSetting(element, "width", 50), file.GetSetting(element, "height", 10));
                    }

                    label.Text = file.GetSetting(element, "text", "Label");
                    label.Location = new Point(file.GetSetting(element, "x", 10), file.GetSetting(element, "y", 10));
                    label.Size = new Size(label.Text.Length * 5, label.Height);

                    f.controls_string.Add(element.Split(':')[1]);
                    f.controls_object.Add(label);

                    f.setBackColor(file, element, label);
                    f.setForeColor(file, element, label);
                }
                else if(element.Split(':')[0] == "CheckBox")
                {
                    CheckBox chk = new CheckBox();
                    chk.Name = element.Split(':')[1];
                    if (file.GetSetting(element, "parent", "") == "")
                    {
                        chk.Parent = this;
                    }
                    else
                    {
                        chk.Parent = f.controls_object[f.controls_string.IndexOf(file.GetSetting(element, "parent", ""))];
                    }

                    chk.Text = file.GetSetting(element, "text", "CheckBox");
                    chk.Location = new Point(file.GetSetting(element, "x", 10), file.GetSetting(element, "y", 10));
                    chk.Checked = file.GetSetting(IniFile.DefaultSectionName, "checked", false);

                    chk.CheckedChanged += (_, __) => f.runCheckBox(chk.Checked, file, element); // checkbox is special, it has its own function

                    f.controls_string.Add(element.Split(':')[1]);
                    f.controls_object.Add(chk);

                    f.setBackColor(file, element, chk);
                    f.setForeColor(file, element, chk);
                }
                else if (element.Split(':')[0] == "RadioButton")
                {
                    RadioButton button = new RadioButton();
                    button.Name = element.Split(':')[1];
                    if (file.GetSetting(element, "parent", "") == "")
                    {
                        button.Parent = this;
                    }
                    else
                    {
                        button.Parent = f.controls_object[f.controls_string.IndexOf(file.GetSetting(element, "parent", ""))];
                    }

                    button.Text = file.GetSetting(element, "text", "RadioButton");
                    button.Location = new Point(file.GetSetting(element, "x", 10), file.GetSetting(element, "y", 10));
                            
                    button.Click += (_, __) => f.runAll(file.GetSetting(element, "onClicked.lua", ""), file.GetSetting(element, "onClicked.cmd", ""), nullargs); // this was a pain at first

                    f.controls_string.Add(element.Split(':')[1]);
                    f.controls_object.Add(button);

                    f.setBackColor(file, element, button);
                    f.setForeColor(file, element, button);
                }
                else if (element.Split(':')[0] == "GroupBox")
                {
                    GroupBox group = new GroupBox();
                    group.Name = element.Split(':')[1];
                    group.Parent = this;

                    group.Text = file.GetSetting(element, "text", "GroupBox");
                    group.Location = new Point(file.GetSetting(element, "x", 10), file.GetSetting(element, "y", 10));
                    group.Size = new Size(file.GetSetting(element, "width", 100), file.GetSetting(element, "height", 100));

                    f.controls_string.Add(element.Split(':')[1]); // parenting system
                    f.controls_object.Add(group); // actual parenting system

                    f.setBackColor(file, element, group);
                    f.setForeColor(file, element, group);
                }
                else if (element.Split(':')[0] == "PictureBox")
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

                    f.controls_string.Add(element.Split(':')[1]);
                    f.controls_object.Add(pic);

                    f.setBackColor(file, element, pic);
                    f.setForeColor(file, element, pic);
                }
                else if (element.Split(':')[0] == "ListBox")
                {
                    ListBox list = new ListBox();
                    list.Name = element.Split(':')[1];
                    if (file.GetSetting(element, "parent", "") == "")
                    {
                        list.Parent = this;
                    }
                    else
                    {
                        list.Parent = f.controls_object[f.controls_string.IndexOf(file.GetSetting(element, "parent", ""))];
                    }

                    list.Location = new Point(file.GetSetting(element, "x", 10), file.GetSetting(element, "y", 10));
                    list.Size = new Size(file.GetSetting(element, "width", 100), file.GetSetting(element, "height", 100));

                    string[] strings = file.GetSetting(element, "items", "Item 1,Item 2").Split(',');

                    foreach (string s in strings)
                    {
                        list.Items.Add(s);
                    }

                    
                    list.SelectedIndexChanged += (_, __) => f.runListBox(file, element, list);

                    f.controls_string.Add(element.Split(':')[1]);
                    f.controls_object.Add(list);

                    f.setBackColor(file, element, list);
                    f.setForeColor(file, element, list);
                }
                else if (element.Split(':')[0] == "TabControl")
                {
                    TabControl tabs = new TabControl();
                    tabs.Name = element.Split(':')[1];

                    if (file.GetSetting(element, "parent", "") == "")
                    {
                        tabs.Parent = this;
                    }
                    else
                    {
                        tabs.Parent = f.controls_object[f.controls_string.IndexOf(file.GetSetting(element, "parent", ""))];
                    }

                    tabs.Location = new Point(file.GetSetting(element, "x", 10), file.GetSetting(element, "y", 10));
                    tabs.Size = new Size(file.GetSetting(element, "width", 100), file.GetSetting(element, "height", 100));

                    tabs.SelectedIndexChanged += (_, __) => f.runTabControl(file, element, tabs);

                    f.controls_string.Add(element.Split(':')[1]);
                    f.controls_object.Add(tabs);

                    f.setBackColor(file, element, tabs);
                    f.setForeColor(file, element, tabs);
                }
                else if (element.Split(':')[0] == "TabPage")
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
                        if (f.controls_object[f.controls_string.IndexOf(file.GetSetting(element, "parent", ""))] is TabControl)
                        {
                            TabControl ctrl = (TabControl)f.controls_object[f.controls_string.IndexOf(file.GetSetting(element, "parent", ""))];
                            ctrl.TabPages.Add(tab);
                        }
                        else
                        {
                            MessageBox.Show("TabPage:" + element.Split(':')[1] + " can only be parented to a tab!", "inui - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Application.Exit();
                        }

                        f.controls_string.Add(f.controls_object[f.controls_string.IndexOf(file.GetSetting(element, "parent", ""))].Name + "." + element.Split(':')[1]);
                        f.controls_object.Add(tab);
                    }

                    f.setBackColor(file, element, tab);
                    f.setForeColor(file, element, tab);
                }
            }
        }
    }
}