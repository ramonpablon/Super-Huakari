using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot8Direction : MonoBehaviour {

    SpriteRenderer atualAimSprite;

    Sprite[] aimSprites;
    Dictionary<string, Sprite> dictionaryAimSprites = new Dictionary<string, Sprite>();


    private InputManager IM;
    private PlayerBehaviour PB;
    float angle;

    // Use this for initialization
    void Start () {
        LoadDictionary();

        atualAimSprite = GetComponent<SpriteRenderer>();


        IM = GetComponentInParent<InputManager>();
        PB = GetComponentInParent<PlayerBehaviour>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        DirectionsShot();
    }

    void DirectionsShot()
    {
        float newAngle = angle;

        angle = Mathf.Atan2(IM.vForce, IM.hForce) * Mathf.Rad2Deg; // cateto oposto / cateto adjacente * 57.296 = angulo em graus

        if (angle < 0) angle = 360 + angle; // sempre fica dentro dos quadrantes trigonométricos

        angle = trucatedAngle8Directions(angle);

        #region Set Aim Sprite
        if (angle == 0 || angle == 90 || angle == 180 || angle == 270)
        {
            if (IM.playerElemento == 0)
                atualAimSprite.sprite = dictionaryAimSprites["aim_12"];
            if (IM.playerElemento == 1)
                atualAimSprite.sprite = dictionaryAimSprites["aim_10"];
            if (IM.playerElemento == 2)
                atualAimSprite.sprite = dictionaryAimSprites["aim_14"];
            if (IM.playerElemento == 3)
                atualAimSprite.sprite = dictionaryAimSprites["aim_13"];
            if (IM.playerElemento == 4)
                atualAimSprite.sprite = dictionaryAimSprites["aim_16"];
        }
        if (angle == 45 || angle == 135 || angle == 225 || angle == 315)
        {
            if (IM.playerElemento == 0)
                atualAimSprite.sprite = dictionaryAimSprites["aim_3"];
            if (IM.playerElemento == 1)
                atualAimSprite.sprite = dictionaryAimSprites["aim_1"];
            if (IM.playerElemento == 2)
                atualAimSprite.sprite = dictionaryAimSprites["aim_6"];
            if (IM.playerElemento == 3)
                atualAimSprite.sprite = dictionaryAimSprites["aim_5"];
            if (IM.playerElemento == 4)
                atualAimSprite.sprite = dictionaryAimSprites["aim_7"];
        }
        #endregion

        if (newAngle != angle)
            AimEffects.PlayerAim(IM.playerPrefix);


        transform.rotation = Quaternion.Euler(0, 0, angle); // rotation recebe index do angulo
    } // direção para onde a seta aponta


    float trucatedAngle8Directions(float angle)
    {
        List<float> angles = new List<float>(new float[] { 0, 45, 90, 135, 180, 225, 270, 315});

        var chooseAngleIndex = 0;

        float minDistance = float.MaxValue; // minDistance = infinito

        for(var i = 0; i < angles.Count; i++)
        {
            var result = Mathf.Abs(angles[i] - angle); // transforma a distancia entre o angulo selecionado e o proximo em positivo (0 - 45 = 45)

            if (result < minDistance)
            {
                minDistance = result; // recebe a distancia entre angulos
                chooseAngleIndex = i; 
            }
        }

        #region Last chooseAngleIndex
        if (IM.hForce == 0 && IM.vForce == 0 && PB.playerSprite.flipX)
            return angles[4];
        if (IM.hForce == 0 && IM.vForce == 0 && !PB.playerSprite.flipX)
            return angles[0];
        #endregion
        return angles[chooseAngleIndex];
    }

    void LoadDictionary()
    {
        aimSprites = Resources.LoadAll<Sprite>("Sprites/Player/aim");
        for (int i = 0; i < aimSprites.Length; i++)
            dictionaryAimSprites.Add(aimSprites[i].name, aimSprites[i]);
    } // add todos os sprites existentes no object "aim"
}
