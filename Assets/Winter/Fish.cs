using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Winter.FishAI
{
    public class Fish : MonoBehaviour
    {
        public enum State { Idle, Chase, Attack }
        public State state;

        public Rigidbody2D rb;

        public float speed;

        public float minAggroDistance;  //For the first time to get noticed by the fish
        public float maxAggroDistance; // To escape the distance between fish and submarine
        public float minAttackDistance;


        public Transform refPlayer;

        public enum MovementType { Sin, RSin, StraightHorizontal, StraightVertical, Stationary}

        public NavMeshAgent agent;

        public float patrolDistance;

        private int patrolDirSwitch = 1;

        public MovementType movementType = MovementType.StraightHorizontal;

        public Vector3 currentDestination;

        private float timer = 0f;

        private float currentNoise;

        public LayerMask fishMask;

        public Transform graphics;

        private Vector3 prevPosition;

        private bool isChasing;

        private float currentAgroAmt;

        public float rotationSpeed;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            state = State.Idle;
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            currentAgroAmt = minAggroDistance;
            prevPosition = transform.position;
            OnStart();
        }


        // Update is called once per frame
        void Update()
        {
            float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(refPlayer.position.x, refPlayer.position.y));
            bool canAttack = distance < minAttackDistance;
            //bool canChase = distance > minAggroDistance && distance < maxAggroDistance && !canAttack;
            isChasing = state == State.Chase ? true : false;
            bool canChase = distance < currentAgroAmt && !canAttack;
           
            if(Mathf.Approximately(currentAgroAmt, minAggroDistance) && state == State.Chase)
            {
                currentAgroAmt = maxAggroDistance;
            }

            if(state != State.Attack)
            {
                agent.enabled = true;
                agent.updatePosition = true;
            }

            if(state != State.Chase)
            {
                currentAgroAmt = minAggroDistance;
            }

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

            //Facing dir of motion

            if (agent.hasPath)
            {
                var dir = agent.nextPosition - transform.position;
                transform.up = dir;
            }
             
        }

        private void FixedUpdate()
        {
            

            if (state != State.Attack)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            }
            else
            {
                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }

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


        public void UpdateGraphicsRotation()
        {
            var dir = transform.position - prevPosition;
            //var rot = Quaternion.LookRotation(dir);

            if(dir != Vector3.zero)
            {
                var rot = Quaternion.Lerp(graphics.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed * Time.deltaTime);
                graphics.rotation = rot;
            }
            
        }

        protected void Move(MovementType mType, float amplitude = 0f, float omega = 0f, float noise =0f)
        {
            agent.speed = speed;

            //ROtate graphics based on dir of motion

            UpdateGraphicsRotation();
            
            

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
            if (!agent.hasPath || Mathf.Approximately(agent.remainingDistance, agent.stoppingDistance))
            {
                patrolDirSwitch *= -1;
                var destination = transform.position + Vector3.right * patrolDistance * patrolDirSwitch;
                agent.SetDestination(destination);
            }
            
        }

        private void StraightVerticalMove()
        {
            if (!agent.hasPath || Mathf.Approximately(agent.remainingDistance, agent.stoppingDistance))
            {
                patrolDirSwitch *= -1;
                var destination = transform.position + Vector3.up * patrolDistance * patrolDirSwitch;
                agent.SetDestination(destination);
            }

        }

        private void RSinMove(float ampl, float omega, float noise)
        {
            if (!agent.hasPath || Mathf.Approximately(agent.remainingDistance, agent.stoppingDistance))
            {
                patrolDirSwitch *= -1;
                currentDestination = transform.position + Vector3.right * patrolDistance * patrolDirSwitch;
                agent.SetDestination(currentDestination);
            }

            else
            {
                //Debug.Log("Improving");

                timer += Time.deltaTime;

                if(timer> 0.5f)
                {
                    currentNoise = UnityEngine.Random.Range(-noise, noise);
                    timer = 0f;

                }


                var destination = currentDestination + new Vector3(0, Mathf.Sin(omega * Time.time) * ampl - currentNoise , 0);
                
                agent.SetDestination(destination);
            }
        }


        private void SinMove(float ampl, float omega)
        {

            if (!agent.hasPath || Mathf.Approximately(agent.remainingDistance, agent.stoppingDistance))
            {
                patrolDirSwitch *= -1;
                currentDestination = transform.position + Vector3.right * patrolDistance * patrolDirSwitch;
                agent.SetDestination(currentDestination);
            }

            else
            {
                Debug.Log("Improving");
                var destination = currentDestination + new Vector3(0, Mathf.Sin(omega * Time.time) * ampl, 0);
                agent.SetDestination(destination);
            }
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

        private void LateUpdate()
        {
            if(transform.position != prevPosition)
                prevPosition = transform.position;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.position, minAggroDistance);

            Gizmos.color = Color.green;

            Gizmos.DrawWireSphere(transform.position, maxAggroDistance);
        }
    }
}


