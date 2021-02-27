using System;

namespace EmikBaseModules
{
    /// <summary>
    /// KMFramework extension methods that makes it easier to assign multiple events to a variable in one statement. Written by Emik.
    /// </summary>
    internal static class EventHelper
    {
        internal static void Assign(this KMSelectable[] kmSelectable,
            Func<int, bool> onCancel = null,
            Func<int, Action> onDefocus = null,
            Func<int, Action> onDeselect = null,
            Func<int, Action> onFocus = null,
            Func<int, Action> onHighlight = null,
            Func<int, Action> onHighlightEnded = null,
            Func<int, bool> onInteract = null,
            Func<int, Action> onInteractEnded = null,
            Func<int, Action> onLeft = null,
            Func<int, Action> onRight = null,
            Func<int, Action> onSelect = null)
        {
            if (kmSelectable == null)
                throw new ArgumentNullException("The array is null. You cannot assign events to a KMSelectable without a reference to a KMSelectable.");
            for (int i = 0; i < kmSelectable.Length; i++)
            {
                int j = i;
                if (kmSelectable[i] == null)
                    throw new ArgumentNullException("The index {0} is null. You cannot assign events to a KMSelectable without a reference to a KMSelectable.".Form(i));
                if (onCancel != null)
                    kmSelectable[i].OnCancel += () => onCancel(j);
                if (onDefocus != null)
                    kmSelectable[i].OnDefocus += onDefocus(j);
                if (onDeselect != null)
                    kmSelectable[i].OnDeselect += onDeselect(j);
                if (onFocus != null)
                    kmSelectable[i].OnFocus += onFocus(j);
                if (onHighlight != null)
                    kmSelectable[i].OnHighlight += onHighlight(j);
                if (onHighlightEnded != null)
                    kmSelectable[i].OnHighlightEnded += onHighlightEnded(j);
                if (onInteract != null)
                    kmSelectable[i].OnInteract += () => onInteract(j);
                if (onInteractEnded != null)
                    kmSelectable[i].OnInteractEnded += onInteractEnded(j);
                if (onLeft != null)
                    kmSelectable[i].OnLeft += onLeft(j);
                if (onRight != null)
                    kmSelectable[i].OnRight += onRight(j);
                if (onSelect != null)
                    kmSelectable[i].OnSelect += onSelect(j);
            }
        }

        internal static void Assign(this KMSelectable kmSelectable,
            Func<bool> onCancel = null,
            Func<Action> onDefocus = null,
            Func<Action> onDeselect = null,
            Func<Action> onFocus = null,
            Func<Action> onHighlight = null,
            Func<Action> onHighlightEnded = null,
            Func<bool> onInteract = null,
            Func<Action> onInteractEnded = null,
            Func<Action> onLeft = null,
            Func<Action> onRight = null,
            Func<Action> onSelect = null)
        {
            if (kmSelectable == null)
                throw new ArgumentNullException("The KMSelectable is null. You cannot assign events to a KMSelectable without a reference to a KMSelectable.");
            if (onCancel != null)
                kmSelectable.OnCancel += () => onCancel();
            if (onDefocus != null)
                kmSelectable.OnDefocus += onDefocus();
            if (onDeselect != null)
                kmSelectable.OnDeselect += onDeselect();
            if (onFocus != null)
                kmSelectable.OnFocus += onFocus();
            if (onHighlight != null)
                kmSelectable.OnHighlight += onHighlight();
            if (onHighlightEnded != null)
                kmSelectable.OnHighlightEnded += onHighlightEnded();
            if (onInteract != null)
                kmSelectable.OnInteract += () => onInteract();
            if (onInteractEnded != null)
                kmSelectable.OnInteractEnded += onInteractEnded();
            if (onLeft != null)
                kmSelectable.OnLeft += onLeft();
            if (onRight != null)
                kmSelectable.OnRight += onRight();
            if (onSelect != null)
                kmSelectable.OnSelect += onSelect();
        }

