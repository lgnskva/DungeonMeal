using UnityEngine;
using UnityEngine.EventSystems;

public class Swipe : MonoBehaviour, IDragHandler, IEndDragHandler
{
    Player player;
    public float delta;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
            player.Swipe(1);
        if (Input.GetKeyDown(KeyCode.A))
            player.Swipe(2);
        if (Input.GetKeyDown(KeyCode.W))
            player.Swipe(3);
        if (Input.GetKeyDown(KeyCode.S))
            player.Swipe(4);
    }

    public void OnEndDrag(PointerEventData data)
    {
        Vector2 dir = (data.position - data.pressPosition).normalized;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            if (dir.x > 0)
                player.Swipe(1);
            else
                player.Swipe(2);
        }
        else
        {
            if (dir.y > 0)
                player.Swipe(3);
            else
                player.Swipe(4);
        }
    }
    public void OnDrag(PointerEventData eventData)
    {

    }
}
