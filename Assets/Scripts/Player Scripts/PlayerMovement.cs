using System;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;

public class PlayerMovement : MonoBehaviourPunCallbacks, IDamageable {

    /*****************/
    /*   VARIABLES   */
    /*****************/

    public Transform playerCam;
    public Transform orientation;

    // items that can be held by the player
    [SerializeField] Item[] items;
    int itemIndex;
    int previousItemIndex = -1;
    
    private Rigidbody rb;

    // rotation and looking
    private float xRotation;
    private float sensitivity = 50f;
    private float sensMultiplier = 1f;
    
    // movement
    public float moveSpeed = 4500;
    public float maxSpeed = 20;
    public bool grounded;
    public LayerMask whatIsGround;
    public float counterMovement = 0.175f;
    private float threshold = 0.01f;
    public float maxSlopeAngle = 90f;
    private Vector3 playerScale;
    private Vector3 normalVector = Vector3.up;
    private Vector3 wallNormalVector;

    // jumping
    private bool readyToJump = true;
    private float jumpCooldown = 0.25f;
    public float jumpForce = 550f;
    
    // input
    float x, y;
    bool jumping, sprinting;

    [SerializeField] Menu escMenu;

    PhotonView PV;

    PlayerManager playerManager;

    // countainer for accessing custom properties
    Hashtable hash;

    public GameObject projectile;

    public float projectileSpeed = 5;

    private LineRenderer lr;

    /* ----------------------------------------------------------------------------------------------------------------- */

    /***************/
    /*   METHODS   */
    /***************/

    void Awake() {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
        playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
    }
    
    void Start() {
		if (PV.IsMine)
		{
            EquipItem(0);
		} else
		{
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rb);
        }

        playerScale = transform.localScale;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate() {
        if (!PV.IsMine)
            return;

        Movement();
    }

    private void Update() {
        if (!PV.IsMine)
            return;

        if (!escMenu.open)
		{
            MyInput();
            Look();
            SelectItem();
            UseItem();
        }
        EscMenu();
    }

    private void SelectItem()
	{
        // item swaping with number buttons
        for (int i = 0; i < items.Length; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                EquipItem(i);
                break;
            }
        }