        internal static void Assign(this KMNeedyModule kmNeedyModule,
            Func<Action> onActivate = null,
            Func<Action> onNeedyActivation = null,
            Func<Action> onNeedyDeactivation = null,
            Func<Action> onTimerExpired = null,
            ModuleScript moduleScript = null)
        {
            if (kmNeedyModule == null)
                throw new ArgumentNullException("The KMNeedyModule is null. You cannot assign events to a KMNeedyModule without a reference to a KMNeedyModule.");
            if (onActivate != null)
                kmNeedyModule.OnActivate += delegate () { onActivate.Invoke().Invoke(); };
            if (onNeedyActivation != null)
                kmNeedyModule.OnNeedyActivation += delegate () { if (moduleScript != null) moduleScript.IsNeedyActive = true; onNeedyActivation.Invoke().Invoke(); };
            if (onNeedyDeactivation != null)
                kmNeedyModule.OnNeedyDeactivation += delegate () { if (moduleScript != null) moduleScript.IsNeedyActive = false; onNeedyDeactivation.Invoke().Invoke(); };
            if (onTimerExpired != null)
                kmNeedyModule.OnTimerExpired += delegate () { onTimerExpired.Invoke().Invoke(); };
        }

        internal static void Assign(this KMGameInfo kmGameInfo,
            Func<bool, Action> onAlarmClockChange = null,
            Func<bool, Action> onLightsChange = null)
        {
            if (onAlarmClockChange != null)
                kmGameInfo.OnAlarmClockChange += on => onAlarmClockChange(on);
            if (onLightsChange != null)
                kmGameInfo.OnLightsChange += on => onLightsChange(on);
        }

        internal static void Assign(this KMBombInfo kmBombInfo,
            Func<Action> onBombExploded = null,
            Func<Action> onBombSolved = null)
        {
            if (onBombExploded != null)
                kmBombInfo.OnBombExploded += delegate () { onBombExploded.Invoke().Invoke(); };
            if (onBombSolved != null)
                kmBombInfo.OnBombSolved += delegate () { onBombSolved.Invoke().Invoke(); };
        }

        internal static void Assign(this KMBombModule kmBombModule,
            Func<Action> onActivate,
            ModuleScript moduleScript = null)
        {
            if (kmBombModule == null)
                throw new ArgumentNullException("The KMBombModule is null. You cannot assign events to a KMBombModule without a reference to a KMBombModule.");
            if (onActivate == null)
                throw new ArgumentNullException("The OnActivate event is null. Considering that KMBombModule has only 1 event, this is considered a mistake.");
            kmBombModule.OnActivate += delegate () { if (moduleScript != null) moduleScript.IsActivate = true; onActivate.Invoke().Invoke(); };
        }

        internal static void Unassign(this KMSelectable[] kmSelectable,
            Func<int, bool> onCancel = null,
            Func<int, Action> onDefocus = null,
            Func<int, Action> onDeselect = null,
            Func<int, Action> onFocus = null,
            Func<int, Action> onHighlight = null,
            Func<int, Action> onHighlightEnded = null,
            Func<int, bool> onInteract = null,
            Func<int, Action> onInteractEnded = null,
            Func<int, Action> onLeft = null,
            Func<int, Action> onRight = null,
            Func<int, Action> onSelect = null)
        {
            if (kmSelectable == null)
                throw new ArgumentNullException("The array is null. You cannot unassign events to a KMSelectable without a reference to a KMSelectable.");
            for (int i = 0; i < kmSelectable.Length; i++)
            {
                int j = i;
                if (kmSelectable[i] == null)
                    throw new ArgumentNullException("The index {0} is null. You cannot unassign events to a KMSelectable without a reference to a KMSelectable.".Form(i));
                if (onCancel != null)
                    kmSelectable[i].OnCancel -= () => onCancel(j);
                if (onDefocus != null)
                    kmSelectable[i].OnDefocus -= onDefocus(j);
                if (onDeselect != null)
                    kmSelectable[i].OnDeselect -= onDeselect(j);
                if (onFocus != null)
                    kmSelectable[i].OnFocus -= onFocus(j);
                if (onHighlight != null)
                    kmSelectable[i].OnHighlight -= onHighlight(j);
                if (onHighlightEnded != null)
                    kmSelectable[i].OnHighlightEnded -= onHighlightEnded(j);
                if (onInteract != null)
                    kmSelectable[i].OnInteract -= () => onInteract(j);
                if (onInteractEnded != null)
                    kmSelectable[i].OnInteractEnded -= onInteractEnded(j);
                if (onLeft != null)
                    kmSelectable[i].OnLeft -= onLeft(j);
                if (onRight != null)
                    kmSelectable[i].OnRight -= onRight(j);
                if (onSelect != null)
                    kmSelectable[i].OnSelect -= onSelect(j);
            }
        }

