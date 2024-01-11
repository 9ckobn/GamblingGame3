using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IEndDragHandler, IDragHandler
{
    private Vector3 spawnPoint;
    private Floor _currentFloor;
    [SerializeField] private Transform mainParent;

    public Vector3 startSize;
    public int MyHp;

    public bool AtFight = false;

    [SerializeField] private TextMeshProUGUI hpText;

    void Start()
    {
        startSize = transform.localScale;
        spawnPoint = transform.position;

        UpdateHp();
    }

    public void UpdateHp() => hpText.text = $"{MyHp}";

    public void OnDrag(PointerEventData eventData)
    {
        if (AtFight)
            return;

        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (AtFight)
            return;

        if (_currentFloor != null)
        {
            _currentFloor.EnterLayout(this);
            // spawnPoint = transform.position;//
        }
        else
        {
            transform.position = spawnPoint;
            transform.localScale = startSize;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Floor>(out var currentFloor))
        {
            _currentFloor = currentFloor;

            _currentFloor.SwitchFloorSprite(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<Floor>(out var currentFloor) && gameObject.activeInHierarchy)
        {
            _currentFloor = null;

            currentFloor.SwitchFloorSprite(false);
            currentFloor.ExitLayout();

            transform.SetParent(mainParent);
            transform.localScale = startSize;
        }
    }
}