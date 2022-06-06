using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using DG.Tweening;

public class Movement : MonoBehaviour
{
    public float speed;

    bool canUseRadar = false;
    public int radarBattery = 10;

    public Image myImg;
    bool myBool = false;

    GameObject[] balls;
    Rigidbody BallsGravity;

    public GameObject plant;
    MeshRenderer plantMesh;
    public Material plantMaterial;
    public Light plantLight;
    bool colorChange = false;

    float aValue;
    bool up;
    public float mySpeed;


    void Start()
    {
        balls = GameObject.FindGameObjectsWithTag("Ball");
        plantMesh = plant.GetComponent<MeshRenderer>();
        plantLight.enabled = false;

    }

    void Update()
    {
        float moveX = -Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float moveZ = -Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(moveZ, 0, moveX);

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


        StartCoroutine(Radar());
        Debug.Log(aValue);
        
        if(myBool)
        {
            myImg.fillAmount -= Time.deltaTime / 5f;
        }

        BallController();
        
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
        }
    }

    IEnumerator Radar()
    {
        if (canUseRadar && Input.GetKey(KeyCode.R))
        {
            myBool = true;
            plantMesh.enabled = true;
            colorChange = true;
            yield return new WaitForSeconds(5f);
            Debug.Log("Battery Died");
            plantMesh.enabled = false;
            colorChange = false;
        }
    }

    void decraseRadarBattery()
    {
        if(radarBattery > 0)
        {
            radarBattery -= 1;
        }
    }

    void BallController()
    {
        if (this.gameObject.transform.position.z <= 18f)
        {
            for (int i = 0; i < balls.Length; i++)
            {
                BallsGravity = balls[i].GetComponent<Rigidbody>();
                BallsGravity.useGravity = true;
            }
            
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
}
