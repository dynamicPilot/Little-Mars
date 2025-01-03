﻿using LittleMars.Settings;
using System;
using UnityEngine;

namespace LittleMars.Common.Catalogues
{
    public class IconsCatalogue
    {
        readonly Settings _settings;

        public IconsCatalogue(Settings settings)
        {
            _settings = settings;
        }

        public Sprite ResourceIcon(Resource type)
        {
            return _settings.Catalogue.Icon((int)type, IconType.resource);
        }

        public Sprite BuildingIcon(BuildingType type)
        {
            //Debug.Log("Need buildingIcon " + type);
            return _settings.Catalogue.Icon((int)type, IconType.building);
        }

        public Sprite GoalTypeIcon(GoalType type)
        {
            return _settings.Catalogue.Icon((int)type, IconType.goalType);
        }

        public Sprite FieldTypeIcon(Resource type)
        {
            return _settings.Catalogue.Icon((int)type, IconType.field);
        }

        public Sprite SlotIsBlockedIcon()
        {
            return _settings.Catalogue.Icon(0, IconType.blockedSlot);
        }

        public Sprite TimerTypeIcon()
        {
            return _settings.Catalogue.Icon(0, IconType.timer);
        }

        public Sprite RouteErrorIcon()
        {
            return _settings.Catalogue.Icon(0, IconType.routeError);
        }

        [Serializable]
        public class Settings
        {
            public Catalogue Catalogue;
        }
    }
}
