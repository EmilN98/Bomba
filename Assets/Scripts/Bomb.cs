using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Collider bombCollider;
    public float explosionTime;
    public int explosionRadius;
    private float elapsedTime;
    private Collider ownerCollider;
    private bool isIgnoringOwnerCollider = true;

    public void SetOwner(GameObject gameObject)
    {
        ownerCollider = gameObject.GetComponent<Collider>();
        if (ownerCollider != null)
        {
            Physics.IgnoreCollision(bombCollider, ownerCollider, isIgnoringOwnerCollider);
        }
    }
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (isIgnoringOwnerCollider && ownerCollider != null)
        {
            if (!ownerCollider.bounds.Intersects(bombCollider.bounds))
            {
                Physics.IgnoreCollision(bombCollider, ownerCollider, false);
                isIgnoringOwnerCollider = false;
            }
        }
        if (elapsedTime >= explosionTime)
        {
            Explode();
            Destroy(gameObject);
        }
    }
    private void Explode()
    {
        float maxDistance = 2 * (explosionRadius - 1);
        //Front
        RaycastHit hit;
        if (Physics.BoxCast(transform.position, Vector3.one, transform.forward, out hit, Quaternion.identity, maxDistance))
        {
            if (hit.collider.CompareTag("Destructible") ||
                hit.collider.GetComponent<PlayerController>() != null)
            {
                Debug.Log("Front hit: " + hit.collider.gameObject.name);
            }
        }
        //Back
        if (Physics.BoxCast(transform.position, Vector3.one, -transform.forward, out hit, Quaternion.identity, maxDistance))
        {
            if (hit.collider.CompareTag("Destructible") ||
                hit.collider.GetComponent<PlayerController>() != null)
            {
                Debug.Log("Back hit: " + hit.collider.gameObject.name);
            }
        }
        //Left
        if (Physics.BoxCast(transform.position, Vector3.one, -transform.right, out hit, Quaternion.identity, maxDistance))
        {
            if (hit.collider.CompareTag("Destructible") ||
                hit.collider.GetComponent<PlayerController>() != null)
            {
                Debug.Log("Left hit: " + hit.collider.gameObject.name);
            }
        }
        //Right
        if (Physics.BoxCast(transform.position, Vector3.one, transform.right, out hit, Quaternion.identity, maxDistance))
        {
            if (hit.collider.CompareTag("Destructible") ||
                hit.collider.GetComponent<PlayerController>() != null)
            {
                Debug.Log("Right hit: " + hit.collider.gameObject.name);
            }
        }
    }
}