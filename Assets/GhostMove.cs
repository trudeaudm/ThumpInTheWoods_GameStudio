using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GhostMove : MonoBehaviour {

    private int spookAmt;
    private float windCharge;
    public GameObject windLeavesChargeParticles, ghostlyWindCastParticles, speechBubble;
    private Image ghostlyPowerBar;
    [SerializeField] private Transform hatPos, shirtPos;
    [SerializeField] private Sprite meSprite, walkingSprite;
    [SerializeField] private SpriteRenderer myRend;
    [SerializeField] private Animator myAnim;
    [SerializeField] private float floatCost, windCost, waterCost;
    private Rigidbody2D myRB;
    public float ghostlyPower = 1.0f;
    public float powerGenRate;
    private bool doNaturalPowerGen = true; // if this is false it will stop natural power generation for the ghost
    private bool powerShot; //if this is true the ghost needs to recover some power before abilities work.
    private float camVertOffset = 3.0f;
    [SerializeField]private GameObject GFX;
    private Text speechBubbleTextMain;
    private GameObject speechBubbleTextParent;
    private RecallPosition RcP;
    public float ghostmoveForce;
    // Use this for initialization
    void Awake () {
        RcP = GetComponent<RecallPosition>();
        speechBubbleTextMain = GameObject.FindGameObjectWithTag("SpeechBubbleText").GetComponent<Text>();
        speechBubbleTextParent = speechBubbleTextMain.transform.parent.gameObject;
        speechBubbleTextParent.SetActive(false);
        ghostlyPowerBar = GameObject.Find("PowerBarSliderFill").GetComponent<Image>();
        myRB = GetComponent<Rigidbody2D>();
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y + camVertOffset, -10);
    }
	public Text GetSpeechBubbleText()
    {
        return speechBubbleTextMain;
    }
    public GameObject GetSpeechBubbleParent()
    {
        return speechBubbleTextParent;
    }
	// Update is called once per frame
	void Update () {
        //keep the ghostly power amount organized
        ManageGhostlyPower();

        if (transform.position.y <= -100) {
            RcP.WakeAtLastPosition();
        }

        //Movement controls
        if (Input.GetAxis("Horizontal") != 0)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(transform.position.x + (10 * Input.GetAxis("Horizontal")), transform.position.y + camVertOffset, -10), 0.75f * Time.fixedDeltaTime);
            myAnim.SetBool("Walking", true);
            myRB.AddRelativeForce(transform.right * Input.GetAxis("Horizontal") * ghostmoveForce * Time.fixedDeltaTime, ForceMode2D.Force);
            GFX.transform.localScale = new Vector3(Input.GetAxis("Horizontal") / Mathf.Abs(Input.GetAxis("Horizontal")), 1, 1);
        }
        else
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(transform.position.x, transform.position.y + camVertOffset, -10), 0.75f * Time.fixedDeltaTime);
            myAnim.SetBool("Walking", false);
        }


        if (Input.GetKey(KeyCode.W) && !powerShot || Input.GetKey(KeyCode.Space) && !powerShot)
        {
            myRB.gravityScale = 0;
            myRB.AddRelativeForce(transform.up * ghostmoveForce * Time.fixedDeltaTime, ForceMode2D.Force);
            ChangeGhostlyPower(floatCost);
        }


        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.Space))
        {
            doNaturalPowerGen = true;
            myRB.gravityScale = 1;
        }

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButton(0) && !powerShot)
            {
                windLeavesChargeParticles.SetActive(true);
                ParticleSystem.MainModule m = windLeavesChargeParticles.GetComponent<ParticleSystem>().main;
                ParticleSystem.EmissionModule e = windLeavesChargeParticles.GetComponent<ParticleSystem>().emission;
                e.rateOverTime = (windCharge / 2.0f) + 1.0f;
                windCharge += 0.2f;
                ChangeGhostlyPower(windCost);
                doNaturalPowerGen = false;
            }

            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - ghostlyWindCastParticles.transform.position;
            difference.Normalize();
            float rotz = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            ghostlyWindCastParticles.transform.rotation = Quaternion.Euler(-rotz, 90, 0f);
            if (Input.GetMouseButtonUp(0))
            {
                CastLeaves();
                doNaturalPowerGen = true;
            }
        }


        
        
    }
    void CastLeaves()
    {
        if (windCharge > 1)
        {
            ParticleSystem.MainModule m = ghostlyWindCastParticles.GetComponent<ParticleSystem>().main;
            m.startLifetime = 5.5f;
            ghostlyWindCastParticles.GetComponent<ParticleSystem>().Emit(Mathf.RoundToInt(windCharge));
        }
        else if (windCharge <= 1)
        {
            ParticleSystem.MainModule m = ghostlyWindCastParticles.GetComponent<ParticleSystem>().main;
            m.startLifetime = 0.3f;

            ghostlyWindCastParticles.GetComponent<ParticleSystem>().Emit(6);
            StartCoroutine(ModifyGhostlyPower(false, 0.1f, 0.01f));
        }


        windCharge = 0;
        StartCoroutine("ShutOffCharge");
    }
    IEnumerator ShutOffCharge()
    {
        ParticleSystem.EmissionModule e = windLeavesChargeParticles.GetComponent<ParticleSystem>().emission;
        e.rateOverTime = 0;
        yield return new WaitForSecondsRealtime(6.0f);
        windLeavesChargeParticles.SetActive(false);
        yield return null;
    }
    void OnMouseOver() {
        myAnim.enabled = false;
        myRend.sprite = meSprite;
    }
    void OnMouseExit()
    {
        myRend.sprite = walkingSprite;
        myAnim.enabled = true;
    }
    public void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public Transform GetObjectPos(int pos)
    {
        if (pos == 1)
        {
            return hatPos;
        }
        else if (pos == 2)
        {
            return shirtPos;
        }
        else
        {
            return null;
        }
    }
    void ManageGhostlyPower()
    {
        if (ghostlyPower < 1.0f)
        {
            if (doNaturalPowerGen)
            {
                ghostlyPower += powerGenRate * Time.fixedDeltaTime;
            }
        }
        if (ghostlyPower > 0.2f)
        {
            powerShot = false;
        }
        if (ghostlyPower < 0.0f)
        {
            ghostlyPower = 0.0f;
            powerShot = true;
            myRB.gravityScale = 1;
        }
        ghostlyPowerBar.fillAmount = ghostlyPower;
        if (ghostlyPower > 1)
        {
            ghostlyPower = 1;
        }
    }
    public IEnumerator ModifyGhostlyPower(bool increase, float amount, float rate)
    {
        if (increase)
        {
            while (amount > 0)
            {
                amount = amount - rate;
                ghostlyPower = ghostlyPower + rate;
                yield return null;
            }
        }
        else
        {
            while (amount > 0)
            {
                amount = amount - rate;
                ghostlyPower = ghostlyPower - rate;
                yield return null;
            }
        }
    }
    void ChangeGhostlyPower(float amt)
    {
        ghostlyPower = ghostlyPower + (amt * Time.fixedDeltaTime);
    }
    public void SetGhostPower(float val)
    {
        ghostlyPower = val;
    }

    

}
