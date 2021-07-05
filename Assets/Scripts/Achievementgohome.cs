using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//업적->게임플레이
public class Achievementgohome : MonoBehaviour
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
