using UnityEngine;
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
        internal virtual ModuleConfig ModuleConfig { get; private set; }

        /// <summary>
        /// The module's display name. This is used for logging.
        /// </summary>
        internal string ModuleName
        {
            get
            {
                if (ModuleConfig.KMBombModule != null)
                    return ModuleConfig.KMBombModule.ModuleDisplayName;
                if (ModuleConfig.KMNeedyModule != null)
                    return ModuleConfig.KMNeedyModule.ModuleDisplayName;
                throw new NotImplementedException("ModuleName expects at least 1 of KMBombModule or KMNeedyModule to not be null.");
            }
        }

        /// <summary>
        /// Are the lights in-game on? (Has the bomb started the timer?)
        /// </summary>
        internal bool IsActivate { get; set; }

        /// <summary>
        /// Is Colorblind mode enabled? (Using KMColorblindMode)
        /// </summary>
        internal bool IsColorblind
        { 
            get 
            {
                if (ModuleConfig.KMColorblindMode == null)
                    throw new MissingReferenceException("IsColorblind expects KMColorblindMode to not be null.");
                return ModuleConfig.KMColorblindMode.ColorblindModeActive;
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
        /// Is the needy currently active in its events?
        /// </summary>
        internal bool IsNeedyActive { get; set; }

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
                if (ModuleConfig.OnTimerTick != null)
                    ((Tuple<Action, KMBombInfo>)ModuleConfig.OnTimerTick).Item1.Invoke();
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
                if (ModuleConfig.KMBossModule == null)
                    throw new MissingReferenceException("IgnoreList expects KMBossModule to not be null.");
                return ModuleConfig.KMBossModule.GetIgnoredModules(ModuleConfig.KMBombModule, _ignoreList);
            }
            set
            {
                _ignoreList = value;
            }
        }
        private string[] _ignoreList;

        /// <summary>
        /// Event initializer.
        /// </summary>
        private void Awake()
        {
            // This gives a unique value of each instantiation, since moduleIdCounter is static.
            ModuleId = ++ModuleIdCounter;

            // Makes sure OnTimerTick is paired with KMBombInfo.
            if (ModuleConfig.OnTimerTick != null && ((Tuple<Action, KMBombInfo>)ModuleConfig.OnTimerTick).Item2 == null)
                throw new NullReferenceException("OnTimerTick has a null KMBombInfo. An instance of KMBombInfo is required to access time remaining.");
        }

        /// <summary>
        /// Runs every frame.
        /// </summary>
        private void Update()
        {
            // Updates the amount of time left within the TimeLeft property.
            if (ModuleConfig.OnTimerTick != null)
                TimeLeft = (int)((Tuple<Action, KMBombInfo>)ModuleConfig.OnTimerTick).Item2.GetTime();
        }
    }
}
