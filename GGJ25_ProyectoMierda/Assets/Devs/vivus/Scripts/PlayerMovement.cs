using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f; 
    public float sprintMultiplier = 1.5f; 
    public float gravity = -9.8f; 

    [Header("Camera Settings")]
    public float mouseSensitivity = 2f; 
    public float maxVerticalAngle = 85f; 

    private Vector3 velocity; 
    private Transform cameraTransform;
    private float verticalRotation = 0f; 
    private bool intro = false;

    [SerializeField] public AudioClip deathSound;
    private AudioSource audioSource;

    [SerializeField] private float _currentLife = 1000.0f;
    [SerializeField] private float _maxLife = 1000.0f;
    [SerializeField] private int coins = 0;

    [SerializeField] private float _healTime = 5.0f;
    private float _healTimer;
    [SerializeField] private float _healPower = 1.0f;

    [SerializeField] private HUDController _hud;

    public float Health { get { return _currentLife; } }

    // ANIMATIONS
    [SerializeField] GameObject _currentWeapon;
    [SerializeField] GameObject[] _weapons;

    private CharacterController characterController; 

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        audioSource = GetComponent<AudioSource>();
        coins = 0;
        _healTimer = _healTime;

        // Cambia el arma actual
        _currentWeapon = _weapons[0];

        // Asegurarse de que el CharacterController está presente
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            characterController = gameObject.AddComponent<CharacterController>();
        }
    }

    void Update()
    {
        if (intro)
        {
            HandleMovement();
            HandleMouseLook();
        }

        if (_healTimer <= 0)
        {
            Heal(_healPower);
        }
        else
        {
            _healTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.X) && mouseSensitivity <= 20f)
        {
            mouseSensitivity += 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.Z) && mouseSensitivity >= 0.0001f)
        {
            mouseSensitivity -= 0.1f;
        }
    }

    public void HandleMovement()
    {
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        
        Vector3 direction = (transform.right * horizontal + transform.forward * vertical).normalized;


        if (direction.magnitude < 0.1f)
        {
            velocity = Vector3.zero;
        }
        else
        {
            // Aplicamos velocidad base
            float speed = moveSpeed;
            
           
            velocity = direction * speed;
        }

    
        if (characterController.isGrounded)
        {
            velocity.y = -1f; // Un valor pequeño para mantenerlo pegado al suelo

        }
        else
        {
            // Si no está en el suelo, aplicamos la gravedad
            velocity.y += gravity * Time.deltaTime;
        }

        characterController.Move(velocity * Time.deltaTime);
    }

    public void HandleMouseLook()
    {
          // Capturamos el input del ratón
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotamos horizontalmente el personaje
        transform.Rotate(Vector3.up * mouseX * mouseSensitivity);

        // Rotamos verticalmente la cámara
        verticalRotation -= mouseY * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -maxVerticalAngle, maxVerticalAngle);

        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

    public void IntroDone()
    {
        intro = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ImproveSpeed(float incr)
    {

        moveSpeed += incr;
  
    }

    public void ChangeWeapon(int i)
    {
        // Desactiva el arma actual
        _currentWeapon.SetActive(false);
    }

    private void Hit(float damage)
    {
        _healTimer = _healTime;
        _currentLife -= damage;

        if (_maxLife * 0.75f >= _currentLife && _currentLife > _maxLife * 0.5f)
        {
            _hud.UpateSplash(1, true);
        }
        else if (_maxLife * 0.5f >= _currentLife && _currentLife > _maxLife * 0.25f)
        {
            _hud.UpateSplash(2, true);
        }
        else if (_maxLife * 0.25f >= _currentLife && _currentLife > _maxLife * 0.1f)
        {
            _hud.UpateSplash(3, true);
        }
        else if (_maxLife * 0.1f >= _currentLife)
        {
            _hud.UpateSplash(4, true);
        }

        if (_currentLife <= 0)
        {
            PlayerDies();
        }
    }

    private void Heal(float incr)
    {
        if (_currentLife + incr >= _maxLife)
        {
            _currentLife = _maxLife;
        }
        else
        {
            _currentLife += incr;
        }

        if (_maxLife * 0.1f <= _currentLife && _currentLife < _maxLife * 0.25f)
        {
            _hud.UpateSplash(4, false);
        }
        else if (_maxLife * 0.25f <= _currentLife && _currentLife < _maxLife * 0.5f)
        {
            _hud.UpateSplash(3, false);
        }
        else if (_maxLife * 0.5f <= _currentLife && _currentLife < _maxLife * 0.75f)
        {
            _hud.UpateSplash(2, false);
        }
        else if (_maxLife * 0.75f <= _currentLife)
        {
            _hud.UpateSplash(1, false);
        }
    }

    public void ImproveMaxLife(float incr)
    {
        _maxLife += incr;
    }

    void PlayerDies()
    {
        audioSource.PlayOneShot(deathSound);
        GameManager.Instance.EndGame();
    }

    public void AddCoins(int nCoins)
    {
        coins += nCoins;
    }

    public void SetCoins(int nCoins)
    {
        coins = nCoins;
    }

    public void SubCoins(int nCoins)
    {
        coins -= nCoins;
    }

    public int GetCoins()
    {
        return coins;
    }

    private void OnCollisionStay(Collision other)
    {
        GameObject otherObject = other.gameObject;

        if (otherObject.layer == 7)
        {
            Hit(otherObject.GetComponent<CacaComponent>().Damage);
            Destroy(otherObject);
        }
        else if (otherObject.layer == 9)
        {
            if (otherObject.GetComponent<Enemy>()._currentHealth > 0) Hit(otherObject.GetComponent<Enemy>()._damage);
        }
    }
}
