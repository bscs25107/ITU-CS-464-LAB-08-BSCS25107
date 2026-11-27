# CS464 LAB 07 · Ruhma Binte Asad · BSCS25107

**Demo Video:** [Drive link](https://drive.google.com/file/d/1ZZGVxNGlBphXgAsJ5Punsr4X2z0EagRu/view?usp=sharing)

### Loading Flow
Main Screen to Main Menu to Mode Select to Level Select to Loadout to loading Panel to Gameplay to Win lose panel
pause, setting panels in gameplay, setting in both main menu and gameplay.

### Mixer Groups
**Master**
- Music
- SFX
- UI
**Exposed parameters:** MasterVolume, MusicVolume, SFXVolume, UIVolume.
  
| Source | Output Group |
| :--- | :--- |
| UiAudio |UI |
| SFXPlayer | SFX |
| MusicPlayer |Music |
<img width="1099" height="939" alt="image" src="https://github.com/user-attachments/assets/0c597f52-cf2e-43bf-b748-16788b284434" />

### Volume Mapping and Persistence 
- The volume sliders range from 0.0001 to 1, and the volume is converted into decibels using a logarithmic formula.
- Use a logarithmic scale instead of a normal linear 0–1 volume scale.
- The minimum slider value is set to 0.0001 rather than 0 because log10(0) is undefined
- Each audio group has its own Mute toggle. When muted, its volume is set to -80 dB, but the slider value is saved.
- The settings screen contains four sliders: Master, Music, SFX, and UI. Each slider also displays the current volume as a percentage, which updates as the user changes the slider
- The same audio settings panel is accessible from both the Main Menu and the Pause Menu, so the user doesn't need separate settings screens.
- 
### Music Transitions

| Trigger | To and From | Fade |
| :--- | :--- | :--- |
| Menu scene starts | main screen to menu music | 0.5s|
| Gameplay starts (other snippits through out the game as well when ever their is a silence) | to Game music (low static sound) | 1.5 s|
| Win/Lose | Gameplay to result | 1.0s|

### C# Events
Gameplay was refactored so the systems that detect things only raise events, and independent listener components react to them.

### Publishers

| Publisher | Events |
| :--- | :--- |
| Health |Damaged, Died |
| Pickup |Collected |
| Flow | GameStarted, GameWon, GameLost, Collected |

### Subcribers

| Listener | Subscriber | Response |
| :--- | :--- | :--- |
| HealthBarListener |Health.Damaged | Updates the health bar fill and colour|
| DamageSfx | Health.Damaged, Death | Hurt and death sounds|
| CollectSfx | Pickup.Collected | Pickup sound|
| MusicDirector | GameStarted, GameWon, GameLost |Music crossfades and win and lose sounds|
|  ResultListener| GameWon, GameLost | Opens the win and lose result panel|


### Asset Credits

| File | Use | Author |Source |
| :--- | :--- | :--- | :--- |
| 251390__deadsillyrabbit__button_hover |UI hover | [URL](https://freesound.org/people/deadsillyrabbit/sounds/251390/)| [Author]|
| Click | Buttons(any) | [URL]()| [Author]|
| Click_2 | some button |[URLl()] | [Author]|
| Lose | some button |[URL] |[Author] |
| Success | some button | [URL] |[Author] |
| res | some button |[URL] |[Author] |
| Entry Beep | pickup |[URL] |[Author] |
| open_close | Health |[URL] |[Author] |
| Death | when player dies |[URL] |[Author] |
| Menu | mune music |[URL] |[Author] |
| Gamemusic | gameplay music | [URL] |[Author] |
*was denied access while retriving source(screenshot attached)*
<img width="1901" height="951" alt="image" src="https://github.com/user-attachments/assets/d8581d3b-6e15-4f57-abd1-f73d10dd55ea" />

