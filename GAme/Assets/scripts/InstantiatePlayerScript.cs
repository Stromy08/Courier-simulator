using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePlayerScript : MonoBehaviour
{
    public GameObject playerPrefab;
    public void InstantiatePlayer()
    {
        Instantiate(playerPrefab, transform.position, transform.rotation);
    }
}
