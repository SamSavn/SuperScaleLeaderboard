using System;
using System.Collections.Generic;

namespace SuperScale.Utils
{
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
