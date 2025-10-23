using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winning : MonoBehaviour
{
    [SerializeField] Canvas _onWinUI;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _onWinUI.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

}

