//->break
=== break === //main stitch
{ shuffle:
    - The bell rings, {lastClass} class has ended. #bg class #box event
    - Students rose from their seats as the {lastClass} class rings its bell. #bg class #box event
}

{love == 0:
    -> normalDialogue
- else:
    -> puppyDialogue
}
-> END

=== normalDialogue ===
What would you like to do before the next class? 
+ Memorize Materials -> study_logic
+ Take a short break -> study_mood
-> END

=== puppyDialogue ===
What would you like to do before the next class? 
+ Memorize Materials -> study_logic
+ Take a short break -> study_mood
+ Talk with Shu  -> highLoveDialogue
-> END

=== study_logic ===
You memorized some materials in the meantime...
~ temp roll = RANDOM(10, 30) / 10.0
You got {roll} Logic!
//~ AddLogic(roll)
-> DONE

=== study_mood ===
{ shuffle:
    - You listen to some music
    - You admired the scenic nature outside the window
}
~ temp roll = RANDOM(5, 20) / 10.0
You got {roll} Mood!
//~ AddMood(roll)
-> DONE

-> END

=== highLoveDialogue ===
~ temp rnd = RANDOM(1, 3)
{rnd == 1:
   -> MovieTopic
        
rnd == 2:
    -> hallwayScene
    
rnd == 3:
    -> handwriting
    
    
}
-> END

=== MovieTopic ===
 "Shu comes to my seat and knocks on the desk. He smiles and takes out a movie CD, asking 'Do you like horror movies?'"
    + "Yes, I love horror movies."
        ~ horrorlove = true
        -> horrorMovieResponse
    + "No, romantic movies are my favorite."
        -> romanticMovieResponse
        
=== horrorMovieResponse ===
"Great! We should watch one together sometime."
-> END

=== romanticMovieResponse ===
"Oh, I see! Maybe we can watch a romantic movie together?"
-> END

=== hallwayScene ===
"You pass by a group of classmates playing with paper airplanes in the hallway. He’s among them."
+ "Let me try! Let’s see whose can fly the farthest."
+ "I’ll sit this one out—class is starting soon." 
-
-> END

=== handwriting ===
You accidentally came across his essay. The paper was clean and neat, even carrying a faint fragrance. To your surprise, his handwriting was exceptionally beautiful, showing the care and effort he put into writing it. It left a very good impression on you.
+ "Wow, your handwriting is really nice!"
    "ahh, Thank you"
    -> END
+ "Silently compare it to your own handwriting"
-> END



















