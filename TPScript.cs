using System;
using System.Collections;
using System.Collections.Generic;
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
    }
}
