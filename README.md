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
debug: true
# HSM Hints Configs (ONLY APPLIES FOR MY PLUGIN AND WONT BREAK OTHER PLUGINS)
global_hint_duration: 5
global_hint_size: 30
global_hint_y: 950
# Custom Item Sound (AudioPath will only DISPLAY your Current Path do NOT edit, edit Clip name and IF nessesary ClipPathFolder)
clip_name: 'CruxifixSound.ogg'
clip_name_bible: 'BibleSound.ogg'
clip_path_folder: 'audio'
clip_duration: 5
max_clip_range: 15
clip_volume: 1
# Custom Item 914 Configs (Wont destroy Basegame but might conflict with other 914 Plugins)
custom914: true
custom_item_recipe_dictionary:
- item_in: KeycardO5
  custom_item_input: 
  item_out: 
  custom_item_output: 6969
  knob_setting: Fine
  chance: 75
- item_in: 
  custom_item_input: 6969
  item_out: Coin
  custom_item_output: 
  knob_setting: Coarse
  chance: 20
- item_in: 
  custom_item_input: 6969
  item_out: Adrenaline
  custom_item_output: 
  knob_setting: OneToOne
  chance: 40
- item_in: 
  custom_item_input: 6969
  item_out: SCP500
  custom_item_output: 
  knob_setting: Fine
  chance: 30
- item_in: 
  custom_item_input: 6969
  item_out: 
  custom_item_output: 6700
  knob_setting: VeryFine
  chance: 15
- item_in: Coin
  custom_item_input: 
  item_out: 
  custom_item_output: 6969
  knob_setting: VeryFine
  chance: 5
- item_in: 
  custom_item_input: 6969
  item_out: 
  custom_item_output: 6999
  knob_setting: Coarse
  chance: 30
- item_in: 
  custom_item_input: 6969
  item_out: 
  custom_item_output: 6969
  knob_setting: OneToOne
  chance: 10
- item_in: 
  custom_item_input: 1488
  item_out: 
  custom_item_output: 6969
  knob_setting: Fine
  chance: 20
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
- Falldown
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
# Effects the Bible Crures when applied (Put NEGATIVE effects here or effects dealing DAMAGE)
bible_heal_effect_list:
- Bleeding
- Corroding
- Poisoned
- Scp207
- Scp1853
- SeveredHands
- CardiacArrest
- SeveredEyes
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
# Custom Items Weight
custom_item_weight: 1
# Holy Bible Configs
bible_custom_item_i_d: 6700
bible_custom_item_range: 15
bible_custom_item_scale:
  x: 6.7
  y: 1
  z: 4.2
bible_custom_item_spawn_properties:
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
bible_custom_item_effects:
- Asphyxiated
- Flashed
- Blurred
- Concussed
bible_custom_effect_dur: 5
bible_custom_schematic_name: 'HolyBible'
bible_custom_animation_name: 'BibleAnimation'
# Custom Items SpawnProperties
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
custom_item_name: 'Crucifix'
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
# Bible Custom Item Use Hint
bible_custom_item_name: 'Holy Bible'
bible_custom_item_description: 'It is indeed Holy!'
bible_custom_item_hint_usage: |-
  You can use the Bible to get out of Dangerous Situations! 
   Just use the Item!
bible_custom_item_no_danger: 'Theres no danger!'
bible_custom_item_show_cooldown: 'You got Cooldown for {DurationCooldown} Seconds!'
bible_custom_item_cooldown: '<color=red>You didn''t held the item Long enough!</color>'
bible_custom_item_holding_hint: '<color=red>You must hold the item for {DurationTime} seconds.</color>!'
# You can Edit the Bible Verses but do NOT Edit the Names: 'PocketDimension, NearbySCP, NearbyEnemy and LowHealth'
bible_verses_for_bible_item:
  PocketDimension:
  - 'Isaiah 55:8-9: ''For my thoughts are not your thoughts, neither are your ways my ways,'' declares the Lord. ''As the heavens are higher than the earth, so are my ways higher than your ways and my thoughts than your thoughts.'''
  - '2 Corinthians 5:1: ''For we know that if the earthly tent we live in is destroyed, we have a building from God, an eternal house in heaven, not built by human hands.'''
  - 'Ephesians 3:18-19: ''May you have power, together with all the Lord’s holy people, to grasp how wide and long and high and deep is the love of Christ, and to know this love that surpasses knowledge—that you may be filled to the measure of all the fullness of God.'''
  NearbySCP:
  - 'Psalm 91:7: ''A thousand may fall at your side, ten thousand at your right hand, but it will not come near you.'''
  - 'Isaiah 54:17: ''No weapon forged against you will prevail, and you will refute every tongue that accuses you. This is the heritage of the servants of the Lord, and this is their vindication from me,’ declares the Lord.'''
  - 'Psalm 27:1: ''The Lord is my light and my salvation—whom shall I fear? The Lord is the stronghold of my life—of whom shall I be afraid?'''
  NearbyEnemy:
  - 'Romans 12:19: ''Do not take revenge, my dear friends, but leave room for God’s wrath, for it is written: ‘It is mine to avenge; I will repay,’ says the Lord.'''
  - 'Psalm 23:5: ''You prepare a table before me in the presence of my enemies. You anoint my head with oil; my cup overflows.'''
  - 'Matthew 5:44: ''But I tell you, love your enemies and pray for those who persecute you.'''
  LowHealth:
  - 'Jeremiah 30:17: ''But I will restore you to health and heal your wounds,’ declares the Lord, ‘because you are called an outcast, Zion for whom no one cares.’'''
  - 'Psalm 147:3: ''He heals the brokenhearted and binds up their wounds.'''
  - 'Isaiah 53:5: ''But he was pierced for our transgressions, he was crushed for our iniquities; the punishment that brought us peace was on him, and by his wounds we are healed.'''
```
## Showcase:

Video: https://www.youtube.com/watch?v=54IqG0VpV4Y
