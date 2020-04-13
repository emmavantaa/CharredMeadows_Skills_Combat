# CharredMeadows_Skills_Combat
A project I worked together with someone else. My job was to create the character/skill progression and combat.<br>
This was the first time I was creating a turn-based battle system and a character progression system.. <br>

Below things will be better explained. 



GameManager: <br>
Gets/Sets Player Ref <br>
Stores player position to PlayerPrefs to save player position while jumping between main world and combat scene. <br>
Gets/Sets Enemy Ref<br>

LevelManager:<br>
World ref (Show/hide worldRoot while entering Combat) <br>
Loads levels<br>

PlayerData[Inherit from CreatureData]:<br>
Contains layer informatio, like health, level etc<br>
It also contains player leveling up progression and adds more after every level gained<br>
<img src="Img/PlayerProgression_Lvl1.png" width="200">
<img src="Img/PlayerProgression_Lvl2.png" width="200">
<br>
<br>
Skills:<br>
Tells how skills work in Battle.<br>
<img src="Img/wolf.png" width="300">
<img src="Img/skillusedwolf.png" width="300">

<br>
SkillData[ScriptableObject]:<br>
Skill information (Invidual skills which uses part from ElementalData)<br>
Levels Skill up (The higher level skill the more it hits)<br>
<img src="Img/skill.png" width="150"> 
<img src="Img/Level_2_Skill.png" width="150">


<br>
ElementalData[ScriptableObject]:<br>
Elemental information (Fire, Water, Wind & Earth)<br>
Levels Elements up (Leveling Elements adds new skills to player)<br>
<img src="Img/scriptableObj_Elem.png" width="200">


 <br>
EnemyManager:<br>
Puts enemy skills in use and tells how it works on the screen.<br>
Randomnizes on what skill enemy uses. <br>
<img src="Img/enemyskillOnplayer.png" width="200">

<br>
BattleManager:<br>
Controls combat progression (Turns)<br>
Sets positions<br>
Puts drops on ground
<img src="Img/enemydeath.png" width="200">
<br>
UIManager:<br>
Shows information<br>

How the planning stage looked for the skill system. <br>
<img src="Img/Screenshot_2.png" width="350">
