using UnityEngine;

public class HPBar : MonoBehaviour
{
    [SerializeField] GameObject health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    public void SetHp(float hpNormalised) 
    {
        health.transform.localScale = new Vector3(hpNormalised, 1f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
