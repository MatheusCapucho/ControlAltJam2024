using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float _movementDuration = 1f;
    [Range(0, 3)][SerializeField] private float _startDelay = .5f;
    [SerializeField] private float _turnDelay = 1f;
    [SerializeField] private LayerMask _groundMask;
    private Vector3 _movePoint;
    private Vector3 _startingPoint;
    private Vector3 _lastPatrolPoint;
    private bool _movingRight = true;

    [SerializeField] private AudioClip _foundPlayer;
    [SerializeField] private AudioClip _distracted;

    private Tween moveTween;

    private void Awake()
    {
        _movePoint = transform.GetChild(0).position;
        _startingPoint = transform.position;
    }

    private void Start()
    {
        MoveEnemy(_movePoint.x, _startDelay);
    }

    private void OnEnable()
    {
        GameEvents.OnPlayerDetected += StopMove;
        GameEvents.OnEnemyDistracted += EnemyDistracted;
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerDetected -= StopMove;
        GameEvents.OnEnemyDistracted -= EnemyDistracted;

    }

    private void EnemyDistracted(float xPos)
    {
        transform.DOKill();
        _movingRight = !_movingRight;
        MoveEnemyToPebble(xPos, _turnDelay);
    }


    private void SetScale()
    {
        var scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
    }
    public void MoveEnemy(float xPoint, float delay)
    {
        _lastPatrolPoint = new Vector3(xPoint, transform.position.y, transform.position.z);
        moveTween = transform.DOMoveX(xPoint, _movementDuration, false).SetEase(Ease.OutFlash).SetDelay(delay).OnStart(() => SetScale()).OnComplete(() => {
            _movingRight = !_movingRight;
            _moving = true;
            if (_movingRight )
            {
                MoveEnemy(_movePoint.x, _turnDelay);
            }
            else
            {
                MoveEnemy(_startingPoint.x, _turnDelay);
            }
        });
    }

    private void MoveEnemyToPebble(float xPos, float turnDelay)
    {
        moveTween = transform.DOMoveX(xPos, _movementDuration, false).SetEase(Ease.OutFlash).SetDelay(turnDelay).OnComplete(() => {
            _movingRight = !_movingRight;
            _moving = true;
            SetScale();
            if (_movingRight)
            {
                MoveEnemy(_movePoint.x, _turnDelay);
            }
            else
            {
                MoveEnemy(_startingPoint.x, _turnDelay);
            }
        });
    }

    private bool _moving = true;
    private void Update()
    {
        if (!CheckGround() && _moving)
        {
            transform.DOKill();
            _moving = false;
            if (_movingRight)
            {
                MoveEnemy(_movePoint.x, _turnDelay);
            }
            else
            {
                MoveEnemy(_startingPoint.x, _turnDelay);
            }
        }
    }

    private bool CheckGround()
    {
        var hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, _groundMask);

        return hit ? true : false;

    }

    public void StopMove()
    {
        transform.DOKill();
    }
}
