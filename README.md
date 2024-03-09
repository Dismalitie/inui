<div align="center">

![inui](https://github.com/Dismalitie/inui/assets/118924562/7a0edfdc-7bf1-4253-a1e8-def621dcba26)

# inui
  
Create basic, static Windows GUIs with INI files

[![forthebadge](https://forthebadge.com/images/badges/made-with-c-sharp.svg)](https://forthebadge.com) [![forthebadge](https://forthebadge.com/images/badges/open-source.svg)](https://forthebadge.com)
</div>

## Changelog - v1.3

```diff
+ Added ForeColor params
+ Added BackColor params
+ Framework restructure + extensibility
+ Extracted cluttered functions to a new class
- width is now width
- height is now height
- x is x
- y is y
- Reworked tabs for flexibility
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
# Class  Name (for parenting) 
#  ↓       ↓ 
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
items=This,is,a,selection,box,!
```
