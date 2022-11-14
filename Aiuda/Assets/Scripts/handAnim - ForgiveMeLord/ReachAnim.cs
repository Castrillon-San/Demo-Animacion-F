using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ReachAnim : MonoBehaviour, IReach
{
    #region --- helper ---
    private enum RigAnimMode
    {
        off,
        inc,
        dec
    }
    #endregion

    [SerializeField] private Rig iKRigChanger;
    [SerializeField] private Transform target;
    [SerializeField] private float grabSpeed = 5f;
    public Vector3 healthPosition
    {
        get;
        set;
    }
    public bool grab
    {
        get;
        set;
    }
    private RigAnimMode mode = RigAnimMode.off;
    
    void Start()
    {
        grab = false;
        healthPosition = Vector3.zero;
        target.position = Vector3.zero;
    }

    void Update()
    {
        if(grab == true)
        {
            grab = false;
            iKRigChanger.weight = 0;
            mode = RigAnimMode.inc;
            target.position = healthPosition;           
        }
    }
    private void FixedUpdate()
    {
        switch (mode)
        {
            case RigAnimMode.inc:
                iKRigChanger.weight = Mathf.Lerp(iKRigChanger.weight, 1, grabSpeed * Time.deltaTime);
                if(iKRigChanger.weight > 0.95f)
                {
                    iKRigChanger.weight = 1;
                    mode = RigAnimMode.dec;
                }
                break;
            case RigAnimMode.dec:
                iKRigChanger.weight = Mathf.Lerp(iKRigChanger.weight, 0, grabSpeed * Time.deltaTime);
                if (iKRigChanger.weight < 0.1f)
                {
                    iKRigChanger.weight = 0;
                    mode = RigAnimMode.off;
                    target.localPosition = Vector3.zero;
                }
                break;
        }
    }
}
