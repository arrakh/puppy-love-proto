//->break
=== break === //main stitch
{ shuffle:
    - The bell rings, {lastClass} class has ended. #bg class #box event
    - Students rose from their seats as the {lastClass} class rings its bell. #bg class #box event
}
 ~love = 5  
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
-> DONE

=== puppyDialogue ===
What would you like to do before the next class? 
+ Memorize Materials -> study_logic
+ Take a short break -> study_mood
+ Talk with Shu  -> highLoveDialogue
-> DONE

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

=== highLoveDialogue ===
...
{love == 1:
    -> PoetryTopic
}
{love == 2:
    -> FoodTopic
}
{love == 3:
    -> Basketball
}
{love == 4:
    -> MovieTopic
}
{love == 5:
    -> Randomsentence
}





-> DONE

=== MovieTopic ===
 "Shu comes to my seat and knocks on the desk. He smiles and takes out a movie CD, asking 'Do you like horror movies?'"
    + "Yes, I love horror movies."
        ~ horrorlove = true
        -> horrorMovieResponse
    + "No, romantic movies are my favorite."
        -> romanticMovieResponse
        
=== horrorMovieResponse ===
"Great! We should watch one together sometime."
~love =5
~unlockActivity("watchmovie")
-> DONE

=== romanticMovieResponse ===
"Oh, I see! Maybe we can watch a romantic movie together?"
~love =5
~unlockActivity("watchmovie")
-> DONE

=== PoetryTopic===
"Hey, Last time you didn't tell me which one is your favorite in Stray Birds."#box right
"Hmm… there’s one I keep thinking about."#box left
"Which one?"#box right  
"'Clouds come floating into my life, no longer to carry rain or usher storm, but to add color to my sunset sky.'"#box left  
"Oh, I love that one. It feels… peaceful." #box right 
"Yeah. Like even sadness can be something gentle." #box left 
"Do you think clouds ever get tired?"  #box right
"Maybe. But they always find their way back to the sky."  #box left
 ~love = 2
 //test{love} love point
 ~unlockActivity("vediocall")
->DONE

=== FoodTopic ===
(Watching Shu eat.)  
"You're like a squirrel, you know that?"  #box left
"Huh?"  #box right
"The way you nibble. And how you keep small bites in your cheeks before swallowing."  #box left
"I do not!"  #box right
"You totally do."  #box left
"...Is it cute at least?"  #box right
"The cutest." #box left
 ~love = 3
  ~unlockActivity("studytogether")
->DONE

=== Basketball ===
(Watching Shu play basketball.)  
"Whoa, that was a nice shot!"  #box left
"Of course."  #box right
"Do it again."  #box left
"Why?"  #box right
"Because I wasn't recording."  #box left
"Are you my coach or my fan?"  #box right
"Both, obviously."  #box left
"Damn. I should start charging tickets." #box right
 ~love = 4
(Shu laughs, takes another shot—swish. )#box event
 ~unlockActivity("playvediogame")
->DONE

=== Randomsentence ===
{ shuffle:
- You and Shu talk about the exam you just had, comparing answers and sighing at the tricky questions.
- Shu stretches lazily, tapping their pen against the desk. 'Finally, a break…'
- You and Shu share a snack, debating whether the vending machine chips taste different today.
- Outside, the sun is warm. You and Shu sit on the steps, watching classmates pass by.
- Shu scrolls through their phone, occasionally showing you funny memes.
}
-> DONE

=== hallwayScene ===
"You pass by a group of classmates playing with paper airplanes in the hallway. He’s among them."
+ "Let me try! Let’s see whose can fly the farthest."
+ "I’ll sit this one out—class is starting soon." 
-
-> DONE

=== handwriting ===
You accidentally came across his essay. The paper was clean and neat, even carrying a faint fragrance. To your surprise, his handwriting was exceptionally beautiful, showing the care and effort he put into writing it. It left a very good impression on you.
+ "Wow, your handwriting is really nice!"
    "ahh, Thank you"
    -> DONE
+ "Silently compare it to your own handwriting"
    -> DONE



















