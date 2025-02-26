using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlannerUI : MonoBehaviour
    {
        public GameController gameController;
        public ClassSlot[] classSlots;
        public RectTransform activitiesArea;
        public PlannerDayUI plannerDayUi;
        public int columnCount = 6;
        
        public TextMeshProUGUI[] textToStrike;

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
                }
            }
        }

        public ActivityData GetActivity(int index) => plannerDayUi.activitySlots[index].activityData;
    }
}