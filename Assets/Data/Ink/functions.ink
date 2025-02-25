EXTERNAL AddMood(delta)
EXTERNAL AddLogic(delta)
EXTERNAL AddStressLevel(delta)
EXTERNAL UnlockActivity(id)

=== function addMood(delta)
~ AddMood(delta)
You feel better! (+{delta} Mood) #box event
~ return

=== function addLogic(delta)
~ AddLogic(delta)
You feel smarter! (+{delta} Logic) #box event
~ return

=== function addStressLevel(delta)
~ AddStressLevel(delta)
You feel more stressed... (+{delta} Stress Level) #box event
~ return

=== function unlockActivity(id)
~ UnlockActivity(id)
New activity unlocked! #box event
~ return