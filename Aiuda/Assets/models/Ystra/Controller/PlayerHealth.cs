using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace inSession
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] float playerHealth = 100;
        [SerializeField] Animator anim;
        [SerializeField] float damageTaken;
        [SerializeField] float healReciben;
        [SerializeField] bool isAlive = true;
        [SerializeField] float limpSpeed;
        private float normalspeed;

        private CharacterMovement movement;

        private void Start()
        {           
            movement = GetComponent<CharacterMovement>();
            normalspeed = movement.movementSpeed;
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
            else if (collision.gameObject.tag == "Health")
            {
                Heal();
            }
        }
    }
}

