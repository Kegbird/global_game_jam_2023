﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class Constants
    {
        public static short MAIN_MENU_SCENE_INDEX = 0;
        public static short INTRO_SCENE_INDEX = 1;
        public static short GAME_SCENE_INDEX = 2;
        public static short BAD_ENDING_SCENE_INDEX = 3;
        public static short GOOD_ENDING_SCENE_INDEX = 4;

        public static float DEFAULT_OXYGEN_LEVEL = 100;
        public static float DEFAULT_OXYGEN_DECREMENT_STEP = 0.5f;
        public static float OXYGEN_DECREMENT_DELAY = 1f;
    }
}