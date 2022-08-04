using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class AchivementHandle : MonoBehaviour
{
    public static AchivementHandle Instance;
    [SerializeField] List<AchivementData> allAchivement;

    [Header("UI")]
    [SerializeField] Image achivementBox;
    [SerializeField] Text achivementName_text;
    private void Awake()
    {
        Instance = this;
    }

    public void UnlockAchivement(string achivementId)
    {
        var getData = allAchivement.Find(x => x.achivementId.Equals(achivementId));
        if (!getData.isUnlock && !string.IsNullOrEmpty(getData.achivementId))
        {
            ShowAchivement(getData);
            getData.isUnlock = true;
        }

    }
    void ShowAchivement(AchivementData data)
    {
        achivementBox.gameObject.SetActive(true);
        achivementName_text.text = data.achivementName;
        achivementBox.color = data.type == AchivementData.AchivementType.unlock ? Color.green : Color.yellow;

        StartCoroutine(HideAchivement());
        IEnumerator HideAchivement()
        {
            yield return new WaitForSeconds(3);
            achivementBox.gameObject.SetActive(false);
        }
    }

    [System.Serializable]
    public class AchivementData
    {
        public string achivementId;
        public string achivementName;
        public bool isUnlock;
        public AchivementType type;

        public enum AchivementType
        {
            unlock,
            reachValue
        }
    }
}
