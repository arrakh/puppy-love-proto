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

        public bool hadFirstMeeting = false;
        public bool isMorning = true;
        public SchoolClassData lastClass;
        
        public TaskCompletionSource<int> scheduleCompleteTsc = new();

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
            storyController.InitializeStory(story.text);

            foreach (var week in weeks) yield return WeekLoop(week);

            yield break;
        }

        private IEnumerator WeekLoop(WeekData week)
        {
            plannerUi.Display(week);
            int dayIndex = 0;
            
            foreach (var day in week.days)
            {
                dayIndex++;
                yield return DayLoop(dayIndex, day);
            }
        }

        private IEnumerator DayLoop(int dayIndex, DayData day)
        {
            isMorning = true;
            variablesController.SyncVariables();
            
            yield return transitionUi.WaitTransitionIn(day.dayName, 0.6f, 1.4f);
            transitionUi.TransitionOut(2f);
                
            SetState(State.DIALOGUE);
            yield return storyController.StartStory("morning");
            yield return new WaitForSeconds(0.6f);
                
            SetState(State.PLANNING);

            plannerUi.plannerDayUi.Display(day, unlockedActivities);
            plannerUi.SetIsPlanningMode(true);
            plannerUi.DisplayProgress(dayIndex, 0);

            yield return new WaitUntil(() => scheduleCompleteTsc.Task.IsCompleted);

            plannerUi.SetIsPlanningMode(false);

            //FIRST ACTIVITY
            yield return DoActivity(plannerUi.GetActivity(0));
            int strikeCount = 1;
            plannerUi.DisplayProgress(dayIndex, strikeCount);
            strikeCount++;

            //CLASSES
            for (var i = 0; i < plannerUi.classSlots.Length; i++)
            {
                var classSlot = plannerUi.classSlots[i];
                yield return new WaitForSeconds(0.8f);
                yield return DoClass(classSlot.classData);
                variablesController.SyncVariables();

                if (!hadFirstMeeting && !quizController.HasPassed)
                    yield return DoFirstMoment();
                else if (i != plannerUi.classSlots.Length) yield return DoBreak();

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
            yield return transitionUi.WaitTransitionIn("", 0.6f, 2.4f);
            SetState(State.DIALOGUE);
            yield return storyController.StartStory("dinner_week");
            yield return transitionUi.WaitTransitionOut(1f);

            yield break;
        }

        private IEnumerator DoClass(SchoolClassData data)
        {
            lastClass = data;
            
            Debug.Log($"DOING CLASS: {data.displayName}");
            yield return transitionUi.WaitTransitionIn(data.displayName, 0.6f, 2.4f);
            
            SetState(State.QUIZ);
            quizController.Clear();

            yield return transitionUi.WaitTransitionOut(3f);

            yield return quizController.StartQuiz(data.parameters, data.entries);
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
            variablesController.SyncVariables();
        }

        public void AddLogic(float delta)
        {
            logic = Mathf.Clamp(logic + delta, 0, float.MaxValue);
            variablesController.SyncVariables();
        }

        public void AddStressLevel(int delta)
        {
            stressLevel = Mathf.Clamp(stressLevel + delta, 0, int.MaxValue);
            variablesController.SyncVariables();
        }

        public void UnlockActivity(string id)
        {
            if (!unlockedActivities.Add(id)) return;
            
        }
    }
}