using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MimicSpace
{
    public class Movement : MonoBehaviour
    {
        [Header("Controls")]
        [Tooltip("Body Height from ground")]
        [Range(0.5f, 5f)]
        public float height = 0.8f;
        public float speed = 5f;
        Vector3 velocity = Vector3.zero;
        public float velocityLerpCoef = 4f;
        Mimic myMimic;

        [Header("Enemy Behavior")]
        public Transform player;
        public float detectionRange = 10f;
        public float attackRange = 2f;
        public float attackCooldown = 1f;
        private bool isAttacking = false;

        private void Start()
        {
            myMimic = GetComponent<Mimic>();
        }

        void Update()
        {
            if (player != null)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, player.position);

                if (distanceToPlayer <= detectionRange)
                {
                    // Move towards player
                    Vector3 directionToPlayer = (player.position - transform.position).normalized;
                    velocity = Vector3.Lerp(velocity, directionToPlayer * speed, velocityLerpCoef * Time.deltaTime);

                    // Check if within attack range
                    if (distanceToPlayer <= attackRange)
                    {
                        if (!isAttacking)
                        {
                            StartCoroutine(Attack());
                        }
                    }
                }
                else
                {
                    // Stop moving if player is out of detection range
                    velocity = Vector3.zero;
                }
            }
            else
            {
                // Player is not set, idle
                velocity = Vector3.zero;
            }

            // Assigning velocity to the mimic to assure great leg placement
            myMimic.velocity = velocity;

            transform.position = transform.position + velocity * Time.deltaTime;
            RaycastHit hit;
            Vector3 destHeight = transform.position;
            if (Physics.Raycast(transform.position + Vector3.up * 5f, -Vector3.up, out hit))
                destHeight = new Vector3(transform.position.x, hit.point.y + height, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, destHeight, velocityLerpCoef * Time.deltaTime);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerStats playerStats = other.GetComponent<PlayerStats>();
                if (playerStats != null)
                {
                    playerStats.TakeDamage(10); // Ajusta el daño según lo que desees
                }
            }
        }

        IEnumerator Attack()
        {
            isAttacking = true;
            myMimic.Attack(); // Call the attack method on the Mimic
            yield return new WaitForSeconds(attackCooldown);
            isAttacking = false;
        }
    }
}
