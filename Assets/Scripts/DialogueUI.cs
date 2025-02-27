using System;
using System.Collections.Generic;
using Ink;
using UnityEngine;

namespace DefaultNamespace
{
    public class DialogueUI : MonoBehaviour
    {
        public InkStoryController inkStoryController;
        public InkTextController inkTextController;
        
        //todo: HANDLE THESE BETTER WITH DICTIONARIES
        public DialogueTextBox leftBox, rightBox, eventBox;
        public GameObject[] backgrounds;

        private void Awake()
        {
            inkStoryController.OnStoryTags += OnStoryTags;
            inkTextController.OnTextDisplay += OnTextDisplay;
        }

        private void OnTextDisplay(string text)
        {
            leftBox.Display(text);
            rightBox.Display(text);
            eventBox.Display(text);
        }

        private void OnStoryTags(List<string> tags)
        {
            foreach (var storyTag in tags)
            {
                if (storyTag.StartsWith("box")) PositionBox(storyTag.Split(' ')[1]);
                if (storyTag.StartsWith("bg")) SetBackground(storyTag.Split(' ')[1]);
            }
        }

        public void SetEmpty()
        {
            SetBackground("none");
            PositionBox("none");
        }
        
        private void SetBackground(string background)
        {
            if (string.IsNullOrWhiteSpace(background)) return;

            foreach (var obj in backgrounds)
                obj.SetActive(obj.name.Equals(background));
        }

        private void PositionBox(string pos)
        {
            if (string.IsNullOrWhiteSpace(pos)) return;

            leftBox.gameObject.SetActive(false);
            rightBox.gameObject.SetActive(false);
            eventBox.gameObject.SetActive(false);

            DialogueTextBox box;
            
            switch (pos)
            {
                case "left": box = leftBox; break;
                case "right": box = rightBox; break;
                case "event": box = eventBox; break;
                default: return;
            }
            
            box.gameObject.SetActive(true);
            box.PopupAnimation();
        }
    }
}