using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CoinProjectile : MonoBehaviour
{
    [SerializeField] private AnimationCurve _moveCurve;
    [SerializeField] private AnimationCurve _rotationCurve;
    [SerializeField] private float _rotationAngle;
    [SerializeField] private float _maxRadius;
    [SerializeField] private float _minRadius;
    [SerializeField] private float _maxHeight;
    [SerializeField] private Resources _resources;
    [SerializeField] private HitEffect _hitEffectPrefab;

    public int coinsAmount;

    private Vector3 _targetPosition;
    private void Start()
    {
        _resources = Singleton.Instance.Resources;
        
        float randomRadius = _minRadius + UnityEngine.Random.value * (_maxRadius - _minRadius);
        float randomAngle = UnityEngine.Random.value * 360f;

        float x = transform.position.x + (float)(randomRadius * Math.Sin(randomAngle));
        float z = transform.position.z + (float)(randomRadius * Math.Cos(randomAngle));
        float y = 0;

        _targetPosition = new Vector3(x, y, z);
        StartMoving();
    }
    public void StartMoving()
    {
        StartCoroutine(MoveToPoint(transform.position, _targetPosition));
    }

    public void Hit()
    {
        HitEffect hitEffect = Instantiate(_hitEffectPrefab, transform.position, Quaternion.identity);

        hitEffect.Init(coinsAmount);
        _resources.CollectCoins(coinsAmount, transform.position);
        Destroy(gameObject);
    }


    private IEnumerator MoveToPoint(Vector3 a, Vector3 b)
    {
        for (float t = 0; t < 1f; t += Time.deltaTime)
        {
            float yInterpolantRot = _rotationCurve.Evaluate(t);
            transform.rotation = Quaternion.Euler(transform.rotation.x, Mathf.LerpUnclamped(transform.rotation.y, _rotationAngle, yInterpolantRot), transform.rotation.z);
            
            float x = Mathf.Lerp(a.x, b.x, t);
            float yInterpolant = _moveCurve.Evaluate(t);
            float y = Mathf.LerpUnclamped(a.y, _maxHeight, yInterpolant);
            float z = Mathf.Lerp(a.z, b.z, t);

            Vector3 position = new Vector3(x, y, z);
            transform.position = position;
            yield return null;
        }
        transform.position = _targetPosition;
        transform.rotation = Quaternion.Euler(transform.rotation.x, _rotationAngle, transform.rotation.z); ;
    }
}
