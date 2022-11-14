using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ParentConstrainWeigthManager : MonoBehaviour
{
    [Range(0,1)] public float weight;
    [SerializeField] private MultiParentConstraint[] targetChain;

    public void SetConstrainsWeights()
    {
        if (targetChain == null || targetChain.Length <= 0) return;

        for (int i = 0; i < targetChain.Length; i++)
        {
            targetChain[i].weight = weight;
        }
    }

    private void OnValidate()
    {
        SetConstrainsWeights();
    }
    public void ChangeWeights()
    {
        SetConstrainsWeights();
    }
}
