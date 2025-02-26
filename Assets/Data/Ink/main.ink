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

