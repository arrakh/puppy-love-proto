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

        public ActivitySlot[] activitySlots;
        public Button confirmButton;
        public TextMeshProUGUI dayText;

        private ActivitySlot selectedSlot;
        private List<ActivityButton> activityButtons;

        private void OnConfirm()
        {
            gameController.scheduleCompleteTsc.SetResult(0);
        }
        private void Awake()
        {
            confirmButton.onClick.AddListener(OnConfirm);

            for (var i = 0; i < activitySlots.Length; i++)
            {
                var slot = activitySlots[i];
                slot.slotIndex = i;
                slot.button.onClick.AddListener(() => OnSlotClicked(slot));
            }

            foreach (var button in activityButtons)
                button.button.onClick.AddListener(() => OnActivityClicked(button));
        }

        public void Display(DayData dayData, HashSet<string> unlockedActivities)
        {
            dayText.text = dayData.dayName;
            
            foreach (var btn in activityButtons)
                btn.gameObject.SetActive(unlockedActivities.Contains(btn.Id));

            foreach (var slot in activitySlots)
                slot.DisplayEmpty();
        }
        
        private void OnSlotClicked(ActivitySlot clicked)
        {
            selectedSlot = clicked;
        }

        private void OnActivityClicked(ActivityButton clicked)
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

            foreach (var btn in activityButtons)
                if (activities.Contains(btn.Id)) btn.gameObject.SetActive(false);
        }

        private void EvaluateConfirm()
        {
            confirmButton.interactable = false;
            foreach (var slot in activitySlots) if (slot.IsEmpty) return;
            confirmButton.interactable = true;
        }
    }
}