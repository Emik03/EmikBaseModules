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
        /// 0 events. 1 extension method: KMAudio.PushButton();
        /// </summary>
        internal virtual KMAudio KMAudio { get; set; }

        /// <summary>
        /// 2 events: OnBombExploded, OnBombSolved.
        /// </summary>
        internal virtual KMBombInfo KMBombInfo { get; set; }
        /// <summary>
        /// Triggers when the bomb runs out of time, or when current strikes equals max strikes.
        /// </summary>
        internal virtual Action OnBombExploded { get; set; }
        /// <summary>
        /// Triggers when all solvable modules are solved.
        /// </summary>
        internal virtual Action OnBombSolved { get; set; }

        /// <summary>
        /// 1 event: OnActivate.
        /// </summary>
        internal virtual KMBombModule KMBombModule { get; set; }
        /// <summary>
        /// Triggers when the lights come on.
        /// </summary>
        internal virtual Action OnActivate { get; set; }

        /// <summary>
        /// 0 events. Used to set 'IgnoreList'.
        /// </summary>
        internal virtual KMBossModule KMBossModule { get; set; }
        /// <summary>
        /// Contains modules that should be ignored, typically for boss modules.
        /// </summary>
        internal string[] IgnoreList { get; set; }

        /// <summary>
        /// 0 events. Used to set 'IsColorblind'.
        /// </summary>
        internal virtual KMColorblindMode KMColorblindMode { get; set; }

        /// <summary>
        /// 2 events: OnAlarmClockChange, OnLightsChange.
        /// </summary>
        internal virtual KMGameInfo KMGameInfo { get; set; }
        /// <summary>
        /// Triggers when the alarm clock goes on to off, or vice versa.
        /// </summary>
        internal virtual Func<bool, Action> OnAlarmClockChange { get; set; }
        /// <summary>
        /// Triggers when the lights goes on to off, or vice versa.
        /// </summary>
        internal virtual Func<bool, Action> OnLightsChange { get; set; }

        /// <summary>
        /// 4 events: OnActivateNeedy, OnNeedyActivation, OnNeedyDeactivation, OnTimerExpired
        /// </summary>
        internal virtual KMNeedyModule KMNeedyModule { get; set; }
        /// <summary>
        /// Triggers when the lights come on.
        /// </summary>
        internal virtual Action OnActivateNeedy { get; set; }
        /// <summary>
        /// Triggers when the needy activates the timer.
        /// </summary>
        internal virtual Action OnNeedyActivation { get; set; }
        /// <summary>
        /// Triggers when the needy deactivates the timer.
        /// </summary>
        internal virtual Action OnNeedyDeactivation { get; set; }
        /// <summary>
        /// Triggers when its timer runs out.
        /// </summary>
        internal virtual Action OnTimerExpired { get; set; }

        /// <summary>
        /// Called whenever the timer tick changes.
        /// </summary>
        internal virtual Action OnTimerTick { get; set; }

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
                if (OnTimerTick != null)
                    OnTimerTick.Invoke();
            }
        }
        private int _timeLeft;

        /// <summary>
        /// Event initializer.
        /// </summary>
        private void Awake()
        {
            // This gives a unique value of each instantiation, since moduleIdCounter is static.
            ModuleId = ++ModuleIdCounter;

            // This determines if it's running in-game or the editor.
            IsEditor = Application.isEditor;

            /* 
             * The following adds all appropriate parameters for each property you assign.
             * The first if statements are to ask whether it should try adding events.
             * The other if statements are to add the events if they exist.
            */

            if (KMBombInfo != null)
            {
                if (OnBombExploded != null)
                    KMBombInfo.OnBombExploded += delegate () { OnBombExploded.Invoke(); };
                if (OnBombSolved != null)
                    KMBombInfo.OnBombSolved += delegate () { OnBombSolved.Invoke(); }; 
            }
            else
            {
                PanicIfParentNull("KMBombInfo", OnBombExploded, OnBombSolved);
            }

            if (KMBombModule != null)
            {
                if (OnActivate != null)
                    KMBombModule.OnActivate += delegate () { IsActivate = true; OnActivate.Invoke(); };

                if (KMBossModule != null)
                    IgnoreList = KMBossModule.GetIgnoredModules(KMBombModule, IgnoreList);
                else if (IgnoreList != null)
                    PanicIfParentNull("KMBossModule", IgnoreList);
            }
            else
            {
                PanicIfParentNull("KMBombModule", OnActivate);
            }

            if (KMColorblindMode != null)
                IsColorblind = KMColorblindMode.ColorblindModeActive;

            if (KMGameInfo != null)
            {
                if (OnAlarmClockChange != null)
                    KMGameInfo.OnAlarmClockChange += on => OnAlarmClockChange(on);
                if (OnLightsChange != null)
                    KMGameInfo.OnLightsChange += on => OnLightsChange(on);
            }
            else
            {
                PanicIfParentNull("KMGameInfo", OnActivateNeedy, OnNeedyActivation, OnNeedyDeactivation, OnTimerExpired);
            }

            if (KMNeedyModule != null)
            {
                if (OnActivateNeedy != null)
                    KMNeedyModule.OnActivate += delegate () { OnActivateNeedy.Invoke(); };
                if (OnNeedyActivation != null)
                    KMNeedyModule.OnNeedyActivation += delegate () { OnNeedyActivation.Invoke(); };
                if (OnNeedyDeactivation != null)
                    KMNeedyModule.OnNeedyDeactivation += delegate () { OnNeedyDeactivation.Invoke(); };
                if (OnTimerExpired != null)
                    KMNeedyModule.OnTimerExpired += delegate () { OnTimerExpired.Invoke(); };
            }
            else
            {
                PanicIfParentNull("KMNeedyModule", OnActivateNeedy, OnNeedyActivation, OnNeedyDeactivation, OnTimerExpired);
            }

            if (OnTimerTick != null)
                PanicIfNull("KMBombInfo", KMBombInfo);
        }

        /// <summary>
        /// Runs every frame.
        /// </summary>
        private void Update()
        {
            // Updates the amount of time left within the TimeLeft property.
            if (KMBombInfo != null)
                TimeLeft = (int)KMBombInfo.GetTime();
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
