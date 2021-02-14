﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Rnd = UnityEngine.Random;

namespace EmikBaseModules
{
    /// <summary>
    /// Helper/extension class for regular and needy modded modules in Keep Talking and Nobody Explodes written by Emik.
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Creates an auto-formatted debug log, typically used to display information about the module. Use String.Format to assign variables.
        /// </summary>
        /// <param name="module">The module that called this method, since it needs to access the module's name and id.</param>
        /// <param name="log">The information to log.</param>
        /// <param name="logType"></param>
        internal static void Log(this ModuleScript module, object log, LogType logType = LogType.Log)
        {
            string formattedLog = "[{0} #{1}]: {2}".Format((object)module.ModuleName, module.ModuleId, log);
            switch (logType)
            {
                case LogType.Error:
                    Debug.LogError(formattedLog);
                    break;
                case LogType.Assert:
                    Debug.LogAssertion(formattedLog);
                    break;
                case LogType.Warning:
                    Debug.LogWarning(formattedLog);
                    break;
                case LogType.Log:
                    Debug.Log(formattedLog);
                    break;
                case LogType.Exception:
                    Debug.LogWarning(formattedLog);
                    break;
                default:
                    throw new NotImplementedException(logType.ToString() + " is not a valid log type.");
            }
        }

        /// <summary>
        /// A method that can play custom/game sounds, and/or shake the bomb. This is a multi-purpose basic enhancement of production value. Null means actions will not be performed.
        /// </summary>
        /// <param name="selectable">The selectable, which is used to call other methods, and use its transform.</param>
        /// <param name="audio">The current instance of KMAudio which is needed to play sound.</param>
        /// <param name="transform">The location of the source, if the selectable isn't being used.</param>
        /// <param name="intensityModifier">Adds bomb movement and controller vibration on interaction, amount is based on the modifier.</param>
        /// <param name="customSound">The custom sound to play, which must be assigned in TestHarness in the editor or mod.bundle in-game for the sound to be heard.</param>
        /// <param name="gameSound">The built-in sound effect to play.</param>
        /// <param name="ignoredCondition">A condition that will cancel this method. The null checks are still performed.</param>
        internal static void Button(this KMSelectable selectable, KMAudio audio = null, Transform transform = null, float? intensityModifier = null, string customSound = null, KMSoundOverride.SoundEffect? gameSound = null, bool ignoredCondition = false)
        {
            if (selectable == null)
                throw new NullReferenceException("Selectable should not be null when calling this method.");
            if (audio == null && (customSound != null || gameSound != null))
                throw new NullReferenceException("Audio should not be null if customSound and gameSound is specified. An instance of KMAudio is required for the sounds to be played.");
            if (ignoredCondition)
                return;
            if (transform == null && selectable != null)
                transform = selectable.transform;
            if (intensityModifier != null)
                selectable.AddInteractionPunch((float)intensityModifier);
            audio.Play(transform, customSound, gameSound);
        }

        /// <summary>
        /// Plays an in-game sound and/or custom sound.
        /// </summary>
        /// <param name="audio">The instance of audio, so that the sound can be played.</param>
        /// <param name="transform">The location of the sound.</param>
        /// <param name="customSound">The custom sound effect, which is ignored if null.</param>
        /// <param name="gameSound">The in-game sound effect, which is ignored if null.</param>
        internal static void Play(this KMAudio audio, Transform transform, string customSound = null, KMSoundOverride.SoundEffect? gameSound = null)
        {
            if (customSound != null)
                audio.PlaySoundAtTransform(customSound, transform);
            if (gameSound != null)
                audio.PlayGameSoundAtTransform((KMSoundOverride.SoundEffect)gameSound, transform);
        }

        /// <summary>
        /// Returns 0 if the array is null, otherwise the array length.
        /// </summary>
        /// <param name="array">The array to return its length.</param>
        /// <returns>The array's length, or if null, 0.</returns>
        internal static int LengthSafe(this Array array)
        {
            return array != null ? array.Length : 0;
        }

        /// <summary>
        /// Returns the element of an IEnumerable, wrapping the index if necessary.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable.</typeparam>
        /// <param name="source">The enumerable to index in.</param>
        /// <param name="i">The index for the enumerable.</param>
        /// <returns>The element of the IEnumerable, specified by the wrapped index (if it's greater than or equal the collection).</returns>
        internal static T ElementAtWrap<T>(this IEnumerable<T> source, int i)
        {
            return source.ElementAtOrDefault(i % source.Count());
        }

        /// <summary>
        /// Appends an element to an IEnumerable.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable.</typeparam>
        /// <param name="source">The enumerable to append with.</param>
        /// <param name="item">The item to append to the enumerable.</param>
        /// <returns>A new instance of the enumerable, with an added last entry being the item.</returns>
        internal static IEnumerable<T> Append<T>(this IEnumerable<T> source, T item)
        {
            return source.Concat(new T[] { item });
        }

