using System;
using System.Collections;
using UnityEngine;

namespace EmikBaseModules
{
    /// <summary>
    /// Stores an enumerator which can be called any time, including non-monobehaviour scripts. Written by Emik.
    /// </summary>
    public class IEnum
    {
        /// <summary>
        /// Stores the enumerator. Reminder that this is a reference type.
        /// </summary>
        /// <param name="enumerator">The enumerator function to store.</param>
        /// <param name="monoBehaviour">The instance of monobehaviour to call when the coroutine runs.</param>
        public IEnum(Func<IEnumerator> enumerator, MonoBehaviour monoBehaviour)
        {
            Enumerator = enumerator;
            MonoBehaviour = monoBehaviour;
        }

        /// <summary>
        /// Sets the IsRunning property to true, waits for the enumerator before setting it to false again.
        /// </summary>
        public void RunCoroutine()
        {
            MonoBehaviour.StartCoroutine(Method());
        }

        /// <summary>
        /// Starts the coroutine using the monobehaviour.
        /// </summary>
        /// <returns>The enumerator.</returns>
        private IEnumerator Method()
        {
            IsRunning = true;
            yield return Enumerator();
            IsRunning = false;
        }

        /// <summary>
        /// Whether the coroutine is running or not.
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// The enumerator to run.
        /// </summary>
        private Func<IEnumerator> Enumerator { get; set; }

        /// <summary>
        /// The instance of monobehaviour to call the enumerator.
        /// </summary>
        private MonoBehaviour MonoBehaviour { get; set; }
    }

    /// <summary>
    /// Datatype that stores an enumerator, and stores the current instance of whether the Coroutine is running.
    /// </summary>
    public class IEnum<T>
    {
        /// <summary>
        /// Stores the enumerator. Reminder that this is a reference type.
        /// </summary>
        /// <param name="enumerator">The enumerator function to store.</param>
        /// <param name="monoBehaviour">The instance of monobehaviour to call when the coroutine runs.</param>
        public IEnum(Func<T, IEnumerator> enumerator, MonoBehaviour monoBehaviour)
        {
            Enumerator = enumerator;
            MonoBehaviour = monoBehaviour;
        }

        /// <summary>
        /// Sets the IsRunning property to true, waits for the enumerator before setting it to false again.
        /// </summary>
        public void RunCoroutine(T t)
        {
            MonoBehaviour.StartCoroutine(Method(t));
        }

        /// <summary>
        /// Starts the coroutine using the monobehaviour.
        /// </summary>
        /// <returns>The enumerator.</returns>
        private IEnumerator Method(T t)
        {
            IsRunning = true;
            yield return Enumerator(t);
            IsRunning = false;
        }

        /// <summary>
        /// Whether the coroutine is running or not.
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// The enumerator to run.
        /// </summary>
        private Func<T, IEnumerator> Enumerator { get; set; }

        /// <summary>
        /// The instance of monobehaviour to call the enumerator.
        /// </summary>
        private MonoBehaviour MonoBehaviour { get; set; }
    }

    /// <summary>
    /// Datatype that stores an enumerator, and stores the current instance of whether the Coroutine is running.
    /// </summary>
    public class IEnum<T1, T2>
    {
        /// <summary>
        /// Stores the enumerator. Reminder that this is a reference type.
        /// </summary>
        /// <param name="enumerator">The enumerator function to store.</param>
        /// <param name="monoBehaviour">The instance of monobehaviour to call when the coroutine runs.</param>
        public IEnum(Func<T1, T2, IEnumerator> enumerator, MonoBehaviour monoBehaviour)
        {
            Enumerator = enumerator;
            MonoBehaviour = monoBehaviour;
        }

        /// <summary>
        /// Sets the IsRunning property to true, waits for the enumerator before setting it to false again.
        /// </summary>
        public void StartCoroutine(T1 t1, T2 t2)
        {
            MonoBehaviour.StartCoroutine(Method(t1, t2));
        }

