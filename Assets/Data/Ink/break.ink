
=== break === //main stitch
{ shuffle:
    - The bell rings, {lastClass} class has ended. #bg class #box event
    - Students rose from their seats as the {lastClass} class rings its bell. #bg class #box event
}

What would you like to do before the next class? 
+ Memorize Materials -> study_logic
+ Increase Mood -> study_mood
+ {hasFirstMeeting} Talk to Shu
-> END

= study_logic
You memorized some materials in the meantime...
~ temp roll = RANDOM(10, 30) / 10.0
You got {roll} Logic!
~ AddLogic(roll)
-> DONE

= study_mood
{ shuffle:
    - You listen to some music
    - You admired the scenic nature outside the window
}
~ temp roll = RANDOM(5, 20) / 10.0
You got {roll} Mood!
~ AddMood(roll)
-> DONE