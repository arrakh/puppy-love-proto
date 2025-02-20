﻿using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Utilities
{
    public static class IEnumeratorExtensions
    {
        public static IEnumerator WaitUntilCompleted<T>(this TaskCompletionSource<T> tcs)
            => new WaitUntil(() => tcs.Task.IsCompleted);
    }
}