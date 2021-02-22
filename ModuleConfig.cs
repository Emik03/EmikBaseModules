using System;

namespace EmikBaseModules
{
    /// <summary>
    /// Contains all information to give to ModuleScript within one class.
    /// </summary>
    internal class ModuleConfig
    {
        internal ModuleConfig(KMBombModule kmBombModule = null,
            KMBossModule kmBossModule = null,
            KMColorblindMode kmColorblindMode = null,
            KMNeedyModule kmNeedyModule = null,
            Tuple<Action, KMBombInfo>? onTimerTick = null)
        {
            KMBombModule = kmBombModule;
            KMBossModule = kmBossModule;
            KMColorblindMode = kmColorblindMode;
            KMNeedyModule = kmNeedyModule;
            OnTimerTick = onTimerTick;
        }

        /// <summary>
        /// Instance of a regular module. Used to get the name of the module.
        /// </summary>
        internal KMBombModule KMBombModule { get; private set; }

        /// <summary>
        /// Instance of a boss module, is used to set the ignore list.
        /// </summary>
        internal KMBossModule KMBossModule { get; private set; }

        /// <summary>
        /// Accesses the colorblind mod in-game, is used to set IsColorblind.
        /// </summary>
        internal KMColorblindMode KMColorblindMode { get; private set; }

        /// <summary>
        /// Instance of a needy module. Used to get the name of the module.
        /// </summary>
        internal KMNeedyModule KMNeedyModule { get; private set; }

        /// <summary>
        /// Called whenever the timer tick changes. Requires KMBombInfo to access the time left.
        /// </summary>
        internal Tuple<Action, KMBombInfo>? OnTimerTick { get; private set; }
    }
}
