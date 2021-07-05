using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//상점->게임플레이
public class Shopgohome : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            SceneManager.LoadScene("gameplay_last");
            DontDestroyOnLoad(other);
        }
    }

}
