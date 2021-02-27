using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace EmikBaseModules
{
    /// <summary>
    /// Base class for TwitchPlays support for regular and needy modded modules in Keep Talking and Nobody Explodes. Written by Emik.
    /// </summary>
    public abstract class TPScript : MonoBehaviour
    {
        /// <summary>
        /// The instance of ModuleScript, so that it can fetch information related to it.
        /// </summary>
        internal abstract ModuleScript ModuleScript { get; }
        /// <summary>
        /// Runs when a twitch command is directed to the module. The string is the command.
        /// </summary>
        internal abstract IEnumerator ProcessTwitchCommand(string command);
        /// <summary>
        /// Runs when the module is put under a forced solve state, either when it runs into an exception, or when the solve command is initiated.
        /// </summary>
        internal abstract IEnumerator TwitchHandleForcedSolve();
        /// <summary>
        /// A help message that can be performed with !{#} help where # is the module id.
        /// </summary>
        internal virtual string TwitchHelpMessage { get { return string.Empty; } }
        /// <summary>
        /// This specifies the link sent when doing !{#} manual. By default this returns the HTML link into the repository.
        /// </summary>
        internal virtual string TwitchManualCode { get { return "https://ktane.timwi.de/HTML/{0}.html".Form(ModuleScript.ModuleName); } }

        /// <summary>
        /// Should the TwitchPlays command be cancelled?
        /// </summary>
        internal bool TwitchShouldCancelCommand;
        /// <summary>
        /// Should TwitchPlays allow this module to skip time around?
        /// </summary>
        internal bool TwitchPlaysSkipTimeAllowed;

        /// <summary>
        /// This interrupts the TwitchPlays command if a module exists within this list.
        /// </summary>
        internal List<KMBombModule> TwitchAbandonModule;

        /// <summary>
        /// These strings represent a bunch of TwitchPlays functionality, read more on here: https://github.com/samfundev/KtaneTwitchPlays/wiki/External-Mod-Module-Support
        /// </summary>
        internal const string Strike = "strike",
            Solve = "solve",
            UnsubmittablePenalty = "unsubmittablepenalty", 
            TryCancelSequence = "trycancelsequence", 
            Cancelled = "cancelled", 
            MultipleStrikes = "multiple strikes", 
            EndMultipleStrikes = "end multiple strikes", 
            AutoSolve = "autosolve", 
            CancelDetonate = "cancel detonate", 
            WaitingMusic = "waiting music", 
            EndWaitingMusic = "end waiting music", 
            ToggleWaitingMusic = "toggle waiting music", 
            HideCamera = "hide camera";

        // These methods also do similar, except they have additional parameters.

        internal string StrikeMessage(string message)
        {
            return AppendIfNotNullOrEmpty("strikemessage", message);
        }

        internal string TryCancel(string message = null)
        {
            return AppendIfNotNullOrEmpty("trycancel", message);
        }

        internal string TryWaitCancel(float time, string message = null)
        {
            return AppendIfNotNullOrEmpty("strikemessage", time, message);
        }

        internal string SendToChat(string message)
        {
            return AppendIfNotNullOrEmpty("sendtochat", message);
        }

        internal string SendToChatError(string message)
        {
            return AppendIfNotNullOrEmpty("sendtochaterror ", message);
        }

        internal string SendDelayedMessage(float time, string message)
        {
            return AppendIfNotNullOrEmpty("senddelayedmessage", time, message);
        }

        internal string Detonate(float? time = null, string message = null)
        {
            return AppendIfNotNullOrEmpty("senddelayedmessage", time, message);
        }

        internal string SkipTime(string seconds = null)
        {
            return AppendIfNotNullOrEmpty("senddelayedmessage", seconds);
        }

        internal string AwardPoints(int points)
        {
            return AppendIfNotNullOrEmpty("awardpoints", points);
        }

        internal string AwardPointsOnSolve(int points)
        {
            return AppendIfNotNullOrEmpty("awardpointsonsolve", points);
        }

        private string AppendIfNotNullOrEmpty(string main, params object[] toAppend)
        {
            for (int i = 0; toAppend != null && i < toAppend.Length; i++)
                main += ' ' + toAppend[i].ToString();
            return main;
        }

        /// <summary>
        /// Halts until the condition is false.
        /// </summary>
        /// <param name="condition">This variable determines whether the IEnumerator keeps running.</param>
        /// <returns>true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true</returns>
        internal IEnumerator HaltUntil(bool condition)
        {
            while (condition) 
                yield return true;
        }

        /// <summary>
        /// If the given condition is true, return the other argument, otherwise null.
        /// </summary>
        /// <param name="condition">The condition to check for.</param>
        /// <param name="output">The output to yield return with. It will return all of them if the condition is true.</param>
        /// <returns>The object if condition is true, otherwise null.</returns>
        internal object Evaluate(bool condition, object output)
        {
            return condition ? output : null;
        }

        /// <summary>
        /// Presses the buttons in a KMSelectable array dictated by an integer array, waiting for 'wait' seconds.
        /// </summary>
        /// <param name="selectables">The KMSelectable array to perform the OnInteract events on.</param>
        /// <param name="indexes">The indexes to push within the KMSelectable.</param>
        /// <param name="wait">The amount of seconds between each interaction.</param>
        /// <param name="interrupt">When true, this interrupts the for-loop, typically when a strike has incurred.</param>
        /// <returns>WaitForSecondsRealtime of wait, the amount of times that indexes is long.</returns>
        internal IEnumerator OnInteractSequenceWithWait(KMSelectable[] selectables, int[] indexes, float wait, bool interrupt = false)
        {
            for (int i = 0; i < indexes.Length && !interrupt; i++)
            {
                selectables[indexes[i]].OnInteract();
                yield return new WaitForSecondsRealtime(wait);
            }
        }

        /// <summary>
        /// Tests a string against a regular expression.
        /// </summary>
        /// <param name="input">The string to test on.</param>
        /// <param name="pattern">The regular expression, or the test itself. An @ symbol is generally recommended.</param>
        /// <param name="lenient">Surrounds your regex with the common ^\s*STRING\s* that is used in most TP commands.</param>
        /// <param name="options">Options, by default these are the ones you generally want for TP.</param>
        /// <returns></returns>
        internal bool IsMatch(string input, string pattern, bool lenient = true, RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)
        {
            return Regex.IsMatch(input, lenient ? @"^\s*" + pattern + @"\s*$" : pattern, options);
        }
    }
}