        /// <summary>
        /// Appends an element to an array.
        /// </summary>
        /// <typeparam name="T">The type that both the array and element are.</typeparam>
        /// <param name="array">The array that needs to be appended.</param>
        /// <param name="element">The element to append to the array.</param>
        /// <returns>The new array, consisting of the old array, then the element.</returns>
        internal static T[] Append<T>(this T[] array, T element)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = element;
            return array;
        }

        /// <summary>
        /// Prepends an element to an IEnumerable.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable.</typeparam>
        /// <param name="source">The enumerable to prepend with.</param>
        /// <param name="item">The item to prepend to the enumerable.</param>
        /// <returns>A new instance of the enumerable, with an added first entry being the item.</returns>
        internal static IEnumerable<T> Prepend<T>(this IEnumerable<T> source, T item)
        {
            return new T[] { item }.Concat(source);
        }

        /// <summary>
        /// Prepends an element to an array.
        /// </summary>
        /// <typeparam name="T">The type that both the array and element are.</typeparam>
        /// <param name="array">The array that needs to be prepended.</param>
        /// <param name="element">The element to prepend to the array.</param>
        /// <returns>The new array, consisting of the old array, with an added element before it.</returns>
        internal static T[] Prepend<T>(this T[] array, T element)
        {
            Array.Resize(ref array, array.Length + 1);
            Array.Copy(array, 0, array, 1, array.Length);
            array[0] = element;
            return array;
        }

        /// <summary>
        /// Gets multiple random floats between two values. Min is inclusive, max is inclusive.
        /// </summary>
        /// <param name="min">The minimum float (inclusive) to generate.</param>
        /// <param name="max">The maximum float (inclusive) to generate.</param>
        /// <param name="times">The length of the array, or the amount of times the numbers are needed to be generated.</param>
        /// <returns>An array of random floats between min and max, of length times.</returns>
        internal static float[] Ranges(float min, float max, int times)
        {
            float[] vs = new float[times];
            for (int i = 0; i < times; i++)
                vs[i] = Rnd.Range(min, max);
            return vs;
        }

        /// <summary>
        /// Gets multiple random integers between two values. Min is inclusive, max is exclusive.
        /// </summary>
        /// <param name="min">The minimum integer (inclusive) to generate.</param>
        /// <param name="max">The maximum integer (exclusive) to generate.</param>
        /// <param name="times">The length of the array, or the amount of times the numbers are needed to be generated.</param>
        /// <returns>An array of random integers between min and max, of length times.</returns>
        internal static int[] Ranges(int min, int max, int times)
        {
            int[] vs = new int[times];
            for (int i = 0; i < times; i++)
                vs[i] = Rnd.Range(min, max);
            return vs;
        }

        /// <summary>
        /// Generates and returns a boolean array that is random.
        /// </summary>
        /// <param name="length">The length of the array.</param>
        /// <returns>A boolean array of random values.</returns>
        internal static bool[] RandomBools(int length, float weighting = 0.5f)
        {
            bool[] array = new bool[length];
            for (int i = 0; i < array.Length; i++)
                array[i] = Rnd.Range(0, 1f) > weighting;
            return array;
        }

        /// <summary>
        /// Emulates a foreach loop that can be used inline alongside other Linq functions.
        /// </summary>
        /// <typeparam name="T">The datatype of the variable.</typeparam>
        /// <param name="source">The variable to apply.</param>
        /// <param name="act">The action to apply to the variable.</param>
        /// <returns>The modification of the variable after each element goes through the action provided.</returns>
        internal static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> act)
        {
            foreach (T element in source) act(element);
            return source;
        }

        /// <summary>
        /// Gets all values from an enum type and returns it as an array.
        /// </summary>
        /// <typeparam name="T">An enum type.</typeparam>
        /// <returns>All values of the type specified.</returns>
        internal static T[] EnumAsArray<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToArray();
        }

        /// <summary>
        /// Returns whether the index of the array is null.
        /// </summary>
        /// <param name="obj">The array to check.</param>
        /// <param name="i">The index to check up on.</param>
        /// <returns>True if the array is null, the index is not out of range, or the element corresponding to the index of the array is null.</returns>
        internal static bool IsIndexNull(this object[] obj, int i)
        {
            return obj == null || i >= obj.Length || obj[i] == null;
        }

        /// <summary>
        /// Returns the character as lowercase.
        /// </summary>
        /// <typeparam name="T">The datatype of the variable.</typeparam>
        /// <param name="source">The variable to apply lowercase to.</param>
        /// <returns>The lowercase version of the character.</returns>
        internal static char ToLower<T>(this T source)
        {
            return source.ToString().ToLowerInvariant()[0];
        }

        /// <summary>
        /// Formats and returns the string with the arguments specified. {#} where # is a number is used as placeholders for these variables.
        /// </summary>
        /// <param name="str">The template string.</param>
        /// <param name="args">The variables to assign into the string.</param>
        /// <returns>The string, replacing {#} where # is a number with the corresponding # index in the object array.</returns>
        internal static string Format(this string str, params object[] args)
        {
            return string.Format(str, args);
        }
    }
}
