# NOCZ (Nuclear Option Cockpit Zoom)

A BepInEx 5 mod for Nuclear Option that lets you set how far the cockpit camera can zoom in and out.

The game lets the cockpit field of view go from 20° (zoomed in) to 120° (zoomed out). This mod adds two settings that narrow that range, so you can zoom freely without overshooting into a fisheye view or zooming in further than you want.

## Settings

Edit them in the BepInEx Configuration Manager (F1 by default) or in `BepInEx/config/com.roque.NOCZ.cfg`. Changes apply immediately.

| Setting | Default | Range | Meaning |
|---|---|---|---|
| Cockpit zoom → Min FOV | 20 | 20–120 | Narrowest view, in degrees (furthest zoom in) |
| Cockpit zoom → Max FOV | 120 | 20–120 | Widest view, in degrees (furthest zoom out) |

The defaults match the game, so with no changes the mod does nothing. The game's own **Default FOV** option still sets the view you start at when you enter the cockpit; if it falls outside your range, the view is held at the nearest limit.

## Install

1. Install [BepInEx 5](https://github.com/BepInEx/BepInEx/releases) for Nuclear Option.
2. Copy `NOCZ.dll` into `Nuclear Option/BepInEx/plugins/`.

## Building

Create a `GameDir.props` next to `NOCZ.csproj` pointing at your game install (see the comment at the top of the csproj), then run `dotnet build -c Release`. The build copies the DLL into the game's `BepInEx/plugins` folder.
