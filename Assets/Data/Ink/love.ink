
=== first_moment ===
Feeling down after failing a class, you walk toward the shelter in the library, expecting to find solace in your usual poetry book. But instead, you see SHU leaning against the shelf, flipping through the pages of Stray Birds. #bg black #box event
+ Look for another copy of Stray Birds -> PRETEND
+ Talk to Shu -> DIRECT

-> END

=== PRETEND ===
...
"Ahhh...S.T..."#box left
"You’re not very good at pretending, you know."#box right
"I… wasn’t pretending."#box left
"Really? So you just happened to need a book from this exact shelf?"#box right
"...Maybe."#box left
"You like poetry too?"#box right
"Yeah… I come here to read it whenever I’m feeling down."#box left
 "Then you should have it. I can always read it another time. Poetry always has magic."#box right
"Thanks. But... Didn’t think someone like you would be into Stray Birds."#box left
"What’s that supposed to mean? Because I’m always on the basketball court?"#box right
"Something like that."#box left
"Guess I’m full of surprises."#box right

Shu handed you the book before leaving the library. Watching his figure disappear, you felt a little lighter, and for the first time, you found yourself looking forward to talking with SHU about Stray Birds next time. #box event

~love = 1
 ~unlockActivity("textmessage")
-> DONE

=== DIRECT ===
...
"I didn’t expect you to read poetry."#box left
"Why not?"#box right
"I just thought… someone sporty like you wouldn’t be into it."#box left
"I guess even athletes need a little poetry sometimes. What about you? You seem… a little down."#box right
"Failed my exam today. Whenever I feel bad, I read this book. It always makes me feel a little lighter."#box left
"Then you should have it. I can always read it another time."#box right
"Are you sure?"#box left
"Yeah. But only if you tell me your favorite poem from it next time."#box right
Shu handed you the book before leaving the library. Watching his figure disappear, you felt a little lighter, and for the first time, you found yourself looking forward to talking with SHU about Stray Birds next time. #box event

~love = 1
 ~unlockActivity("textmessage")
-> DONE

