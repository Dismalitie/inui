# inui
Create basic, static Windows GUIs with INI files

## Changelog - v1,1

```diff
+ Added TabControl
+ Added ListBox
+ New controls, new docs
+ Added experimental settings and config in "inui.exe.config"
- Parser string cutoffs
- Stub code left from cut features
```

### Coming Soon...

```ini
[ Lua Library for transfering values ]
[ Lua Library for creating GUI elements ]

[ Might change sz.x to width, sz.y to height, pos.x to x, pos.y to y ]
[ Might change tab construction method to individual definition ] 
```

# Current Classes

1. Button
2. Label
3. RadioButton
4. PictureBox
5. GroupBox
6. CheckBox
7. TabControl
8. ListBox

## Window Properties

```ini
# These don't have to be under a section
window.title=my window
window.sz.x=500
window.sz.y=500
window.topmost=true
```

## Button

```ini
# Class  Name (for parenting) 
#  ↓       ↓ 
[Button:button1]
text=I am a button!
# Default for pos is 10
pos.x=10 
pos.y=10
sz.x=100
sz.y=100
# When clicked, it will run clicked.lua in the res folder
onClicked.lua=res\clicked.lua
```

## Label

```ini
text=<-- that is a button!
pos.x=120
pos.y=10
```

## PictureBox 

```ini
[PictureBox:picture1]
pos.x=10
pos.y=120
# Image is a square but the sizes are not
sz.x=200 
sz.y=100
# This will zoom the image to a square. others are: tile, stretch, center. try changing it to stretch!
scaling=zoom 
# Path to the image, can be a URL
path=res\woes.png
```

## GroupBox

```ini
[GroupBox:group1]
text=Some controls
pos.y=220
pos.x=10
sz.x=200
sz.y=100
```

## CheckBox

```ini
[CheckBox:chk1]
text=Is picture cool?
# Its default state
checked=true
pos.x=20
pos.y=20
# Checkbox will be inside the group
parent=group1
```

## RadioButton

```ini
[RadioButton:radbtn1]
text=Radio button!
pos.x=20
pos.y=40
parent=group1
```

## TabControl

```ini
[TabControl:tbs]
pos.x=225
pos.y=25
sz.x=200
sz.y=200
# Tabs separated by commas
tabs=Tab1,Tab2,Tab3
```

## ListBox

```ini
[ListBox:list]
sz.x=50
sz.y=180
pos.x=0
pos.y=0
items=This,is,a,selection,box,!
```
