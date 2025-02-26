EXTERNAL AddMood(delta)
EXTERNAL AddLogic(delta)
EXTERNAL AddStressLevel(delta)
EXTERNAL UnlockActivity(id)

=== function addMood(delta)
~ AddMood(delta)
{ delta > 0:
    You feel better! (+{delta} Feel) #box event
- else:
    You feel worse... ({delta} Feel) #box event
}

~ return

=== function addLogic(delta)
~ AddLogic(delta)
{ delta > 0:
    You feel smarter! (+{delta} Logic) #box event
- else:
    You feel dumber... ({delta} Logic) #box event
}
~ return

=== function addStressLevel(delta)
~ AddStressLevel(delta)
{ delta > 0:
    You feel less stressed! #box event
- else:
    You feel more stressed... #box event
}
~ return

=== function unlockActivity(id)
~ UnlockActivity(id)
New activity unlocked! #box event
~ return