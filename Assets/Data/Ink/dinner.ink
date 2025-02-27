=== dinner_first_time ===
As much as you dislike them, you admit eating at the same time everyday is good for your health. #bpx event #bg dinner
Not like you have a choice. Any excuse you've made ends up in a long lecture anyways.
I could've used this time to study instead... #box left
What did you say? #box right
Nothing, father #box left
Your father and I would rather work too, but you know this is tradition. #box right
Yes, mother #box left
Now stop with that nonsense and eat. #box Alright
~ addStressLevel(1)

-> END

=== dinner_week ===
So, how was your week? Anything worth mentioning? #box right #bg dinner
* It was fine, pretty much the same as last week. #box left
* Exhausting… there was so much homework. #box left
* Not bad, at least nothing went terribly wrong. #box left

- How’s your studying going? Making any progress? #box right
* Pretty good, I feel like I’ve got a solid grasp on everything. #box left
* It’s okay… there are some parts I don’t quite understand. #box left
* The workload is overwhelming. I can barely keep up. #box left

- your grades? How did your latest exams go? #box right
* They’re fine, nothing really changed. #box left
* I might not have done well… but I really tried. #box left
* Why do we have to talk about grades every time we eat? #box left

- You finish your food in silence... #box event
->END

=== dinner_straight_a ===
You fix your gaze to the food served in front of you. #box event #bg dinner
Looking at today's results, they nod dismissively. #box event
{ shuffle:
- Your college entrance exam is the most important thing right now. Just focus on studying—nothing else matters. #box right
- Make sure you study hard. Don’t think I’m nagging for no reason—we’re only doing this for your own good. #box right
- Your test scores are not bad — keep up the good work. #box right
- You've improved, not bad. #box right
}

-> END


=== dinner_fail ===
As hard as you try to look down, you can feel a gaze of disappointment. #box event #bg dinner
Looking at today's results, they sigh heavily. #box event
{ shuffle:
- Why did your grades drop? You need to work harder. #box right
- No more distractions. From now on, focus only on your studies! #box right
- Stop wasting time and focus on your studies! You can't afford to slack off. #box right
- Other students can manage good grades, so why can’t you? You need to work harder! #box right
- You’re not a child anymore. It’s time to take responsibility for your own future. #box right
}
~ addStressLevel(1)

-> END