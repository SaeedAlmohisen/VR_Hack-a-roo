using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkPlayer : MonoBehaviour
{

    private PhotonView photonView;
    private Transform headRig;
    private Transform leftHandRig;
    private Transform rightHandRig;

    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    public Animator leftHandAnimator;
    public Animator rightHandAnimator;

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        // Setup for user ownership
        photonView = GetComponent<PhotonView>();
        XRRig rig = FindObjectOfType<XRRig>();
        headRig = rig.transform.Find("Camera Offset/Camera");
        leftHandRig = rig.transform.Find("Camera Offset/LeftHand Controller");
        rightHandRig = rig.transform.Find("Camera Offset/RightHand Parent");

        UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.LeftHand), leftHandAnimator);
        UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.RightHand), rightHandAnimator);
    }

    // Update is called once per frame
    void Update()
    {
        // only shows and controls if the player owns the objects
        if (photonView.IsMine)
        {
            rightHand.gameObject.SetActive(false);
            leftHand.gameObject.SetActive(false);
            head.gameObject.SetActive(false);
            MapPosition(head, headRig);
            MapPosition(leftHand, leftHandRig);
            MapPosition(rightHand, rightHandRig);
        }
    }

    void UpdateHandAnimation(InputDevice targetDevice, Animator handAnimator)
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))

        {

            handAnimator.SetFloat("Trigger", triggerValue);

        }

        else

        {

            handAnimator.SetFloat("Trigger", 0);

        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))

        {

            handAnimator.SetFloat("Grip", gripValue);

        }

        else

        {

            handAnimator.SetFloat("Grip", 0);

        }

    }


    // network call to relay player positions
    void MapPosition(Transform target, Transform rigTransform)
    {

        target.position = rigTransform.position;
        target.rotation = rigTransform.rotation;
    }


}
