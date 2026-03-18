using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    public Transform destination;
    private bool canTeleport = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!canTeleport) return;

        if (other.CompareTag("Player"))
        {
            other.transform.position = destination.position;

            PortalTeleport otherPortal = destination.GetComponent<PortalTeleport>();

            if (otherPortal != null)
                otherPortal.DisableTeleportTemporarily();
        }
    }

    public void DisableTeleportTemporarily()
    {
        canTeleport = false;
        Invoke(nameof(EnableTeleport), 0.2f);
    }

    void EnableTeleport()
    {
        canTeleport = true;
    }
}