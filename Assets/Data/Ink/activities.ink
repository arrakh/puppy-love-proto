=== activity_memorize ===
{ shuffle:
- You memorized tomorrow's class materials...
- You reviewed your notes and tested yourself on the key points...
- You repeated important facts in your head until they stuck...
- You used flashcards to reinforce what you learned...
- You tried to recall what you studied yesterday to strengthen memory...
}
~ addLogic(1)
-> END

=== activity_appreciate ===
{ shuffle:
- You listen to some classical music, focusing on the intricacies of the melody and rhythm...
- You researched modern painting and examined how it makes you feel...
- You watched a documentary about an artist and their work...
- You took a moment to admire the architecture of a nearby building...
- You listened to a poem being recited and reflected on its meaning...
}
~ addMood(2)
-> END

=== activity_writing ===
{ shuffle:
- You wrote a short story on success...
- You jotted down your thoughts in a journal...
- You created a poem about nature...
- You crafted a letter to your future self...
- You described a dream you had in vivid detail...
}
~ addMood(1)
-> END

=== activity_self_quiz ===
{ shuffle:
- You tested yourself with a few tricky questions...
- You tried to recall facts from memory without checking your notes...
- You challenged yourself with a set of flashcards...
- You wrote down answers to random questions to see what you still remember...
- You created a quiz for yourself and tried to beat your previous score...
}
~ addLogic(2)
-> END

=== activity_exercise ===
{ shuffle:
- You did some push ups...
- You went for a quick jog around the block...
- You stretched to loosen up your muscles...
- You practiced balancing on one foot for a minute...
- You lifted some light weights to build endurance...
}
~ addMood(1)
~ addLogic(1)
-> END

=== activity_watchmovie ===
You decided to text Shu. #bg text #box event
Movie at your place? #box left
{isMorning == true:
    -> cantMovie
- else:
    -> watchMovie
}
-> END
=== cantMovie ===
It's 7AM and we have class soon, what are you talking about #box right
Right... #box left
-> DONE

=== watchMovie ===
Yep, see you there #box right
{horrorlove == false:
    ->horrorMovie
- else:
    -> romanticMovie
}
->DONE
=== horrorMovie ===
TV glow paints both of your faces blue. A door creaks onscreen.#bg movie #box event
"This was your brilliant date idea?"#box left
"Yep, because I noticed someone literally googled 'how to look brave' yesterday—"#box right

A shadow lunges. Both gasp. The popcorn bowl flies.#box event
"WHY’S THE CAMERA ZOOMING IN ON THE CLOSET?!"#box right
"Pause! I need to check if the front door’s—"#box left
"Don’t you dare move. And stop breathing so loud."#box right

Silence. Their shoulders tense as the killer’s footsteps resume.#box event
"…Do psychos kill people who failed algebra?"#box left
"AHA, I know someone is afraid now!"#box right
"Shuuu! We should focus on the movie."#box left
~ addMood(2)
->DONE

=== romanticMovie ===
The screen flickers with the ballroom scene from Pride and Prejudice. Shu's knees hasn’t stopped jiggling since the third corset-lacing montage.#bg movie #box event
"You’re judging Darcy harder than my mom judges my math scores."#box left
"Bro took 90 minutes to say ‘I like you’." (snorts) "Could've DMed her."#box right
"Says the guy who circled my desk 17 times before asking for a pen."#box left
"Your pen had that…glitter cat charm... Distracting..."#box right
"OK, You always say so. But I know."#box right
~ addMood(2)
-> DONE

=== activity_playvediogame ===
It takes Two is quite popular in class recently.#bg game #box event
"Alright, you push the block, and I'll stand on the switch."#box left
"Got it! pushes the block the wrong way"#box right
"SHU!"#box left
"Wait, wait, I can fix this—"#box right
"You just trapped us in the corner."#box left
" …Reset the level?"#box right
"I’m never letting you lead again."#box left
~ addMood(2)
~ addStressLevel(-1)
-> END

=== activity_textmessage ===
Phone screen glows. A new message from SHU.#bg text #box event
"You asleep?"#box right
"Almost. Why?"#box left
"Just thinking."#box right
"Uh oh. About what?"#box left
"If we’re holding hands, and I need to sneeze, do I let go or power through?"#box right
"…Go to sleep, SHU."#box left
"No but fr, what would you do?"#box right
"Sneeze into your other hand and never touch me again."#box left
"Rude."#box right
"Love you tho. Now sleep."#box left
"Love you too. Also, I’d power through."#box right
~ addMood(2)
~ addStressLevel(-1)
-> END

=== activity_vediocall ===
You are lying on bed, staring at the ceiling, on the phone with SHU.#bg video #box event
"Ugh. I hate this."#box right
"What now?"#box left
"Everyone keeps asking what major I’m choosing. I have no idea!"#box right
"Yeah, it’s kinda stressful. Any ideas at all?"#box left
"I mean… I like basketball, but my parents keep saying I should do something “practical.”#box right
"Classic. So, business? Engineering?"#box left
"I’d rather eat my own math homework."#box right
"So… not practical, got it."#box left
"What about you? You’ve had it figured out since forever."#box right
"Computer science. I like coding, it makes sense."#box left
"Must be nice, having a functional brain."#box right
"You’re smart too. You just overthink everything."#box left
"Maybe I’ll just pick something random and hope for the best."#box right
"Or, hear me out—pick something you actually like."#box left
"Bold of you to assume I know what I like."#box right
"Okay, new plan: I make a list, and you rank them from 'tolerable' to 'please no.'"#box left
"That… actually sounds helpful."#box right
"I know. That’s why I suggested it."#box left
"Alright, fine. Send me the list. Just don’t put math on there."#box right
"No promises."#box left
~ addMood(1)
~ addStressLevel(-1)
-> END

=== activity_studytogether ===
PLAYVEDIOGAME#bg desk #box left
You are reading, SHU is lying on the table, groaning.#box event
"I can’t do this anymore. I’m gonna pass out."#box right
"We’ve studied for 20 minutes."#box left
"Exactly. My brain is fried. Let me nap."#box right
"No. One more section. Then nap."#box left
"You’re heartless!"#box right
"Here. handmade Pillow."#box left
"YOU LOVE ME."#box right
"Unfortunately."#box left
"Wake me up in five minutes."#box right
"Gonna let you sleep for thirty so I can study in peace."#box left
~ addMood(2)
~ addLogic(1)
~ addStressLevel(-1)
-> END