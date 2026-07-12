using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace ShadowWeaver.Core
{
    public static class GameEvents
    {
        public static event Action<bool> OnWorldSwitched;
        public static void RaiseWorldSwitched(bool isMirrorWorld) => OnWorldSwitched?.Invoke(isMirrorWorld);

        public static event Action OnPlayerDied;
        public static void RaisePlayerDied() => OnPlayerDied?.Invoke();

        public static event Action<string> OnCheckpointReached;
        public static void RaiseCheckpointReached(string checkpointId) => OnCheckpointReached?.Invoke(checkpointId);

        public static event Action<string> OnPuzzleSolved;
        public static void RaisePuzzleSolved(string puzzleId) => OnPuzzleSolved?.Invoke(puzzleId);

        public static event Action OnLevelCompleted;
        public static void RaiseLevelCompleted() => OnLevelCompleted?.Invoke();

        public static event Action OnShadowSpotted;
        public static void RaiseShadowSpotted() => OnShadowSpotted?.Invoke();

        public static event Action<bool> OnPauseStateChanged;
        public static void RaisePauseStateChanged(bool isPaused) => OnPauseStateChanged?.Invoke(isPaused);
    }
}