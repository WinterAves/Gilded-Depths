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
            base.OnStart();

            agent.updateRotation = false;
        }

        protected override void OnChase()
        {
            base.OnChase();
            GetComponentInChildren<SpriteRenderer>().color = Color.red;
            agent.SetDestination(refPlayer.position);
        }

        protected override void OnAttack()
        {
            base.OnAttack();

            GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }

        protected override void OnIdle()
        {
            base.OnIdle();

            GetComponentInChildren<SpriteRenderer>().color = Color.black;
        }
    }
}


