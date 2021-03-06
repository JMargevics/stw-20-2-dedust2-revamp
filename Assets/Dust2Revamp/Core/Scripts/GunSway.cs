using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MLAPI;

public class GunSway : NetworkedBehaviour
{
    PlayerManager playerManger;

    public float swaySpeed = 10.0f;
    void Start()
    {
        playerManger = GetComponent<PlayerManager>();

        playerManger.gun.transform.position = playerManger.weaponSlot.transform.position;
    }

    private void Update()
    {
        float step = swaySpeed * Time.deltaTime;
    }

    void LateUpdate()
    {

        //playerManger.gun.transform.position = playerManger.weaponSlot.transform.position;
        //playerManger.gun.transform.rotation = playerManger.weaponSlot.transform.rotation;

        if(IsLocalPlayer)
            playerManger.gun.transform.position = Vector3.MoveTowards(playerManger.gun.transform.position, playerManger.weaponSlot.transform.position, swaySpeed);
    }
}
