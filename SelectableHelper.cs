using System;

namespace EmikBaseModules
{
    internal static class SelectableHelper
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
                throw new NullReferenceException("The array is null. You cannot assign events to a KMSelectable without a reference to a KMSelectable.");
            for (int i = 0; i < kmSelectable.Length; i++)
            {
                int j = i;
                if (kmSelectable[i] == null)
                    throw new NullReferenceException("The index {0} is null. You cannot assign events to a KMSelectable without a reference to a KMSelectable.".Format(i));
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
                throw new NullReferenceException("The KMSelectable is null. You cannot assign events to a KMSelectable without a reference to a KMSelectable.");
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
                throw new NullReferenceException("The array is null. You cannot unassign events to a KMSelectable without a reference to a KMSelectable.");
            for (int i = 0; i < kmSelectable.Length; i++)
            {
                int j = i;
                if (kmSelectable[i] == null)
                    throw new NullReferenceException("The index {0} is null. You cannot unassign events to a KMSelectable without a reference to a KMSelectable.".Format(i));
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
                throw new NullReferenceException("The KMSelectable is null. You cannot unassign events to a KMSelectable without a reference to a KMSelectable.");
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
    }
}
