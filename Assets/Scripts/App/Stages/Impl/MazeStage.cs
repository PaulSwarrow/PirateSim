using System;
using System.Collections;
using App.Tools;
using Game.Ui.Dialogs;
using UnityEngine;

namespace DefaultNamespace.Components
{
    public class MazeStage : BaseStageScenario, IStageGoalProvider, ITimeSpanTracker
    {
        private const string HighScoreProperty = "MazeHighscore";
        public bool GoalAchieved { get; private set; }
        public string GoalDescription { get; private set; }
        public string GoalState { get; private set; }
        [SerializeField] private GameTrigger trigger;
        [SerializeField] private TutorialDialog dialog;
        private float startTimestamp;
        private TimeSpan timePassed;
        private TimeSpan highscore;
        private bool firsttime;


        public TimeSpan TimeSpan => timePassed;
        public string TimeSpanString => timePassed.ToString("mm':'ss':'fff");

        private void Start()
        {
            firsttime = !PlayerPrefs.HasKey(HighScoreProperty);
            if (!firsttime)
            {
                highscore = TimeSpan.FromSeconds(PlayerPrefs.GetFloat(HighScoreProperty));
            }

            trigger.EnterEvent += OnFinish;
            StartCoroutine(Scenario());
        }

        private void OnFinish(GameTrigger arg1, Collider arg2)
        {
            GoalAchieved = true;
        }

        private IEnumerator Scenario()
        {
            GoalDescription = "Pass the maze as faster as you can";
            yield return Tutorial(new[] {dialog});
            startTimestamp = Time.time;

            while (!GoalAchieved)
            {
                timePassed = TimeSpan.FromSeconds(Time.time - startTimestamp);
                GoalState =
                    $"{timePassed.ToString("mm':'ss':'fff")} \n Best time: {highscore.ToString("mm':'ss':'fff")}";
                yield return null;
            }

            if (firsttime || timePassed < highscore)
            {
                PlayerPrefs.SetFloat(HighScoreProperty, (float) timePassed.TotalSeconds);
                PlayerPrefs.Save();
            }
            
        }
    }
}