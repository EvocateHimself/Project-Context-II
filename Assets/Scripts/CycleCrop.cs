using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum Dpad { None, Right, Left }

public class CycleCrop : MonoBehaviour {

    private bool flag = true;
    Dpad control = Dpad.None;

    public int selectedCrop = 0;
    public GameObject[] objList;

    [SerializeField]
    private Image cabbage, carrot, apple, pesticide;
    [SerializeField]
    private GameObject cabbageArrow, carrotArrow, appleArrow, pesticideArrow;
    [SerializeField]
    private Sprite selectedCropSprite, selectedPesticideSprite;

    private Sprite defaultCropSprite, defaultPesticideSprite;

    private void Start() {
        defaultCropSprite = cabbage.sprite;
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
            cabbage.sprite = selectedCropSprite;
            carrot.sprite = defaultCropSprite;
            apple.sprite = defaultCropSprite;
            pesticide.sprite = defaultPesticideSprite;
            cabbageArrow.SetActive(true);
            carrotArrow.SetActive(false);
            appleArrow.SetActive(false);
            pesticideArrow.SetActive(false);
        } 
        else if (selectedCrop == 1) {
            cabbage.sprite = defaultCropSprite;
            carrot.sprite = selectedCropSprite;
            apple.sprite = defaultCropSprite;
            pesticide.sprite = defaultPesticideSprite;
            cabbageArrow.SetActive(false);
            carrotArrow.SetActive(true);
            appleArrow.SetActive(false);
            pesticideArrow.SetActive(false);
        } 
        else if (selectedCrop == 2) {
            cabbage.sprite = defaultCropSprite;
            carrot.sprite = defaultCropSprite;
            apple.sprite = selectedCropSprite;
            pesticide.sprite = defaultPesticideSprite;
            cabbageArrow.SetActive(false);
            carrotArrow.SetActive(false);
            appleArrow.SetActive(true);
            pesticideArrow.SetActive(false);
        }
        else if (selectedCrop == 3) {
            cabbage.sprite = defaultCropSprite;
            carrot.sprite = defaultCropSprite;
            apple.sprite = defaultCropSprite;
            pesticide.sprite = selectedPesticideSprite;
            cabbageArrow.SetActive(false);
            carrotArrow.SetActive(false);
            appleArrow.SetActive(false);
            pesticideArrow.SetActive(true);
        }

        if (selectedCrop > 2) {
            selectedCrop = 0;
        }
        if (selectedCrop < 0) {
            selectedCrop = 2;
        }
    }

    private IEnumerator DpadControl(Dpad value) {
        flag = false;
        yield return new WaitForSeconds(0.01f);
        if (value == Dpad.Right) selectedCrop++;
        if (value == Dpad.Left) selectedCrop--;

        StopCoroutine("DpadControl");
    }
}
