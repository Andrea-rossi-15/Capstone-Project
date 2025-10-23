using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PauseUI : MonoBehaviour
{
    [SerializeField] Canvas _pauseUI;
    [SerializeField] Canvas _onDeathUI;
    [SerializeField] Canvas _onWinUI;
    private bool _isPause;

    public PlayerLifeController _PlayerController;


    void Awake()
    {
        _pauseUI.gameObject.SetActive(false);
    }
    void Update()
    {
        OpenOnDeathUI();
        OpenPauseUI();

    }

    public void ClosePauseUI()
    {
        _isPause = false;
        _pauseUI.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void OpenPauseUI()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _isPause == false)
        {
            _isPause = true;
            _pauseUI.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _isPause)
        {
            _isPause = false;
            _pauseUI.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void OpenOnDeathUI()
    {
        if (_PlayerController._isPlayerAlive == false)
        {
            _onDeathUI.gameObject.SetActive(true);
            SoundManager.PlaySound(SoundType.ONPLAYERDEATHUI);
        }
    }

    public void CloseOnWinUI()
    {
        _onWinUI.gameObject.SetActive(false);
    }
}
