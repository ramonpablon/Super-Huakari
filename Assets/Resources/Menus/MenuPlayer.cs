using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPlayer : MonoBehaviour
{
    public string playerPrefix;
    public GameObject entrar, confirmar, volta;
    private bool entrou, confirmado;
    public MenuPrincipal menu;
    int numeroDePlayers, playersConfirmados;

    void Start()
    {
        entrou = false;
        confirmado = false;
        numeroDePlayers = playersConfirmados = 0;
    }

    void Update()
    {
        Entrar();
        Sair();
        Confirmar();
        Voltar();
    }

    void Entrar()
    {
        if (Input.GetButtonDown(playerPrefix + "_Entrar") && !entrou)
        {
            entrar.gameObject.SetActive(true);
            entrou = true;
            numeroDePlayers++;
            menu.NumeroDePlayers(numeroDePlayers);
            Debug.Log(numeroDePlayers);
        }
    }

    void Sair()
    {
        if (Input.GetButtonDown(playerPrefix + "_Sair") && entrou)
        {
            entrar.gameObject.SetActive(false);
            volta.gameObject.SetActive(false);
            confirmar.gameObject.SetActive(true);
            entrou = false;
            confirmado = false;
            numeroDePlayers--;
            playersConfirmados--;
            menu.NumeroDePlayers(numeroDePlayers);
            menu.PlayersConfirmados(playersConfirmados);
        }
    }

    void Confirmar()
    {
        if (Input.GetButtonDown(playerPrefix + ("_Confirmar")) && entrou && !confirmado)
        {
            confirmar.gameObject.SetActive(false);
            volta.gameObject.SetActive(true);
            confirmado = true;
            playersConfirmados++;
            menu.PlayersConfirmados(playersConfirmados);
        }
    }

    void Voltar()
    {
        if (Input.GetButtonDown(playerPrefix + ("_Voltar")) && entrou && confirmado)
        {
            volta.gameObject.SetActive(false);
            confirmar.gameObject.SetActive(true);
            confirmado = false;
            playersConfirmados--;
            menu.PlayersConfirmados(playersConfirmados);
        }
    }

    public void Reiniciar()
    {
        entrar.gameObject.SetActive(false);
        volta.gameObject.SetActive(false);
        confirmar.gameObject.SetActive(true);
        numeroDePlayers = playersConfirmados = 0;
        entrou = confirmado = false;
    }
}
