using System.Collections.Generic;
using Ink;
using Ink.Runtime;
using UnityEngine;

namespace DefaultNamespace
{
    public class StoryFunctionsController : MonoBehaviour
    {
        public GameController gameController;
        public InkStoryController storyController;
        public bool debugFakeFunc = false;
        
        private Story story;

        private void Awake() => storyController.OnStoryInitialized += OnStoryInitialized;

        private void OnStoryInitialized(Story obj)
        {
            story = obj;

            if (debugFakeFunc)
            {
                FakeFunctions();
                return;
            }
            
            story.BindExternalFunction("AddMood", (float delta) => gameController.AddMood(delta));
            story.BindExternalFunction("AddLogic", (float delta) => gameController.AddLogic(delta));
            story.BindExternalFunction("AddStressLevel", (int delta) => gameController.AddStressLevel(delta));
            story.BindExternalFunction("UnlockActivity", (string id) => gameController.UnlockActivity(id));
        }

        private void FakeFunctions()
        {
            story.BindExternalFunction("AddMood", (float delta) => 0);
            story.BindExternalFunction("AddLogic", (float delta) => 0);
            story.BindExternalFunction("AddStressLevel", (int delta) => 0);
            story.BindExternalFunction("UnlockActivity", (string id) => 0);
        }
    }
}