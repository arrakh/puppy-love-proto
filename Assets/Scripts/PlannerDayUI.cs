using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class PlannerDayUI : MonoBehaviour
    {
        public GameController gameController;
        public RectTransform rect;
        public ActivitySlot[] activitySlots;
        public ClassSlot[] classSlots;
        public Button confirmButton;
        public Button clearButton;
        public TextMeshProUGUI dayText;
        public float[] dailyXPositions;

        private ActivitySlot selectedSlot;
        public event Action onActivitiesEvaluated;

        private void OnConfirm()
        {
            gameController.scheduleCompleteTsc.SetResult(0);
        }

        private void Awake()
        {
            confirmButton.onClick.AddListener(OnConfirm);
            clearButton.onClick.AddListener(OnClear);

            for (var i = 0; i < activitySlots.Length; i++)
            {
                var slot = activitySlots[i];
                slot.slotIndex = i;
                slot.button.onClick.AddListener(() => OnSlotClicked(slot));
            }
        }

        private void OnClear()
        {
            if (selectedSlot != null) selectedSlot.Display(null);
            selectedSlot = null;

            foreach (var slot in activitySlots)
                slot.DisplayEmpty();
            
            EvaluateActivities();
        }

        public void Display(int dayIndex, DayData dayData)
        {
            var pos = rect.anchoredPosition;
            pos.x = dailyXPositions[dayIndex];
            rect.anchoredPosition = pos;
            
            dayText.text = dayData.dayName;

            foreach (var slot in activitySlots)
                slot.DisplayEmpty();

            for (var i = 0; i < classSlots.Length; i++)
            {
                var slot = classSlots[i];
                slot.Display(dayData.classes[i]);
            }

            EvaluateConfirm();
        }
        
        private void OnSlotClicked(ActivitySlot clicked)
        {
            selectedSlot = clicked;
        }

        public void OnActivityClicked(ActivityButton clicked)
        {
            if (selectedSlot != null)
            {
                selectedSlot.Display(clicked.data);
                selectedSlot = null;
                EvaluateActivities();

                return;
            }

            foreach (var slot in activitySlots)
                if (slot.activityData == null)
                {
                    slot.Display(clicked.data);
                    break;
                }
            
            EvaluateActivities();
        }
        
        /*public void SetIsPlanningMode(bool isPlanningMode)
        {
            confirmButton.gameObject.SetActive(isPlanningMode);
            
            for (var i = 0; i < activitySlots.Length; i++)
            {
                var slot = activitySlots[i];
                slot.SetIsButton(isPlanningMode);
            }
        }*/
        
        private void EvaluateActivities()
        {
            EvaluateConfirm();

            HashSet<string> activities = new();
            foreach (var slot in activitySlots)
            {
                if (slot.activityData == null) continue;
                activities.Add(slot.activityData.activityId);
            }
            
            onActivitiesEvaluated?.Invoke();
        }

        private void EvaluateConfirm()
        {
            confirmButton.interactable = false;
            foreach (var slot in activitySlots) if (slot.IsEmpty) return;
            confirmButton.interactable = true;
        }
    }
}