        internal static void Unassign(this KMSelectable kmSelectable,
            Func<bool> onCancel = null,
            Func<Action> onDefocus = null,
            Func<Action> onDeselect = null,
            Func<Action> onFocus = null,
            Func<Action> onHighlight = null,
            Func<Action> onHighlightEnded = null,
            Func<bool> onInteract = null,
            Func<Action> onInteractEnded = null,
            Func<Action> onLeft = null,
            Func<Action> onRight = null,
            Func<Action> onSelect = null)
        {
            if (kmSelectable == null)
                throw new ArgumentNullException("The KMSelectable is null. You cannot unassign events to a KMSelectable without a reference to a KMSelectable.");
            if (onCancel != null)
                kmSelectable.OnCancel -= () => onCancel();
            if (onDefocus != null)
                kmSelectable.OnDefocus -= onDefocus();
            if (onDeselect != null)
                kmSelectable.OnDeselect -= onDeselect();
            if (onFocus != null)
                kmSelectable.OnFocus -= onFocus();
            if (onHighlight != null)
                kmSelectable.OnHighlight -= onHighlight();
            if (onHighlightEnded != null)
                kmSelectable.OnHighlightEnded -= onHighlightEnded();
            if (onInteract != null)
                kmSelectable.OnInteract -= () => onInteract();
            if (onInteractEnded != null)
                kmSelectable.OnInteractEnded -= onInteractEnded();
            if (onLeft != null)
                kmSelectable.OnLeft -= onLeft();
            if (onRight != null)
                kmSelectable.OnRight -= onRight();
            if (onSelect != null)
                kmSelectable.OnSelect -= onSelect();
        }

        internal static void Unassign(this KMNeedyModule kmNeedyModule,
            Func<Action> onActivateNeedy = null,
            Func<Action> onNeedyActivation = null,
            Func<Action> onNeedyDeactivation = null,
            Func<Action> onTimerExpired = null,
            ModuleScript moduleScript = null)
        {
            if (kmNeedyModule == null)
                throw new ArgumentNullException("The KMNeedyModule is null. You cannot assign events to a KMNeedyModule without a reference to a KMNeedyModule.");
            if (onActivateNeedy != null)
                kmNeedyModule.OnActivate -= delegate () { onActivateNeedy.Invoke().Invoke(); };
            if (onNeedyActivation != null)
                kmNeedyModule.OnNeedyActivation -= delegate () { if (moduleScript != null) moduleScript.IsNeedyActive = true; onNeedyActivation.Invoke().Invoke(); };
            if (onNeedyDeactivation != null)
                kmNeedyModule.OnNeedyDeactivation -= delegate () { if (moduleScript != null) moduleScript.IsNeedyActive = false; onNeedyDeactivation.Invoke().Invoke(); };
            if (onTimerExpired != null)
                kmNeedyModule.OnTimerExpired -= delegate () { onTimerExpired.Invoke().Invoke(); };
        }

        internal static void Unassign(this KMGameInfo kmGameInfo,
            Func<bool, Action> onAlarmClockChange = null,
            Func<bool, Action> onLightsChange = null)
        {
            if (kmGameInfo == null)
                throw new ArgumentNullException("The KMGameInfo is null. You cannot assign events to a KMGameInfo without a reference to a KMGameInfo.");
            if (onAlarmClockChange != null)
                kmGameInfo.OnAlarmClockChange -= on => onAlarmClockChange(on);
            if (onLightsChange != null)
                kmGameInfo.OnLightsChange -= on => onLightsChange(on);
        }

        internal static void Unassign(this KMBombInfo kmBombInfo,
            Func<Action> onBombExploded = null,
            Func<Action> onBombSolved = null)
        {
            if (onBombExploded != null)
                kmBombInfo.OnBombExploded -= delegate () { onBombExploded.Invoke().Invoke(); };
            if (onBombSolved != null)
                kmBombInfo.OnBombSolved -= delegate () { onBombSolved.Invoke().Invoke(); };
        }

        internal static void Unassign(this KMBombModule kmBombModule,
            Func<Action> onActivate,
            ModuleScript moduleScript = null)
        {
            if (kmBombModule == null)
                throw new ArgumentNullException("The KMBombModule is null. You cannot assign events to a KMBombModule without a reference to a KMBombModule.");
            if (onActivate == null)
                throw new ArgumentNullException("The OnActivate event is null. Considering that KMBombModule has only 1 event, this is considered a mistake.");
            kmBombModule.OnActivate -= delegate () { if (moduleScript != null) moduleScript.IsActivate = true; onActivate.Invoke().Invoke(); };
        }
    }
}
