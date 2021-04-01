using UnityEngine;
using UnityEngine.AI;

public class AIScript : MonoBehaviour{
  public NavMeshAgent agent; //reference to agent
  public Transform player;
  public LayerMask GroundSensor;
  public LayerMask PlayerSensor;
    public LayerMask WallSensor;
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

    // items that can be held by the bot
    [SerializeField] Item[] items;
    int itemIndex;
    int previousItemIndex = -1;
    private void Awake(){
      player = GameObject.Find("PlayerController").transform;
      //agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
      EquipItem(0);
    }
    private void Update(){
        //playerInSightRange = Physics.CheckSphere(transform.position, sightRange, PlayerSensor);
        //playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, PlayerSensor);

        playerInSightRange = !Physics.Linecast(transform.position, player.position,WallSensor)&&Physics.CheckSphere(transform.position, sightRange, PlayerSensor);

        playerInAttackRange = !Physics.Linecast(transform.position, player.position, WallSensor) &&Physics.CheckSphere(transform.position, attackRange, PlayerSensor);

        if (playerInSightRange && playerInAttackRange){
            AttackMode();
      }
      /*
      if (!playerInSightRange && !playerInAttackRange){
        PatrolMode();
      }
      if (playerInSightRange && !playerInAttackRange){
        ChaseMode();
      }
      */
    }   
 
    private void AttackMode(){
        //agent.SetDestination(transform.position);
        transform.LookAt(player);

        //for fully auto guns
        //items[itemIndex].HoldDown();//add it back after merge...

        //for single shot
        if (!alreadyAttacked)
        {
            items[itemIndex].Use();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), attackCooldown);
        }
    }
    void EquipItem(int index)
    {
        if (index == previousItemIndex)
            return;

        itemIndex = index;

        items[itemIndex].itemGameObject.SetActive(true);

        if (previousItemIndex != -1)
        {
            items[previousItemIndex].itemGameObject.SetActive(false);
        }

        previousItemIndex = itemIndex;
        /*
        // I leave it here in case you will need it
        if (PV.IsMine)
        {
            hash = PhotonNetwork.LocalPlayer.CustomProperties;
            hash.Remove("itemIndex");
            hash.Add("itemIndex", itemIndex);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
        */
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
       // Wait(0.5f);
        killEnemy();
      }
   }
  
  private void killEnemy(){
      Destroy(gameObject);
    }
  
  //uncomment to make sightRange & attackRange visible in-game
  private void makeSightRangesVisible() {
  Gizmos.color = Color.red;
  Gizmos.DrawWireSphere(transform.position, attackRange);
  Gizmos.color = Color.yellow;
  Gizmos.DrawWireSphere(transform.position, sightRange);
  }
  
}
