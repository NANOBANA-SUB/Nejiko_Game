using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    const int StageTipSize = 30;

    int currentTipIndex;

    public Transform character;
    public GameObject[] stageTips;
    public int startTipIndex;
    public int preInstantiate;
    public List<GameObject> generatedStageList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        currentTipIndex = startTipIndex - 1;
        UpdateStage(preInstantiate);
    }

    // Update is called once per frame
    void Update()
    {
        //�L�����N�^�[�̈ʒu���猻�݂̃X�e�[�W�`�b�v�̃C���f�b�N�X���v�Z
        int charaPositionIndex = (int)(character.position.z / StageTipSize);

        //���̃X�e�[�W�`�b�v�ɓ�������X�e�[�W�̍X�V�������s��
        if (charaPositionIndex + preInstantiate > currentTipIndex)
        {
            UpdateStage(charaPositionIndex + preInstantiate);
        }
    }

    //�w���Index�܂ł̃X�e�[�W�`�b�v�𐶐����āA�Ǘ����ɒu��
    void UpdateStage(int toTipIndex)
    {
        if (toTipIndex <= currentTipIndex)
            return;

        //�w��̃X�e�[�W�`�b�v�܂ł��쐬
        for (int i = currentTipIndex + 1; i <= toTipIndex; i++)
        {
            GameObject stageobject = GenerateStage(i);
            generatedStageList.Add(stageobject);
        }
        //�X�e�[�W�ێ�������ɂȂ�܂ŌÂ��X�e�[�W���폜
        while (generatedStageList.Count > preInstantiate + 2)
            DestroyoldestStage();
        currentTipIndex = toTipIndex;
    }

    //�w��̈ʒu�̃C���f�b�N�X�ʒu��Stage�I�u�W�F�N�g�������_���ɐ���
    GameObject GenerateStage (int tipIndex)
    {
        int nextStageTip = Random.Range(0, stageTips.Length);

        GameObject stageobject = (GameObject)Instantiate(
            stageTips[nextStageTip],
            new Vector3(0, 0, tipIndex * StageTipSize),
            Quaternion.identity);
        return stageobject;
    }

    //��ԌÂ��X�e�[�W���폜
    void DestroyoldestStage()
    {
        GameObject oldStage = generatedStageList[0];
        generatedStageList.RemoveAt(0);
        Destroy(oldStage);
    }
}
