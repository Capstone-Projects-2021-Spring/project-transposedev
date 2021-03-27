using UnityEngine;
using UnityEngine.AI;

public class AIScript : MonoBehavior{
  public NavMeshAgent agent; //reference to agent
  public Transform player;
  public LayerMask GroundSensor;
  public LayerMask PlayerSensor;
  public GameObject projectile;
  public float health;
  
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
      agent.SetDestination(transform.position);
      transform.LookAt(player);
      if(alreadyAttacked != true){
         Rigidbody R = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
         R.addForce(transform.forward*32f, ForceMode.Impulse);
         R.addForce(transform.up*8f, ForceMode.Impulse);
         alreadyAttacked = true;
         Invoke(nameOf(ResetAttack), attackCooldown);
      }
    }
 
 private void PatrolMode(){
      if (walkpointSet != true){
         SearchWalkpoint();
      }
      else
         agent.SetDestination(walkpoint);
   
      Vector3 distToWalkpoint = transform.position - walkpoint;
      if(distToWalkpoint.magnitude < 1f){
          walkpointSet = false;
      }
    }
   
 private void ChaseMode(){
      agent.SetDestination(player.position);
    }  
 
 private void ResetAttack(){
      alreadyAttacked = false;
    } 
  
 private void SearchWalkpoint(){
      float randomX = Random.Range(-walkpointRange, walkpointRange);
      float randomZ = Random.Range(-walkpointRange, walkpointRange);
      walkpoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
      if (Physics.Raycast(walkpoint, -transform.up, 2f, GroundSensor)){ //checks if walkpoint is on map
        walkpointSet = true;
      }
    }
  
  public void TakeDamage(int damage){
      health = health - damage;
      if (health <= 0){
        Invoke(nameOf(killEnemy), 0.5f);
      }
   }
  
  private void killEnemy(){
      Destroy(gameObject);
    }
  
  //uncomment to make sightRange & attackRange visible in-game
  //private void makeSightRangesVisible() {
  //Gizmos.color = Color.red;
  //Gizmos.DrawWireSphere(transform.position, attackRange);
  //Gizmos.color = Color.yellow;
  //Gizmos.DrawWireSphere(transform.position, sightRange);
  //}
  
}
