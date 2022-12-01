using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : InputManager
{
    private bool isWrappingX = false;//Wrap() atravessando a tela no X
    private bool isWrappingY = false;//Wrap() atravessando a tela no Y
    private Renderer[] renderers;//Wrap()
    public static string poder;// registrar o poder do personagem

    public GameController gameController = null;

	public LifeController lifeController = null;//pra poder acessar o roubo de vida 

    [HideInInspector] public SpriteRenderer playerSprite;

    private GameObject particle;

	bool canIdleSpawnPoint;
	float powerDuration;
	public float powerDurationTeste;

    [Space(10)] public GameObject[] particleTipes;

    void Start()
    {
        poder = "Neutro";
        renderers = GetComponentsInChildren<Renderer>();//Wrap()
        playerSprite = GetComponent<SpriteRenderer>();
        particle = GameObject.Find(playerPrefix + "_Player_Particles");

		keepHForce = false;//pra poder andar quando a mira ta desligada

		playerElemento = 4;

		superJump = false;

		canIdleSpawnPoint = true;

		powerDuration = powerDurationTeste;
    }

    void FixedUpdate()
    {
        Inputs(); // comandos do joystick

        PlayerMirror(); // espelhamento do personagem

        ScreenWrap(); // wrap do cenario

        PowerDuration(); // duração do poder recolhido


        an.SetBool("OnGround", onGround);
        an.SetFloat("Speed", Mathf.Abs(rb.velocity.x)); // manda o valor da movimentação para executar a animação;
        an.SetFloat("vSpeed", rb.velocity.y); // manda o valor da velocidade do player no eixo y, para executar a animação do blend tree
    }

    void PowerDuration()
	{
		if (playerElemento != 4)
		{
			powerDuration -= Time.deltaTime;
			if (powerDuration <= 0)
			{
				playerElemento = 4;
				powerDuration = powerDurationTeste;
				an.runtimeAnimatorController = Resources.Load ("Animation/Player_Animations/Normal_Animation/Normal_Controller") as RuntimeAnimatorController;

                Particles(false, false, false, false);
            }
        }
	}

    #region Renderers
    public void PlayerMirror()
    {
        if (hForce > 0)
        {
            playerSprite.flipX = false;
            direction = -1;
            position = Vector3.right; // muda a direção do hit

            particle.transform.localScale = new Vector3 (1,1,1);
        }
        else if (hForce < 0)
        {
            playerSprite.flipX = true;
            direction = 1;
            position = -Vector3.right; // muda a direção do hit

            particle.transform.localScale = new Vector3(-1, 1, 1);
        }
    } // espelha o gameobject do player por completo
    void ScreenWrap()
    {
        if (Time.timeSinceLevelLoad > 2)//pra não inverter a posição no início
        {
            bool isVisible = CheckRenderers();
            if (isVisible)
            {
                isWrappingX = false;
                isWrappingY = false;
                return;
            }
            if (isWrappingX || isWrappingY)
                return;
            Vector3 newPosition = transform.position;
            if (newPosition.x > 5 || newPosition.x < -5) //5 e -5 são valores aproximados dos limites laterais da tela
            {
                isWrappingX = true;
                newPosition.x = -(newPosition.x * 0.95f);//*0.95f pra deslocar o cara pra dentro da tela, senão ele aparece do lado de fora
            }
            else if (newPosition.y > 4.3f) //4.3 valor aproximado do limite superior da tela
            {
                isWrappingY = true;
                newPosition.y = -newPosition.y * 1.05f;
            }
            else if (newPosition.y < -4.3f) //-4.3 valor aproximado do limite inferior da tela
            {
                isWrappingY = true;
                newPosition.y = -newPosition.y * 0.95f;
            }
            transform.position = newPosition;
        }
    }
    bool CheckRenderers()//ver se todos os renderers estão visíveis
    {
        foreach (Renderer renderer in renderers)
        {
            if (renderer.isVisible)
            {
                return true;
            }
        }
        return false;
    }
    #endregion

    protected void Inputs()
    {
        if (!onWall)
            Axis();
        if (!keepHForce)
            Move();
        Look();
    } // declara todos os inputs de comandos

    void Particles(bool fire, bool water, bool earth, bool air)
    {
        particleTipes[0].SetActive(fire);
        particleTipes[1].SetActive(water);
        particleTipes[2].SetActive(earth);
        particleTipes[3].SetActive(air);
    }

    # region Collisions
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag.Equals("Shot"))
        {
            int enemyID = coll.gameObject.GetComponent<ShootBehaviour>().playerNumber;
            int bulletElement = coll.gameObject.GetComponent<ShootBehaviour>().elemento;
            if (enemyID != playerID)
            {
                float damageModifier = gameController.multipicadoresDeDanoTabela[bulletElement, playerElemento];
                float damage = gameController.danoBase * damageModifier;
                
                gameController.AddDamage(playerID, enemyID, damage);

                lifeController.RouboDeVida(playerID, enemyID, damage);
               
                #region Player Collsision Sound Effects
                if (bulletElement == 0)
                {
                    WaterEffects.PlayerCollider();
                    
                }
                if (bulletElement == 1)
                    FireEffects.PlayerCollider();
                if (bulletElement == 2)
                    AirEffects.PlayerCollider();
                if (bulletElement == 3)
                    EarthEffects.PlayerCollider();
                if (bulletElement == 4)
                    NeutroEffects.PlayerCollider();
                #endregion
                
                Destroy(coll.gameObject);
            }
        } // tomar dano
    }



    void OnTriggerEnter2D(Collider2D trigg)
    {
        if (trigg.gameObject.tag.Equals("Mola"))
            superJump = true;

        if (playerElemento == 4)
        {
            if (trigg.gameObject.tag.Equals("Stone"))
            {
                if (trigg.gameObject.name.Equals("Air Stone(Clone)"))
                {
                    CollectedEffects.EagleSound();

                    an.runtimeAnimatorController = Resources.Load("Animation/Player_Animations/Air_Animation/Air_Controller") as RuntimeAnimatorController;
                    playerElemento = 2;

                    GameController.idleStone_1 = true;
                    GameController.IdleStones.Add(1);

                    Particles(false, false, false, true);
                }

                else if (trigg.gameObject.name.Equals("Earth Stone(Clone)"))
                {
                    CollectedEffects.BearSound();

                    an.runtimeAnimatorController = Resources.Load("Animation/Player_Animations/Earth_Animation/Earth_Controller") as RuntimeAnimatorController;
                    playerElemento = 3;

                    GameController.idleStone_0 = true;
                    GameController.IdleStones.Add(0);

                    Particles(false, false, true, false);
                }

                else if (trigg.gameObject.name.Equals("Fire Stone(Clone)"))
                {
                    CollectedEffects.FoxSound();

                    an.runtimeAnimatorController = Resources.Load("Animation/Player_Animations/Fire_Animation/Fire_Controller") as RuntimeAnimatorController;
                    playerElemento = 1;

                    GameController.idleStone_2 = true;
                    GameController.IdleStones.Add(2);

                    Particles(true, false, false, false);
                }

                else if (trigg.gameObject.name.Equals("Water Stone(Clone)"))
                {
                    CollectedEffects.SharkSound();

                    an.runtimeAnimatorController = Resources.Load("Animation/Player_Animations/Water_Animation/Water_Controller") as RuntimeAnimatorController;
                    playerElemento = 0;

                    GameController.idleStone_3 = true;
                    GameController.IdleStones.Add(3);

                    Particles(false, true, false, false);
                }
                Destroy(trigg.gameObject);
                canIdleSpawnPoint = true;
            }
        } // pegar powerUps
    }

    void OnTriggerExit2D(Collider2D trigg)
    {
        if (trigg.gameObject.tag.Equals("Mola"))
            superJump = false;

        if (canIdleSpawnPoint)
        {
            if (trigg.gameObject.name.Equals("SP 1") && GameController.idleSPoint1 == false)
            {
                GameController.idleSPoint1 = true;
                GameController.idleSpawnPoints.Add(0);
                canIdleSpawnPoint = false;

            }
            if (trigg.gameObject.name.Equals("SP 2") && GameController.idleSPoint2 == false)
            {
                GameController.idleSPoint2 = true;
                GameController.idleSpawnPoints.Add(1);
                canIdleSpawnPoint = false;
            }
            if (trigg.gameObject.name.Equals("SP 3") && GameController.idleSPoint3 == false)
            {
                GameController.idleSPoint3 = true;
                GameController.idleSpawnPoints.Add(2);
                canIdleSpawnPoint = false;
            }
            if (trigg.gameObject.name.Equals("SP 4") && GameController.idleSPoint4 == false)
            {
                GameController.idleSPoint4 = true;
                GameController.idleSpawnPoints.Add(3);
                canIdleSpawnPoint = false;
            }
            if (trigg.gameObject.name.Equals("SP 5") && GameController.idleSPoint5 == false)
            {
                GameController.idleSPoint5 = true;
                GameController.idleSpawnPoints.Add(4);
                canIdleSpawnPoint = false;
            }
        }
    }// Repõe o elemento no conjunto dos spawnpoints e deixa o spawnpoint livre
    #endregion
}