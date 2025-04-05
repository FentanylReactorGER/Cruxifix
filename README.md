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
# Custom Item Effects (Look up the Discord for more infos, Effect Duration is decided by the 'CustomItemHealDur' value 
custom_item_effects:
- Asphyxiated
- Flashed
- Blurred
- Concussed
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
custom_item_u_h:
- |-
  "For whoever wants to save their life will lose it, but whoever loses their life for me will find it."
  — Matthew 16:25
- |-
  "Even though I walk through the valley of the shadow of death, I will fear no evil, for you are with me."
  — Psalm 23:4
- |-
  "The Lord will fight for you; you need only to be still."
  — Exodus 14:14
- |-
  "God is our refuge and strength, an ever-present help in trouble."
  — Psalm 46:1
- |-
  "The name of the Lord is a fortified tower; the righteous run to it and are safe."
  — Proverbs 18:10
- |-
  "I have told you these things, so that in me you may have peace. In this world you will have trouble. But take heart! I have overcome the world."
  — John 16:33
- |-
  "He will cover you with his feathers, and under his wings you will find refuge."
  — Psalm 91:4
- |-
  "When you pass through the waters, I will be with you; and when you pass through the rivers, they will not sweep over you."
  — Isaiah 43:2
- |-
  "The Lord is my light and my salvation—whom shall I fear?"
  — Psalm 27:1
- |-
  "Do not be afraid of those who kill the body but cannot kill the soul."
  — Matthew 10:28
- |-
  "The Lord your God goes with you; he will never leave you nor forsake you."
  — Deuteronomy 31:6
- |-
  "He gives strength to the weary and increases the power of the weak."
  — Isaiah 40:29
- |-
  "My grace is sufficient for you, for my power is made perfect in weakness."
  — 2 Corinthians 12:9
- |-
  "Be strong and courageous. Do not be afraid; do not be discouraged."
  — Joshua 1:9
- |-
  "Cast all your anxiety on him because he cares for you."
  — 1 Peter 5:7
- |-
  "I sought the Lord, and he answered me; he delivered me from all my fears."
  — Psalm 34:4
- |-
  "No weapon forged against you will prevail."
  — Isaiah 54:17
- |-
  "In peace I will lie down and sleep, for you alone, Lord, make me dwell in safety."
  — Psalm 4:8
- |-
  "Call on me in the day of trouble; I will deliver you."
  — Psalm 50:15
- |-
  "With God we will gain the victory, and he will trample down our enemies."
  — Psalm 60:12
- |-
  "Because you are my help, I sing in the shadow of your wings."
  — Psalm 63:7
- |-
  "You are my hiding place; you will protect me from trouble."
  — Psalm 32:7
- |-
  "Fear not, for I have redeemed you; I have called you by name, you are mine."
  — Isaiah 43:1
- |-
  "He will not let your foot slip—he who watches over you will not slumber."
  — Psalm 121:3
- |-
  "The angel of the Lord encamps around those who fear him, and he delivers them."
  — Psalm 34:7
```
## Showcase:

Video: https://youtu.be/54IqG0VpV4Y
