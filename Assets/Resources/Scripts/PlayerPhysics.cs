using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour {

    protected Animator an;
    private InputManager IM;

    public int playerID, playerElemento;// Numero do player, elemento que o player é.

    [HideInInspector] public float hForce, vForce, lookTrigger, fireTrigger;// força que se aplica na horizontal, vertical e nos dois gatilhos trazeiros (1,0,-1)

    protected bool keepHForce;//manter o sentido de movimentãção do personagem durante a mira no ar
    public float jumpForce = 400;
    protected float speed;


    protected bool onGround, onWall = false;
    [Space(10)] public LayerMask groudDetected;
    protected Transform groundCheck;
    protected Transform wallCheck;


    protected Rigidbody2D rb;

    protected RaycastHit2D hit;
    private float distance = 0.2f; // distancia do hit
    protected Vector3 position; // direção do hit

    protected float timer, lookTimer = 0;

    public Sprite[] tribeSprite = new Sprite[5];
    protected GameObject tribe;


    void Awake () {
		IM = GetComponent<InputManager> ();
        an = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
		groundCheck = transform.Find(IM.playerPrefix + "_GroundCheck"); // identifica o verificador de chão do player

        tribe = GameObject.Find(IM.playerPrefix + "_Tribe");

        IM.ammo = 4;
    }
	
	// Update is called once per frame
	void Update () 
	{
        IM.Ammo();

        timer += Time.deltaTime; lookTimer += Time.deltaTime;

        Physics2D.queriesStartInColliders = false;

        onGround = Physics2D.OverlapCircle(groundCheck.position, 0.16f, groudDetected);


        hit = Physics2D.Raycast(transform.position + new Vector3(0,0.3f,0), position, distance); // cria uma linha para verificar se esta colidindo com a parede

        IM.Jump();
    }
}
