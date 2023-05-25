using System;
using System.Collections.Generic;
using UnityEngine;

namespace SuperScale.UI.Transitions
{
    public interface ITransition
    {
        float Duration { get; }
        ITransition Start();
        ITransition Stop();
        ITransition Reset();
        ITransition Reverse();
        ITransition OnComplete(Action onComplete);
    } 
}
