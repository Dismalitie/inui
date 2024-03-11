<div align="center">

![inui](https://github.com/Dismalitie/inui/assets/118924562/7a0edfdc-7bf1-4253-a1e8-def621dcba26)

# inui
  
Create basic, static Windows GUIs with INI files

[![cs](https://forthebadge.com/images/badges/made-with-c-sharp.svg)](https://forthebadge.com) [![foss](https://forthebadge.com/images/badges/open-source.svg)](https://forthebadge.com)
</div>

## Changelog - v1.5

```diff
+ C# side can now interact with the Lua side!
+ Added Graphite Installation Hive
+ Completely redid docs! Again...
- Removed unused stuff
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

# Control Docs

All the documentaion for all the controls, can be found below.

## Button

Just a basic clickable button

```ini
[Button:button1]
```

### Properties

`text` What the button will display as it's label

`x` The position along the X axis relative to its parent

`y` The position along the Y axis relative to its parent

`width` How thicc the control will be

`height` How tall the control will be

`parent` What the control belongs to

`backColor` The background color of the control

`foreColor` The color of the text

### Events

|Name|args|
|:---|:---|
|`onClicked.lua`|❌|
|`onClicked.cmd`|❌|

### Example Snippet

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

Display small snippets of text

```ini
[Label:label1]
```

### Properties

`text` What the control will display as it's label

`x` The position along the X axis relative to its parent

`y` The position along the Y axis relative to its parent

`width` How thicc the control will be

`height` How tall the control will be

`parent` What the control belongs to

`backColor` The background color of the control

`foreColor` The color of the text

Has no events.

## CheckBox

A toggleable button I suppose

```ini
[CheckBox:chk1]
```

### Properties

`text` What the control will display as it's label

`x` The position along the X axis relative to its parent

`y` The position along the Y axis relative to its parent

`width` How thicc the control will be

`height` How tall the control will be

`parent` What the control belongs to

`backColor` The background color of the control

`foreColor` The color of the text

`checked` Whether the `CheckBox` is enabled by default

### Events

|Name|args|
|:---|:---|
|`onClicked.lua`|`arg[1]` : checked|
|`onClicked.cmd`|❌|
|`onChecked.lua`|❌|
|`onChecked.cmd`|❌|
|`onUnchecked.lua`|❌|
|`onUnchecked.cmd`|❌|

### Example Snippet

```ini
[CheckBox:chk1]
text=Is picture cool?
# Its default state
checked=true
x=20
y=20
```

## RadioButton

Just a worse version of the button

```ini
[RadioButton:radbtn1]
```

### Properties

`text` What the control will display as it's label

`x` The position along the X axis relative to its parent

`y` The position along the Y axis relative to its parent

`width` How thicc the control will be

`height` How tall the control will be

`parent` What the control belongs to

`backColor` The background color of the control

`foreColor` The color of the text

### Events

|Name|args|
|:---|:---|
|`onClicked.lua`|❌|
|`onClicked.cmd`|❌|

### Example Snippet

```ini
[RadioButton:radbtn1]
text=Radio button!
x=20
y=40
```

## GroupBox

A box to put things in, coming built in with a cool little label

```ini
[GroupBox:group1]
```

### Properties

`text` What the control will display as it's label

`x` The position along the X axis relative to its parent

`y` The position along the Y axis relative to its parent

`width` How thicc the control will be

`height` How tall the control will be

`parent` What the control belongs to

`backColor` The background color of the control

`foreColor` The color of the text

Has no events.

### Example Snippet

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

## PictureBox

Display cool pictures

```ini
[PictureBox:pic]
```

### Properties

`x` The position along the X axis relative to its parent

`y` The position along the Y axis relative to its parent

`width` How thicc the control will be

`height` How tall the control will be

`parent` What the control belongs to

`path` The filepath that the image will be loaded from

`sizing` How the image will be sized / scaled if it does not fit correctly

1. `tile` Repeats the image to fill the dimensions
2. `zoom` Zooms in the image to fill at least one axis
3. `center` Centers the image
4. `stretch` Stretches the image to fit the dimensions

Has no events.

### Example Snippet

```ini
[PictureBox:picture1]
x=10
y=120
# Image is a square but the sizes are not
width=100
height=100
# This will zoom the image to a square. others are: tile, stretch, center. try changing it to stretch!
sizing=zoom
# Path to the image
path=res\woes.png
```

## ListBox

A list of items to select

```ini
[ListBox:lst1]
```

### Properties

`x` The position along the X axis relative to its parent

`y` The position along the Y axis relative to its parent

`width` How thicc the control will be

`height` How tall the control will be

`parent` What the control belongs to

`items` The items that will be in the list, separated by commas

|Name|args|
|:---|:---|
|`onSelected.lua`|`arg[1]` : selected item (text), `arg[1]` : selected item (index)|
|`onSelected.cmd`|❌|

### Example Snippet

```ini
[ListBox:list]
width=50
height=180
x=0
y=0
parent=tbs.tab2
items=This,is,a,selection,box,!
```

## TabControl

A collection of `TabPage`s to choose from

```ini
[TabControl:tabs1]
```

### Properties

`x` The position along the X axis relative to its parent

`y` The position along the Y axis relative to its parent

`width` How thicc the control will be

`height` How tall the control will be

`parent` What the control belongs to

### Events

|Name|args|
|:---|:---|
|`onTabChanged.lua`|`arg[1]` : selected tab (text), `arg[1]` : selected tab (index)|
|`onTabChanged.cmd`|❌|

### Example Snippet

```ini
[TabControl:tabs1]
x=225
y=25
width=200
height=200
```

## TabPage

A component of the `TabControl`, parent items to it like `tabcontrol.tabpage`

```ini
[TabPage:tab1]
```

### Properties

`title` The text that will display on the tabs handle

`x` The position along the X axis relative to its parent

`y` The position along the Y axis relative to its parent

`width` How thicc the control will be

`height` How tall the control will be

`parent` What the control belongs to, can only be parented to `TabControl`s

Has no events.

### Example Snippet

```ini
[TabPage:tab1]
title=Tab 1
parent=tbs
```