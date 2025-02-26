using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlannerUI : MonoBehaviour
    {
        public GameController gameController;
        public ClassSlot[] classSlots;
        public ActivityWeekButton[] activityTexts;
        public RectTransform activitiesArea;
        public PlannerDayUI plannerDayUi;
        public int columnCount = 6;
        public int activitiesPerDay = 3;
        public ActivityButton activityButtonPrefab;

        public TextMeshProUGUI[] textToStrike;

        private List<ActivityButton> spawnedButtons = new();

        private void Awake()
        {
            plannerDayUi.onActivitiesEvaluated += OnActivitiesEvaluated;
        }

        private void OnActivitiesEvaluated()
        {
            HashSet<string> selected = new();

            foreach (var activitySlot in plannerDayUi.activitySlots)
                if (activitySlot.activityData != null)
                    selected.Add(activitySlot.activityData.activityId);

            foreach (var btn in spawnedButtons)
                btn.gameObject.SetActive(!selected.Contains(btn.Id));
        }

        public void DisplayProgress(int day, int index)
        {
            var targetIndex = (day * columnCount) + index;
            for (int i = 0; i < textToStrike.Length; i++)
            {
                bool shouldStrike = i < targetIndex;
                var text = textToStrike[i];
                text.fontStyle = shouldStrike ? FontStyles.Strikethrough : FontStyles.Normal;
                text.color = i == targetIndex ? new Color(243/255f, 115/255f, 41/255f) : Color.black;
            }
        }

        public void SetIsPlanningMode(bool isPlanningMode)
        {
            activitiesArea.gameObject.SetActive(isPlanningMode);
            plannerDayUi.gameObject.SetActive(isPlanningMode);
        }

        public void Display(WeekData weekData)
        {
            int slotIndex = 0;
            foreach (var day in weekData.days)
            {
                foreach (var schoolClass in day.classes)
                {
                    var slot = classSlots[slotIndex];
                    slot.Display(schoolClass);
                    slotIndex++;
                }
            }
        }

        public void EvaluateAndSpawnActivities(ActivityDatabase database, HashSet<string> unlockedActivities)
        {
            foreach (var btn in spawnedButtons)
                Destroy(btn.gameObject);
            
            spawnedButtons.Clear();
            
            foreach (var activity in database.allActivities)
            {
                if (!unlockedActivities.Contains(activity.activityId)) continue;
                var button = Instantiate(activityButtonPrefab, activitiesArea);
                button.Display(activity);
                button.button.onClick.AddListener(() => plannerDayUi.OnActivityClicked(button));
                spawnedButtons.Add(button);
            }
        }

        public ActivityData GetActivity(int index) => plannerDayUi.activitySlots[index].activityData;

        public void ApplyTodayActivities(int dayIndex)
        {
            var activity = plannerDayUi.activitySlots.Select(x => x.activityData).ToArray();

            var startIndex = dayIndex * activitiesPerDay;

            for (int i = 0; i < activity.Length; i++)
            {
                activityTexts[startIndex + i].text.text = activity[i].displayName;
            }
        }
    }
}