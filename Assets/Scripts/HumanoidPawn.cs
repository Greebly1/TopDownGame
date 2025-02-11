using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;
using UnityEngine.Animations;
using System.Security.Cryptography;

/// <summary>
/// The 
/// </summary>
public class HumanoidPawn : Pawn, IHolder, IRagdoll
{
    #region vars

    [SerializeField] GameObject heldObject = null;
    [SerializeField] GameObject leftHandIKTarget = null;
    [SerializeField] GameObject rightHandIKTarget = null;
    [SerializeField] GameObject heldObjectEmpty = null;
    [SerializeField] BlendableRig rightHandIK = null;
    [SerializeField] BlendableRig leftHandIK = null;

    IHoldable heldItem { get { return heldObject.GetComponentInChildren<IHoldable>(); } }

    bool sprinting = false;
    public bool Sprinting
    {
        get { return sprinting; }
    }

    public bool isSprinting
    {
        get { return currSpeed > 0.67f; }
    }

    bool crouched = false;
    #endregion

    #region Input Functions
    public void toggleSprint(bool isButtonHeld)
    {
        if (!takingInput) { return; } //ragdolling or another mechanic can disabled input

        if (!isButtonHeld && crouched) { 
            toggleCrouch();
            return; } // early out

        if (!isButtonHeld && !sprinting)
        {
            return; // early out
        }

        sprinting = !sprinting;
        //Debug.Log("Sprinting is :" + sprinting + ", button is held is: " + isButtonHeld);
        //if (!isSprintBlending) { sprintBlending = StartCoroutine("SprintToggleTimer"); }
        if (sprinting && crouched) { crouched = false; }

        UpdateAnimator();
    }

    public void toggleCrouch()
    {
        if (!takingInput) { return; } //ragdolling or another mechanic can disabled input

        crouched = !crouched;
        sprinting = false;

        UpdateAnimator();
    }

    //TODO: extract some attack logic into the base pawn class so nonhumanoid pawns can still attack
    public void PrimaryAction(bool isInitiating)
    {
        if (!takingInput) { return; } //ragdolling or another mechanic can disabled input

        try
        {
            (heldItem as Weapon)?.Action_Primary(isInitiating);
        } catch
        {
            //we either 1: are not holding an item or
            //          2: the item we are holding is not a weapon
            //TODO: make default attack functionality for one handed or two handed items, and default unarmed attack functionality
        }
    }

    public void SecondaryAction(bool isInitiating)
    {
        if (!takingInput) { return; } //ragdolling or another mechanic can disabled input

        try
        {
            (heldItem as Weapon)?.Action_Secondary(isInitiating);
        }
        catch
        {
            //we either 1: are not holding an item or
            //          2: the item we are holding is not a weapon
            //TODO: make default attack functionality for one handed or two handed items, and default unarmed attack functionality
        }
    }

    //I do not know what this function was meant to be in the past
    public void FocusItem(bool isInitiating)
    {
        try
        {
            //depending on isInitiating

            //play an animation to set up the aim animation layers
            //play an animation to raise the weapon
        }
        catch
        {
            //we either 1: are not holding an item or
            //          2: the item we are holding is not a weapon
        }
    }

    #endregion

    private void Start()
    {
        if (heldObject != null)
        {
            HoldObject(heldObject, destroyHeld: false);
        }

        isRagdoll = isRagdoll; //initializes the ragdoll via a setter
    }

    #region Pawn class overrides
    protected override void UpdateAnimator()
    {
        if (AnimationController == null) return; //don't do anything, if there is no animation controller the pawn is likely dead or disabled,

        base.UpdateAnimator(); //support base animation controller params from parent class

        //Humanoid skeletal mesh animation controllers supports crouching and sprinting input
        AnimationController.SetBool("Crouching", crouched);
        AnimationController.SetBool("Sprinting", sprinting);
        //Debug.Log(lookAtTarget);
        AnimationController.SetBool("Aiming", lookAtTarget);
    }

    public override void ResetInput()
    {
        base.ResetInput();
        crouched = false;
        sprinting = false;
        lookAtTarget = false;
        PrimaryAction(false);
        SecondaryAction(false);
    }

    public override void ResetAnimator()
    {
        base.ResetAnimator();
        AnimationController.SetBool("Aiming", false);
        AnimationController.SetBool("Crouching", false);
        AnimationController.SetBool("Sprinting", false);
    }
    #endregion


