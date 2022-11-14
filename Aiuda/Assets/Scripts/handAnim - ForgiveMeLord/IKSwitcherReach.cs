using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class IKSwitcherReach : MonoBehaviour, IReach
{
    #region --- helper ---
    private enum RigAnimMode
    {
        off,
        inc,
        dec
    }
    #endregion

    ///Forgive Me Lord for I have sinned, esta es la mayor averracion que he hecho en mi vida
    ///lo unico que cambia en este codigo y el ReachAnim es la variable que esta inmediatamente abajo
    ///pero el tiempo y el hecho de que los 4 rigs IKs sean diferentes han forzado mi mano.
    [SerializeField] private IKSwitcher iKRigChanger;
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
        target.localPosition = Vector3.zero;
    }

    void Update()
    {
        if (grab == true)
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
                iKRigChanger.ChangeWeights();
                if (iKRigChanger.weight > 0.95f)
                {
                    iKRigChanger.weight = 1;
                    mode = RigAnimMode.dec;
                }
                break;
            case RigAnimMode.dec:
                iKRigChanger.weight = Mathf.Lerp(iKRigChanger.weight, 0, grabSpeed * Time.deltaTime);
                iKRigChanger.ChangeWeights();
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
