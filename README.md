<div align="center">

![inui](https://github.com/Dismalitie/inui/assets/118924562/7a0edfdc-7bf1-4253-a1e8-def621dcba26)

# inui
  
Create basic, static Windows GUIs with INI files

[![cs](https://forthebadge.com/images/badges/made-with-c-sharp.svg)](https://forthebadge.com) [![foss](https://forthebadge.com/images/badges/open-source.svg)](https://forthebadge.com)
</div>

## Changelog - v1.4

```diff
+ Updated Docs
+ Fixed TabPage foreColor and backColor bug
+ Minimised package size
+ Optimisations with else ifs
+ Fixed glitchy buttons with long labels
- Unused tmp folder from cut feature
- Duplicate PUC-Lua files
- Unused cross-lang bindings
- Unused lib references
```

# Current Controls

1. Button
2. Label
3. RadioButton
4. PictureBox
5. GroupBox
6. CheckBox
7. TabControl
8. ListBox
9. TabPage

## General Properties and Methods

All controls have the following properties:
1. `width` How tall the control is in pixels.
2. `height` How tall the control is in pixels.
3. `x` Where the control is placed on the X axis relevant to it's parent.
4. `y` Where the control is placed on the Y axis relevant to it's parent.
5. `parent` The property that defines what the control belongs to. Normally used for parenting to `TabPages` or `GroupBoxes`.
6. `backColor` The BackColor property of standard WinForms controls. Defines the background color in RGB format (`245,101,101`) **NO SPACES**
7. `foreColor` The ForeColor property of standard WinForms controls. Usually text.

All properties that consist of two or more words will use camelCase. e.g:

`foreColor`
`backColor`

All controls follow the same way of defining them:
```ini
# (Class) (Name, for parenting) 
#   ↓       ↓ 
[Button:button1]
```

## Window Properties

```ini
# These don't have to be under a section
window.title=my window
window.width=500
window.height=500
window.topmost=true
```

## Button

```ini
[Button:button1]
text=I am a button!
# Default for pos is 10
x=10
y=10
width=100
height=100
# Cmd will run on the command line, lua will run with standard PUC-Lua 54 runtime
onClicked.lua=res\clicked.lua
onClicked.cmd=echo File made with CMD! > clicked_cmd.txt
# A RGB value to represent the foreColor, in this case it is the text. Set it to inherit to have the same color as its parent.
foreColor=255,0,0
```

## Label

```ini
[Label:label1]
text=<-- that is a button!
x=120
y=10
```

## PictureBox 

```ini
[PictureBox:picture1]
x=10
y=120
# Image is a square but the sizes are not
width=200 
height=100
# This will zoom the image to a square. others are: tile, stretch, center. try changing it to stretch!
scaling=zoom 
# Path to the image, can be a URL
path=res\woes.png
```

## GroupBox

```ini
[GroupBox:group1]
text=Some controls
y=220
x=10
width=200
height=100
# An RGB value for the background color of the control. Change it to inherit and see what happens!
backColor=0,255,0
```

## CheckBox

```ini
[CheckBox:chk1]
text=Is picture cool?
# Its default state
checked=true
x=20
y=20
# Checkbox will be inside the group
parent=group1
```

## RadioButton

```ini
[RadioButton:radbtn1]
text=Radio button!
x=20
y=40
parent=group1
```

## TabControl

```ini
[TabControl:tbs]
x=225
y=25
width=200
height=200
```

## TabPage
```ini
[TabPage:tabpage1]
title=Tab 1!
parent=tbs # Can only be parented to TabControls
```

## ListBox

```ini
[ListBox:list]
width=50
height=180
x=0
y=0
parent=tbs.tab2
items=This,is,a,selection,box,!
```
