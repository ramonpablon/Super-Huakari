using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBehaviour : MonoBehaviour
{
    public float speed; // velocidade do tiro
    public int playerNumber; // de qual player é o tiro

    public int elemento;


    private bool isWrappingX = false;//Wrap() atravessando a tela no X
    private bool isWrappingY = false;//Wrap() atravessando a tela no Y
    private Renderer[] renderers;//Wrap()

    void Start()
    {
        renderers = GetComponents<Renderer>();//Wrap()
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnRenderObject()
    {
        ScreenWrap();
    }

    void OnCollisionEnter2D(Collision2D trigg)
    {
        if (trigg.gameObject.tag.Equals("Ground") || trigg.gameObject.tag.Equals("Wall"))
        {
            #region Map Collision Effects
            if (elemento == 0)
            {
                WaterEffects.MapCollider();
            }
            if (elemento == 1)
            {
                FireEffects.MapCollider();
            }
            if (elemento == 2)
            {
                AirEffects.MapCollider();
            }
            if (elemento == 3)
            {
                EarthEffects.MapCollider();
            }
            if (elemento == 4)
            {
                NeutroEffects.MapCollider();
            }
            #endregion

            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D trigg)
    {
        if (trigg.gameObject.tag.Equals("Seeker") && trigg.gameObject.GetComponentInParent<PlayerPhysics>().playerID != playerNumber) 
        {
            Vector2 direction = new Vector2(trigg.gameObject.transform.position.x - transform.position.x, trigg.gameObject.transform.position.y - transform.position.y);

            transform.up = Vector3.Lerp(transform.up, direction, 0.1f);
        }
    } // tiro perseguidor

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
            if (newPosition.x > 7 || newPosition.x < -7) //5 e -5 são valores aproximados dos limites laterais da tela
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
}
