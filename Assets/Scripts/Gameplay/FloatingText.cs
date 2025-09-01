using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _upwardsSpeed;
    [SerializeField] private TMP_Text _text;

    private Transform _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main.transform;
        Destroy(gameObject, _lifeTime);
    }

    public void SetText(string text)
    {
        _text.text = text;
    }

    private void LateUpdate()
    {
        if (_mainCamera != null)
        {
            transform.rotation = _mainCamera.rotation;
        }
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.up, _upwardsSpeed * Time.deltaTime);
    }
}