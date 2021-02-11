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
        internal virtual KMBombInfo.KMBombSolvedDelegate OnBombExploded { get; set; }
        /// <summary>
        /// Triggers when all solvable modules are solved.
        /// </summary>
        internal virtual KMBombInfo.KMBombSolvedDelegate OnBombSolved { get; set; }

        /// <summary>
        /// 1 event: OnActivate.
        /// </summary>
        internal virtual KMBombModule KMBombModule { get; set; }
        /// <summary>
        /// Triggers when the lights come on.
        /// </summary>
        internal virtual KMBombModule.KMModuleActivateEvent OnActivate { get; set; }

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
        internal virtual KMGameInfo.KMAlarmClockChangeDelegate OnAlarmClockChange { get; set; }
        /// <summary>
        /// Triggers when the lights goes on to off, or vice versa.
        /// </summary>
        internal virtual KMGameInfo.KMLightsChangeDelegate OnLightsChange { get; set; }

        /// <summary>
        /// 4 events: OnActivateNeedy, OnNeedyActivation, OnNeedyDeactivation, OnTimerExpired
        /// </summary>
        internal virtual KMNeedyModule KMNeedyModule { get; set; }
        /// <summary>
        /// Triggers when the lights come on.
        /// </summary>
        internal virtual KMNeedyModule.KMModuleActivateEvent OnActivateNeedy { get; set; }
        /// <summary>
        /// Triggers when the needy activates the timer.
        /// </summary>
        internal virtual KMNeedyModule.KMNeedyActivationEvent OnNeedyActivation { get; set; }
        /// <summary>
        /// Triggers when the needy deactivates the timer.
        /// </summary>
        internal virtual KMNeedyModule.KMNeedyDeactivationEvent OnNeedyDeactivation { get; set; }
        /// <summary>
        /// Triggers when its timer runs out.
        /// </summary>
        internal virtual KMNeedyModule.KMTimerExpiredEvent OnTimerExpired { get; set; }

        /// <summary>
        /// 11 events: OnCancel, OnDefocus, OnFocus, OnHighlight, OnHighlightEnded, OnInteract, OnInteractEnded, OnLeft, OnRight, OnSelect.
        /// </summary>
        internal virtual KMSelectable[][] KMSelectable { get; set; }
        /// <summary>
        /// Triggers when player backs out of this selectable, an example would be zooming out of a module. Return is whether it should drill out to parent.
        /// </summary>
        internal virtual Func<int, KMSelectable.OnCancelHandler>[] OnCancel { get; set; }
        /// <summary>
        /// Triggers when a module is defocused, this is when a different selectable becomes the focus or the module has been backed out of.
        /// </summary>
        internal virtual Func<int, Action>[] OnDefocus { get; set; }
        /// <summary>
        /// Triggers when the selectable stops being the current selectable.
        /// </summary>
        internal virtual Func<int, Action>[] OnDeselect { get; set; }
        /// <summary>
        /// Triggers when a module is focused, this is when it is interacted with from the bomb face level and this module's children can be selected.
        /// </summary>
        internal virtual Func<int, Action>[] OnFocus { get; set; }
        /// <summary>
        /// Triggers when the highlight is turned on.
        /// </summary>
        internal virtual Func<int, Action>[] OnHighlight { get; set; }
        /// <summary>
        /// Triggers when the highlight is turned off.
        /// </summary>
        internal virtual Func<int, Action>[] OnHighlightEnded { get; set; }
        /// <summary>
        /// Triggers when player interacts with this selectable. Done on button down.  Return is whether it should drill in to children.
        /// </summary>
        internal virtual Func<int, KMSelectable.OnInteractHandler>[] OnInteract { get; set; }
        /// <summary>
        /// Triggers when a player is interacting with this selectable and releases the mouse or controller button.
        /// </summary>
        internal virtual Func<int, Action>[] OnInteractEnded { get; set; }
        /// <summary>
        /// Called when player pulls stick left while this selectable is focused.
        /// </summary>
        internal virtual Func<int, Action>[] OnLeft { get; set; }
        /// <summary>
        /// Called when player pulls stick right while this selectable is focused.
        /// </summary>
        internal virtual Func<int, Action>[] OnRight { get; set; }
        /// <summary>
        /// Called whenever this selectable becomes the current selectable.
        /// </summary>
        internal virtual Func<int, Action>[] OnSelect { get; set; }

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
                    KMBombInfo.OnBombExploded += OnBombExploded;
                if (OnBombSolved != null)
                    KMBombInfo.OnBombSolved += OnBombSolved;
            }
            else
            {
                PanicIfParentNull("KMBombInfo", OnBombExploded, OnBombSolved);
            }

            if (KMBombModule != null)
            {
                if (OnActivate != null)
                    KMBombModule.OnActivate += (() => IsActivate = true) + OnActivate;

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
                    KMGameInfo.OnAlarmClockChange += OnAlarmClockChange;
                if (OnLightsChange != null)
                    KMGameInfo.OnLightsChange += OnLightsChange;
            }
            else
            {
                PanicIfParentNull("KMGameInfo", OnActivateNeedy, OnNeedyActivation, OnNeedyDeactivation, OnTimerExpired);
            }

            if (KMNeedyModule != null)
            {
                if (OnActivateNeedy != null)
                    KMNeedyModule.OnActivate += OnActivateNeedy;
                if (OnNeedyActivation != null)
                    KMNeedyModule.OnNeedyActivation += OnNeedyActivation;
                if (OnNeedyDeactivation != null)
                    KMNeedyModule.OnNeedyDeactivation += OnNeedyDeactivation;
                if (OnTimerExpired != null)
                    KMNeedyModule.OnTimerExpired += OnTimerExpired;
            }
            else
            {
                PanicIfParentNull("KMNeedyModule", OnActivateNeedy, OnNeedyActivation, OnNeedyDeactivation, OnTimerExpired);
            }

            if (KMSelectable != null)
            {
                for (int i = 0; i < KMSelectable.Length; i++)
                    for (int j = 0; j < KMSelectable[i].Length; j++)
                        if (KMSelectable[i][j] != null)
                            AssignSelectable(KMSelectable[i][j], ref i, ref j);
                        else
                            PanicIfNull("KMSelectable[{0}][{1}]".Format(i, j), KMSelectable[i][j]);
            }
            else
            {
                PanicIfParentNull("KMSelectable", OnCancel, OnDefocus, OnDeselect, OnFocus, OnHighlight, OnHighlightEnded, OnInteract, OnInteractEnded, OnLeft, OnRight, OnSelect);
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
        /// Assigns all events to the selectable, since there are so many of them.
        /// </summary>
        /// <param name="selectable">The selectable to assign events to.</param>
        /// <param name="i">The index of the methods array.</param>
        /// <param name="j">The number to pass into the method.</param>
        private void AssignSelectable(KMSelectable selectable, ref int i, ref int j)
        {
            if (!OnCancel.IsIndexNull(i))
                selectable.OnCancel += OnCancel[i](j);
            if (!OnDefocus.IsIndexNull(i))
                selectable.OnDefocus += OnDefocus[i](j);
            if (!OnDeselect.IsIndexNull(i))
                selectable.OnDeselect += OnDeselect[i](j);
            if (!OnFocus.IsIndexNull(i))
                selectable.OnFocus += OnFocus[i](j);
            if (!OnHighlight.IsIndexNull(i))
                selectable.OnHighlight += OnHighlight[i](j);
            if (!OnHighlightEnded.IsIndexNull(i))
                selectable.OnHighlightEnded += OnHighlightEnded[i](j);
            if (!OnInteract.IsIndexNull(i))
                selectable.OnInteract += OnInteract[i](j);
            if (!OnInteractEnded.IsIndexNull(i))
                selectable.OnInteractEnded += OnInteractEnded[i](j);
            if (!OnLeft.IsIndexNull(i))
                selectable.OnLeft += OnLeft[i](j);
            if (!OnRight.IsIndexNull(i))
                selectable.OnRight += OnRight[i](j);
            if (!OnSelect.IsIndexNull(i))
                selectable.OnSelect += OnSelect[i](j);
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