    #region IHolder Interface
    public GameObject leftHand { get => leftHandIKTarget;  }
    public GameObject rightHand { get => rightHandIKTarget; }

    public bool isHoldingObject { get => heldObject != null; }

    public GameObject heldObjectEmptyTarget { get => heldObjectEmpty; }

    public void HoldObject(GameObject obj, bool destroyHeld)
    {
        if (heldObject != null && destroyHeld) { Destroy(heldObject); }

        heldObject = obj;
        IHoldable holdableObj = obj.GetComponent<IHoldable>();
        if (holdableObj != null)
        {
            //First get the hand targeters
            Targeter rightHandTargeter = rightHand.GetComponent<Targeter>();
            Targeter leftHandTargeter = leftHand.GetComponent<Targeter>();
            if (leftHandTargeter == null || rightHandTargeter == null) { Debug.LogError("The hands of this humanoid pawn do not contain targeting components"); return; } //early out, it failed
        
            //Second, get the rig layers
            //These are blendable rigs, which is just a helper class I made that extends the normal rig class with a SmoothdampLayerWeight function
            BlendableRig rightHandIKPosition = rightHand.GetComponent<BlendableRig>();
            BlendableRig leftHandIKPosition = leftHand.GetComponent<BlendableRig>();
            if (rightHandIKPosition == null || leftHandIKPosition == null) { Debug.LogError("The hands of this humanoid pawn do not contain blendable rigs"); return; } //early out, it failed

            //Set the position of the targeters
            rightHandTargeter.setTarget(holdableObj.handPos_Left.transform, follow: true); //!!! these targeting positions are flipped, but they work
            leftHandTargeter.setTarget(holdableObj.handPos_Right.transform, follow: true);

            //tell the rigs to smoothdamp the layer weights up to 1
            rightHandIKPosition.SmoothdampLayerWeight(1);
            leftHandIKPosition.SmoothdampLayerWeight(1);
            rightHandIK.SmoothdampLayerWeight(1);
            leftHandIK.SmoothdampLayerWeight(1);

            //Finally, move the object to the held object mount, and parent it to that
            obj.transform.SetParent(heldObjectEmptyTarget.transform, worldPositionStays: false);
            obj.transform.localPosition = Vector3.zero;

            holdableObj.setTeam(team);
        } else
        {
            Debug.Log("Tried to hold an object that doesn't have an IHoldable interface");
        }
    }

    public void StopHoldingObject()
    {
        

        //get the rig layers
        //These are blendable rigs, which is just a helper class I made that extends the normal rig class with a SmoothdampLayerWeight function
        BlendableRig rightHandIKPosition = rightHand.GetComponent<BlendableRig>();
        BlendableRig leftHandIKPosition = leftHand.GetComponent<BlendableRig>();
        if (rightHandIKPosition == null || leftHandIKPosition == null) { Debug.LogError("The hands of this humanoid pawn do not contain blendable rigs"); return; } //early out, it failed

        //set the layer weights to 0
        rightHandIKPosition.SmoothdampLayerWeight(0);
        leftHandIKPosition.SmoothdampLayerWeight(0);
        rightHandIK.SmoothdampLayerWeight(0);
        leftHandIK.SmoothdampLayerWeight(0);

        //for now destroy the object when we are done
        Destroy(heldObject);
        heldObject = null;
    }

    #endregion

    #region IRagdoll Interface

    [SerializeField]
    GameObject rootBone;

    bool _isRagdoll = false;

    public bool isRagdoll {
        get => _isRagdoll;
        set {
            _isRagdoll = value;
            if(_isRagdoll)
            {
                EnableRagdoll();
            } else { DisableRagdoll(); }
        }
    }

    public void EnableRagdoll()
    {
        //reset all input; stop fire guns, uncrouch, stop sprinting, stop moving, etc
        ResetInput();
        ResetAnimator();

        StopHoldingObject(); //let go of the object we were holding

        //turn off animator
        AnimationController.enabled = false;

        RagdollUtility.EnableRigPhysics(rootBone);
        
    }

    public void DisableRagdoll()
    {
        RagdollUtility.DisableRigPhysics(rootBone);
        //turn on animator
        AnimationController.enabled = true;
    }


    #endregion
}

