using Ink;
using Ink.Runtime;
using UnityEngine;

namespace DefaultNamespace
{
    public class StoryVariablesController : MonoBehaviour
    {
        private const string MOOD = "mood";
        private const string LOGIC = "logic";
        private const string STRESS_LEVEL = "stressLevel";
        private const string LAST_CLASS = "lastClass";
        private const string HAS_FIRST_MEETING = "stressLevel";
        private const string IS_MORNING = "isMorning";
        
        public InkStoryController storyController;
        public GameController gameController;
        
        private Story story;

        private void Awake() => storyController.OnStoryInitialized += OnStoryInitialized;

        private void OnStoryInitialized(Story obj) => story = obj;

        public void SyncVariables()
        {
            story.variablesState[MOOD] = gameController.mood;
            story.variablesState[LOGIC] = gameController.logic;
            story.variablesState[STRESS_LEVEL] = gameController.stressLevel;
            story.variablesState[HAS_FIRST_MEETING] = gameController.hadFirstMeeting;
            story.variablesState[IS_MORNING] = gameController.isMorning;
            
            if (gameController.lastClass != null)
                story.variablesState[LAST_CLASS] = gameController.lastClass.displayName;
        }
    }
}