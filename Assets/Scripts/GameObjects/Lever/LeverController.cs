using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] Sprite _leverSpriteUp;
    [SerializeField] GameObject _door;
    private bool _isActive;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _text.gameObject.SetActive(false);
    }

    void Update()
    {
        ActiveLever();
    }
    void ActiveLever()
    {
        if (_isActive && Input.GetKeyDown(KeyCode.E))
        {
            _spriteRenderer.sprite = _leverSpriteUp;
            this._door.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _text.gameObject.SetActive(true);
            _isActive = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _text.gameObject.SetActive(false);
            _isActive = false;
        }
    }
}
