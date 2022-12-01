using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeController : MonoBehaviour 
{
	public float lerpTime;//se quiser por lerp, se nao quiser eh soh por 1
	public float deslocamentoDaBarra;//quanto as barras de vida se deslocam na horizontal

	GameObject BlueSymbol;
	GameObject GreenSymbol; 
	GameObject YellowSymbol;
    public GameObject[] players;

	public float kuririnHP,majinbuHP,cellHP,freezaHP;

	Vector2 majinbuPos,cellPos,freezaPos; 

	void Start () 
	{
		
		BlueSymbol = GameObject.Find("BlueSimbol");
        majinbuPos = BlueSymbol.transform.position;

        if (SceneManager.GetActiveScene().name == "PvP_main3")
        {
            GreenSymbol = GameObject.Find("GreenSimbol");
            cellPos = GreenSymbol.transform.position; 
        }

        if (SceneManager.GetActiveScene().name == "PvP_main4")
        {
            GreenSymbol = GameObject.Find("GreenSimbol");
            cellPos = GreenSymbol.transform.position;

            YellowSymbol = GameObject.Find("YellowSimbol");
            freezaPos = YellowSymbol.transform.position;
        }

    }
	public void RouboDeVida(int perde, int ganha, float dmg)//é pra ser, (playerID, enemyID, dmg) mas ta invertido
	{
		if (ganha == 0 && perde == 1 && majinbuHP>0)
		//if (Input.GetKeyDown (KeyCode.Q) && majinbuHP> 0)
		{
			kuririnHP += dmg;
			majinbuHP -= dmg;
			majinbuPos.x += deslocamentoDaBarra*(dmg/10);

			if(majinbuHP <0)//corrigir hp e posição caso a vida fique negativa
			{
				majinbuPos.x -= Mathf.Abs (deslocamentoDaBarra * (majinbuHP/10));

				kuririnHP -= Mathf.Abs (majinbuHP);
				majinbuHP += Mathf.Abs (majinbuHP);
			}
		}
		if (ganha == 0 && perde == 2 && cellHP>0)//if (Input.GetKeyDown (KeyCode.A) && cellHP> 0)// && scene >4)
		{
			kuririnHP += dmg;
			cellHP -= dmg;
			majinbuPos.x += deslocamentoDaBarra*(dmg/10);
			cellPos.x +=deslocamentoDaBarra*(dmg/10);
			if(cellHP <0)
			{
				cellPos.x -= Mathf.Abs (deslocamentoDaBarra * (cellHP/10));
				majinbuPos.x -= Mathf.Abs (deslocamentoDaBarra * (cellHP/10));

				kuririnHP -= Mathf.Abs (cellHP);
				cellHP+= Mathf.Abs (cellHP);
			}
		}
		if (ganha == 0 && perde == 3 && freezaHP>0)
		//if (Input.GetKeyDown (KeyCode.Z) && freezaHP > 0)// && scene >5)
		{
			kuririnHP += dmg;
			freezaHP -= dmg;
			majinbuPos.x += deslocamentoDaBarra*(dmg/10);
			cellPos.x +=deslocamentoDaBarra*(dmg/10);
			freezaPos.x += deslocamentoDaBarra*(dmg/10);
			if(freezaHP <0)
			{
				freezaPos.x -= Mathf.Abs (deslocamentoDaBarra * (freezaHP/10));
				cellPos.x -= Mathf.Abs (deslocamentoDaBarra * (freezaHP/10));
				majinbuPos.x -= Mathf.Abs (deslocamentoDaBarra * (freezaHP/10));

				kuririnHP -= Mathf.Abs (freezaHP);
				freezaHP += Mathf.Abs (freezaHP);
			}
		}
		if (ganha == 1 && perde == 0 && kuririnHP> 0)
	//	if (Input.GetKeyDown (KeyCode.W) && kuririnHP> 0)
		{
			majinbuHP += dmg;
			kuririnHP -= dmg;
			majinbuPos.x -= deslocamentoDaBarra*(dmg/10);
			if(kuririnHP <0)
			{
				majinbuPos.x += Mathf.Abs (deslocamentoDaBarra * (kuririnHP/10));

				majinbuHP -= Mathf.Abs (kuririnHP);
				kuririnHP += Mathf.Abs (kuririnHP);
			}
		}
		if (ganha == 1 && perde == 2 && cellHP> 0)
	//	if (Input.GetKeyDown (KeyCode.S) && cellHP> 0)// && scene >4)
		{
			majinbuHP+= dmg;
			cellHP -= dmg;
			cellPos.x +=deslocamentoDaBarra*(dmg/10);
			if(cellHP <0)
			{
				cellPos.x -= Mathf.Abs (deslocamentoDaBarra * (cellHP/10));

				majinbuHP -= Mathf.Abs (cellHP);
				cellHP+= Mathf.Abs (cellHP);
			}
		}
		if (ganha == 1 && perde == 3 && freezaHP>0)
	//	if (Input.GetKeyDown (KeyCode.X) && freezaHP > 0)// && scene >5)
		{
			majinbuHP += dmg;
			freezaHP -= dmg;
			cellPos.x +=deslocamentoDaBarra*(dmg/10);
			freezaPos.x += deslocamentoDaBarra*(dmg/10);
			if(freezaHP<0)
			{
				freezaPos.x -= Mathf.Abs (deslocamentoDaBarra * (freezaHP/10));
				cellPos.x -= Mathf.Abs (deslocamentoDaBarra * (freezaHP/10));

				majinbuHP -= Mathf.Abs (freezaHP);
				freezaHP+= Mathf.Abs (freezaHP);
			}
		}
		if (ganha == 2 && perde == 0 && kuririnHP > 0)
	//	if (Input.GetKeyDown (KeyCode.E) && kuririnHP > 0)// && scene >4)
		{
			cellHP += dmg;
			kuririnHP -= dmg;
			majinbuPos.x -= deslocamentoDaBarra*(dmg/10);
			cellPos.x -=deslocamentoDaBarra*(dmg/10);
			if(kuririnHP <0)
			{
				cellPos.x += Mathf.Abs (deslocamentoDaBarra * (kuririnHP/10));
				majinbuPos.x += Mathf.Abs (deslocamentoDaBarra * (kuririnHP/10));

				cellHP-= Mathf.Abs (kuririnHP);
				kuririnHP += Mathf.Abs (kuririnHP);
			}
		}
		if (ganha == 2 && perde == 1 && majinbuHP>0)
	//	if (Input.GetKeyDown (KeyCode.D) && majinbuHP> 0)// && scene >4)
		{
			cellHP += dmg;
			majinbuHP -= dmg;
			cellPos.x -=deslocamentoDaBarra*(dmg/10);
			if(majinbuHP<0)
			{
				cellPos.x += Mathf.Abs (deslocamentoDaBarra * (majinbuHP/10));

				cellHP-= Mathf.Abs (majinbuHP);
				majinbuHP += Mathf.Abs (majinbuHP);
			}

		}
		if (ganha == 2 && perde == 3 && freezaHP > 0)
		//if (Input.GetKeyDown (KeyCode.C) && freezaHP > 0)// && scene >5)
		{
			cellHP += dmg;
			freezaHP -= dmg;
			freezaPos.x += deslocamentoDaBarra*(dmg/10);
			if(freezaHP<0)
			{
				freezaPos.x -= Mathf.Abs (deslocamentoDaBarra * (freezaHP/10));

				cellHP-= Mathf.Abs (freezaHP);
				freezaHP+= Mathf.Abs (freezaHP);
			}
		}
		if (ganha == 3 && perde == 0 && kuririnHP > 0)
		//if (Input.GetKeyDown (KeyCode.R) && kuririnHP > 0)// && scene >5)
		{
			freezaHP += dmg;
			kuririnHP -= dmg;
			majinbuPos.x -= deslocamentoDaBarra*(dmg/10);
			cellPos.x -=deslocamentoDaBarra*(dmg/10);
			freezaPos.x -= deslocamentoDaBarra*(dmg/10);
			if(kuririnHP<0)
			{
				freezaPos.x += Mathf.Abs (deslocamentoDaBarra * (kuririnHP/10));
				cellPos.x += Mathf.Abs (deslocamentoDaBarra * (kuririnHP/10));
				majinbuPos.x += Mathf.Abs (deslocamentoDaBarra * (kuririnHP/10));

				freezaHP-= Mathf.Abs (kuririnHP);
				kuririnHP+= Mathf.Abs (kuririnHP);
			}
		}
		if (ganha == 3 && perde == 1 && majinbuHP > 0)
		//if (Input.GetKeyDown (KeyCode.F) && majinbuHP > 0)// && scene >5)
		{
			freezaHP += dmg;
			majinbuHP -= dmg;
			cellPos.x -=deslocamentoDaBarra*(dmg/10);
			freezaPos.x -= deslocamentoDaBarra*(dmg/10);
			if(majinbuHP<0)
			{
				freezaPos.x += Mathf.Abs (deslocamentoDaBarra * (majinbuHP/10));
				cellPos.x += Mathf.Abs (deslocamentoDaBarra * (majinbuHP/10));

				freezaHP-= Mathf.Abs (majinbuHP);
				majinbuHP+= Mathf.Abs (majinbuHP);
			}
		}
		if (ganha == 3 && perde == 2 && cellHP > 0)
		//if (Input.GetKeyDown (KeyCode.V) && cellHP > 0)// && scene >5)
		{
			freezaHP += dmg;
			cellHP -= dmg;
			freezaPos.x -= deslocamentoDaBarra*(dmg/10);
			if(cellHP<0)
			{
				freezaPos.x += Mathf.Abs (deslocamentoDaBarra * (cellHP/10));

				freezaHP-= Mathf.Abs (cellHP);
				cellHP+= Mathf.Abs (cellHP);
			}
		}
	


		BlueSymbol.transform.position = Vector2.Lerp (BlueSymbol.transform.position, majinbuPos, lerpTime);
        if (SceneManager.GetActiveScene().name == "PvP_main3")
        {
            GreenSymbol.transform.position = Vector2.Lerp(GreenSymbol.transform.position, cellPos, lerpTime);
        }

        if (SceneManager.GetActiveScene().name == "PvP_main4")
        {
            GreenSymbol.transform.position = Vector2.Lerp(GreenSymbol.transform.position, cellPos, lerpTime);

            YellowSymbol.transform.position = Vector2.Lerp(YellowSymbol.transform.position, freezaPos, lerpTime);
        }
    }
}