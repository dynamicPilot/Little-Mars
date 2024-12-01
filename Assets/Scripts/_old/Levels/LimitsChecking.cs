using System.Collections.Generic;
using UnityEngine;

public class LimitsChecking : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private MapControl mapControl;
    [SerializeField] private LevelLimitsMenuUI levelLimitsMenuUI;
    [SerializeField] private GameTime gameTime;

    private State state;

    private void Awake()
    {
        state = inventory.GetComponent<State>();
    }

    public bool AddHourToLevelLimitsTimers(BasicLevelLimits limits)
    {
        bool needChecking = false;
        if (limits.IsBuildingNumber)
        {           
            if (limits.hoursSum != -1)
            {
                //Debug.Log("LimitsChecking: update timer");
                // timer is on but not finish
                limits.hoursSum++;
                levelLimitsMenuUI.UpdateProductInSum(limits.IsBuildingNumberTimerIndex, (float)limits.hoursSum, (float)limits.TimerInHoursAfterReaching, true);

                if (limits.hoursSum >= limits.TimerInHoursAfterReaching)
                {
                    //Debug.Log("LimitsChecking: reach goal");
                    needChecking = true;
                }
            }
        }
        return needChecking;
    }

    public KeyValuePair<bool, List<int>> CheckLimits(BasicLevelLimits limits, bool allAtTheSameTime = false)
    {
        List<bool> haveIt = new List<bool>();
        List<int> indexesToNotification = new List<int>();
        bool tempToAddToNotification = false;

        if (limits.IsBuildingNumber)
        {
            haveIt.Add(true);
            foreach (BuildingUnit unit in limits.Buildings)
            {
                if (inventory.GetNumberOfBuildingOfType(unit.Item.Type) < unit.Amount)
                {
                    //Debug.Log("LimitsChecking: goal is not reached -- > reset timer");
                    haveIt[haveIt.Count - 1] = false;
                    limits.hoursSum = -1;
                    break;
                }
            }

            tempToAddToNotification = CheckForNotEverReached(limits, limits.IsBuildingNumberIndex, haveIt[haveIt.Count - 1]);

            if (tempToAddToNotification)
                indexesToNotification.Add(limits.IsBuildingNumberIndex);

            if (limits.TimerInHoursAfterReaching != 0 && haveIt[haveIt.Count - 1])
            {
                haveIt.Add(true);
                if (limits.hoursSum != -1 && limits.hoursSum < limits.TimerInHoursAfterReaching)
                {
                    // timer is on but not finish
                    haveIt[haveIt.Count - 1] = false;
                }
                else if (limits.hoursSum == -1)
                {
                    // start timer
                    //Debug.Log("LimitsChecking: goal is reached -- > start timer");
                    limits.hoursSum = 0;
                    haveIt[haveIt.Count - 1] = false;
                }
                else
                {
                    if (!limits.everReachThisLimit[limits.IsBuildingNumberTimerIndex])
                    {
                        limits.everReachThisLimit[limits.IsBuildingNumberTimerIndex] = true;
                        //Debug.Log("LimitsChecking: have building time Limit");
                        indexesToNotification.Add(limits.IsBuildingNumberTimerIndex);
                    }
                }

                levelLimitsMenuUI.SetLevelLimitAsReached(limits.IsBuildingNumberTimerIndex, haveIt[haveIt.Count - 1]);
                levelLimitsMenuUI.UpdateProductInSum(limits.IsBuildingNumberTimerIndex, (float)limits.hoursSum, (float)limits.TimerInHoursAfterReaching, true);
            }

            
        }

        if (limits.IsResourcesNumber)
        {
            haveIt.Add(true);
            foreach (ProductionUnit unit in limits.Resources)
            {
                if (state.Resources[unit.ResourseType].DayAmount < unit.DayAmount)
                {
                    //Debug.Log("LimitsChecking: do not enought -- > " + state.Resources[unit.ResourseType].DayAmount + " need " + unit.DayAmount);
                    haveIt[haveIt.Count - 1] = false;
                    break;
                }
            }

            tempToAddToNotification = CheckForNotEverReached(limits, limits.IsResourcesNumberIndex, haveIt[haveIt.Count - 1]);

            if (tempToAddToNotification)
                indexesToNotification.Add(limits.IsResourcesNumberIndex);
        }

        if (limits.IsProductionNumber)
        {
            haveIt.Add(true);
            foreach (ProductionUnit unit in limits.Production)
            {
                if (gameTime.Period == GameTime.PERIOD.day && state.Production[unit.ResourseType].DayAmount < unit.DayAmount)
                {
                    haveIt[haveIt.Count - 1] = false;
                    break;
                }
                else if (gameTime.Period == GameTime.PERIOD.night && state.Production[unit.ResourseType].NightAmount < unit.NightAmount)
                {
                    haveIt[haveIt.Count - 1] = false;
                    break;
                }
            }

            tempToAddToNotification = CheckForNotEverReached(limits, limits.IsProductionNumberIndex, haveIt[haveIt.Count - 1]);

            if (tempToAddToNotification)
                indexesToNotification.Add(limits.IsProductionNumberIndex);
        }

        if (limits.IsProductionInSumNumber)
        {
            haveIt.Add(true);
            foreach (ProductionUnit unit in limits.ProductionInSum)
            {
                //Debug.Log("LimitsChecking: production in sum -- > " + state.ProductionInSum[unit.ResourseType].DayAmount);
                if (state.ProductionInSum[unit.ResourseType].DayAmount < unit.DayAmount)
                {
                    //Debug.Log("LimitsChecking: do not enought -- > " + state.Resources[unit.ResourseType].DayAmount + " need " + unit.DayAmount);
                    haveIt[haveIt.Count - 1] = false;
                    break;
                }
            }

            tempToAddToNotification = CheckForNotEverReached(limits, limits.IsProductionInSumNumberIndex, haveIt[haveIt.Count - 1], true);

            if (tempToAddToNotification)
                indexesToNotification.Add(limits.IsProductionInSumNumberIndex);
        }

        if (limits.IsDayNumber)
        {
            haveIt.Add(true);
            if (gameTime.DaysCounter < limits.Days)
            {
                haveIt[haveIt.Count - 1] = false;
            }


            tempToAddToNotification = CheckForNotEverReached(limits, limits.IsDayNumberIndex, haveIt[haveIt.Count - 1]);

            if (tempToAddToNotification)
                indexesToNotification.Add(limits.IsDayNumberIndex);
        }

        if (limits.IsMapSlotNumber)
        {
            haveIt.Add(true);
            if (mapControl.GetNumberOfEmptySlots() < limits.Slots)
            {
                haveIt[haveIt.Count - 1] = false;
            }

            tempToAddToNotification = CheckForNotEverReached(limits, limits.IsMapSlotNumberIndex, haveIt[haveIt.Count - 1]);

            if (tempToAddToNotification)
                indexesToNotification.Add(limits.IsMapSlotNumberIndex);
        }

        if (haveIt.Count == 0)
        {
            return new KeyValuePair<bool, List<int>> (false, null);
        }
        else
        {
            bool result = false;
            if (!allAtTheSameTime)
            {
                result = haveIt.Contains(true);
            }
            else
            {
                result = !haveIt.Contains(false);
            }

            if (indexesToNotification.Count > 0)
            {
                return new KeyValuePair<bool, List<int>>(result, indexesToNotification);
            }
            else
            {
                return new KeyValuePair<bool, List<int>>(result, null);
            }
        }

    }

    bool CheckForNotEverReached(BasicLevelLimits limits, int limitIndex, bool haveIt, bool needAddScore = false)
    {

        levelLimitsMenuUI.SetLevelLimitAsReached(limitIndex, haveIt);

        if (needAddScore && limitIndex == limits.IsProductionInSumNumberIndex)
        {
            bool isFirst = true;
            // add score
            foreach (ProductionUnit unit in limits.ProductionInSum)
            {
                levelLimitsMenuUI.UpdateProductInSum(limitIndex, state.ProductionInSum[unit.ResourseType].DayAmount, unit.DayAmount, isFirst);
                isFirst = false;
            }            
        }
        else if (needAddScore && limitIndex == limits.IsBuildingNumberTimerIndex)
        {
            levelLimitsMenuUI.UpdateProductInSum(limitIndex, limits.hoursSum, limits.TimerInHoursAfterReaching, true);
        }

        if (haveIt)
        {
            if (!limits.everReachThisLimit[limitIndex])
            {
                limits.everReachThisLimit[limitIndex] = true;
                //Debug.Log("LimitsChecking: have Limit with description " + limits.AllDescriptions[limitIndex]);
                return true;
            }
        }

        return false;
    }
}
