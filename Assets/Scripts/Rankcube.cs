using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//�����÷���->��ŷ
public class Rankcube : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            SceneManager.LoadScene("Rank");
            DontDestroyOnLoad(other);
        }
    }
}
