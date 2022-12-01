using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot8Direction : MonoBehaviour {

    private InputManager IM;
    private PlayerBehaviour PB;
    float angle;

    // Use this for initialization
    void Start () {
        IM = GetComponentInParent<InputManager>();
        PB = GetComponent<PlayerBehaviour>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        DirectionsShot();
    }

    void DirectionsShot()
    {
        angle = Mathf.Atan2(IM.vForce, IM.hForce) * Mathf.Rad2Deg; // cateto oposto / cateto adjacente * 57.296 = angulo em graus

        if (angle < 0) angle = 360 + angle; // sempre fica dentro dos quadrantes trigonométricos

        angle = trucatedAngle8Directions(angle);

        transform.rotation = Quaternion.Euler(0, 0, angle); // rotation recebe index do angulo
    } // direção para onde a seta aponta


    float trucatedAngle8Directions(float angle)
    {
        List<float> angles = new List<float>(new float[] { 0, 45, 90, 135, 180, 225, 270, 315, 360 });

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

        //#region Last Position
        if (IM.hForce == 0 && IM.vForce == 0 && PB.playerSprite.flipX)
            return angles[0];
        if (IM.hForce == 0 && IM.vForce == 0 && !PB.playerSprite.flipX)
            return angles[4];
       // #endregion
        return angles[chooseAngleIndex];

        AimEffects.PlayerAim(IM.playerPrefix);
    }
}
