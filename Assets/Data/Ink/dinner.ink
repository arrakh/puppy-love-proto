
=== dinner_week ===
How was your week? Anything worth mentioning? #box right #bg dinner
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
~ addStressLevel(1)
->END

=== dinner_straight_a ===
{ shuffle:
- Your college entrance exam is the most important thing right now. Just focus on studying—nothing else matters.
- Make sure you study hard. Don’t think I’m nagging for no reason—we’re only doing this for your own good.
- Your test scores are not bad — keep up the good work.
- You've improved, not bad.
}

-> END


=== dinner_fail ===
{ shuffle:
- Why did your grades drop? You need to work harder.
- No more distractions. From now on, focus only on your studies!
- Stop wasting time and focus on your studies! You can't afford to slack off.
- Other students can manage good grades, so why can’t you? You need to work harder!
- You’re not a child anymore. It’s time to take responsibility for your own future.
}
~ addStressLevel(1)

-> END