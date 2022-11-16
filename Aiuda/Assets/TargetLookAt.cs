using inSession;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;

public class TargetLookAt : MonoBehaviour
{
    [SerializeField] protected List<MultiAimConstraint> aimConstraintList;
    [SerializeField] Transform targetObject = null;
    [SerializeField] private RigBuilder rigbuilder;
    PlayerHealth aliveStatus;

    [SerializeField]
    [Range(0f, 180f)] float rangeAngle;
    
    bool isViewing;
    bool isDeath;
    private void Start()
    {
        //rigbuilder = GetComponent<RigBuilder>();
    }

    private void Update()
    {
        if (isViewing)
        {
            foreach (var constraint in aimConstraintList)
            {
                constraint.weight = Mathf.Lerp(0f, 1f, 0.1f);
            }
            Vector3 targetDir = targetObject.position - transform.position;
            float _angle = Vector3.Angle(targetDir, transform.forward);

            if(_angle >= rangeAngle || _angle <= (rangeAngle * -1)) //Within this angles object cant view
            {
                foreach (var constraint in aimConstraintList)
                {
                    constraint.weight = 0;
                }
            }
            else
            {
                foreach (var constraint in aimConstraintList)
                {
                    constraint.weight = 1;
                }
            }
        }
        if (aliveStatus.isAlive == false)
        {
            isViewing = false;
            foreach (var constraint in aimConstraintList)
            {
                constraint.weight = 0;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag == "LookAt")
        {
            isViewing = true;
            foreach (var constraint in aimConstraintList)
            {
                var data = constraint.data.sourceObjects;
                data.Clear();
                targetObject = other.gameObject.transform;
                data.Add(new WeightedTransform(targetObject, 1));
                constraint.data.sourceObjects = data;
            }
            rigbuilder.Build();
            //aimConstraint.weight = 1;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.transform.tag == "LookAt")
        {
            foreach (var constraint in aimConstraintList)
            {
                constraint.weight = 0;
            }
            targetObject = null;
            isViewing = false;
        }
    }


}
