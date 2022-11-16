using inSession;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;

public class TigerLookAt : MonoBehaviour
{
    [SerializeField] List<AimConstraint> aimConstraint;
    [SerializeField] GameObject targetObject;
    PlayerHealth aliveStatus;

    [SerializeField]
    [Range(0f, 180f)] float rangeAngle;

    bool isViewing;
    private void Start()
    {
        aliveStatus = GetComponent<PlayerHealth>();
        foreach(var constraint in aimConstraint)
        {
            constraint.constraintActive = false;
        }
        
    }

    private void Update()
    {

        if (isViewing)
        {
            //aimConstraint.weight = Mathf.Lerp(0f, 1f, 0.1f);
            Vector3 targetDir = targetObject.transform.position - transform.position;
            float _angle = Vector3.Angle(targetDir, transform.forward);

            if (_angle >= rangeAngle || _angle <= rangeAngle * -1) //Within this angles object cant view
            {
                //aimConstraint.weight = 0;
                foreach (var constraint in aimConstraint)
                {
                    constraint.constraintActive = false;
                }
            }
            else
            {
                //aimConstraint.weight = 1;
                foreach(var constraint in aimConstraint)
                {
                    constraint.constraintActive = true;
                }
            }
        }
        if(aliveStatus.isAlive == false)
        {
            isViewing = false;
            foreach (var constraint in aimConstraint)
            {
                constraint.constraintActive = false;
            }
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag == "LookAt")
        {
            isViewing = true;
            targetObject.transform.position = other.gameObject.transform.position;
            //aimConstraint.weight = 1;
            foreach (var constraint in aimConstraint)
            {
                constraint.constraintActive = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.tag == "LookAt")
        {
            //aimConstraint.weight = 0;
            foreach (var constraint in aimConstraint)
            {
                constraint.constraintActive = false;
            }
            isViewing = false;
        }
    }
}
