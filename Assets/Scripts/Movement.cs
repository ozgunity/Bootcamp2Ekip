using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public float speed;

    bool canUseRadar = false;
    public int radarBattery = 10;


    public GameObject plant;
    MeshRenderer plantMesh;
    public Material plantMaterial;
    Light plantLight;
    bool colorChange = false;

    float aValue;
    bool up;
    public float mySpeed;

    float health = 100f;
    public Text healthValue;
    public Text countDown;
    public Image HealthBarFront;
    public Image HealthBarRear;

    GameObject[] platforms;
    GameObject[] platforms2;

    

    bool level1 = false;
    bool level2 = false;
    bool level3 = false;

    void Start()
    {
        
        platforms = GameObject.FindGameObjectsWithTag("Platform");
        platforms2 = GameObject.FindGameObjectsWithTag("Platform2");
       
        plantMesh = plant.GetComponentInChildren<MeshRenderer>();
        plantLight = plant.GetComponentInChildren<Light>();
        healthValue = GetComponentInChildren<Text>();
        plantLight.enabled = false;

        
    }

    void Update()
    {
        float moveX = -Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float moveZ = -Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(moveZ, 0, moveX);

        healthValue.text = health.ToString();

        if(aValue<=0)
        {
            up = true;
        }
        else if (aValue >= 1f)
        {
            up = false;
        }
        if(colorChange)
        {
            changeColor();
        }
        else if(!colorChange)
        {
            plantLight.enabled = false;
        }

        
        PlatformController();
    }

    
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Plant")
        {
            Destroy(col.gameObject);
        }

        else if (col.gameObject.tag == "Eye")
        {
            SceneManager.LoadScene(0);
        }

        else if (col.gameObject.tag == "Radar")
        {
            Destroy(col.gameObject);
            canUseRadar = true;
            level1 = true;
        }

        else if(col.gameObject.tag == "Engel")
        {
            health -= 10f;
            HealthBarFront.fillAmount -= .1f;
        }
        else if (col.gameObject.tag == "Ball")
        {
            health -= 10f;
            HealthBarFront.fillAmount -= .1f;
        }

        else if(col.gameObject.tag == "Level2")
        {
            Destroy(col.gameObject);
            level2 = true;
        }

        else if(col.gameObject.tag =="Level3")
        {
            level3 = true;
        }
    }
   

    void changeColor()
    {
        plantLight.enabled = true;
        Color color = plantMaterial.color;
        color.a = aValue;
        plantLight.intensity = aValue * 5;
        plantMaterial.color = color;

        if (up)
        {
            aValue += mySpeed * Time.deltaTime;
        }
        else
        {
            aValue -= mySpeed * Time.deltaTime;
        }
    }

   void PlatformController()
   {
        if(level1)
        {
            GameObject _platform = platforms[(Random.Range(0, platforms.Length))];

            Vector3 myPlatform1 = _platform.transform.position;

            Instantiate(plant, myPlatform1, Quaternion.identity);

            level1 = false;
        }

        if(level2)
        {
            GameObject _platform2 = platforms2[(Random.Range(0, platforms2.Length))];
            
            Vector3 myPlatform2 = _platform2.transform.position;
            
            Instantiate(plant, myPlatform2, Quaternion.identity);

            level2 = false;
        }

        if(level3)
        {

        }
   }
}
