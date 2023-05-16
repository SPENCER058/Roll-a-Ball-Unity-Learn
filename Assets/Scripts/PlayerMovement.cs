using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed = 0.0f;
    [SerializeField] private int count;
    [SerializeField] private int maxPickable = 18;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private GameObject winTextObject;

    private Rigidbody thisRigidbody;
    private float movementX;
    private float movementY;

    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    public void OnMove (InputAction.CallbackContext context) {
        Vector2 movementVector = context.ReadValue<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    
    private void SetCountText () {
        countText.text = $"Count: {count.ToString()}" ;
        if (count >= maxPickable) {
            winTextObject.SetActive(true);
        }
    }

	private void FixedUpdate () {
		Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        thisRigidbody.AddForce (movement * speed);
	}

	private void OnTriggerEnter (Collider other) {
        if (other.CompareTag("PickUp")) {
            Destroy(other.gameObject);
            count++;

            SetCountText ();
		}
	}
}
