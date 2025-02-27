INCLUDE love.ink
INCLUDE dinner.ink
INCLUDE break.ink
INCLUDE variables.ink
INCLUDE activities.ink
INCLUDE functions.ink



////TAGS
//#bg (name)
//changes background displayed in the dialog
//names:
//- black
//- dinner
//- break
//- school
//- dinner

//#box (position)
//changes the text box displayed in the dialog
//positions:
//- left
//- right
//- event
-> first_moment

=== morning ===

//This line needs to be here
Yawn... \<i>(Click anywhere to continue) #bg black #box left 

{ shuffle:
    - Another day, another knowledge.
    - What class to do today...
    - Rise and shine, me.
} 

-> END

=== fail ===
The piece of paper stares at you, reminding you of your failure...
~ addStressLevel(1)
-> END

=== game_over ===
The throbbing in your head doesn't stop #bg black #box event
Unnatural to your routine, your feet stops by a bridge on the way home.
This is stupidly out of character... #box left
For a single moment, you entertain the thought. #box event
-> END