        /// <summary>
        /// Starts the coroutine using the monobehaviour.
        /// </summary>
        /// <returns>The enumerator.</returns>
        private IEnumerator Method(T1 t1, T2 t2)
        {
            IsRunning = true;
            yield return Enumerator(t1, t2);
            IsRunning = false;
        }

        /// <summary>
        /// Whether the coroutine is running or not.
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// The enumerator to run.
        /// </summary>
        private Func<T1, T2, IEnumerator> Enumerator { get; set; }

        /// <summary>
        /// The instance of monobehaviour to call the enumerator.
        /// </summary>
        private MonoBehaviour MonoBehaviour { get; set; }
    }

    /// <summary>
    /// Datatype that stores an enumerator, and stores the current instance of whether the Coroutine is running.
    /// </summary>
    public class IEnum<T1, T2, T3>
    {
        /// <summary>
        /// Stores the enumerator. Reminder that this is a reference type.
        /// </summary>
        /// <param name="enumerator">The enumerator function to store.</param>
        /// <param name="monoBehaviour">The instance of monobehaviour to call when the coroutine runs.</param>
        public IEnum(Func<T1, T2, T3, IEnumerator> enumerator, MonoBehaviour monoBehaviour)
        {
            Enumerator = enumerator;
            MonoBehaviour = monoBehaviour;
        }

        /// <summary>
        /// Sets the IsRunning property to true, waits for the enumerator before setting it to false again.
        /// </summary>
        public void RunCoroutine(T1 t1, T2 t2, T3 t3)
        {
            MonoBehaviour.StartCoroutine(Method(t1, t2, t3));
        }

        /// <summary>
        /// Starts the coroutine using the monobehaviour.
        /// </summary>
        /// <returns>The enumerator.</returns>
        private IEnumerator Method(T1 t1, T2 t2, T3 t3)
        {
            IsRunning = true;
            yield return Enumerator(t1, t2, t3);
            IsRunning = false;
        }

        /// <summary>
        /// Whether the coroutine is running or not.
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// The enumerator to run.
        /// </summary>
        private Func<T1, T2, T3, IEnumerator> Enumerator { get; set; }

        /// <summary>
        /// The instance of monobehaviour to call the enumerator.
        /// </summary>
        private MonoBehaviour MonoBehaviour { get; set; }
    }

    /// <summary>
    /// Datatype that stores an enumerator, and stores the current instance of whether the Coroutine is running.
    /// </summary>
    public class IEnum<T1, T2, T3, T4>
    {
        /// <summary>
        /// Stores the enumerator. Reminder that this is a reference type.
        /// </summary>
        /// <param name="enumerator">The enumerator function to store.</param>
        /// <param name="monoBehaviour">The instance of monobehaviour to call when the coroutine runs.</param>
        public IEnum(Func<T1, T2, T3, T4, IEnumerator> enumerator, MonoBehaviour monoBehaviour)
        {
            Enumerator = enumerator;
            MonoBehaviour = monoBehaviour;
        }

        /// <summary>
        /// Sets the IsRunning property to true, waits for the enumerator before setting it to false again.
        /// </summary>
        public void RunCoroutine(T1 t1, T2 t2, T3 t3, T4 t4)
        {
            MonoBehaviour.StartCoroutine(Method(t1, t2, t3, t4));
        }

        /// <summary>
        /// Starts the coroutine using the monobehaviour.
        /// </summary>
        /// <returns>The enumerator.</returns>
        private IEnumerator Method(T1 t1, T2 t2, T3 t3, T4 t4)
        {
            IsRunning = true;
            yield return Enumerator(t1, t2, t3, t4);
            IsRunning = false;
        }

        /// <summary>
        /// Whether the coroutine is running or not.
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// The enumerator to run.
        /// </summary>
        private Func<T1, T2, T3, T4, IEnumerator> Enumerator { get; set; }

        /// <summary>
        /// The instance of monobehaviour to call the enumerator.
        /// </summary>
        private MonoBehaviour MonoBehaviour { get; set; }
    }
}
