using Photon.Pun;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class AIScript : MonoBehaviourPunCallbacks, IDamageable
{
    
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
    public bool playerInSight;

    // items that can be held by the bot
    [SerializeField] Item[] items;
    int itemIndex;
    int previousItemIndex = -1;

    PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        EquipItem(0);
        Invoke(nameof(SetPlayer), 3);
    }

    private void Update()
    {
        foreach(PlayerMovement p in FindObjectsOfType<PlayerMovement>()) //Designate nearest target
        {
            float minDistance = float.MaxValue;
            if(Vector3.Distance(transform.position, p.transform.position) < minDistance)
            {
                //Debug.Log("Selected Target");
                player = p.transform;
            }
        }

      playerInSightRange = Physics.CheckSphere(transform.position, sightRange, PlayerSensor);
      playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, PlayerSensor);

        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            if (hit.transform.GetComponentInParent<PlayerMovement>() != null) {
                playerInSight = true;
            }
            else {
                playerInSight = false;
            }

            //Debug.Log(hit.transform.name + " AHHHHHHHHHHHH it sees that object");
        }
        
      if (playerInSightRange && playerInAttackRange)
      {
            AttackMode();
      }
      
        if (!playerInSightRange && !playerInAttackRange)
        {
            PatrolMode();
        }

        if (playerInSightRange && !playerInAttackRange)
        {
            ChaseMode();
        }
    }   
 
    private void AttackMode()
    {
        //Debug.Log("Entering Attack Mode");
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        //for fully auto guns
        items[itemIndex].HoldDown();//add it back after merge...

        if (playerInSight) { // if AI is staring at PlayerController
            if (!alreadyAttacked) { //for single shot
                items[itemIndex].Use();
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), attackCooldown);
            }
        } else { // There's something in the way
            if (player != null)
                agent.SetDestination(player.position); // Go find player
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

    private void PatrolMode()
    {
        //Debug.Log("Entering Patrol Mode");
        if (walkpointSet != true)
        {
            SearchWalkpoint();
        }
        else
            agent.SetDestination(walkpoint);
   
        Vector3 distToWalkpoint = transform.position - walkpoint;
        if(distToWalkpoint.magnitude < 1f)
        {
             walkpointSet = false;
        }
        //after a certain amount of time in patrol mode, change weapon
    }
   
    private void ChaseMode()
    {
        //Debug.Log("Entering Chase Mode");
        agent.SetDestination(player.position);
        //if bot is in chase mode after a certain amount of time{
        //change weapon to longer range weapon if not equipped already}
    }  
 
    private void ResetAttack()
    {
        alreadyAttacked = false;
    } 
  
    private void SearchWalkpoint()
    {
        // Debug.Log("Searching for walkpoint");
        float randomX = Random.Range(-walkpointRange, walkpointRange);
        float randomZ = Random.Range(-walkpointRange, walkpointRange);
        
        walkpoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        
        //checks if walkpoint is on map
        if (Physics.Raycast(walkpoint, -transform.up, 2f, GroundSensor))
        {
            walkpointSet = true;
        }
        //Debug.Log("walkpoint is " + walkpoint.ToString());
    }
  
    private void killEnemy()
    {
        Destroy(gameObject);
    }
  
    //uncomment to make sightRange & attackRange visible in-game
    private void makeSightRangesVisible() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    private void SetPlayer()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
    }

    public void TakeDamage(float damage, Component source)
    {
        PV.RPC("RPC_TakeDamage", RpcTarget.All, damage);
    }

    [PunRPC]
    void RPC_TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            if (PV.IsMine)
            {
                // destroy the game object taking damage (kill the AI player)...
                GameManager.Instance.DestroyAI(gameObject);
            }
        }
    }
}
