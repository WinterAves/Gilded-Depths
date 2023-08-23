using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.AI;

namespace Winter.FishAI
{
    public class Fish : MonoBehaviour
    {
        public enum State { Idle, Chase, Attack }
        public State state;

        public Rigidbody rb;

        public float speed;

        public float minAggroDistance;  //For the first time to get noticed by the fish
        public float maxAggroDistance; // To escape the distance between fish and submarine
        public float minAttackDistance;


        public Transform refPlayer;

        public enum MovementType { Sin, RSin, StraightHorizontal, StraightVertical, Stationary}

        public NavMeshAgent agent;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            state = State.Idle;
            agent = GetComponent<NavMeshAgent>();
            OnStart();
        }


        // Update is called once per frame
        void Update()
        {
            float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(refPlayer.position.x, refPlayer.position.y));
            bool canAttack = distance < minAttackDistance;
            bool canChase = distance > minAggroDistance && distance < maxAggroDistance && !canAttack;

            if (!canChase && !canAttack)
                state = State.Idle;
            else if (canChase)
                state = State.Chase;
            else
                state = State.Attack;

            switch (state)
            {
                case State.Idle:
                    OnIdle();
                    break;
                case State.Chase:
                    OnChase();
                    break;
                case State.Attack:
                    OnAttack();
                    break;
            }
        }

        private void FixedUpdate()
        {
            switch (state)
            {
                case State.Idle:
                    OnIdleFixed();
                    break;
                case State.Chase:
                    OnChaseFixed();
                    break;
                case State.Attack:
                    OnAttackFixed();
                    break;
            }
        }

        protected void Move(MovementType mType, float amplitude = 0f, float omega = 0f, float noise =0f)
        {
            switch (mType)
            {
                case MovementType.Stationary:
                    StandStill();
                    break;
                case MovementType.Sin:
                    SinMove(amplitude, omega);
                    break;
                case MovementType.RSin:
                    RSinMove(amplitude, omega, noise);
                    break;
                case MovementType.StraightVertical:
                    StraightVerticalMove();
                    break;
                case MovementType.StraightHorizontal:
                    StraightHorizontalMove();
                    break;
            }
        }

        private void StraightHorizontalMove()
        {
            
        }

        private void StraightVerticalMove()
        {
            
        }

        private void RSinMove(float ampl, float omega, float noise)
        {
            
        }

        private void SinMove(float ampl, float omega)
        {
            
        }

        private void StandStill()
        {
            
        }

        protected virtual void OnAttack()
        {

        }

        protected virtual void OnChase()
        {

        }

        protected virtual void OnIdle()
        {

        }
        protected virtual void OnAttackFixed()
        {

        }

        protected virtual void OnChaseFixed()
        {

        }

        protected virtual void OnIdleFixed()
        {

        }

        protected virtual void OnStart()
        {

        }

    }
}


