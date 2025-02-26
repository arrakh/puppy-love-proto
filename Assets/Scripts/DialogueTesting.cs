using System;
using System.Collections;
using Ink;
using UnityEngine;

namespace DefaultNamespace
{
    public class DialogueTesting : MonoBehaviour
    {
        public InkStoryController storyController;
        public TextAsset storyInk;
        public string startPath = "morning";

        private IEnumerator Start()
        {
            storyController.InitializeStory(storyInk.text);
            yield return storyController.StartStory(startPath);
        }
    }
}