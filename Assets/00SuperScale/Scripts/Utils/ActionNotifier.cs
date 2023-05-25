using System;
using System.Collections.Generic;

namespace SuperScale.Utils
{
    /// <summary>
    /// Can be used to execute methods when there is a change of state
    /// </summary>
    public class ActionNotifier
    {
        private readonly List<Action> _subcribers;

        public ActionNotifier()
        {
            _subcribers = new List<Action>();
        }

        public void Subscribe(Action action)
        {
            if(action == null)
            {
                return;
            }

            _subcribers.Add(action);
        }

        public void Notify()
        {
            for (int i = 0; i < _subcribers.Count; i++)
            {
                _subcribers[i]?.Invoke();
            }

            Clear();
        }

        public void Clear()
        {
            _subcribers.Clear();
        }
    }
}
