using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class PlannerUI : MonoBehaviour
    {
        public GameController gameController;
        public TextMeshProUGUI dayText;
        public ActivitySlot[] activitySlots;
        public ClassSlot[] classSlots;
        public ActivityButton[] activityButtons;
        public TextMeshProUGUI[] textToStrike;
        public RectTransform activitiesArea;
        
        public Button confirmButton;

        private ActivitySlot selectedSlot;

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

        public void DisplayProgress(int index)
        {
            for (int i = 0; i < textToStrike.Length; i++)
            {
                bool shouldStrike = i < index;
                var text = textToStrike[i];
                text.fontStyle = shouldStrike ? FontStyles.Strikethrough : FontStyles.Normal;
                text.color = i == index ? new Color(243/255f, 115/255f, 41/255f) : Color.black;
            }
        }

        public void SetIsPlanningMode(bool isPlanningMode)
        {
            confirmButton.gameObject.SetActive(isPlanningMode);
            activitiesArea.gameObject.SetActive(isPlanningMode);
            
            for (var i = 0; i < activitySlots.Length; i++)
            {
                var slot = activitySlots[i];
                slot.SetIsButton(isPlanningMode);
            }
        }

        private void OnConfirm()
        {
            gameController.scheduleCompleteTsc.SetResult(0);
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
                EvaluateConfirm();

                return;
            }

            foreach (var slot in activitySlots)
                if (slot.activityData == null)
                {
                    slot.Display(clicked.data);
                    break;
                }
            
            EvaluateConfirm();
        }

        private void EvaluateConfirm()
        {
            confirmButton.interactable = false;

            foreach (var slot in activitySlots) if (slot.IsEmpty) return;

            confirmButton.interactable = true;
        }

        public void Display(DayData dayData)
        {
            dayText.text = dayData.dayName;

            foreach (var slot in activitySlots)
                slot.DisplayEmpty();

            for (var i = 0; i < classSlots.Length; i++)
            {
                var slot = classSlots[i];
                slot.Display(dayData.classes[i]);
            }
        }
    }
}