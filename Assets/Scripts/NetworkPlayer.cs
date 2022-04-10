using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkPlayer : NetworkBehaviour
{
    [SerializeField]
    private Vector3 placementArea = new Vector3(0f, 0f, 0f);

    public override void OnNetworkSpawn() => DisableClientInput();

    private void DisableClientInput()
    {
        if (IsClient && !IsOwner)
        {
            var moveProvider = GetComponent<NetworkMoveProvider>();
            var controllers = GetComponentsInChildren<ActionBasedController>();
            var turnProvider = GetComponent<ActionBasedSnapTurnProvider>();
            var head = GetComponentInChildren<TrackedPoseDriver>();
            var camera = GetComponentInChildren<Camera>();

            camera.enabled = false;
            moveProvider.enableInputActions = false;
            turnProvider.enableTurnAround = false;
            turnProvider.enableTurnLeftRight = false;
            head.enabled = false;

            foreach (var input in controllers)
            {
                input.enableInputActions = false;
                input.enableInputTracking = false;
            }
        }
    }

    private void Start()
    {
        if (IsClient || IsOwner)
        {
            transform.position = new Vector3(placementArea.x, placementArea.y, placementArea.z);
        }
    }

    public void OnSelectGrabbable(SelectEnterEventArgs eventArgs)
    {
        if (IsClient && IsOwner)
        {
            NetworkObject networkObjectSelected = eventArgs.interactableObject.transform.GetComponent<NetworkObject>();
            if (networkObjectSelected != null)
                RequestGrabbableOwnershipServerRpc(OwnerClientId, networkObjectSelected);
        }
    }

    [ServerRpc]
    public void RequestGrabbableOwnershipServerRpc(ulong newOwnerClientId, NetworkObjectReference networkObjectReference)
    {
        if (networkObjectReference.TryGet(out NetworkObject networkObject))
        {
            networkObject.ChangeOwnership(newOwnerClientId);
        }
    }
}