using System;
using System.Threading.Tasks;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Sequence = DG.Tweening.Sequence;


public class CardAnimation : MonoBehaviour
{
    private Sequence _sequence;
    private SpawnConfig _config;

    [SerializeField, Range(0, 1)] private float _moveSpeed;
    [SerializeField, Range(0, 1)] private float _appearSpeed;

    [Inject]
    private void Construct(SpawnConfig spawnConfig)
    {
        _config = spawnConfig;
    }

    public virtual void MoveTo(int x, int y)
    {
        var position = transform.position;
        var endPos = new Vector3(position.x + _config.WidthCard * x, position.y + _config.HeightCard * y, 0);
        
        _sequence = DOTween.Sequence();
        _sequence.Append(transform.DOMove(endPos, _moveSpeed));
    }

    public void Appear()
    {
        var sprite = gameObject.GetComponentInChildren<Image>();
        var color = sprite.color;
        sprite.color = Color.clear;
        
        _sequence = DOTween.Sequence();
        _sequence.Append(sprite.DOColor(color, _appearSpeed));
    }
    public async Task Death()
    {
        var sprite = gameObject.GetComponentInChildren<Image>();

        await sprite.DOColor(Color.clear, 0.2f)
            .AsyncWaitForKill();
        
        Destroy(gameObject);
    }
}
