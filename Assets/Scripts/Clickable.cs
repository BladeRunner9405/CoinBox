using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(BoxCollider))]
public class Clickable : MonoBehaviour
{

    [SerializeField] private AnimationCurve _scaleCurve;
    [SerializeField] private float _scaleTime = 0.25f;
    [SerializeField] private HitEffect _hitEffectPrefab;
    [SerializeField] private CoinProjectile _projectilePrefab;
    [SerializeField] private Resources _resources;
    [SerializeField] private Transform _NamePosition;

    private BoxCollider _collider;

    private int _coinsPerClick = 1;
    private void Start()
    {
        _collider = GetComponent<BoxCollider>();
    }

    // Метод вызывается из Interaction при клике на объект
    public void Hit()
    {
        // HitEffect hitEffect = Instantiate(_hitEffectPrefab, transform.position, Quaternion.identity);
        CoinProjectile projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
        // hitEffect.Init(_coinsPerClick);
        // _resources.CollectCoins(1, transform.position);
        StartCoroutine(HitAnimation());
    }

    // Анимация колебания куба
    private IEnumerator HitAnimation()
    {
        for (float t = 0; t < 1f; t += Time.deltaTime / _scaleTime)
        {
            float scale = _scaleCurve.Evaluate(t);
            transform.localScale = Vector3.one * scale;
            yield return null;
        }
        transform.localScale = Vector3.one;
    }

    // Этот метод увеличивает количество монет, получаемой при клике
    public void AddCoinsPerClick(int value)
    {
        _coinsPerClick += value;
    }

    public void SetColliderScale(float value)
    {
        _collider.size = Vector3.one * value;
        _collider.center = new Vector3(0, value / 2, 0);
        _NamePosition.position = new Vector3(_NamePosition.position.x, value + 1, _NamePosition.position.z);
    }

}
