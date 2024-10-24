using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CharacterController _charControl;
    GroundCheck _groundCheck;

    [SerializeField] float _moveSpeed = 3.5f;
    [SerializeField] float _gravity = 10;
    [SerializeField] float _jumpPower = 5;

    [SerializeField] InputController _inputController;
    AnimController _animController;

    float _inputH;
    float _inputV;
    [SerializeField] Vector3 _moveDir;
    public Vector3 _MoveDir => _moveDir;

    float _speedBuff;
    float _passiveSpeed;
    float _finalSpeed;
    public float _FinalSpeed => _finalSpeed;

    public bool _canMove = true;

    private void Awake()
    {
        _charControl = GetComponent<CharacterController>();
        _animController = GetComponent<AnimController>();
        _groundCheck = GetComponent<GroundCheck>();

        _finalSpeed = _moveSpeed;
    }
    private void Update()
    {
        if(_canMove)
        {
            MovePlayer();
        }
        
        if (Input.GetKeyDown(KeyCode.K))
        Debug.Log((1 + _speedBuff + _passiveSpeed));
        
    }

    private void MovePlayer()
    {
        if (_groundCheck.IsGrounded())
        {
            _inputH = _inputController._inputDir.x;
            _inputV = _inputController._inputDir.y;

            _moveDir = new Vector3(_inputH, 0, _inputV);

            if (_moveDir != Vector3.zero)
            {
                transform.rotation = Quaternion.Euler(0, Mathf.Atan2(_inputH, _inputV) * Mathf.Rad2Deg, 0);
            }
        }
        else
        {
            ApplyGravity();
        }

        _animController._moveSpeed = _moveDir.magnitude * _moveSpeed;

        _finalSpeed = _moveSpeed * (1 + _speedBuff + _passiveSpeed);
        _charControl.Move(_moveDir * _finalSpeed * Time.deltaTime);
    }

    private void ApplyGravity() { _moveDir += Vector3.down * _gravity * Time.deltaTime; }
    public void UpdateBuffSpeed(float moveSpeed)
    {
        _speedBuff = moveSpeed;
    }
    public void UpdatePassiveSpeed(float moveSpeed)
    {
        _passiveSpeed = moveSpeed;
    }
    void Move(Vector3 dir)
    {
        _charControl.Move(dir * Time.deltaTime);
    }
    public void Dash(float time, float speed)
    {
        StartCoroutine(CRT_Dash(time, speed));
    }
    public void Dash(Vector3 targetPos, float speed, int combo)
    {
        StartCoroutine(CRT_Dash(targetPos, speed, combo));
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
    IEnumerator CRT_Dash(Vector3 targetPos, float speed, int combo)
    {
        _canMove = false;
        _animController.SetChiseAnimation(combo);
        while (Vector3.Distance(transform.position, targetPos) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }
        _canMove = true;
    }

}
