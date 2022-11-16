using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace inSession
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] float playerHealth = 100;
        [SerializeField] Animator anim;
        [SerializeField] float damageTaken;
        [SerializeField] float healReciben;
        [SerializeField] public bool isAlive = true;
        [SerializeField] float limpSpeed;
        [SerializeField] Slider healthBar;
        [SerializeField] Canvas pushText;
        private float normalspeed;
        private IReach reach;
        //private ReachAnim reach;
        [SerializeField] int characterNum;

        private CharacterMovement movement;

        private void Start()
        {           
            movement = GetComponent<CharacterMovement>();
            if (characterNum == 0) reach = GetComponent<ReachAnim>();
            else if (characterNum == 1) reach = GetComponent<ReachParentWeight>();
            else if (characterNum == 3) reach = GetComponent<IKSwitcherReach>();
            normalspeed = movement.movementSpeed;            
            healthBar = FindObjectOfType<Slider>();
            healthBar.value = playerHealth;
        }
        private void Update()
        {
           
            ///estar chequeando y seteando la vid del jugador cada frame es una muy mala
            ///practica, pero yo soy de anim y no de videojuegos, asi que dejare que el teso
            ///de santi corrija esta atrocidad despues.
            if (playerHealth > 0 && !isAlive) 
            {
                isAlive = true;
                anim.SetBool("alive", true);
                SetLimp();
                
            }
        }
        private void SetLimp()
        {
            if (playerHealth <= 20)
            {
                anim.SetBool("limp", true);
                movement.movementSpeed = limpSpeed;
            }
            else if (playerHealth >= 20)
            {
                anim.SetBool("limp", false);
                movement.movementSpeed = normalspeed;
            }
        }

        private void Harm()
        {
            playerHealth = playerHealth - damageTaken;
            if (playerHealth < 0) playerHealth = 0;
            healthBar.value = playerHealth;
            anim.SetTrigger("harm");
            if (playerHealth <= 0)
            {
                Die();
                return;
            }
            Debug.Log(gameObject.name + " current health is:" + playerHealth);
            SetLimp();
            
        }
        private void Heal()
        {
            playerHealth = playerHealth + healReciben;
            if (playerHealth > 100) playerHealth = 100;
            healthBar.value = playerHealth;
            Debug.Log(gameObject.name + " current health is:" + playerHealth);
            SetLimp();
        }
        public void Die()
        {
            isAlive = false;
            anim.SetBool("alive", false);
            anim.SetTrigger("die");
            movement.movementSpeed = 0;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Harm();
            }
            
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Health")
            {
                pushText = other.gameObject.GetComponentInChildren<Canvas>();
                if(pushText.enabled == false) pushText.enabled = true;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    reach.healthPosition = other.gameObject.transform.position;
                    reach.grab = true;
                    Heal();
                    Destroy(other.gameObject);
                }               
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Health")
            {
                pushText.enabled = false;
                pushText = null;
            }
        }
    }
}

