using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 public class EnemyController : MonoBehaviour
{
//      private Animator animator;
//     private bool isDead = false;
//     public int maxHealth = 5; // Salud máxima
//     private int currentHealth; // Salud actual
//     public float speed = 2f; // Velocidad de movimiento
//     public Transform player; // Referencia al superhéroe
//     public float attackRange = 1.5f; // Distancia para atacar

//     void Start()
//     {
//         animator = GetComponent<Animator>();
//         currentHealth = maxHealth; // Inicializa la salud actual
//     }

//     void Update()
//     {
//         if (isDead) return;

//         FollowPlayer();

//         // Condición para atacar cuando está cerca del jugador
//         if (Vector3.Distance(transform.position, player.position) < attackRange)
//         {
//             Attack();
//         }
//         else
//         {
//             Walk();
//         }
//     }

//     void FollowPlayer()
//     {
//         // Mueve al enemigo hacia el jugador
//         Vector3 direction = (player.position - transform.position).normalized;
//         transform.position += direction * speed * Time.deltaTime;
//     }

//     void Walk()
//     {
//         animator.SetBool("isWalking", true);
//         animator.SetBool("isAttacking", false);
//         animator.SetBool("isGettingHit", false);
//     }

//     void Attack()
//     {
//         animator.SetBool("isWalking", false);
//         animator.SetBool("isAttacking", true);
        
//         // Aquí puedes agregar lógica para dañar al superhéroe si está en rango
//         if (Vector3.Distance(transform.position, player.position) < attackRange)
//         {
//             // Supongamos que tienes un script en el superhéroe llamado SuperHeroController
//             // SuperHeroController hero = player.GetComponent<SuperHeroController>();
//             if (hero != null)
//             {
//                 // hero.TakeDamage(1); // Daño fijo, puedes cambiarlo si lo deseas
//             }
//         }
//     }

//     public void TakeDamage(int damage)
//     {
//         currentHealth -= damage; // Resta vida al enemigo

//         if (currentHealth <= 0)
//         {
//             Die();
//         }
//         else
//         {
//             GetHit();
//         }
//     }

//     void GetHit()
//     {
//         animator.SetBool("isGettingHit", true);
//         Invoke("ResetGettingHit", 1f); // Espera 1 segundo antes de volver a Idle
//     }

//     void ResetGettingHit()
//     {
//         animator.SetBool("isGettingHit", false);
//         Idle();
//     }

//     void Idle()
//     {
//         animator.SetBool("isWalking", false);
//         animator.SetBool("isAttacking", false);
//     }

//     void Die()
//     {
//         isDead = true;
//         animator.SetBool("isDead", true);
        
//         // Aquí puedes desactivar el objeto después de un tiempo o al final de la animación
//         Destroy(gameObject, 2f); // Destruye después de 2 segundos
//     }
}
