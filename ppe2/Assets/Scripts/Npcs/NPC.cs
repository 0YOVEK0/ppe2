using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject exclamationMark;
    public float activationDistance = 5f;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        exclamationMark.SetActive(false);
    }

    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < activationDistance)
        {
            exclamationMark.SetActive(true);
        }
        else
        {
            exclamationMark.SetActive(false);
        }
    }
}
