=== activity_memorize ===
Doing memory...
~ addLogic(1)
-> END

=== activity_appreciate ===
Appreciating art... #bg black #box event
~ addMood(1)
~ unlockActivity("self_quiz")
-> END

=== activity_writing ===
Writing stories...
~ addMood(1)
-> END

=== activity_exercise ===
Stretching limbs...
~ addLogic(1)
-> END

=== activity_watchmovie ===
...
{isMorning == true:
    -> cantMovie
- else:
    -> watchMovie
}
-> END
=== cantMovie ===
It's morning! Better to study...
-> DONE

=== watchMovie ===
{horrorlove == false:
    ->horrorMovie
- else:
    -> romanticMovie
}
->DONE
=== horrorMovie ===
TV glow paints their faces blue. A door creaks onscreen.#box event
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
~ addMood(1)
->DONE
=== romanticMovie ===
Screen flickers with the ballroom scene from Pride and Prejudice. His knee hasn’t stopped jiggling since the third corset-lacing montage.#box event
"You’re judging Darcy harder than my mom judges my math scores."#box left
"Bro took 90 minutes to say ‘I like you’." (snorts) "Could've DMed her."#box right
"Says the guy who circled my desk 17 times before asking for a pen."#box left
"Your pen had that…glitter cat charm... Distracting..."#box right
"OK, You always say so. But I know."#box right
~ addMood(2)
-> DONE

=== activity_playvediogame ===
It takes Two is quite popular in class recently.#box event
"Alright, you push the block, and I'll stand on the switch."#box left
"Got it! pushes the block the wrong way"#box right
"SHU!"#box left
"Wait, wait, I can fix this—"#box right
"You just trapped us in the corner."#box left
" …Reset the level?"#box right
"I’m never letting you lead again."#box left
~ addMood(2)
-> END

=== activity_textmessage ===
Phone screen glows. A new message from SHU.#box event
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
-> END

=== activity_vediocall ===
You are lying on bed, staring at the ceiling, on the phone with SHU.#box event
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
~ addMood(0)
-> END

=== activity_studytogether ===
PLAYVEDIOGAME#box left
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
~ AddLogic(1)
-> END