using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public PauseManager PM;
    GameObject pauseCanvas;
    public GameObject timer;

    #region Stones Variables
    public GameObject[] stones, spawnPoints;
    GameObject chosenStone, chosenSpawnPoint;//Depois de sortear, a pedra e o lugar onde vai aparecer

    [Header("Tempo de Spawn das Pedras")]
    public float countDown;//incremento de tempo a cada spawn
    float spawnTime;//tempo em que a pedra aparece no jogo a partir do load
    int randomNumber; //armazena o número sorteado da pedra e do spawn point
    int lastChosenStone; // armazena o tipo de pedra que spawnou por último

    public static List<int> idleSpawnPoints = new List<int> { 0, 1, 2, 3, 4 };// conjunto dos números a serem sorteados no Random.range
    public static bool idleSPoint1, idleSPoint2, idleSPoint3, idleSPoint4, idleSPoint5;//pra verificar se o spawnpoint está idle ou não

	List<int> resetIdleSpawnPoints = new List<int> { 0, 1, 2, 3, 4 };//repõe os elementos do idlespawnpoint quando a partida eh reiniciada

    public static List<int> IdleStones = new List<int> { 0, 1, 2, 3 };
    public static bool idleStone_0, idleStone_1, idleStone_2, idleStone_3;
    List<int> ResetIdleStones = new List<int> { 0, 1, 2, 3 };
    #endregion

    #region Damage Elements
    [Header("Elementos de skill")]
    [Space(5)] public float danoBase;
    [Space(5)] public float[] modificadoresDeDanoLista;
    public float[,] multipicadoresDeDanoTabela;

    public float[] playersLife = null;
    #endregion

    public float temporizador; // tempo de partida
    public Text timertext; // texto do timer
    GameObject background2, background3;
    float t = 0;
    float duration = 60;

    public GameObject posJogo;
    public Slider p1Slider, p2Slider, p3Slider, p4Slider;

    bool active = false;

    void Start()
    {
        pauseCanvas = GameObject.Find("PauseCanvas");

        background2 = GameObject.Find("2"); background3 = GameObject.Find("3");

        TabelaDeMultiplicadores();

		spawnTime = 0;
        idleSpawnPoints = resetIdleSpawnPoints;
        IdleStones = ResetIdleStones;

        ValidateStones();
    }

    void Update()
    {
        SpawnStones();

        Timer();

        BackgroundFade();


        if (Input.GetButtonDown("Pause"))
        {
            active = !active;
            PM.Pause();
        }
        pauseCanvas.SetActive(active);

    }


    void SpawnStones()
    {
		if (Time.timeSinceLevelLoad > spawnTime)
		{
			if (idleSpawnPoints.Count != 0 && IdleStones.Count !=0) //verifica o número de elementos do conjunto
			{
                #region Sorteio de pedras
                //randomNumber = Random.Range(0, stones.Length); //Sortear a pedra
                randomNumber = IdleStones[Random.Range(0,IdleStones.Count)];
				chosenStone = stones [randomNumber];
				IdleStones.Remove (randomNumber);
				if (randomNumber == 0)
					idleStone_0 = false;
				else if (randomNumber == 1)
					idleStone_1 = false;
				else if (randomNumber == 2)
					idleStone_2 = false;
				else if (randomNumber == 3)
					idleStone_3 = false;
            #endregion

                #region Sorteio de SpawnPoints
                randomNumber = idleSpawnPoints[Random.Range(0, idleSpawnPoints.Count)]; //Sortear o spawnpoint

                chosenSpawnPoint = spawnPoints[randomNumber];

                idleSpawnPoints.Remove(randomNumber);//retira o spawnpoint enquanto ele estiver ocupado(é reposto no playerbehavior OnTrigger)
                if (randomNumber == 0)
                    idleSPoint1 = false;
                else if (randomNumber == 1)
                    idleSPoint2 = false;
                else if (randomNumber == 2)
                    idleSPoint3 = false;
                else if (randomNumber == 3)
                    idleSPoint4 = false;
                else if (randomNumber == 4)
                    idleSPoint5 = false;
                #endregion

                Instantiate(chosenStone, chosenSpawnPoint.transform.position, transform.rotation);

                SpawnEffects.StoneSpawn();

            }
			spawnTime += countDown;
        }

    }

    public void AddDamage(int playerIDReceiveDamage, int playerIDReceiveLife, float damage)
    {
		
		playersLife [playerIDReceiveDamage] -= damage;
		playersLife [playerIDReceiveLife] += damage;


		//não deixa o player ficar com vida negativa
		if (playersLife [playerIDReceiveDamage] < 0)
		{
			playersLife [playerIDReceiveLife] -= Mathf.Abs (playersLife [playerIDReceiveDamage]);
			playersLife [playerIDReceiveDamage] += Mathf.Abs (playersLife [playerIDReceiveDamage]);
		}
    }

    public void TabelaDeMultiplicadores()
    {
        int guardaPasso = 0;
		int numeroDeFilas = 5;

        multipicadoresDeDanoTabela = new float[5, 5];

        for (int linha = 0; linha < numeroDeFilas; linha++)
        {
            for (int coluna = 0; coluna < numeroDeFilas; coluna++)
            {
                if (guardaPasso == 0)
                {
                    multipicadoresDeDanoTabela[linha, coluna] = modificadoresDeDanoLista[coluna];
                }
                else if (guardaPasso == 1)
                {
                    multipicadoresDeDanoTabela[linha, coluna] = modificadoresDeDanoLista[coluna + numeroDeFilas];
                }
                else
                {
                    multipicadoresDeDanoTabela[linha, coluna] = modificadoresDeDanoLista[coluna + numeroDeFilas * linha];
                }
            }
            guardaPasso++;
        }
    }

    void Timer()
    {
        temporizador -= Time.deltaTime;
        timertext.text = "Timer: \n" + Mathf.Round(temporizador); // seta o temporizador


        if (temporizador <= 30)
            timer.SetActive(true);
        if (temporizador <= 1)
        {
            TimerEffects.Gongo();
            TimerEffects.TicTac();
        }

        if (temporizador <= 0) // detecta se o tempo chego 0 
        {
            AudioMusic.Victory();
            posJogo.SetActive(true);
            p1Slider.value = playersLife[0];
            p2Slider.value = playersLife[1];
            p3Slider.value = playersLife[2];
            p4Slider.value = playersLife[3];
            Time.timeScale = 0;
        }


    }

    void BackgroundFade()
    {
        if (temporizador > 59 && temporizador < 60) t = 0; // renova o timer do alpha
        else t += Time.deltaTime / duration; // de 0 à 60, soma o duration

        Color color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), t);

        if (temporizador > 60)
            background3.GetComponent<SpriteRenderer>().color = color;
        else
            background2.GetComponent<SpriteRenderer>().color = color;
    }

    void ValidateStones()
    {
        idleSPoint1 = true;
        idleSPoint2 = true;
        idleSPoint3 = true;
        idleSPoint4 = true;
        idleSPoint5 = true;

        idleStone_0 = true;
        idleStone_1 = true;
        idleStone_2 = true;
        idleStone_3 = true;
    }
}
