using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public interface IInputListener
{
    Action<InputAction.CallbackContext>[] ListenerFunctions { get;}
    
}
