using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace LittleMars.UI.Notifications
{
    public class NotificationUI : MenuUI
    {
        [SerializeField] Image _sign;

        void SetSign(Sprite sign)
        {
            _sign.enabled = true;
            _sign.sprite = sign;
        }

        void HideSign()
        {
            _sign.enabled = false;
        }

        void MakeNotification()
        {
        }
    }
}
