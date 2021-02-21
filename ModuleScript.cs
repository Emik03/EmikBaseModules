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
        internal virtual KMBombModule KMBombModule { get; set; }

        /// <summary>
        /// Instance of a boss module, is used to set the ignore list.
        /// </summary>
        internal virtual KMBossModule KMBossModule { get; set; }

        /// <summary>
        /// Accesses the colorblind mod in-game, is used to set IsColorblind.
        /// </summary>
        internal virtual KMColorblindMode KMColorblindMode { get; set; }

        /// <summary>
        /// Instance of a needy module. Used to get the name of the module.
        /// </summary>
        internal virtual KMNeedyModule KMNeedyModule { get; set; }

        /// <summary>
        /// Called whenever the timer tick changes. Requires KMBombInfo to access the time left.
        /// </summary>
        internal virtual Tuple<Action, KMBombInfo> OnTimerTick { get; set; }

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
        internal bool IsColorblind { get; private set; }
        /// <summary>
        /// Is the module played in the editor?
        /// </summary>
        internal bool IsEditor { get; private set; }
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
        internal string[] IgnoreList { get; set; }

        /// <summary>
        /// Event initializer.
        /// </summary>
        private void Awake()
        {
            // This gives a unique value of each instantiation, since moduleIdCounter is static.
            ModuleId = ++ModuleIdCounter;

            // This determines if it's running in-game or the editor.
            IsEditor = Application.isEditor;

            // Sets ignore list.
            if (KMBossModule != null)
                IgnoreList = KMBossModule.GetIgnoredModules(KMBombModule, IgnoreList);
            else if (IgnoreList != null)
                PanicIfParentNull("KMBossModule", IgnoreList);

            // Sets colorblind.
            if (KMColorblindMode != null)
                IsColorblind = KMColorblindMode.ColorblindModeActive;

            // Makes sure OnTimerTick is paired with KMBombInfo.
            if (OnTimerTick.Item2 != null)
                PanicIfNull("OnTimerTick.Item2", OnTimerTick.Item2);
        }

        /// <summary>
        /// Runs every frame.
        /// </summary>
        private void Update()
        {
            // Updates the amount of time left within the TimeLeft property.
            if (OnTimerTick.Item2 != null)
                TimeLeft = (int)OnTimerTick.Item2.GetTime();
        }

        /// <summary>
        /// Throws an error if the event handler is null but any of its events are not null, since this is considered a mistake.
        /// </summary>
        /// <param name="parent">The parent object's name, since reflection cannot be used to obtain an unknown property.</param>
        /// <param name="objs">The list of events to do null validation on.</param>
        private void PanicIfParentNull(string parent, params object[] objs)
        {
            const string ErrorMessage = @"{0} is not null despite the parent object {1} being null. Be sure to check that you assigned your properties correctly!";

            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i] != null)
                    this.Log(ErrorMessage.Format(objs[i], parent), LogType.Error);
            }
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
