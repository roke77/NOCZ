# NOCZ — Nuclear Option Cockpit Zoom

A [BepInEx](https://github.com/BepInEx/BepInEx) client-side mod for [Nuclear Option](https://store.steampowered.com/app/2168680/Nuclear_Option/) that lets you choose how far the cockpit camera zooms in and out.

## What it does

In the cockpit, the game lets the field of view (FOV) go anywhere from 20° (fully zoomed in) to 120° (fully zoomed out). Zoom past what you want and you end up with a fisheye view, or too close to see anything around you.

NOCZ adds two settings, **Min FOV** and **Max FOV**, that set your own limits inside the game's 20–120° range. Zoom works as before, but it stops at your limits, so you can zoom freely without overshooting.

- Only the cockpit view is affected. External, orbit and free cameras keep the game's own limits.
- Changes apply immediately, even mid-flight.
- With the default settings (20 and 120) the mod changes nothing.
- Runs entirely client-side, so it's safe on public servers.

## Install

1. Install [BepInEx 5](https://github.com/BepInEx/BepInEx/releases) for Nuclear Option, if you haven't already.
2. Download `NOCZ.dll` from the [latest release](https://github.com/roke77/NOCZ/releases/latest).
3. Copy `NOCZ.dll` into `BepInEx/plugins/` in your Nuclear Option install folder.
4. Start the game.

**Optional, recommended:** install [BepInEx.ConfigurationManager](https://github.com/BepInEx/BepInEx.ConfigurationManager) to change the settings from an in-game menu.

## How to use

### In-game (with ConfigurationManager)

1. Press **F1** in-game.
2. Find **NOCZ** and open the **Cockpit zoom** section.
3. Set **Min FOV** and **Max FOV**.
4. Zoom with your usual Zoom View binding. The view stops at your limits.

### Config file

Without ConfigurationManager, edit `BepInEx/config/com.roque.NOCZ.cfg`. NOCZ creates this file the first time the game starts with it installed. Edit it with the game closed, or restart the game afterwards.

```ini
[Cockpit zoom]

## Narrowest field of view in degrees (furthest zoom in). The game allows 20-120.
Min FOV = 20

## Widest field of view in degrees (furthest zoom out). The game allows 20-120.
Max FOV = 120
```

### Settings

| Setting | Default | Range | Meaning |
|---|---|---|---|
| Min FOV | 20 | 20–120 | Narrowest view in degrees: how far you can zoom **in** |
| Max FOV | 120 | 20–120 | Widest view in degrees: how far you can zoom **out** |

Lower numbers mean more zoom. For example, Min FOV 30 and Max FOV 80 let you zoom between a moderate close-up and a normal wide view, but never into a fisheye.

### Good to know

- The game's own **Default FOV** option still sets the view you start with when you enter the cockpit. If it falls outside your range, the view is held at the nearest limit.
- Setting Min FOV higher than Max FOV doesn't break anything: NOCZ treats the lower one as the minimum.
- Values outside 20–120 aren't accepted. NOCZ narrows the game's range but never goes past it.

## Uninstall

Delete `NOCZ.dll` from `BepInEx/plugins/`, and `com.roque.NOCZ.cfg` from `BepInEx/config/` if you want the settings gone too.

## Build

Requires a local Nuclear Option install (for `Assembly-CSharp.dll`). Create a `GameDir.props` next to `NOCZ.csproj` pointing at your game folder (see the comment at the top of `NOCZ.csproj`), then:

```bash
dotnet build -c Release
```

The build copies `NOCZ.dll` into the game's `BepInEx/plugins/` folder.

## License

[MIT](LICENSE) © Roque Alejandro Cuello
