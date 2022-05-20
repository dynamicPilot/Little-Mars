using System.Collections.Generic;
using UnityEngine;

public class BuildingTurnOffTimer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float veryHighPriorityBuildingTimer;
    [SerializeField] private int actionSoundIndex = 0;

    [Header("Scripts")]
    [SerializeField] private List<BasicBuilding> buildingsToControl;
    [SerializeField] private LevelControl levelControl;
    
    private List<float> timers;
    private bool pauseUpdate = true;

    private AudioControl audioControl;

    private void Awake()
    {
        pauseUpdate = true;
        audioControl = AudioControl.Instance;

        buildingsToControl = new List<BasicBuilding>();
        timers = new List<float>();
    }

    private void FixedUpdate()
    {
        if (!pauseUpdate)
        {
            List<int> indexesToRemove = new List<int>();
            for (int i = 0; i < timers.Count; i++)
            {
                timers[i] -= Time.fixedDeltaTime;

                if (buildingsToControl[i].IsOn)
                {
                    indexesToRemove.Add(i);
                    //StopTimer();
                }
                else if (timers[i] <= 0)
                {
                    StopTimer(true, true);
                }
            }

            if (indexesToRemove.Count > 0)
            {
                foreach (int index in indexesToRemove)
                {
                    buildingsToControl.RemoveAt(index);
                    timers.RemoveAt(index);
                }
            }

            if (buildingsToControl.Count > 0 && indexesToRemove.Count > 0)
            {
                StopTimer(false, false);
            }
            else if (buildingsToControl.Count == 0 && indexesToRemove.Count > 0)
            {
                StopTimer();
            }
        }
    }

    public void StartTimer(BasicBuilding building)
    {
        //Debug.Log("BuildingTurnOffTimer: start timer for building " + building.ItemName);

        if (!buildingsToControl.Contains(building))
        {
            buildingsToControl.Add(building);
            timers.Add(veryHighPriorityBuildingTimer);
        }
        
        //timer = veryHighPriorityBuildingTimer;
        if (pauseUpdate) pauseUpdate = false;
        audioControl.StartActionSound(actionSoundIndex);
    }

    public void StopTimer(bool stopSoundAndUpdate = true, bool timerIsEnd = false)
    {        
        if (stopSoundAndUpdate)
        {
            pauseUpdate = true;
            audioControl.StopActionSound(actionSoundIndex);
        }
        
        if (timerIsEnd)
        {
            //Debug.Log("BuildingTurnOffTimer: END GAME!");
            levelControl.LevelGameOver("You can not turn off domes!");
        }
    }
}
