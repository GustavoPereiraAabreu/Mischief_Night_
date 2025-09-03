using UnityEngine;

public class HideInside : MonoBehaviour
{
    [Header("Hide Settings")]
    [SerializeField] private KeyCode hideKey = KeyCode.E;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Rigidbody2D rb;

    [HideInInspector] public bool isHidden = false;

    private Transform currentHideSpot;
    private Vector3 lastPositionBeforeHide;

    public void Start()
    {
        if (sr == null) sr = GetComponent<SpriteRenderer>();
        if (rb == null) rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (currentHideSpot != null && Input.GetKeyDown(hideKey))
        {
            isHidden = !isHidden;

            if (isHidden)
            {
                lastPositionBeforeHide = rb.position;

                sr.color = new Color(1, 1, 1, 0.5f);
                rb.simulated = false;
                rb.position = currentHideSpot.position;

                Debug.Log("Player se escondeu no: " + currentHideSpot.name);
            }
            else
            {
                sr.color = Color.white;
                rb.simulated = true;


                float direction = (lastPositionBeforeHide.x < currentHideSpot.position.x) ? -1f : 1f;
                rb.position = currentHideSpot.position + Vector3.right * direction;

                Debug.Log("Player saiu do esconderijo: " + currentHideSpot.name);
            }
        }


        if (isHidden && currentHideSpot != null)
        {
            transform.position = currentHideSpot.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HideSpot"))
        {
            currentHideSpot = collision.transform;
            Debug.Log("Entrou na trigger do HideSpot: " + collision.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("HideSpot"))
        {
            if (!isHidden)
                currentHideSpot = null;

            Debug.Log("Saiu da trigger do HideSpot: " + collision.name);
        }
    }
}
