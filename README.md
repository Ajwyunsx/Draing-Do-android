# Draing-Do-android

Unity Android project for the public-version Daring Do game port.

## Project Info

- Unity version: `2022.3.62f3c1`
- Target platform: Android
- Package name: `com.dd.DaringDo`
- Target SDK: Android API 35
- Architectures: ARMv7 + ARM64

## Current Porting Notes

This project was exported and repaired for newer Unity/Android builds. The current state includes compatibility work for:

- Android player settings and app icon replacement.
- Legacy shader/material compatibility fixes to remove pink/missing shader rendering.
- Video playback and cutscene compatibility.
- Mobile joystick behavior improvements.
- Enemy AI movement fixes, including rat/mole, worm/snake, and boss-style grounded movement.
- Death visual cleanup so death animation is not mixed with hit/glow effects.
- Weapon swing, whip, strike FX, and bloom/exposure tuning.
- Campfire/save interaction and common right-click/touch interaction compatibility.

## Build

1. Open this folder in Unity `2022.3.62f3c1`.
2. Switch platform to Android in `File > Build Settings`.
3. Let Unity import assets after opening the project.
4. Build from `File > Build Settings > Build` or `Build And Run`.

## Repository Notes

The repository intentionally excludes Unity generated folders and build output:

- `Library/`
- `Temp/`
- `Obj/`
- `Build/`
- `Builds/`
- `Logs/`
- `UserSettings/`
- `*.apk`
- `*.aab`

Do not commit generated Unity cache folders. Commit only source assets, scripts, packages, and project settings.

## Known Warnings

Some Unity warnings from old assets can still appear during import, especially obsolete APIs or old particle/GUI components. These warnings are expected while maintaining compatibility with the original project assets.
