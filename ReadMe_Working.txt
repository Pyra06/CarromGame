# Simple Carrom Game

It is unity carrom game project in which I have implemented following features:-
1) Added all game elements of Carrom. 
2) Sling like shot ability for Striker.
3) Custom Physics for a smooth game.
4) Added boundaries to the carrom board sprite for striker and coin bounce.
5) Included a score system - White(1), Black(1), Queen(2).
6) Custom timer for each player (~ 10sec)
7) Implemented logic for Player2 so that it can take shots on its own.


## Working

1) When the game boots, it shows a Start_Button, when pressed it populates the board with Coins and Striker.
2) Striker - To shoot a Striker, it needs to be dragged back. Once we drag it an arrow and power_Circle appear. Now at which ever power and direction we release the striker, the power gets multiplied to the direction of arrow and this force is applied on striker as Impulse.
3) Timer - If the player fail to hit the striker before it runs out, then the timer resets when its lifetime is over and starts for Player2 and this repeats. But if the player do hit the striker then timer does not wait for its lifetime to get over, instead it resets at that very moment.
4) Score - If a White Coin goes to any of the 4 holes Player1 gets 1 point, if Black Coin is potted then Player2 gets 1 point and if the Queen is potted then which ever player took the shot gets 2 points. 
5) BotPlayer - How Player2 works is that, the bot select a Black Coin at random to take a shot at. Then it calculates the direction of that coin. After this the bot hits the striker in that direction with a random Hit_Multiplier(<4).

## Script Activity Flow

1) When the Start_Button is pressed 4 actions take place. Coins are populated (Using PrefabManager Script), Striker for Player1 is populated (Using PrefabManager Script), Timer for Player1 starts (using CircularTimer Script) and The button is set InActive.
a) CircularTimer Script - This script is responsible for Switching the timer between Player1 and Player2 as well as it is responsible for Starting and Resetting the timer.
b) PrefabManager Script - A custom logic is implemented in this script for populating coins in a desired pattern. And a logic for populating Striker based on whose turn it is.

2) Guiding Arrow - Script StrikerMovement is attached to Striker. So when we drag on striker SliderInput() method is called and  when we release the striker it is detected using OnMouseUP() which calls StikerHit() method if the sticker is not that of bot.
a) SliderInput() - It is responsible for changing scale and rotation of Guiding_Arrow and Power_Circle.
b) StikerHit() - It is responsible for adding impulse force to our striker based on direction and power we get from SliderInput().

3) Striker Shoot - There are 2 different methods for Hitting the striker, one for player i.e StikerHit() and other for bot i.e EnemyStikerHit().
a) EnemyStikerHit() - It handles the logic for calculating the direction * hit power for the strike and implemnts it when it is the bot's turn. 

4) Scoring System - It uses the HoleDetection script to increment the score of either Player1 or Bot based on some parameters passed in that script.
a) HoleDetection Script - If a Coin hits any of the 4 holes a trigger is initiated. This trigger checks the tag of coin, if it is Black Coin then Bot gets 1 point, if it is White Coin then Player1 gets 1 point and if it is Queen then which ever player or bot took the shot gets 2 points. 

5) There are 3 more scripts such as BotBehaviour, MouseFollow and CoinMovement.
6) BotBehaviour Script - It is the script that is responsible for deciding whether it is Player's turn or the Bot's turn. 
7) MouseFollow Script - This script is responsible for getting the position of mouse pointer when it is called in StrikerMovement script. This position is responsible for giving shootPower a value.
8) CoinMovement Script - It keeps the coin with a clamped boundary(i.e our border limit of carrom board).