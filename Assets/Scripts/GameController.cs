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
        public DayData[] daysData;
        public QuizController quizController;
        public PlannerUI plannerUi;
        public TransitionUI transitionUi;
        public TextAsset story;
        public InkStoryController storyController;
        public DialogueUI dialogueUi;

        public TaskCompletionSource<int> scheduleCompleteTsc = new();

        public enum State
        {
            QUIZ,
            DIALOGUE,
            PLANNING
        }

        private bool hasFirstFail = false;
        private State state;
        
        private IEnumerator Start()
        {
            storyController.InitializeStory(story.text);

            foreach (var day in daysData)
            {
                yield return transitionUi.WaitTransitionIn(day.dayName, 0.6f, 1.4f);
                transitionUi.TransitionOut(2f);
                
                SetState(State.DIALOGUE);
                yield return storyController.StartStory("morning");
                yield return new WaitForSeconds(0.6f);
                
                SetState(State.PLANNING);

                plannerUi.Display(day);
                plannerUi.SetIsPlanningMode(true);
                plannerUi.DisplayProgress(0);

                yield return new WaitUntil(() => scheduleCompleteTsc.Task.IsCompleted);

                plannerUi.SetIsPlanningMode(false);

                //FIRST ACTIVITY
                yield return DoActivity(plannerUi.activitySlots[0].activityData);
                int strikeCount = 1;
                plannerUi.DisplayProgress(strikeCount);
                strikeCount++;

                //CLASSES
                foreach (var classSlot in plannerUi.classSlots)
                {
                    yield return new WaitForSeconds(0.8f);
                    yield return DoClass(classSlot.classData);

                    if (!hasFirstFail && !quizController.HasPassed)
                        yield return DoFirstMoment();
                    
                    plannerUi.DisplayProgress(strikeCount);  
                    strikeCount++;
                }

                //SECOND AND THIRD ACTIVITY
                for (int i = 1; i < 3; i++)
                {
                    Debug.Log($"DOING ACTIVITY {i + 1}");
                    yield return DoActivity(plannerUi.activitySlots[i].activityData);
                    plannerUi.DisplayProgress(strikeCount);
                    strikeCount++;
                }

                yield return DoDinner();

                scheduleCompleteTsc = new();
            }
            
            yield break;
        }

        private IEnumerator DoFirstMoment()
        {
            hasFirstFail = true;
            
            SetState(State.DIALOGUE);
            yield return storyController.StartStory("first_moment");
            
            yield return transitionUi.WaitTransitionIn("", 0.6f, 2.4f);
            yield return transitionUi.WaitTransitionOut(1f);

            SetState(State.PLANNING);
        }

        private IEnumerator DoDinner()
        {
            Debug.Log("DOING DINNER...");
            yield return new WaitForSeconds(2f);

            yield break;
        }

        private IEnumerator DoClass(SchoolClassData data)
        {
            Debug.Log($"DOING CLASS: {data.displayName}");
            yield return transitionUi.WaitTransitionIn(data.displayName, 0.6f, 2.4f);
            
            SetState(State.QUIZ);
            quizController.Clear();

            yield return transitionUi.WaitTransitionOut(3f);

            yield return quizController.StartQuiz(data.entries);
            

        }

        private IEnumerator DoActivity(FreeActivityData activityData)
        {
            Debug.Log($"DOING ACTIVITY: {activityData.displayName}");

            yield return new WaitForSeconds(2f);
            
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
    }
}