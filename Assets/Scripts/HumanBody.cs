﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBody : MonoBehaviour
{
    [Header("--- Settings ---")]
    [SerializeField] private float rotationSpeed = 5;
    [SerializeField] private float subbaseMoveSpeed = 5;

    [Header("--- Components ---")]
    [SerializeField] private GameObject m_BodyBase;

    [Header("--- Targets ---")]
    [SerializeField] private GameObject m_LeftArmTarget;
    [SerializeField] private GameObject m_RightArmTarget;
    [SerializeField] private GameObject m_LeftLegTarget;
    [SerializeField] private GameObject m_RightLegTarget;
    [SerializeField] private GameObject m_HeadTarget;

    [Header("--- Upper Chain ---")]
    [SerializeField] private GameObject m_upperChainSubbase;
    [SerializeField] private List<GameObject> m_upperChain = new List<GameObject>();
    [SerializeField] private bool _upperChainLocked = true;
    [SerializeField] private bool _upperChainConstraints = false;
    private FABRIKv3 _upperChainFabrik = new FABRIKv3();

    [Header("--- Left Arm ---")]
    [SerializeField] private List<GameObject> m_leftArm = new List<GameObject>();
    [SerializeField] private GameObject m_leftArmRestPoint;
    [SerializeField] private bool _leftArmLocked = true;
    [SerializeField] private bool _leftArmRest = false;
    [SerializeField] private bool _leftArmConstraints = false;
    private FABRIKv3 _leftArmFabrik = new FABRIKv3();

    [Header("--- Right Arm ---")]
    [SerializeField] private List<GameObject> m_rightArm = new List<GameObject>();
    [SerializeField] private GameObject m_rightArmRestPoint;
    [SerializeField] private bool _rightArmLocked = true;
    [SerializeField] private bool _rightArmRest = false;
    [SerializeField] private bool _rightArmConstraints = false;
    private FABRIKv3 _rightArmFabrik = new FABRIKv3();

    [Header("--- Head ---")]
    [SerializeField] private List<GameObject> m_Head = new List<GameObject>();
    [SerializeField] private GameObject m_headRestPoint;
    [SerializeField] private bool _headLocked = true;
    [SerializeField] private bool _headRest = false;
    [SerializeField] private bool _headConstraints = false;
    private FABRIKv3 _headFabrik = new FABRIKv3();

    [Header("--- Lower Chain ---")]
    [SerializeField] private GameObject m_lowerChainSubbase;
    [SerializeField] private List<GameObject> m_lowerChain = new List<GameObject>();
    [SerializeField] private bool _lowerChainLocked = true;
    [SerializeField] private bool _lowerChainConstraints = false;
    private FABRIKv3 _lowerChainFabrik = new FABRIKv3();


    [Header("--- Left Leg ---")]
    [SerializeField] private List<GameObject> m_leftLeg = new List<GameObject>();
    [SerializeField] private GameObject m_leftLegRestPoint;
    [SerializeField] private bool _leftLegLocked = true;
    [SerializeField] private bool _leftLegRest = false;
    [SerializeField] private bool _leftLegConstraints = false;
    private FABRIKv3 _leftLegFabrik = new FABRIKv3();

    [Header("--- Right Leg ---")]
    [SerializeField] private List<GameObject> m_rightLeg = new List<GameObject>();
    [SerializeField] private GameObject m_rightLegRestPoint;
    [SerializeField] private bool _rightLegLocked = true;
    [SerializeField] private bool _rightLegRest = false;
    [SerializeField] private bool _rightLegConstraints = false;
    private FABRIKv3 _rightLegFabrik = new FABRIKv3();

    [Header("~~~~ DEBUG ~~~~")]
    [SerializeField] private bool _doFabrikForwardsOnce = false;
    [SerializeField] private bool _doFabrikForwardsContinously = false;

    [SerializeField] private bool _doFabrikBackwardsOnce = false;
    [SerializeField] private bool _doFabrikBackwardsContinously = false;

    [SerializeField] private bool _LeftArmDoFabrikBackwardsOnce = false;
    [SerializeField] private bool _LeftArmDoFabrikForwardsOnce = false;

    [Header("~~~~ Modes ~~~~")]
    [SerializeField] private bool _fivePointStreched = false;



    private void Start()
    {
        _headFabrik.SetupFabrik(m_Head, m_upperChain[m_upperChain.Count - 1], m_HeadTarget, m_headRestPoint);

        _leftArmFabrik.SetupFabrik(m_leftArm, m_upperChain[m_upperChain.Count - 1], m_LeftArmTarget, m_leftArmRestPoint);
        _rightArmFabrik.SetupFabrik(m_rightArm, m_upperChain[m_upperChain.Count - 1], m_RightArmTarget, m_rightArmRestPoint);

        _upperChainFabrik.SetupFabrik(m_upperChain, m_BodyBase, m_upperChainSubbase);
        _lowerChainFabrik.SetupFabrik(m_lowerChain, m_BodyBase, m_lowerChainSubbase);

        _leftLegFabrik.SetupFabrik(m_leftLeg, m_lowerChain[m_lowerChain.Count - 1], m_LeftLegTarget, m_leftLegRestPoint);
        _rightLegFabrik.SetupFabrik(m_rightLeg, m_lowerChain[m_lowerChain.Count - 1], m_RightLegTarget, m_rightLegRestPoint);

        _leftArmFabrik._constraintsOn = _leftArmConstraints;
        _rightArmFabrik._constraintsOn = _rightArmConstraints;
        _upperChainFabrik._constraintsOn = _upperChainConstraints;
        _lowerChainFabrik._constraintsOn = _lowerChainConstraints;
        _leftLegFabrik._constraintsOn = _leftLegConstraints;
        _rightLegFabrik._constraintsOn = _rightLegConstraints;

        _leftArmFabrik._limp = _leftArmLocked;
        _rightArmFabrik._limp = _rightArmLocked;
        _upperChainFabrik._limp = _upperChainLocked;
        _lowerChainFabrik._limp = _lowerChainLocked;
        _leftLegFabrik._limp = _leftLegLocked;
        _rightLegFabrik._limp = _rightLegLocked;

        _leftArmFabrik.rotationSpeed = rotationSpeed;
        _rightArmFabrik.rotationSpeed = rotationSpeed;
        _headFabrik.rotationSpeed = rotationSpeed;
        _leftLegFabrik.rotationSpeed = rotationSpeed;
        _rightLegFabrik.rotationSpeed = rotationSpeed;

        _leftArmFabrik._keepAtRest = _leftArmRest;
        _rightArmFabrik._keepAtRest = _rightArmRest;
        _headFabrik._keepAtRest = _headRest;
        _leftLegFabrik._keepAtRest = _leftLegRest;
        _rightLegFabrik._keepAtRest = _rightLegRest;
    }

    // Update is called once per frame
    void Update()
    {
        _headFabrik._constraintsOn = _headConstraints;
        _leftArmFabrik._constraintsOn = _leftArmConstraints;
        _rightArmFabrik._constraintsOn = _rightArmConstraints;
        _upperChainFabrik._constraintsOn = _upperChainConstraints;
        _lowerChainFabrik._constraintsOn = _lowerChainConstraints;
        _leftLegFabrik._constraintsOn = _leftLegConstraints;
        _rightLegFabrik._constraintsOn = _rightLegConstraints;

        _headFabrik._limp = _headLocked;
        _leftArmFabrik._limp = _leftArmLocked;
        _rightArmFabrik._limp = _rightArmLocked;
        _upperChainFabrik._limp = _upperChainLocked;
        _lowerChainFabrik._limp = _lowerChainLocked;
        _leftLegFabrik._limp = _leftLegLocked;
        _rightLegFabrik._limp = _rightLegLocked;

        _leftArmFabrik.rotationSpeed = rotationSpeed;
        _rightArmFabrik.rotationSpeed = rotationSpeed;
        _headFabrik.rotationSpeed = rotationSpeed;
        _leftLegFabrik.rotationSpeed = rotationSpeed;
        _rightLegFabrik.rotationSpeed = rotationSpeed;

        _leftArmFabrik._keepAtRest = _leftArmRest;
        _rightArmFabrik._keepAtRest = _rightArmRest;
        _headFabrik._keepAtRest = _headRest;
        _leftLegFabrik._keepAtRest = _leftLegRest;
        _rightLegFabrik._keepAtRest = _rightLegRest;

        if (_fivePointStreched)
        {
            _headFabrik.Forwards();

            _leftArmFabrik.Forwards();
            _rightArmFabrik.Forwards();

            _leftLegFabrik.Forwards();
            _rightLegFabrik.Forwards();

            DetermineSubbasePosition(m_lowerChainSubbase, new List<GameObject>() { m_leftLeg[0], m_rightLeg[0] });
            DetermineSubbasePosition(m_upperChainSubbase, new List<GameObject>() { m_leftArm[0], m_rightArm[0], m_Head[0] });

            _upperChainFabrik.Forwards();
            _lowerChainFabrik.Forwards();

            DetermineSubbasePosition(m_BodyBase, new List<GameObject>() { m_upperChain[0], m_lowerChain[0] });

            _upperChainFabrik.Backwards();
            _lowerChainFabrik.Backwards();

            m_upperChainSubbase.transform.position = m_upperChain[m_upperChain.Count - 1].transform.position;
            m_lowerChainSubbase.transform.position = m_lowerChain[m_lowerChain.Count - 1].transform.position;

            _headFabrik.Backwards();

            _leftArmFabrik.Backwards();
            _rightArmFabrik.Backwards();

            _leftLegFabrik.Backwards();
            _rightLegFabrik.Backwards();
        }



        if (_doFabrikForwardsOnce || _doFabrikForwardsContinously)
        {
            

            _doFabrikForwardsOnce = false;
        }

        if (_doFabrikBackwardsOnce || _doFabrikBackwardsContinously)
        {
           


            _doFabrikBackwardsOnce = false;
        }
    }

    void DetermineSubbasePosition(GameObject subbase , List<GameObject> chainBases)
    {
        float magnitudes = 0;
        Vector3 normals = new Vector3(0, 0, 0);

        foreach (GameObject chainBase in chainBases)
        {
            magnitudes += chainBase.transform.position.magnitude;
            normals += chainBase.transform.position.normalized;
        }

        magnitudes = magnitudes / chainBases.Count;
        normals = normals / chainBases.Count;

        subbase.transform.position = Vector3.Lerp(subbase.transform.position, normals * magnitudes, subbaseMoveSpeed * Time.deltaTime);
    }
}
