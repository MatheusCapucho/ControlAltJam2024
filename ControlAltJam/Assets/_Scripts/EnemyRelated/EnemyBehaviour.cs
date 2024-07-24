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
    private Vector3 _movePoint;
    private Vector3 _startingPoint;
    private Vector3 _lastPatrolPoint;
    private bool _movingRight = true;

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
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerDetected -= StopMove;
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

    public void ForceEnemyPosition(float xPosition)
    {

    }

    public void StopMove()
    {
        transform.DOKill();
    }
}
