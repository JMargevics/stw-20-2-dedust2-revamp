using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerManager : MonoBehaviour
{
    public GameObject gun;
    public Transform weaponSlot;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public AudioSource audioSource;
    
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = gun.GetComponentInChildren<Animator>();
    }
}
