using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SuperScale.Services
{
    /// <summary>
    /// Responsable for handling coroutine in non-monobehavior scripts
    /// </summary>
    public class CoroutineService : Service
    {
        private readonly MonoBehaviour _runner;

        public CoroutineService(MonoBehaviour runner)
        {
            _runner = runner;
        }

        /// <summary>
        /// Starts a IEnumerator
        /// </summary>
        /// <param name="routine">The IEnumerator to start</param>
        /// <returns>Coroutine</returns>
        public Coroutine StartCoroutine(IEnumerator routine)
        {
            return _runner.StartCoroutine(routine);
        }

        /// <summary>
        /// Stops a IEnumerator
        /// </summary>
        /// <param name="routine">The IEnumerator to stop</param>
        public void StopCoroutine(IEnumerator routine)
        {
            if(routine != null)
            {
                _runner.StopCoroutine(routine);
            }
        }

        /// <summary>
        /// Stops a Coroutine and sets it to null
        /// </summary>
        /// <param name="routine">The Coroutine to stop</param>
        public void StopCoroutine(ref Coroutine routine)
        {
            if (routine != null)
            {
                _runner.StopCoroutine(routine);
                routine = null;
            }
        }

        /// <summary>
        /// Stops all the coroutines
        /// </summary>
        public void StopAllCoroutines()
        {
            _runner.StopAllCoroutines();
        }

        public override void Dispose()
        {
            ServiceRegistry.Unregister<CoroutineService>();
        }
    }
}
