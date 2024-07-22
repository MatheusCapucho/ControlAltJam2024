using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float _duration = 5f;
    private Vector3 _movePoint;
    private Vector3 _startingPoint;
    private bool _movingRight = true;

    private Tween moveTween;

    private void Awake()
    {
        _movePoint = transform.GetChild(0).position;
        _startingPoint = transform.position;
    }

    private void Start()
    {
        MoveEnemy(_movePoint.x);
    }
    public void MoveEnemy(float xPoint)
    {
        moveTween = transform.DOMoveX(xPoint, _duration, false).SetEase(Ease.Linear).OnComplete(() => {
            _movingRight = !_movingRight;
            var scale = transform.localScale;
            scale.x *= -1f;
            transform.localScale = scale;
            if (_movingRight )
            {
                MoveEnemy(_movePoint.x);
            }
            else
            {
                MoveEnemy(_startingPoint.x);
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
