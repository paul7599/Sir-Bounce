# Sir-Bounce
Game created in Unity for CS50

This is a game I created in Unity using C#. It is an endless game, with randomly generated terrain. 
The goal is simple - the higher you make Sir Bounce go, the more points you score.
The game becomes increasingly harder as you reach higher, by generating the obstacles closer to each other, thus leaving fewer safe paths.
You can also use power-ups (Slow Motion, Reverse Gravity, Transparency, Safety Net, Hat Bigger), that are also randomly generated.
If you drop Sir Bounce out of the screen, the game is over.
For the design of the hero, the hat and the obstacles, I used Photoshop and Affinity Designer.

I created 6 scripts in order to better control the different features of the game:
• heroScript - takes care of the movements of the hero (Sir Bounce)
• cicrleScript - instantiates the hat that you use to propel the hero (Sir Bounce)
• terrainScript - randomly generates the terrain to come, and erases the terrain that's passed
• powerupScript - randomly generates power-ups
• powerupCheck - makes sure that the power-ups are generated within the walls of the terrain, and don't end up inside an obstacle
• cameraMovement - follows the hero (Sir Bounce) as he bounces through the obstacles
