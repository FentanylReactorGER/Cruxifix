![Cruxifix](https://github.com/user-attachments/assets/d4132677-609e-4f77-bb4b-6bc35ac4a40c)

<br><br><br>
[![downloads](https://img.shields.io/github/downloads/FentanylReactorGER/Cruxifix/total?style=for-the-badge&logo=icloud&color=%233A6D8C)](https://github.com/FentanylReactorGER/Cruxifix/releases/latest)
![Latest](https://img.shields.io/github/v/release/FentanylReactorGER/Cruxifix?style=for-the-badge&label=Latest%20Release&color=%23D91656)

# Cruxifix for EXILED

### Minimum Exiled Version: [9.5.6](https://github.com/ExMod-Team/EXILED/releases/latest)
## Features:
- Adding A Custom Schematic
- Adding Custom Sounds
- Adding a Custom Item

# How to install?

- Just Download the Cruxifix.dll and put it into EXILED/Plugins.
- Download the Dependencies.
- Restart your Server.
- Drag the Sound into Plugins/audio/
- Drag the Schematic into MapEditorReborn/Schematics/

# Dependencies
- AudioPlayer by [@Killers0992](https://github.com/Killers0992) download here: [AudioPlayer](https://github.com/Killers0992/AudioPlayer/releases/latest)
- MapEditorReborn by [@Michal78900](https://github.com/Michal78900) download here: [MapEditorReborn](https://github.com/Michal78900/MapEditorReborn/releases/latest)
- HintServiceMeow by [@MeowServer](https://github.com/MeowServer) download here: [HintServiceMeow](https://github.com/MeowServer/HintServiceMeow/releases/latest)

### Additional:
n/A

# Credits:
n/A

## Config:

```yaml
# Should the plugin be enabled
is_enabled: true
# Should the plugin display a debug message
debug: false
# HSM Hints Configs (ONLY APPLIES FOR MY PLUGIN AND WONT BREAK OTHER PLUGINS)
global_hint_duration: 5
global_hint_size: 30
global_hint_y: 950
# Custom Item Sound (AudioPath will only DISPLAY your Current Path do NOT edit, edit Clip name and IF nessesary ClipPathFolder)
clip_name: 'CruxifixSound.ogg'
clip_path_folder: 'audio'
audio_path: 'C:\Users\Administrator\AppData\Roaming\EXILED\Plugins\audio\CruxifixSound.ogg'
clip_duration: 5
max_clip_range: 15
clip_volume: 1
# Custom Item Schematic Configs
custom_item_schematic: 'Crucifix'
# Custom Item Damage Type Whitelist (Look up the Discord for more infos)
custom_item_damage_types:
- Com15
- Com18
- Crossvec
- Logicer
- ParticleDisruptor
- Shotgun
- Jailbird
- Revolver
- Scp018
- Scp049
- Scp0492
- Scp096
- Scp106
- Scp173
- Scp939
- Scp207
- PocketDimension
- Tesla
- Decontamination
- Asphyxiation
- Recontainment
- Poison
- Bleeding
- Explosion
- Frmg0
- Fsp9
- CardiacArrest
# Custom Item Advanced Editing (More on your Requests)
custom_item_offset:
  x: 0
  y: 0.5
  z: 0.43
custom_item_rotation:
  x: -90
  y: 0
  z: 0
custom_item_scale:
  x: 1
  y: 1
  z: 1
custom_item_scale_item:
  x: 1
  y: 1
  z: 7.4
# Custom Item Full Heal Duration
custom_item_heal_dur: 2
# Custom Item ID
custom_item_i_d: 6969
# Custom Item Weight
custom_item_weight: 1
# Custom Item SpawnProperties
custom_item_spawn_properties:
  limit: 7
  dynamic_spawn_points: []
  static_spawn_points: []
  role_spawn_points: []
  room_spawn_points: []
  locker_spawn_points:
  - zone: Unspecified
    use_chamber: true
    offset:
      x: 0
      y: 0
      z: 0
    type: Medkit
    chance: 70
  - zone: Unspecified
    use_chamber: true
    offset:
      x: 0
      y: 0
      z: 0
    type: Misc
    chance: 100
```

## Translation:

```yaml
# Custom Item Name
custom_item_name: 'Cruxifix'
# Custom Item Description
custom_item_description: 'Saves you from Death!'
# Custom Item Use Hint
custom_item_u_h: |-
  "For whoever wants to save their life will lose it, but whoever loses their life for me will find it."
  — Matthew 16:25
```
## Showcase:

SOON IM LAZY RN!!!
