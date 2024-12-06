using LittleMars.UI.GoalDisplays;
using UnityEngine;

namespace LittleMars.UI.LevelMenus.Addons
{
    /// <summary>
    /// A base menu add-on for goal slots. 
    /// </summary>
    public class MenuStrategiesBase : MonoBehaviour
    {
        public virtual void Set(IGoalDisplayStrategy[] strategies)
        {
        }
    }
}
