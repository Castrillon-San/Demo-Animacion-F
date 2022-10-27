using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

namespace inSession
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerController : MonoBehaviour
    {
        private const string DEFAULT_PLAYER_RESOURCES_PATH = "PlayerController";
        private static PlayerController mainPlayer;

        private PlayerInput inputHandler;

        public static PlayerController MainPlayer
        {
            get
            {
                if(mainPlayer == null)
                {
                    mainPlayer = SpawnPlayer();
                    DontDestroyOnLoad(mainPlayer);
                }
                return mainPlayer;
            }
        }

        private static PlayerController SpawnPlayer()
        {
            PlayerController prefab = Resources.Load<PlayerController>(DEFAULT_PLAYER_RESOURCES_PATH);
            if(prefab == null)
            {
                throw new NullReferenceException($"There is not default player at path. {DEFAULT_PLAYER_RESOURCES_PATH}");
            }
            return Instantiate(prefab);
        }

        private void Awake()
        {
            if(mainPlayer == null)
            {
                mainPlayer = this;
                DontDestroyOnLoad(this);
            }
            else if( mainPlayer != this)
            {
                Destroy(gameObject);
            }

            inputHandler = GetComponent<PlayerInput>();
        }

        public void ControlActor(Actor controlledActor)
        {
            controlledActor.BindInputActions(inputHandler);
        }
    }
}

