using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    private string nomeDaCena;
    private bool volumeDoJogo;
	bool estaNoMenu, estaPreJogo, creditos, tutoriais, preJogo;

	Animator menus;
    int numeroDePlayers, playersConfirmados;

    public MenuPlayer[] menuPlayer;

    float time = 2;
    public Text timertext;
    public GameObject timerImage;

    public Button jogarButton;
    public GameObject iniciarpartida;
    bool canplay = false;

    public Image mapPrevia;
    public Sprite[] previaMapSprite;


    bool load2, load3, load4;


    public AudioSource[] ads = new AudioSource[2];

    void Awake()
    {
       // DontDestroyChildOnLoad(gameObject);
    }

    void Start()
    {
        nomeDaCena = SceneManager.GetActiveScene().name;
        estaNoMenu = true;
		estaPreJogo = false;
		creditos = false;
		tutoriais = false;
		menus = GetComponentInChildren<Animator> ();
        numeroDePlayers = playersConfirmados = 0;
    }

    void Update()
    {

        if (numeroDePlayers < 0)
            numeroDePlayers = 0;

        if (SceneManager.GetActiveScene().name != nomeDaCena)// manda a informaçao do audiolistner, se esta ativo ou nao, para outra cena.
        {
            AudioListener.pause = volumeDoJogo;
            Destroy(gameObject);
        }
        AudioListener.pause = volumeDoJogo;
        if (!estaNoMenu && Input.GetButtonDown("Cancel"))// caso esteja em outa pagina de menu que nao seja na principal, e o jogador apertar 'b', volta para o menu principal.
        {
            ads[1].Play();
			creditos = false;
			tutoriais = false;
            estaNoMenu = true;
        }
        if (estaPreJogo && Input.GetButtonDown("Voltar"))
        {
            jogarButton.enabled = true;

            ads[1].Play();
            preJogo = false;
            estaPreJogo = false;
            for (int guardapasso = 0; guardapasso < menuPlayer.Length; guardapasso++)
            {
                menuPlayer[guardapasso].Reiniciar();
            }
            numeroDePlayers = playersConfirmados = 0;
        }
		menus.SetBool ("Tutorial", tutoriais);
		menus.SetBool ("Credito", creditos);
		menus.SetBool ("PreJogo", preJogo);

        if (playersConfirmados >= 2) // mostra botão start partida
            iniciarpartida.SetActive(true);
        else iniciarpartida.SetActive(false);

        if (numeroDePlayers < 2) // seta uma imagem qualquer no map previa
            mapPrevia.sprite = previaMapSprite[3];

        if (numeroDePlayers >= 2)
        {
            mapPrevia.sprite = previaMapSprite[0];

            if (numeroDePlayers == playersConfirmados)
            {
                canplay = true;

            }
        } // load scene pvp2
        if (numeroDePlayers >= 3 )
        {
            mapPrevia.sprite = previaMapSprite[1];

            if (numeroDePlayers == playersConfirmados)
            {
                canplay = true;
            }
        }// load scene pvp3
        if (numeroDePlayers >= 4)
        {
            mapPrevia.sprite = previaMapSprite[2];

            if (numeroDePlayers == playersConfirmados)
            {
                canplay = true;
            }
        } // load scene pvp4

        if (playersConfirmados < 2) canplay = false;

        #region verifica input
        if (playersConfirmados == 2 && canplay)
        {
            if (Input.GetButtonDown("P1_Entrar") || Input.GetButtonDown("P2_Entrar") ||
            Input.GetButtonDown("P3_Entrar") || Input.GetButtonDown("P4_Entrar"))
            {
                load2 = true;
            }
        }
        if (playersConfirmados == 3 && canplay)
        {
            if (Input.GetButtonDown("P1_Entrar") || Input.GetButtonDown("P2_Entrar") ||
            Input.GetButtonDown("P3_Entrar") || Input.GetButtonDown("P4_Entrar"))
            {
                load3 = true;
            }
        }
        if (playersConfirmados == 4 && canplay)
        {
            if (Input.GetButtonDown("P1_Entrar") || Input.GetButtonDown("P2_Entrar") ||
            Input.GetButtonDown("P3_Entrar") || Input.GetButtonDown("P4_Entrar"))
            {
                load4 = true;
            }
        }

        #endregion

        if(load2 == true)
        {
           // timerImage.SetActive(true);
            time -= Time.deltaTime;
            //timertext.text = "A partida inicia em " + Mathf.Round(time) + '.';
			menus.Play("FadeOutAnim");
            if (time <= 0)
            {
                SceneManager.LoadScene("PvP_main2");
            }
        }
        if(load3 == true)
        {
            time -= Time.deltaTime;
			menus.Play("FadeOutAnim");
            //timerImage.SetActive(true);
            //timertext.text = "A partida inicia em " + Mathf.Round(time) + '.';
            if (time <= 0)
            {
                SceneManager.LoadScene("PvP_main3");
            }
        }
        if(load4 == true)
        {
            time -= Time.deltaTime;
			menus.Play("FadeOutAnim");
            //timerImage.SetActive(true);
            //timertext.text = "A partida inicia em " + Mathf.Round(time) + '.';
            if (time <= 0)
            {
                SceneManager.LoadScene("PvP_main4");
            }
        }
    }

    public void Jogar()
    {
        ads[0].Play();
		preJogo = true;
        estaPreJogo = true;

        jogarButton.enabled = false;
    }

    public void Tutorial()
    {
        ads[0].Play();
        tutoriais = true;
		estaNoMenu = false;
    }

    public void Creditos()
    {
        ads[0].Play();
        creditos = true;
		estaNoMenu = false;
    }

    public void Som()// seta se o som do jogo está ativado ou não.
    {
        if (volumeDoJogo)
        {
            volumeDoJogo = false;
        }
        else
        {
            volumeDoJogo = true;
        }
    }

    public void NumeroDePlayers(int numero)
    {
        if(numero == 1)
        {
            numeroDePlayers++;
        }
        else
        {
            numeroDePlayers--;
        }
    }

    public void PlayersConfirmados(int numero)
    {
        if (numero == 1)
        {
            playersConfirmados++;
        }
        else
        {
            playersConfirmados--;
        }
    }

    public void DontDestroyChildOnLoad(GameObject child)
    {
        Transform parentTransform = child.transform; // If this object doesn't have a parent then its the root transform. 
        while (parentTransform.parent != null)
        { // Keep going up the chain. 
            parentTransform = parentTransform.parent;
        }
        DontDestroyOnLoad(parentTransform.gameObject);
    }

}
