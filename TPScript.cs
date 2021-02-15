using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace EmikBaseModules
{
    /// <summary>
    /// Base class for TwitchPlays support in regular and needy modded modules written by Emik.
    /// </summary>
    public abstract class TPScript : MonoBehaviour
    {
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
        internal abstract string TwitchHelpMessage { get; }

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
        /// Parses each element of an array into an integer array. If it fails, it returns null.
        /// </summary>
        /// <typeparam name="T">Typically char or string array, though this can theoretically be anything.</typeparam>
        /// <param name="ts">The array to parse.</param>
        /// <param name="min">Adds a constriction: The inclusive minimum value for each index.</param>
        /// <param name="max">Adds a constriction: The inclusive maximum value for each index.</param>
        /// <param name="minLength">Adds a constriction: The inclusive minimum value for the length of the array.</param>
        /// <param name="maxLength">Adds a constriction: The inclusive maximum value for the length of the array.</param>
        /// <returns>The provided array as an integer array if it can parse all elements successfully, otherwise null.</returns>
        internal int[] ToNumbers<T>(T[] ts, int? min = null, int? max = null, int? minLength = null, int? maxLength = null)
        {
            int _;
            return (minLength == null || minLength <= ts.Length) && (maxLength == null || maxLength >= ts.Length) && 
                ts.All(t => int.TryParse(t.ToString(), out _) && (min == null || min <= _) && (max == null || max >= _))
                ? ts.Select(t => int.Parse(t.ToString())).ToArray() : null;
        }

        /// <summary>
        /// Tests a string against a regular expression.
        /// </summary>
        /// <param name="input">The string to test on.</param>
        /// <param name="pattern">The regular expression, or the test itself. An @ symbol is generally recommended.</param>
        /// <param name="options">Options, by default these are the ones you generally want for TP.</param>
        /// <returns></returns>
        internal bool IsMatch(string input, string pattern, RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)
        {
            return Regex.IsMatch(input, pattern, options);
        }
    }
}
