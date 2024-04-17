using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GravityController : MonoBehaviour
{
    //private UIController controller;
    private bool isLaunched = false;
    [SerializeField] private Rigidbody earth;
    [SerializeField] private Rigidbody fatMan;
    private Vector3 earth_position, fatman_position;

    //Variables para las ecuaciones
    private const float G = 6.672f, GA = 0.05f;
    private float earthMassV, fatManMassV, forceV;

    private void Start()
    {
        //controller = UIController.Instance;
        //controller.OnClick += OnLaunch;
    }

    private void OnLaunch(float earthMass, float fatManMass, float force)
    {
        earthMassV = earthMass;
        fatManMassV = fatManMass;
        forceV = force;

        fatMan.AddForce(Vector3.up * forceV, ForceMode.Impulse);
        isLaunched = true;
    }

    private void FixedUpdate()
    {
        if (!isLaunched) return;
        earth_position = earth.transform.position;
        fatman_position = fatMan.transform.position;
        fatMan.AddForce(Direction() * PullForce(), ForceMode.Acceleration);
    }

    private float PullForce()
    {
        var x = ((G * earthMassV * fatManMassV) / Mathf.Pow(Distance(), 2)) * GA;
        return x;
    }

    private Vector3 Direction()
    {
        return (earth_position - fatman_position).normalized;
    }

    private float Distance()
    {
        return Vector3.Distance(earth_position, fatman_position);
    }
}
