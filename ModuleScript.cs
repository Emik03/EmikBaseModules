﻿using UnityEngine;
using System;

namespace EmikBaseModules
{
    /// <summary>
    /// Base class for regular and needy modded modules in Keep Talking and Nobody Explodes written by Emik.
    /// </summary>
    public abstract class ModuleScript : MonoBehaviour
    {
        /// <summary>
        /// Instance of a regular module. Used to get the name of the module.
        /// </summary>
        internal virtual KMBombModule KMBombModule { get; private set; }

        /// <summary>
        /// Instance of a boss module, is used to set the ignore list.
        /// </summary>
        internal virtual KMBossModule KMBossModule { get; private set; }

        /// <summary>
        /// Accesses the colorblind mod in-game, is used to set IsColorblind.
        /// </summary>
        internal virtual KMColorblindMode KMColorblindMode { get; private set; }

        /// <summary>
        /// Instance of a needy module. Used to get the name of the module.
        /// </summary>
        internal virtual KMNeedyModule KMNeedyModule { get; private set; }

        /// <summary>
        /// Called whenever the timer tick changes. Requires KMBombInfo to access the time left.
        /// </summary>
        internal virtual Tuple<Action, KMBombInfo> OnTimerTick { get; private set; }

        /// <summary>
        /// The module's display name. This is used for logging.
        /// </summary>
        internal string ModuleName
        {
            get
            {
                if (KMBombModule != null)
                    return KMBombModule.ModuleDisplayName;
                if (KMNeedyModule != null)
                    return KMNeedyModule.ModuleDisplayName;
                throw new NotImplementedException("ModuleName expects at least 1 of KMBombModule or KMNeedyModule to not be null.");
            }
        }

        /// <summary>
        /// Are the lights in-game on? (Has the bomb started the timer?)
        /// </summary>
        internal bool IsActivate { get; private set; }

        /// <summary>
        /// Is Colorblind mode enabled? (Using KMColorblindMode)
        /// </summary>
        internal bool IsColorblind
        { 
            get 
            {
                if (KMColorblindMode == null)
                    throw new NotImplementedException("IsColorblind expects KMColorblindMode to not be null.");
                return KMColorblindMode.ColorblindModeActive;
            } 
        }

        /// <summary>
        /// Is the module played in the editor?
        /// </summary>
        internal bool IsEditor { get { return Application.isEditor; } }

        /// <summary>
        /// Has the module been solved?
        /// </summary>
        internal bool IsSolve { get; set; }

        /// <summary>
        /// Is TwitchPlays active?
        /// </summary>
        internal bool TwitchPlaysActive;

        /// <summary>
        /// Is Time Mode active?
        /// </summary>
        internal bool TimeModeActive;

        /// <summary>
        /// Is Zen Mode active?
        /// </summary>
        internal bool ZenModeActive;

        /// <summary>
        /// The last instantiation of the module. Use moduleId == moduleIdCounter when only 1 module needs to do something.
        /// </summary>
        internal static int ModuleIdCounter { get; private set; }

        /// <summary>
        /// The moduleId, for logging.
        /// </summary>
        internal int ModuleId { get; private set; }

        /// <summary>
        /// Contains the current amount of seconds remaining on the timer. Requires KMBombInfo to be assigned.
        /// </summary>
        internal int TimeLeft
        {
            get
            {
                return _timeLeft;
            }
            set
            {
                if (_timeLeft == value)
                    return;
                _timeLeft = value;
                if (OnTimerTick.Item1 != null)
                    OnTimerTick.Item1.Invoke();
            }
        }
        private int _timeLeft;

        /// <summary>
        /// Contains modules that should be ignored, typically for boss modules.
        /// </summary>
        internal string[] IgnoreList 
        {
            get
            {
                if (KMBossModule == null)
                    throw new NotImplementedException("IgnoreList expects KMBossModule to not be null.");
                return KMBossModule.GetIgnoredModules(KMBombModule, IgnoreList);
            }
        }

        /// <summary>
        /// Event initializer.
        /// </summary>
        private void Awake()
        {
            // This gives a unique value of each instantiation, since moduleIdCounter is static.
            ModuleId = ++ModuleIdCounter;

            // Makes sure OnTimerTick is paired with KMBombInfo.
            if (OnTimerTick.Item2 == null)
                throw new NotImplementedException("OnTimerTick has a null KMBombInfo. An instance of KMBombInfo is required to access time remaining.");
        }

        /// <summary>
        /// Runs every frame.
        /// </summary>
        private void Update()
        {
            // Updates the amount of time left within the TimeLeft property.
            TimeLeft = (int)OnTimerTick.Item2.GetTime();
        }

        /// <summary>
        /// Throws an error if the object is null, since this is considered a mistake.
        /// </summary>
        /// <param name="name">The name to assign it in the error message, since reflection cannot be used to obtain an unknown property.</param>
        /// <param name="obj">The object to check null validity on.</param>
        private void PanicIfNull(string name, object obj)
        {
            const string ErrorMessage = @"{0} is null. Be sure to check that you assigned your public fields correctly!";

            if (obj == null)
            {
                this.Log(ErrorMessage.Format((object)name), LogType.Error);
                enabled = false;
            }
        }
    }
}
