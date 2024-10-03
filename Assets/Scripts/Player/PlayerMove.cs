using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CharacterController _charControl;
    GroundCheck _groundCheck;

    [SerializeField] float _moveSpeed = 5;
    [SerializeField] float _gravity = 10;
    [SerializeField] float _jumpPower = 5;

    [SerializeField] InputController _inputController;
    AnimController _animController;

    float _inputH;
    float _inputV;
    public Vector3 _moveDir;

    int _speedIncrease;

    bool _canMove = true;
    private void Awake()
    {
        _charControl = GetComponent<CharacterController>();
        _animController = GetComponent<AnimController>();
        _groundCheck = GetComponent<GroundCheck>();
    }
    private void Update()
    {
        if(_canMove)
            MovePlayer();
    }

    private void MovePlayer()
    {
        if (_groundCheck.IsGrounded())
        {
            _inputH = _inputController._inputDir.x;
            _inputV = _inputController._inputDir.y;

            _moveDir = new Vector3(_inputH, 0, _inputV) * _moveSpeed;

            if (_moveDir != Vector3.zero)
                transform.rotation = Quaternion.Euler(0, Mathf.Atan2(_inputH, _inputV) * Mathf.Rad2Deg, 0);

            if (Input.GetKeyDown(KeyCode.Space))
                _moveDir.y += _jumpPower;
        }
        else
            ApplyGravity();

        _animController._moveSpeed = _moveDir.magnitude;


        _charControl.Move(_moveDir * Time.deltaTime);
    }

    private void ApplyGravity() { _moveDir += Vector3.down * _gravity * Time.deltaTime; }
    public void UpdatePassiveStat()
    {
        _speedIncrease += 5;
    }
    void Move(Vector3 dir)
    {
        _charControl.Move(dir * Time.deltaTime);
    }
    public void Dash(float time, float speed)
    {
        StartCoroutine(CRT_Dash(time, speed));
    }
    IEnumerator CRT_Dash(float time, float speed)
    {
        _canMove = false;
        float timer = 0;
        while(timer < time)
        {
            timer += Time.deltaTime;
            Move(transform.forward * speed);
            yield return null;
        }
        _canMove = true;
    }

}
