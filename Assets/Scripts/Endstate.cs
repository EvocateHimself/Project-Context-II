using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public enum DpadV { None, Up, Down }

public class Endstate : MonoBehaviour {

    private bool flag = true;
    DpadV control = DpadV.None;

    [HideInInspector]
    public int selectedState = 0;

    [SerializeField]
    private Image playAgainBtnFarmer, quitBtnFarmer;

    [SerializeField]
    private Sprite selectedPlayAgainSprite, selectedQuitSprite;

    private Sprite defaultPlayAgainSprite, defaultQuitSprite;

    [SerializeField]
    private TextMeshProUGUI totalCoinsText, totalCabbagesText, totalCarrotsText, totalApplesText, totalPesticideText, totalPlagueText;

    [SerializeField]
    private TextMeshProUGUI totalResourcesText, totalFlowerbedsEatenText, totalPlaguesEatenText;

    [HideInInspector]
    public bool hasEnded = false;

    GameManager gameManager;
    CropPlacement cropPlacement;
    FarmerStats farmerStats;
    BeetleStats beetleStats;
    CycleCrop cycleCrop;
    RandomInfect randomInfect;

    private void Start() {
        gameManager = GameManager.instance;
        cropPlacement = gameManager.GetComponent<CropPlacement>();
        farmerStats = gameManager.GetComponent<FarmerStats>();
        beetleStats = gameManager.GetComponent<BeetleStats>();
        cycleCrop = gameManager.GetComponent<CycleCrop>();
        randomInfect = gameManager.GetComponent<RandomInfect>();

        defaultPlayAgainSprite = playAgainBtnFarmer.sprite;
        defaultQuitSprite = quitBtnFarmer.sprite;
    }

    void Update() {

        if (hasEnded) {
            float selectState = GlobalInputManager.DPADVerticalFarmer();

            if (selectState == 0) {
                control = DpadV.None;
                flag = true;
            }
            if (selectState == 1 && flag) {
                StartCoroutine("DpadControl", DpadV.Down);
            }
            if (selectState == -1 && flag) {
                StartCoroutine("DpadControl", DpadV.Up);
            }

            CheckState();

            // Restart game
            if (selectState == 0 && GlobalInputManager.CrossButtonFarmer()) {
                FMODUnity.RuntimeManager.PlayOneShot("event:/UI/PressButton");
                Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
            }

            // Quit game
            else if (selectState == 1 && GlobalInputManager.CrossButtonFarmer()) {
                FMODUnity.RuntimeManager.PlayOneShot("event:/UI/PressButton");
                Application.Quit();
            }
        }
    }

    private void CheckState() {
        if (selectedState == 0) {
            playAgainBtnFarmer.sprite = selectedPlayAgainSprite;
            quitBtnFarmer.sprite = defaultQuitSprite;
        }
        else if (selectedState == 1) {
            playAgainBtnFarmer.sprite = defaultPlayAgainSprite;
            quitBtnFarmer.sprite = selectedQuitSprite;
        }

        if (selectedState > 1) {
            selectedState = 0;
        }
        if (selectedState < 0) {
            selectedState = 1;
        }
    }

    public void EndGame() {
        // start endscreen
        totalCoinsText.text = farmerStats.totalCoins.ToString();
        totalCabbagesText.text = farmerStats.totalCabbagesPlanted.ToString();
        totalCarrotsText.text = farmerStats.totalCarrotsPlanted.ToString();
        totalApplesText.text = farmerStats.totalApplesPlanted.ToString();
        totalPesticideText.text = farmerStats.totalPesticideUsed.ToString();
        totalPlagueText.text = farmerStats.totalBugs.ToString();

        totalResourcesText.text = beetleStats.totalResources.ToString();
        totalFlowerbedsEatenText.text = beetleStats.totalFlowerbedsEaten.ToString();
        totalPlaguesEatenText.text = beetleStats.totalPlagueEaten.ToString();

        farmerStats.endScreenFarmer.SetActive(true);
        beetleStats.endScreenBeetle.SetActive(true);
        farmerStats.farmerMovementEnabled = false;
        beetleStats.beetleMovementEnabled = false;
        randomInfect.stop = true;
    }

    private IEnumerator DpadControl(DpadV value) {
        flag = false;
        yield return new WaitForSeconds(0.01f);
        if (value == DpadV.Down) selectedState++;
        if (value == DpadV.Up) selectedState--;

        if (selectedState == 0 || selectedState == 1 || selectedState == 2 || selectedState == -1) FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Scroll");

        StopCoroutine("DpadControl");
    }
}
