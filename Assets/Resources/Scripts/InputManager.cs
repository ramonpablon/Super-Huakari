using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : PlayerPhysics {


	[Header("Referencia o Player")]
	public string playerPrefix; // referencia o player e seu controle (P1_,P2_,P3_,P4_)

    protected bool fired;

    public GameObject normalBullet, airBullet, earthBullet, waterBullet, fireBullet, aimPoint;

    [Header("Elementos de Tiro")]
    public Transform shotSpawner;

    private float nextJump = 0.25f;
    protected float direction;

    protected bool superJump;


    public int ammo = 4;
    private float ammoLoad = 0;



    public void Jump()
    {

        if (Input.GetButtonDown(playerPrefix + "_Jump") && onGround && superJump)
        {
            JumppadEffects.Jumppadsound();
            rb.AddForce(Vector3.up * jumpForce * 1.3f, ForceMode2D.Impulse);
            onGround = false;
        }

        else if (Input.GetButtonDown(playerPrefix + "_Jump") && onGround)
        {
            JumpEffects.PlayerJump(playerPrefix);

            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            onGround = false;
        }

        if(!onGround)
        {
            if (Input.GetButtonDown(playerPrefix + "_Jump") && hit.collider.tag.Equals("Wall"))
            {
                JumpEffects.PlayerJump(playerPrefix);

                timer = 0;
                onWall = true;
                if (timer < nextJump)
                {
                    DirectionJump(direction); // recebe temp hforce
                    rb.velocity = new Vector2(hForce * hit.normal.x, 8); // adiciona uma força na direção oposta do player quando ele toca o wall
                }
            } // wall jump
        }

        if (timer >= nextJump)
        {
            onWall = false;
        } // normaliza o hforce do player

    } // pula quando preciona "X"
    public void DirectionJump(float hforce)
    {
        hForce = hforce;
    } // hforce recebe direction como parametro se estiver tocando Wall

    public void Look()
    {
        float lookTrigger = Input.GetAxisRaw(playerPrefix + "_Look");
        
        if (lookTrigger > 0.3)
        {
            an.SetBool("Looking", true);
            an.SetFloat("lookTimer", lookTimer);


            Fire();
            speed = 0f;
            aimPoint.SetActive(true);
            keepHForce = true;
        }
        else
        {
            lookTimer = 0;
            an.SetBool("Looking", false);

            speed = 4f;
            aimPoint.SetActive(false);
            keepHForce = false;
        }

        // animações
        if (lookTimer >= 0.3 && !fired) lookTimer -= 0.05f;
        else if(lookTimer >= 0.3 && fired) lookTimer = 0.45f;
        // animações

        if (onGround)
            keepHForce = false;

    } // gatilho de mira do joystick
    public void Fire()
    {
        fireTrigger = Input.GetAxisRaw(playerPrefix + "_Fire");

        if (ammo > 0)
		{
            if (fireTrigger < 0.1f) fired = false;

            if (fired == false)
            {
				if (fireTrigger > 0.5f)
                {
                    if (playerElemento == 4) {

                        NeutroEffects.ShotThrow();

                        GameObject projectileObject = Instantiate (normalBullet, shotSpawner.position, shotSpawner.rotation) as GameObject;
						ShootBehaviour shootBehaviour = projectileObject.GetComponent<ShootBehaviour> ();

                        shootBehaviour.playerNumber = playerID;


					} // Neutral bullet
                    else if (playerElemento == 3) {
						GameObject projectileObject = Instantiate (earthBullet, shotSpawner.position, shotSpawner.rotation) as GameObject;
						ShootBehaviour shootBehaviour = projectileObject.GetComponent<ShootBehaviour> ();

                        shootBehaviour.playerNumber = playerID;

                        EarthEffects.ShotThrow();

					}// Earth bullet
                    else if (playerElemento == 2) {
						GameObject projectileObject = Instantiate (airBullet, shotSpawner.position, shotSpawner.rotation) as GameObject;
						ShootBehaviour shootBehaviour = projectileObject.GetComponent<ShootBehaviour> ();

                        shootBehaviour.playerNumber = playerID;

                        AirEffects.ShotThrow();

                    } // Air bullet
                    else if (playerElemento == 1) {
						GameObject projectileObject = Instantiate (fireBullet, shotSpawner.position, shotSpawner.rotation) as GameObject;
						ShootBehaviour shootBehaviour = projectileObject.GetComponent<ShootBehaviour> ();

                        shootBehaviour.playerNumber = playerID;

                        FireEffects.ShotThrow();

					} // Fire bullet
                    else if (playerElemento == 0) {
						GameObject projectileObject = Instantiate (waterBullet, shotSpawner.position, shotSpawner.rotation) as GameObject;
						ShootBehaviour shootBehaviour = projectileObject.GetComponent<ShootBehaviour> ();

                        shootBehaviour.playerNumber = playerID;

                        WaterEffects.ShotThrow();

					} // Water bullet
					ammo--;
                    fired = true;
                }
            }
        }

    } // gatilho de tiro do joystick
    public void Ammo()
    {
        tribe.GetComponent<SpriteRenderer>().sprite = tribeSprite[ammo];

        ammoLoad += Time.deltaTime;

        if (ammoLoad > 1.5f && ammo < 4)
        {
            ammo++;
            ammoLoad = 0;
        }
    }


    public void Axis()
    {
        hForce = Input.GetAxisRaw(playerPrefix + "_Horizontal");
        vForce = Input.GetAxisRaw(playerPrefix + "_Vertical");
    } // Força que os analogicos exercem
    public void Move(){
		rb.velocity = new Vector2(hForce * speed, rb.velocity.y);
    } // veelocidade recebe a direção que o player esta andando
}
