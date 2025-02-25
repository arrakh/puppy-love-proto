=== break === //main stitch

{ shuffle:
    - The bell rings, {lastClass} class has ended. #bg class #box event
    - Students rose from their seats as the {lastClass} class rings its bell. #bg class #box event
}

What would you like to do before the next class? 
+ Memorize Materials -> study_logic
+ Increase Mood -> study_mood
+ {hasFirstMeeting} Talk to Shu

= study_logic
You memorized some materials in the meantime...
~ temp random = random(10, 30)
~ random = random / 10
You got {random} Logic!
~ AddLogic(random)
-> DONE

= study_mood
{ shuffle:
    - You listen to some music
    - You admired the scenic nature outside the window
}

~ temp random = random(5, 20)
~ random = random / 10
You got {random} Mood!
~ AddMood(random)
-> DONE