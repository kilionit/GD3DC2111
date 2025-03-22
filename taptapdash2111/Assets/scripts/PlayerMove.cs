using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float _speed;

    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravityScale;

    private bool _isGround;
    private bool _isJump;

    private Vector3 _movement; //ñìåùåíèå â ìèðîâûõ êîðäèíàòàõ

    private Vector3 _previosPosition;
    private float _velosity;
    private float _rotationX;

    private void Start()
    {
        _previosPosition = transform.position;
        _rotationX = transform.rotation.x;
        _isJump = false;
        _rb = GetComponent<Rigidbody>();
        _movement = Vector3.forward;  //íàñòðîéêà ñìåùåíèÿ äëÿ äâèæåíèÿ âïåðåä
    }


    private void FixedUpdate()
    {
        _velosity = (_previosPosition - transform.position).magnitude / Time.fixedDeltaTime;
        Debug.Log(_velosity);
        _previosPosition = transform.position;
        //óñòàíîâèòü ïîçèöèþ ãåðîÿ  ò.å. òåêóùåå ïîëîæåíèå + ñìåùåíèå óìíîæåííîå íà ñêîðîñòü è íà èçìåíåíèå âðåìåíè
        _rb.MovePosition(transform.position + _movement * _speed * Time.fixedDeltaTime);

        if (_isJump)
        {
            _isJump = false;
            //_rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _movement.y = _jumpForce;
        }

        if (!_isGround)
        {
            _movement.y -= _gravityScale * Time.fixedDeltaTime;
        }

        if (_isGround)
        {
            _movement.y = 0;
        }
        if (_velosity < 1 && transform.position.z >=0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && _isGround)
        {
            _isGround = false;
            _isJump = true;
            StartCoroutine(rotateCube());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            _isGround = true;
        }

        if (collision.gameObject.tag == "kill")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    IEnumerator rotateCube()
    {
        for (int i = 0; i < 90; i++)
        {
            _rotationX += 1;
            transform.rotation = Quaternion.Euler(_rotationX, 0, 0);
            yield return new WaitForSeconds(0.0005f);
        }
    }
}