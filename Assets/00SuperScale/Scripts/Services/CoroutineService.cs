﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SuperScale.Services
{
    public class CoroutineService : Service
    {
        private readonly MonoBehaviour _runner;

        public CoroutineService(MonoBehaviour runner)
        {
            _runner = runner;
        }

        public void StartCoroutine(IEnumerator routine)
        {
            _runner.StartCoroutine(routine);
        }

        public void StopCoroutine(IEnumerator routine)
        {
            if(routine != null)
            {
                _runner.StopCoroutine(routine);
            }
        }

        public void StopAllCoroutines()
        {
            StopAllCoroutines();
        }

        public override void Dispose()
        {
            ServiceRegistry.Unregister<CoroutineService>();
        }
    }
}
