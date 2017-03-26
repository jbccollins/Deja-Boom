Contents

1. Functional Description
2. Controls and Movement
3. Known limitations / bugs
4. Sources

----------------------------------------------------------------

1. Functional Description

Project Title: Deja Boom
Team Members: James Collins

The game has an online web player at https://jbccollins.github.io/Deja-Boom/
Note that Google Chrome does not support the unity web player so you will have to use a different browser.

A video demonstrating the game’s features and how to beat it can be found at https://www.youtube.com/watch?v=0y8tRRhk_qc&feature=youtu.be

Short Description:

Deja Boom is a 3D puzzle game in which you create clones to help you
defuse a bomb within a certain time limit. You must avoid and/or
deactivate walls of lightning while maneuvering around the play space
to reach your end goal.

In-Depth explanation:

You can clone your player when a few conditions are satisfied

 - 1. You are alive
 - 2. All past versions of yourself have expired 
     (i.e. there are no past versions of yourself active in the play space)
 - 3. The level has not ended

When you clone your player the game state will reset to what it looked like at the
beginning of the level. The old player will attempt to traverse the same path
that your player took up until the moment you created the clone. For example
Say you start at time 0s and move the player forward for two seconds and then 
right for two seconds. At four seconds you clone your player. Your clone will spawn
in a new location while your old player will follow the same path that you just took.
It will move forward for two seconds and then move right for two seconds while you 
control the new clone. Old players expire after their duration (i.e. their game
objects are removed from the play space. They do not persist after traversing their
paths)

If this sounds confusing the video submitted for this project should help to clarify
how the cloning process actually works.

Your actions can determine the fate of the old players. If you trigger an event
that leads to the death of an old player then the level is over. You can bump
into old players with your new player which can alter the path of your old player
and affect the outcome of that level.

You can die in three ways

 - 1. You fall off the map
 - 2. You collide with a lightning wall
 - 3. Time runs out and the bomb explodes

Your goal is to reach the green circle in the play space.
Other circles in the play space will activate and deactivate objects in the 
game space when a player enters them.


----------------------------------------------------------------

2. Controls and Movement

Control player movement with the arrow keys.
Jump with space bar
Clone your player with ‘c’
Press ‘r’ to restart the current level at any point

---------------------------------------------------------------

3. Known limitations / bugs

The accuracy of recording deteriorates slowly over time. Meaning that 
the longer player’s path is recorded the more likely it is that the recorded 
player will exhibit slight deviations from the original path when the
recording is played back. The library (InputVCR) attempts to correct for such 
deviations but this corrective action can interfere with the true position of
the player sometimes. To mitigate this issue avoid cloning while the player is
in motion. Clone after the player has been still for about a second to let the 
library catch up.

---------------------------------------------------------------

4. Sources

Downloaded Assets:
- InputVCR: This library handles the core recording functionality. I made some
  modifications as necessary but most of the code is in it’s original state. It
  was created by Eddie Cameron who’s website can be found here: 
  http://www.grapefruitgames.com/
- Character Model: This model and all of it’s animations were included in the 
  demo scene that comes with the InputVCR library
- Nuclear Bomb: The nuclear bomb model in the start menu scene was downloaded from
  the unity asset store. It was published by KK Design. You can find it by searching
  for “Nuclear Bomb” with the “Free Only” filter on and it should be the first result
- The explosion effects that happen when the player runs out of time are generated
  by a library called “Detonator Explosion Framework” 
  written by Ben Throop: https://twitter.com/ben_throop
- The lightning effects on the death barriers use an asset called “Electricity/Lightning Shader”
  created by Ori Hanegby: https://www.facebook.com/hanegby
- All game music was downloaded from: http://soundimage.org/fantasywonder/
- The Wilhelm scream and zap sound effects were downloaded from a royalty free sounds website
  that I can’t seem to find anymore. In any case I did not create them.
- The wall and floor materials were part of free texture packs downloaded from the unity
  asset store. They are called “ADG_Textures” and “pbr_textures”.

The lightning poles were made by me out of basic 3D shapes.
