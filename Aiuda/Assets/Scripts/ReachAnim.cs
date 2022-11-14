using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ReachAnim : MonoBehaviour
{
    #region --- helper ---
    private enum RigAnimMode
    {
        off,
        inc,
        dec
    }
    #endregion

    [SerializeField] private Rig iKChanger;
    [SerializeField] private Transform target;
    [SerializeField] private float grabSpeed = 5f;
    public Vector3 healthPosition;
    public bool grab = false;
    private RigAnimMode mode = RigAnimMode.off;
    
    void Start()
    {
        healthPosition = Vector3.zero;
        target.position = Vector3.zero;
    }

    void Update()
    {
        if(grab == true)
        {
            grab = false;
            iKChanger.weight = 0;
            mode = RigAnimMode.inc;
            target.position = healthPosition;           
        }
    }
    private void FixedUpdate()
    {
        switch (mode)
        {
            case RigAnimMode.inc:
                iKChanger.weight = Mathf.Lerp(iKChanger.weight, 1, grabSpeed * Time.deltaTime);
                if(iKChanger.weight > 0.95f)
                {
                    iKChanger.weight = 1;
                    mode = RigAnimMode.dec;
                }
                break;
            case RigAnimMode.dec:
                iKChanger.weight = Mathf.Lerp(iKChanger.weight, 0, grabSpeed * Time.deltaTime);
                if (iKChanger.weight < 0.1f)
                {
                    iKChanger.weight = 0;
                    mode = RigAnimMode.off;
                    target.localPosition = Vector3.zero;
                }
                break;
        }
    }
}
