using System.Collections.Generic;
using UnityEngine;

public enum BrainWaveType { None = -1, Alpha, Beta, Setha, Delta, Count }

public class BrainWaveInput : MonoBehaviour
{
    Dictionary<BrainWaveType, int> brainWaveValue = new Dictionary<BrainWaveType, int>();

    void Awake()
    {
        InitializeBrainWaveValue();
    }

    void InitializeBrainWaveValue()
    {
        brainWaveValue.Clear();
        for (int index = 0; index < (int)BrainWaveType.Count; index++)
        {
            brainWaveValue.Add((BrainWaveType)index, 0);
        }
    }
}
