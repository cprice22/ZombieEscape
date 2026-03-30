# ZombieEscape

**Development Environment**
- Engine: Unity 6000.3.9f1
- Render Pipeline: Universal Render Pipeline (URP)
- Platform: Android
- Input System: UI Button-based mobile input
- Physics: Rigidbody-based movement
- Tested On: Windows 11 (Editor testing) & Samsung Galaxy S9+ Android mobile device (Build & Run testing)

**How to Play**
Objective
- Survive as long as possible while avoiding zombies and obstacles to achieve the highest possible run time.

Core Movement
 - The player is constantly moving forward automatically
 - The player can switch between three lanes (left, center, right)
 - All inputs move the player one lane at a time

Touch Controls (On-Screen Buttons)
 - On-screen Left and Right buttons are displayed
 - Tapping: Left button → moves player one lane to the left
 - Tapping: Right button → moves player one lane to the right

Swipe Controls
 - Swipe gestures can also be used for movement
 - Swiping: Swipe Left → moves player one lane left
 - Swiping: Swipe Right → moves player one lane right

Gyroscope Controls (Tilt)
 - The game can use the device’s gyroscope for movement
 - Tilting the device: Left → moves player left
 - Tilting the device: Right → moves player right
Note:
 - Sensitivity may vary depending on the device
 - Sudden tilts may cause rapid lane switching

Home Screen
 - Start button → begins the game
 - Instructions button → opens the instructions page
 - Quit button → exits the game

Pause Control
 - A Pause button is available on screen
 - Tapping Pause:
 - Stops gameplay
 - Freezes player movement
 - Opens the pause menu

Game Over Interaction
When the player collides with a zombie:
 - Movement stops
 - Timer stops
 - Game Over screen appears
From here, the player can:
 - Return to the main menu

Notes
 - Movement is restricted to lane-based switching (not free movement)
 - Inputs are ignored if the player is already at the far left or right lane
 - All controls only work when the game is active (not paused)

Gameplay Flow
- Start from the Start Screen
- The player runs forward automatically
- Switch lanes to avoid zombies
- If you collide with a zombie, the game ends
- Your total survival time is displayed on the Game Over screen
- Restart to play again
