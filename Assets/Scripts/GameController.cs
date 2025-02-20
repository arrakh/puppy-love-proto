using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameController : MonoBehaviour
    {
        public DayData[] daysData;
        public QuizController quizController;
        public PlannerUI plannerUi;

        public TaskCompletionSource<int> scheduleCompleteTsc = new();

        private IEnumerator Start()
        {
            foreach (var day in daysData)
            {
                plannerUi.DisplayProgress(0);

                quizController.gameObject.SetActive(false);
                plannerUi.gameObject.SetActive(true);
                
                plannerUi.Display(day);
                
                plannerUi.SetIsPlanningMode(true);

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
                    yield return DoClass(classSlot.classData);
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

        private IEnumerator DoDinner()
        {
            Debug.Log("DOING DINNER...");
            yield return new WaitForSeconds(2f);

            yield break;
        }

        private IEnumerator DoClass(SchoolClassData classSlotClassData)
        {
            Debug.Log($"DOING CLASS: {classSlotClassData.displayName}");
            yield return new WaitForSeconds(2f);

            yield break;
        }

        private IEnumerator DoActivity(FreeActivityData activityData)
        {
            Debug.Log($"DOING ACTIVITY: {activityData.displayName}");

            yield return new WaitForSeconds(2f);
            
            yield break;
        }
    }
}