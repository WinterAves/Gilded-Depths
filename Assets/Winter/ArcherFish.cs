using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Winter.FishAI
{
    public class ArcherFish : Fish
    {
        protected override void OnStart()
        {
            agent.updateRotation = false;
        }

        protected override void OnChase()
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.red;
            agent.SetDestination(refPlayer.position);
        }

        protected override void OnAttack()
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }

        protected override void OnIdle()
        {
            Move(movementType, 2f, 0.001f, 5f);
        }
    }
}


