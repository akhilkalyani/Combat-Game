using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2;
    public float damage = 25f;
    public float range = 100f;
    public Camera cam;
    void OnEnable()
    {
        InputManager.onMove += Move;
        InputManager.onShoot += Shoot;
    }

    private void Shoot(string tag)
    {
        if (tag == "Player")
        {
            //shoot logic
            Debug.Log("Shooting.");
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, range))
            {
                // Enemy enemy = hit.collider.GetComponent<Enemy>();
                // if (enemy != null)
                // {
                //     enemy.TakeDamage(damage);
                // }

                Debug.DrawLine(ray.origin, hit.point, Color.red, 1f); // Visual debug
            }
        }
    }
    private void Move(string tag, Vector2 moveVector)
    {
        if (tag == "Player" && moveVector != Vector2.zero)
        {
            // Convert 2D input into 3D direction
            Vector3 direction = new Vector3(moveVector.x, 0, moveVector.y).normalized;

            // Rotate toward direction
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            targetRotation.x = 0;  // Lock pitch
            targetRotation.z = 0;  // Lock roll
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);

            // Move in the direction
            transform.position += -direction * moveSpeed * Time.deltaTime;
        }
    }
    void OnDisable()
    {
        InputManager.onMove -= Move;
        InputManager.onShoot -= Shoot;
    }
}
