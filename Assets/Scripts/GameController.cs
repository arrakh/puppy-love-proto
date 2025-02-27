using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ink;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameController : MonoBehaviour
    {
        public float logic = 10f;
        public float mood = 10f;
        public int stressLevel = 0;

        public WeekData[] weeks;
        public QuizController quizController;
        public PlannerUI plannerUi;
        public TransitionUI transitionUi;
        public TextAsset story;
        public InkStoryController storyController;
        public StoryVariablesController variablesController;
        public DialogueUI dialogueUi;
        public ResourceUI resourceUI;

        public bool hadFirstMeeting = false;
        public bool isMorning = true;
        public bool hasFailedToday = false;
        public bool hadFirstDinner = false;
        public SchoolClassData lastClass;
        public ActivityDatabase activityDatabase;
        
        public TaskCompletionSource<int> scheduleCompleteTsc = new();

        private int dayIndex = 0;

        public event Action<float> onLogicUpdated; 
        public event Action<float> onMoodUpdated; 
        public event Action<int, int> onStressUpdated; 

        private HashSet<string> unlockedActivities = new()
        {
            "memorize",
            "appreciate",
            "writing",
            "exercise"
        };

        public enum State
        {
            QUIZ,
            DIALOGUE,
            PLANNING
        }

        private State state;
        
        private IEnumerator Start()
        {
            onMoodUpdated?.Invoke(mood);
            onLogicUpdated?.Invoke(mood);
            onStressUpdated?.Invoke(0, stressLevel);
            
            activityDatabase.Initialize();
            storyController.InitializeStory(story.text);

            foreach (var week in weeks) yield return WeekLoop(week);

            yield break;
        }

        private IEnumerator WeekLoop(WeekData week)
        {
            plannerUi.Display(week);
            dayIndex = 0;
            
            foreach (var day in week.days)
            {
                yield return DayLoop(dayIndex, day);
                dayIndex++;
            }
        }

        private IEnumerator DayLoop(int dayIndex, DayData day)
        {
            isMorning = true;
            hasFailedToday = false;
            variablesController.SyncVariables();
            
            yield return transitionUi.WaitTransitionIn(day.dayName, 0.6f, 1.4f);
            transitionUi.TransitionOut(2f);
                
            SetState(State.DIALOGUE);
            yield return storyController.StartStory("morning");
            yield return new WaitForSeconds(0.6f);
                
            //PLANNING
            SetState(State.PLANNING);

            plannerUi.EvaluateAndSpawnActivities(activityDatabase, unlockedActivities);
            plannerUi.plannerDayUi.Display(dayIndex, day);
            plannerUi.SetIsPlanningMode(true);
            plannerUi.DisplayProgress(dayIndex, 0);

            yield return new WaitUntil(() => scheduleCompleteTsc.Task.IsCompleted);

            plannerUi.ApplyTodayActivities(dayIndex);
            plannerUi.SetIsPlanningMode(false);

            //FIRST ACTIVITY
            yield return DoActivity(plannerUi.GetActivity(0));
            int strikeCount = 1;
            plannerUi.DisplayProgress(dayIndex, strikeCount);
            strikeCount++;

            //CLASSES
            for (var i = 0; i < day.classes.Length; i++)
            {
                var classData = day.classes[i];
                yield return new WaitForSeconds(0.8f);
                yield return DoClass(classData);
                variablesController.SyncVariables();

                if (!quizController.HasPassed)
                {
                    hasFailedToday = true;
                    if (!hadFirstMeeting) yield return DoFirstMoment();
                    else yield return DoFail(); 
                }
                else if (i < day.classes.Length - 1) yield return DoBreak();

                yield return transitionUi.WaitTransitionIn("", 0.6f);
                
                SetState(State.PLANNING);
                yield return transitionUi.WaitTransitionOut(1f);
                plannerUi.DisplayProgress(dayIndex, strikeCount);
                strikeCount++;
            }
            
            isMorning = false;
            variablesController.SyncVariables();

            //SECOND AND THIRD ACTIVITY
            for (int i = 1; i < 3; i++)
            {
                Debug.Log($"DOING ACTIVITY {i + 1}");
                yield return DoActivity(plannerUi.GetActivity(i));
                plannerUi.DisplayProgress(dayIndex, strikeCount);
                strikeCount++;
            }

            yield return DoDinner();

            scheduleCompleteTsc = new();
        }

        private IEnumerator DoFail()
        {
            SetState(State.DIALOGUE);
            yield return storyController.StartStory("fail");
        }

        private IEnumerator DoBreak()
        {
            SetState(State.DIALOGUE);
            dialogueUi.SetEmpty();
            
            yield return transitionUi.WaitTransitionIn("BREAK", 0f, 1f);
            yield return transitionUi.WaitTransitionOut(1f);
            
            yield return storyController.StartStory("break");
        }
        
        private IEnumerator DoFirstMoment()
        {
            SetState(State.DIALOGUE);
            yield return storyController.StartStory("first_moment");
            hadFirstMeeting = true;
        }

        private IEnumerator DoDinner()
        {
            yield return transitionUi.WaitTransitionIn("Later that night...", 0.6f, 2.4f);
            SetState(State.DIALOGUE);

            yield return transitionUi.WaitTransitionOut(1f);

            string path = hasFailedToday ? "dinner_fail" : "dinner_straight_a";
            
            if (!hadFirstDinner)
            {
                hadFirstDinner = true;
                path = "dinner_first_time";
            }

            if (dayIndex == 4) path = "dinner_week";

            yield return storyController.StartStory(path);
        }

        private IEnumerator DoClass(SchoolClassData data)
        {
            lastClass = data;
            
            Debug.Log($"DOING CLASS: {data.displayName}");
            yield return transitionUi.WaitTransitionIn(data.displayName, 0.6f, 2.4f);
            
            SetState(State.QUIZ);
            quizController.Clear();

            yield return transitionUi.WaitTransitionOut(3f);

            resourceUI.SetIsGame(true);
            yield return quizController.StartQuiz(data.parameters, data.entries);
            resourceUI.SetIsGame(false);
        }

        private IEnumerator DoActivity(ActivityData data)
        {
            //yield return plannerUi.PlannerAnimation
            yield return transitionUi.WaitTransitionIn(data.displayName, 0.6f, 2.4f);
            
            dialogueUi.SetEmpty();
            SetState(State.DIALOGUE);
            yield return transitionUi.WaitTransitionOut(0.25f);

            yield return storyController.StartStory($"activity_{data.activityId}");

            SetState(State.PLANNING);
            
            yield break;
        }

        private void SetState(State newState)
        {
            state = newState;
            
            quizController.gameObject.SetActive(false);
            plannerUi.gameObject.SetActive(false);
            dialogueUi.gameObject.SetActive(false);

            switch (state)
            {
                case State.QUIZ: quizController.gameObject.SetActive(true); break;
                case State.DIALOGUE: dialogueUi.gameObject.SetActive(true); break;
                case State.PLANNING: plannerUi.gameObject.SetActive(true); break;
            }
        }

        public void AddMood(float delta)
        {
            mood = Mathf.Clamp(mood + delta, 0, float.MaxValue);
            onMoodUpdated?.Invoke(mood);
            variablesController.SyncVariables();
        }

        public void AddLogic(float delta)
        {
            logic = Mathf.Clamp(logic + delta, 0, float.MaxValue);
            onLogicUpdated?.Invoke(logic);
            variablesController.SyncVariables();
        }

        public void AddStressLevel(int delta)
        {
            var stress = stressLevel;
            stressLevel = Mathf.Clamp(stressLevel + delta, 0, 4);
            onStressUpdated?.Invoke(stress, stressLevel);
            variablesController.SyncVariables();
        }

        public void UnlockActivity(string id)
        {
            if (!unlockedActivities.Add(id)) return;
            
        }
    }
}