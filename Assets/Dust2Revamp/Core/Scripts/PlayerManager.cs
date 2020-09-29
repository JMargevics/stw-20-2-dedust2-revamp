using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject gun;
    public Transform weaponSlot;
    [HideInInspector]
    public Animator animator;
    
    void Awake()
    {
        animator = gun.GetComponentInChildren<Animator>();
    }
}
