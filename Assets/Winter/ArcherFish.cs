using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Winter.FishAI
{
    public class ArcherFish : Fish
    {
        public float currentDepth;

        public GameObject bulletPrefab;

        protected override void OnStart()
        {
            agent.updateRotation = false;
        }

        protected override void OnChase()
        {
            agent.isStopped = false;
            agent.enabled = true;
            UpdateGraphicsRotation();

            agent.speed = speed;
            agent.SetDestination(refPlayer.position);
        }

        protected override void OnAttack()
        {
            agent.updatePosition = false;
            var dir = refPlayer.position - transform.position;

            if(dir != Vector3.zero)
            {
                var rot = Quaternion.Lerp(graphics.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed * Time.deltaTime);
                graphics.rotation = rot;
            }
            
        }

        protected override void OnAttackFixed()
        {
            var dir = refPlayer.position - transform.position; 
            dir.Normalize();
           
            rb.velocity = new Vector2(dir.x, dir.y) * 8f * Mathf.Sin(10f * Time.time);
        }

        protected override void OnIdle()
        {
            agent.isStopped = false;
            agent.enabled = true;
            agent.updatePosition = true;
            Move(movementType, 2f, 0.001f, 5f);
        }
    }
}