        // item swaping with scroll wheel
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            if (itemIndex >= items.Length - 1)
            {
                EquipItem(0);
            }
            else
            {
                EquipItem(itemIndex + 1);
            }
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            if (itemIndex <= 0)
            {
                EquipItem(items.Length - 1);
            }
            else
            {
                EquipItem(itemIndex - 1);
            }
        }
    }

    private void UseItem()
	{
        // use equipped item
        if (Input.GetMouseButtonDown(0))
        {
            items[itemIndex].Use();
            if (itemIndex == 3)
            {
                PV.RPC("RPC_LaunchProjectile", RpcTarget.All, items[itemIndex].gameObject.transform.position, items[itemIndex].gameObject.transform.rotation,
                    items[itemIndex].gameObject.transform.TransformDirection(new Vector3(0, 0, projectileSpeed)));
            }
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            items[itemIndex].HoldDown();
        }
        if (Input.GetMouseButtonUp(0))
        {
            items[itemIndex].Release();
        }
    }

    /// Finds the player's inputs for player movement
    private void MyInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        jumping = Input.GetButton("Jump");
    }

    private void Movement()
    {
        // adding some extra gravity
        rb.AddForce(Vector3.down * Time.deltaTime * 10);
        
        // find the actual velocity of player relative to where player is looking
        Vector2 mag = FindVelRelativeToLook();
        float xMag = mag.x;
        float yMag = mag.y;

        //Counteract sliding and sloppy movement
        CounterMovement(x, y, mag);

        // if holding jump && ready to jump... 
        if (readyToJump && jumping)
        {
            // make the player jump
            Jump();
        }

        //Set max speed
        float maxSpeed = this.maxSpeed;

        // if speed is larger than maxspeed...
        if (x > 0 && xMag > maxSpeed)
        {
            // cancel out the input so you don't go over max speed
            x = 0;
        }
        if (x < 0 && xMag < -maxSpeed)
        {
            // cancel out the input so you don't go over max speed
            x = 0;
        }
        if (y > 0 && yMag > maxSpeed)
        {
            // cancel out the input so you don't go over max speed
            y = 0;
        }
        if (y < 0 && yMag < -maxSpeed)
        {
            // cancel out the input so you don't go over max speed
            y = 0;
        }

        float multiplier = 1f;
        float multiplierV = 1f;
        
        // movement in air
        if (!grounded)
        {
            multiplier = 0.5f;
            multiplierV = 0.5f;
        }

        // apply forces to move player
        rb.AddForce(orientation.transform.forward * y * moveSpeed * Time.deltaTime * multiplier * multiplierV);
        rb.AddForce(orientation.transform.right * x * moveSpeed * Time.deltaTime * multiplier);
    }

    private void Jump()
    {
        if (grounded && readyToJump)
        {
            readyToJump = false;

            // add jump forces
            rb.AddForce(Vector2.up * jumpForce * 1.5f);
            rb.AddForce(normalVector * jumpForce * 0.5f);
            
            // if jumping while falling... 
            Vector3 vel = rb.velocity;
            if (rb.velocity.y < 0.5f)
            {
                rb.velocity = new Vector3(vel.x, 0, vel.z);
            }
            else if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector3(vel.x, vel.y / 2, vel.z);
            }

            // reset y velocity
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }
    
    private void ResetJump()
    {
        readyToJump = true;
    }
    
    private float desiredX;
    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime * sensMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime * sensMultiplier;

        // find the player's current look rotation
        Vector3 rot = playerCam.transform.localRotation.eulerAngles;
        desiredX = rot.y + mouseX;
        
        // rotate making sure not to over or under rotate
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // perform the rotations
        playerCam.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0);
        orientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0);
    }

    private void CounterMovement(float x, float y, Vector2 mag)
    {
        if (!grounded || jumping)
        {
            return;
        }

        // counter movement
        if (Math.Abs(mag.x) > threshold && Math.Abs(x) < 0.05f || (mag.x < -threshold && x > 0) || (mag.x > threshold && x < 0))
        {
            rb.AddForce(moveSpeed * orientation.transform.right * Time.deltaTime * -mag.x * counterMovement);
        }
        if (Math.Abs(mag.y) > threshold && Math.Abs(y) < 0.05f || (mag.y < -threshold && y > 0) || (mag.y > threshold && y < 0))
        {
            rb.AddForce(moveSpeed * orientation.transform.forward * Time.deltaTime * -mag.y * counterMovement);
        }
        
        // limits diagonal running
        if (Mathf.Sqrt((Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2))) > maxSpeed)
        {
            float fallspeed = rb.velocity.y;
            Vector3 n = rb.velocity.normalized * maxSpeed;
            rb.velocity = new Vector3(n.x, fallspeed, n.z);
        }
    }

    /// finds of the player the velocity relative to where the player is looking; useful for vectors calculations regarding movement and limiting movement
    public Vector2 FindVelRelativeToLook()
    {
        float lookAngle = orientation.transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;

        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;

        float magnitue = rb.velocity.magnitude;
        float yMag = magnitue * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = magnitue * Mathf.Cos(v * Mathf.Deg2Rad);
        
        return new Vector2(xMag, yMag);
    }

    private bool IsFloor(Vector3 v)
    {
        float angle = Vector3.Angle(Vector3.up, v);
        return angle < maxSlopeAngle;
    }

    private bool cancellingGrounded;
    
    /// Handles ground detection for the player; allows the game to know if the player is touching a surface categorized as the ground
    private void OnCollisionStay(Collision other)
    {
        // only check for layers considered 'Ground' in Unity; check for walkable objects for the player
        int layer = other.gameObject.layer;
        if (whatIsGround != (whatIsGround | (1 << layer)))
        {
            return;
        }

        // iterate through every collision in a physics update
        for(int i = 0; i < other.contactCount; i++)
        {
            Vector3 normal = other.contacts[i].normal;
            if (IsFloor(normal))
            {
                grounded = true;
                cancellingGrounded = false;
                normalVector = normal;
                CancelInvoke(nameof(StopGrounded));
            }
        }

        // invoke ground/wall cancel
        float delay = 3f;
        if (!cancellingGrounded)
        {
            cancellingGrounded = true;
            Invoke(nameof(StopGrounded), Time.deltaTime * delay);
        }
    }

    private void StopGrounded()
    {
        grounded = false;
    }


    // handles equiping items
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

        if (PV.IsMine)
		{
            hash = PhotonNetwork.LocalPlayer.CustomProperties;
            hash.Remove("itemIndex");
            hash.Add("itemIndex", itemIndex);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
		}
	}

	public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
	{
		if (!PV.IsMine && targetPlayer == PV.Owner)
		{
            EquipItem((int)changedProps["itemIndex"]);
		}
	}

    // ran by the shooter
    public void TakeDamage(float damage)
    {
        Debug.Log(PhotonNetwork.LocalPlayer + " is the shooter");
        PV.RPC("RPC_TakeDamage", RpcTarget.All, damage, PhotonNetwork.LocalPlayer);
    }

    // ran by the target
    [PunRPC]
    void RPC_TakeDamage(float damage, Player shooter)
	{
        if (!PV.IsMine)
            return;

        Debug.Log(PhotonNetwork.LocalPlayer + " I am the target and my shooter is: " + shooter);

        GetComponent<PlayerStats>().LoseHealth((int)damage);

        if (GetComponent<PlayerStats>().GetHealth() <= 0)
		{
            Die(shooter);
		}
	}

    void Die(Player shooter)
	{
        playerManager.Die(shooter);
	}


    /***************/
    /*   Esc Menu  */
    /***************/

    void EscMenu()
	{
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (escMenu.open)
			{
                escMenu.Close();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
			else
			{
                escMenu.Open();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    public void OnClickReturn()
    {
        if (!PV.IsMine)
            return;
        Debug.Log("Return Button Pressed");
    }
    public void OnClickQuit()
    {
        if (!PV.IsMine)
            return;
        GameManager.Instance.LeaveRoom();
    }


    /***************/
    /*   Projectile weapon  */
    /***************/

    
    [PunRPC]
    void RPC_LaunchProjectile(Vector3 position, Quaternion rotation, Vector3 velocity)
	{
        if (PV.IsMine)
            return;

        GameObject instantiatedProjectile = (GameObject)Instantiate(projectile, position, rotation);
        instantiatedProjectile.GetComponent<Rigidbody>().velocity = velocity;
        Destroy(instantiatedProjectile, 3);
    }


    /***************/
    /*   Grapple  */
    /***************/


    [PunRPC]
    void RPC_Grapple(int positionCount, Vector3 startPosition, Vector3 endPosition)
    {
        if (PV.IsMine)
            return;

        lr.positionCount = positionCount;
        if (positionCount == 2)
        {
            lr.SetPosition(0, startPosition);
            lr.SetPosition(1, endPosition);
        }
    }

}
