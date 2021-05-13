using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ParticleShield : MonoBehaviour
{
    public float MaxCapacity = 100;
    public float DrainSpeed = 10;
    public float RechargeSpeed = 2.5f;
    [Tooltip("The PS when the shield is activated")] public ParticleSystem ShieldPS;
    [Tooltip("The PS activated when something hits the shield")] public GameObject DeflectPS;
    public float CurrentCapacity;
    private Rigidbody ShieldBody;
    private Collider collider;
    private MeshRenderer MeshRend;
    [HideInInspector] public bool ShieldOn = false;

    //Added By Ricardo For U.I.
    //public Scriptforui scriptForUI;
    //public GameSounds gameSounds;

    private void OnEnable()
    {
        collider = GetComponent<Collider>();

        gameObject.layer = 13;
        gameObject.tag = "Sheild";

        //Debug.Log("Enable called");
        CurrentCapacity = 0;

        ShieldBody = GetComponent<Rigidbody>();
        ShieldBody.isKinematic = true;
        ShieldBody.useGravity = false;

        MeshRend = GetComponent<MeshRenderer>();

        // Disable meshrenderer and collider at start
        collider.enabled = false;
        MeshRend.enabled = false;


        //Added By Ricardo For U.I.
        //shieldOn = false;
        //scriptForUI = GameObject.FindObjectOfType<Scriptforui>();
        //gameSounds = GameObject.FindObjectOfType<GameSounds>();
        
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (CurrentCapacity > 0)
            {
                CurrentCapacity -= DrainSpeed * Time.deltaTime;

                MeshRend.enabled = true;
                collider.enabled = true;
                if (!ShieldPS.isPlaying)
                {
                    ShieldPS.Play();
                }
                ShieldOn = true;
            }
            else
            {
                MeshRend.enabled = false;
                collider.enabled = false;
                if (!ShieldPS.isPlaying)
                {
                    ShieldPS.Play();
                }
                ShieldOn = false;
            }
        }
        else
        {
            if (CurrentCapacity < MaxCapacity)
            {
                CurrentCapacity += RechargeSpeed * Time.deltaTime;
            }

            MeshRend.enabled = false;
            collider.enabled = false;
            ShieldPS.Stop();
            ShieldOn = false;
        }

        if(CurrentCapacity > MaxCapacity)
        {
            CurrentCapacity = MaxCapacity;
        }
        if(CurrentCapacity < 0)
        {
            CurrentCapacity = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            //Find where the point of contact
            ContactPoint Contact = collision.contacts[0];

            //Play PS at point of contact
            ObjectPooling.Spawn(DeflectPS, new Vector3(Contact.point.x, Contact.point.y, Contact.point.z), Quaternion.identity);

            //Destroy object hit
            Destroy(collision.gameObject);
        }
    }

    public void RecoverShield(float Recovery)
    {
        CurrentCapacity += Recovery;
    }
}
