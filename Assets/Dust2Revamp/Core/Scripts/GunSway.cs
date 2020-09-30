using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GunSway : MonoBehaviour
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

        playerManger.gun.transform.position = Vector3.MoveTowards(playerManger.gun.transform.position, playerManger.weaponSlot.transform.position, swaySpeed);
    }
}
