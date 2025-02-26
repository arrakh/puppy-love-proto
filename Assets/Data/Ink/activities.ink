=== activity_memorize ===
Doing memory...
-> END

=== activity_appreciate ===
Appreciating art... #bg black #box event
~ addMood(1)
~ unlockActivity("self_quiz")
-> END

=== activity_writing ===
Writing stories...
-> END

=== activity_exercise ===
Stretching limbs...
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
-> END

=== activity_textmessage ===
-> END

=== activity_vediocall ===
-> END

=== activity_studytogether ===
-> END