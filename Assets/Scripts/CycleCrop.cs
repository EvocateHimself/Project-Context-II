using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum Dpad { None, Right, Left }

public class CycleCrop : MonoBehaviour {

    private bool flag = true;
    Dpad control = Dpad.None;

    [HideInInspector]
    public int selectedCrop = 0;
    public GameObject[] objList;

    [SerializeField]
    private Image cabbage, carrot, apple, pesticide;
    [SerializeField]
    private GameObject cabbageArrow, carrotArrow, appleArrow, pesticideArrow;
    [SerializeField]
    private Sprite selectedCabbageSprite, selectedCarrotSprite, selectedAppleSprite, selectedPesticideSprite;

    private Sprite defaultCabbageSprite, defaultCarrotSprite, defaultAppleSprite, defaultPesticideSprite;

    private void Start() {
        defaultCabbageSprite = cabbage.sprite;
        defaultCarrotSprite = carrot.sprite;
        defaultAppleSprite = apple.sprite;
        defaultPesticideSprite = pesticide.sprite;
    }

    void Update() {

        float selectCrop = GlobalInputManager.DPADHorizontalFarmer();

        if (selectCrop == 0) {
            control = Dpad.None;
            flag = true;
        }
        if (selectCrop == 1 && flag) {
            StartCoroutine("DpadControl", Dpad.Right);
        }
        if (selectCrop == -1 && flag) {
            StartCoroutine("DpadControl", Dpad.Left);
        }

        CheckCrops();
    }

    private void CycleRight() {
        objList[selectedCrop].SetActive(false);

        selectedCrop++;

        if ((selectedCrop) >= objList.Length) {
            selectedCrop = 0;
        }
        objList[selectedCrop].SetActive(true);
    }

    private void CheckCrops() {
        if (selectedCrop == 0) {
            cabbage.sprite = selectedCabbageSprite;
            carrot.sprite = defaultCarrotSprite;
            apple.sprite = defaultAppleSprite;
            pesticide.sprite = defaultPesticideSprite;
            cabbageArrow.SetActive(true);
            carrotArrow.SetActive(false);
            appleArrow.SetActive(false);
            pesticideArrow.SetActive(false);
        }
        else if (selectedCrop == 1) {
            cabbage.sprite = defaultCabbageSprite;
            carrot.sprite = selectedCarrotSprite;
            apple.sprite = defaultAppleSprite;
            pesticide.sprite = defaultPesticideSprite;
            cabbageArrow.SetActive(false);
            carrotArrow.SetActive(true);
            appleArrow.SetActive(false);
            pesticideArrow.SetActive(false);
        }
        else if (selectedCrop == 2) {
            cabbage.sprite = defaultCabbageSprite;
            carrot.sprite = defaultCarrotSprite;
            apple.sprite = selectedAppleSprite;
            pesticide.sprite = defaultPesticideSprite;
            cabbageArrow.SetActive(false);
            carrotArrow.SetActive(false);
            appleArrow.SetActive(true);
            pesticideArrow.SetActive(false);
        }
        else if (selectedCrop == 3) {
            cabbage.sprite = defaultCabbageSprite;
            carrot.sprite = defaultCarrotSprite;
            apple.sprite = defaultAppleSprite;
            pesticide.sprite = selectedPesticideSprite;
            cabbageArrow.SetActive(false);
            carrotArrow.SetActive(false);
            appleArrow.SetActive(false);
            pesticideArrow.SetActive(true);
        }

        if (selectedCrop > 3) {
            selectedCrop = 0;
        }
        if (selectedCrop < 0) {
            selectedCrop = 3;
        }
    }

    private IEnumerator DpadControl(Dpad value) {
        flag = false;
        yield return new WaitForSeconds(0.01f);
        if (value == Dpad.Right) selectedCrop++;
        if (value == Dpad.Left) selectedCrop--;

        if (selectedCrop == 0 || selectedCrop == 1 || selectedCrop == 2 || selectedCrop == 4) FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Scroll");
        else if (selectedCrop == 3 || selectedCrop == -1) FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Scroll Pesticide");

        StopCoroutine("DpadControl");
    }
}
