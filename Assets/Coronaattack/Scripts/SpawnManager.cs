using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject virusRed, virusBlack, virusTemp, groceryItem, sanitizer, mask, virusLeft, virusRight, leftPos, rightPos, virusPingPong;
    GameObject[] immunityArray = new GameObject[2];

    public float respawnTimeblackVirus = 1.0f;
    public float respawnTimeRedVirus = 5.0f;
    public float respawnTimeHorizontal = 10.0f;
    public float respawnTimeDiagVirus = 15.0f;
    public float respawnTimeGrocery = 18.0f;
    public float respawnTimeImmunityItems = 25.0f;
    public float respawnTimePingPongVirus = 50.0f;

    private Vector2 screenBounds;

    // Use this for initialization
    void Start()
    {
        immunityArray[0] = mask;
        immunityArray[1] = sanitizer;

        StartCoroutine(BlackVirusWave());
        StartCoroutine(RedVirusWave());
        StartCoroutine(VirusWaveHorizontal());
        StartCoroutine(GroceryItem());
        StartCoroutine(ImmunityItems());
        StartCoroutine(VirusWaveLeft());
        StartCoroutine(VirusWaveRight());
        StartCoroutine(VirusPingPong());
    }


    private void SpawnBlackVirus()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        GameObject a = Instantiate(virusBlack) as GameObject;
        a.transform.position = new Vector2(Random.Range(screenBounds.x, -screenBounds.x), screenBounds.y);
        //a.transform.position = new Vector2(Random.Range(-2.8f, 2.8f), Camera.main.transform.position.y);
    }
    IEnumerator BlackVirusWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTimeblackVirus);
            SpawnBlackVirus();
        }
    }



    private void SpawnRedVirus()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        GameObject b = Instantiate(virusRed) as GameObject;
        b.transform.position = new Vector2(Random.Range(screenBounds.x, -screenBounds.x), screenBounds.y);
    }
    IEnumerator RedVirusWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTimeRedVirus);
            SpawnRedVirus();
        }
    }


    private void SpawnVirusHorizontal()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        GameObject c = Instantiate(virusTemp) as GameObject;
        c.transform.position = new Vector2(Random.Range(screenBounds.x, -screenBounds.x), screenBounds.y);
    }
    IEnumerator VirusWaveHorizontal()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTimeHorizontal);
            SpawnVirusHorizontal();
        }
    }


    private void SpawnGroceries()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        GameObject d = Instantiate(groceryItem) as GameObject;
        d.transform.position = new Vector2(Random.Range(screenBounds.x, -screenBounds.x), screenBounds.y);       
    }
    IEnumerator GroceryItem()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTimeGrocery);
            SpawnGroceries();
        }
    }


    private void SpawnImmunityItems()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        int value = Random.Range(0, 2);
        GameObject e = Instantiate(immunityArray[value]) as GameObject;
        e.transform.position = new Vector2(Random.Range(screenBounds.x, -screenBounds.x), screenBounds.y);
    }
    IEnumerator ImmunityItems()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTimeImmunityItems);
            SpawnImmunityItems();
        }
    }


    private void SpawnVirusLeft()
    {    
        GameObject f = Instantiate(virusLeft) as GameObject;
        f.transform.position = leftPos.transform.position;
    }
    IEnumerator VirusWaveLeft()
    {
        respawnTimeDiagVirus = Random.Range(11, 18);
        while (true)
        {
            yield return new WaitForSeconds(respawnTimeDiagVirus);
            SpawnVirusLeft();
        }
    }


    private void SpawnVirusRight()
    {
        GameObject g = Instantiate(virusRight) as GameObject;
        g.transform.position = rightPos.transform.position;
    }
    IEnumerator VirusWaveRight()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTimeDiagVirus);
            SpawnVirusRight();
        }
    }

    private void SpawnVirusPingPong()
    {
        GameObject h = Instantiate(virusPingPong) as GameObject;
        h.transform.position = rightPos.transform.position;
    }
    IEnumerator VirusPingPong()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTimePingPongVirus);
            SpawnVirusPingPong();
        }
    }
}