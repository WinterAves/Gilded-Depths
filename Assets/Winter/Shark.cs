using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Winter.FishAI
{
    public class Shark : Fish
    {

        public Animator animator;
        

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
            var dir = refPlayer.position - transform.position;

            if (dir != Vector3.zero)
            {
                var rot = Quaternion.Lerp(graphics.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed * Time.deltaTime);
                graphics.rotation = rot;
            }

            animator.SetBool("E", true);

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



