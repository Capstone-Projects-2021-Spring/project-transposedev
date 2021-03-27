using UnityEngine;
using UnityEngine.AI;

public class AIScript : MonoBehavior{
  public NavMeshAgent agent; //reference to agent
  public Transform player;
  public LayerMask GroundSensor;
  public LayerMask PlayerSensor;
  
  public Vector3 walkpoint;
  bool walkpointSet;
  public float walkpointRange;
  
  public float attackCooldown;
  bool alreadyAttacked;
  
  public float sightRange;
  public float attackRange;
  public bool playerInSightRange;
  public bool playerInAttackRange;
  
  private void Awake(){
      player = GameObject.Find("PlayerObj").transform;
      agent = GetComponent<NavMeshAgent>();
    }
    
 private void Update(){
      playerInSightRange = Physics.CheckSphere(transform.position, sightRange, PlayerSensor);
      playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, PlayerSensor);
      if (playerInSightRange && playerInAttackRange){
        AttackMode();
      }
      if (!playerInSightRange && !playerInAttackRange){
        PatrolMode();
      }
      if (playerInSightRange && !playerInAttackRange){
        ChaseMode();
      }
    }   
 
 private void AttackMode(){
    }
 
 private void PatrolMode(){
    }
   
 private void ChaseMode(){
    }  
}
