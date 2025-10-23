using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] SO_Player _playerData;//SO Class
    [SerializeField] private Animator _playerLegAnimator;
    private Rigidbody2D _playerRb;
    private Vector3 _moveDirection;
    private Vector3 _rollDirection;
    private float _rollSpeed;
    private Animator _playerAnimator;
    private PlayerAttacking _playerAttacking;
    bool _isRunning = false;


    void Awake()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponentInChildren<Animator>();
        _playerAttacking = GetComponent<PlayerAttacking>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_moveDirection == Vector3.zero)
            {
                _rollDirection = transform.up;
            }
            else
            {
                _rollDirection = _moveDirection.normalized;
            }
            _rollSpeed = _playerData.PlayerSpeed * 2f;
        }
    }
    void FixedUpdate()
    {
        // Ruota il giocatore per guardare la posizione del mouse
        Vector2 _mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.up = _mousePosition - new Vector2(transform.position.x, transform.position.y);

        // Ottieni input di movimento
        _moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (_rollSpeed > 0)
        {
            ///Dodge();
        }
        else
        {
            // Calcola la velocità di corsa e applica la velocità normale
            float _speed = Run();
            _playerRb.velocity = _moveDirection.normalized * _speed;
        }

        // Aggiorna velocità animazione gambe basata sulla velocità
        _playerLegAnimator.SetFloat("Idle-Walk", _playerRb.velocity.magnitude);
    }


    public float Run()
    {
        float runningSpeed = _playerData.PlayerSpeed;
        _isRunning = Input.GetKey(KeyCode.LeftShift);
        if (_isRunning)
        {
            runningSpeed *= _playerData.PlayerRunningSpeed;
        }
        _playerLegAnimator.SetFloat("ToRun", _isRunning ? _playerRb.velocity.magnitude / (_playerData.PlayerSpeed * 2) : 0);
        return runningSpeed;
    }

    /// <summary>
    ///  public void Dodge()
    ///   {
    // Applica velocità di roll nella direzione di roll
    ///    _playerRb.velocity = _rollDirection * _rollSpeed;

    // Riduci linearmente la velocità di roll
    /// _rollSpeed = Mathf.Max(0, _rollSpeed - 20f * Time.deltaTime);
    // }
    /// </summary>



}
