using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasTreeSkillButton : MonoBehaviour
{
    TreesManager treesManager;
    public GameObject tree;
    public GameObject canvasTree;
    public string skill;
    public string text;

    private void Awake()
    {
        treesManager = GameObject.FindGameObjectWithTag("TreesManager").GetComponent<TreesManager>();
    }
    private void Update()
    {
        if(text != null)
        {
            this.gameObject.transform.Find("Text").GetComponent<Text>().text = text;
        }
    }

    public void AddPoint()
    {

        Debug.Log("Dupa");
        if(tree.transform.tag == "TreeFruits")
        {
            if (skill == ("Health Max"))
            {
                tree.GetComponent<TreeFruits>().healthMax = tree.GetComponent<TreeFruits>().healthMax + 5;
                tree.GetComponent<TreeFruits>().points = tree.GetComponent<TreeFruits>().points - 1;
                
            }

            if (skill == ("Recovery"))
            {
                if (tree.GetComponent<TreeFruits>().healthReg >= tree.GetComponent<TreeFruits>().healthRegMax)
                {
                    CanvasTreeInfo.text = "This tree can not learn more about this skill. It can increace only with Recovery Gem";
                }

                if (tree.GetComponent<TreeFruits>().healthReg < tree.GetComponent<TreeFruits>().healthRegMax)
                {
                    tree.GetComponent<TreeFruits>().healthReg = tree.GetComponent<TreeFruits>().healthReg + 1;
                    tree.GetComponent<TreeFruits>().points = tree.GetComponent<TreeFruits>().points - 1;

                    CanvasTreeInfo.text = "This tree health recovery has grown!";
                }
            }

            if (skill == ("Fruits"))
            {
                if (tree.GetComponent<TreeFruits>().fruits >= tree.GetComponent<TreeFruits>().fruitsMax)
                {
                    CanvasTreeInfo.text = "write sth here";
                }
                if (tree.GetComponent<TreeFruits>().fruits < tree.GetComponent<TreeFruits>().fruitsMax)
                {
                    tree.GetComponent<TreeFruits>().fruits = tree.GetComponent<TreeFruits>().fruits + 1;
                    tree.GetComponent<TreeFruits>().points = tree.GetComponent<TreeFruits>().points - 1;

                    CanvasTreeInfo.text = "write sth here";
                }


            }

            if (skill == ("Fruits Power"))
            {
                if (tree.GetComponent<TreeFruits>().fruitsPower >= tree.GetComponent<TreeFruits>().fruitsPowerMax)
                {
                    CanvasTreeInfo.text = "write sth here";
                }

                if(tree.GetComponent<TreeFruits>().fruitsPower < tree.GetComponent<TreeFruits>().fruitsPowerMax)
                {
                    tree.GetComponent<TreeFruits>().fruitsPower = tree.GetComponent<TreeFruits>().fruitsPower + 1;
                    tree.GetComponent<TreeFruits>().points = tree.GetComponent<TreeFruits>().points - 1;

                    CanvasTreeInfo.text = "write sth here";
                }
            }

            if (skill == ("Wild Fruits"))
            {
                if(tree.GetComponent<TreeFruits>().fruitsWild >= tree.GetComponent<TreeFruits>().fruitsWildMax)
                {
                    CanvasTreeInfo.text = "write sth here";
                }
                if (tree.GetComponent<TreeFruits>().fruitsWild < tree.GetComponent<TreeFruits>().fruitsWildMax)
                {
                    tree.GetComponent<TreeFruits>().fruitsWild = tree.GetComponent<TreeFruits>().fruitsWild + 1;
                    tree.GetComponent<TreeFruits>().points = tree.GetComponent<TreeFruits>().points - 1;

                    CanvasTreeInfo.text = "write sth here";
                }
            }
        }
         
        if(tree.transform.tag == "TreeFighting")
        {
            if (skill == ("Health Max"))
            {
                tree.GetComponent<TreeFruits>().healthMax = tree.GetComponent<TreeFruits>().healthMax + 5;
                tree.GetComponent<TreeFruits>().points = tree.GetComponent<TreeFruits>().points - 1;
                CanvasTreeInfo.text = "write sth here";
            }

            if (skill == ("Recovery"))
            {
                if (tree.GetComponent<TreeFighting>().healthReg >= tree.GetComponent<TreeFighting>().healthRegMax)
                {
                    CanvasTreeInfo.text = "write sth here";
                }
                if (tree.GetComponent<TreeFighting>().healthReg < tree.GetComponent<TreeFighting>().healthRegMax)
                {
                    tree.GetComponent<TreeFighting>().healthReg = tree.GetComponent<TreeFighting>().healthReg + 1;
                    tree.GetComponent<TreeFighting>().points = tree.GetComponent<TreeFighting>().points - 1;
                    CanvasTreeInfo.text = "write sth here";
                }
            }

            if (skill == "Attack")
            {
                if(tree.GetComponent<TreeFighting>().attackPower >= tree.GetComponent<TreeFighting>().attackPowerMax)
                {
                    CanvasTreeInfo.text = "write sth here";
                }

                if (tree.GetComponent<TreeFighting>().attackPower < tree.GetComponent<TreeFighting>().attackPowerMax)
                {
                    tree.GetComponent<TreeFighting>().attackPower = tree.GetComponent<TreeFighting>().attackPower + 1;
                    tree.GetComponent<TreeFighting>().points = tree.GetComponent<TreeFighting>().points - 1;
                    CanvasTreeInfo.text = "write sth here";
                }
                
            }

            if (skill == "Attack Quality")
            {
                if(tree.GetComponent<TreeFighting>().attackQuality >= tree.GetComponent<TreeFighting>().attackQualityMax)
                {
                    CanvasTreeInfo.text = "write sth here";
                }

                if(tree.GetComponent<TreeFighting>().attackQuality < tree.GetComponent<TreeFighting>().attackQualityMax)
                {
                    tree.GetComponent<TreeFighting>().attackQuality = tree.GetComponent<TreeFighting>().attackQuality + 1;
                    tree.GetComponent<TreeFighting>().points = tree.GetComponent<TreeFighting>().points - 1;

                    CanvasTreeInfo.text = "write sth here";
                }
            }

            if (skill == "Attack Distance")
            {
                if(tree.GetComponent<TreeFighting>().attackDistance >= tree.GetComponent<TreeFighting>().attackDistanceMax)
                {
                    CanvasTreeInfo.text = "write sth here";
                }
                if(tree.GetComponent<TreeFighting>().attackDistance < tree.GetComponent<TreeFighting>().attackDistanceMax)
                {
                    tree.GetComponent<TreeFighting>().attackDistance = tree.GetComponent<TreeFighting>().attackDistance + 1;
                    tree.GetComponent<TreeFighting>().points = tree.GetComponent<TreeFighting>().points - 1;
                    CanvasTreeInfo.text = "write sth here";
                }
            }
        }

        if(tree.transform.tag == "TreeDefending")
        {
            if (skill == ("Health Max"))
            {
                tree.GetComponent<TreeDefending>().healthMax = tree.GetComponent<TreeDefending>().healthMax + 5;
                tree.GetComponent<TreeDefending>().points = tree.GetComponent<TreeDefending>().points - 1;
                CanvasTreeInfo.text = "write sth here";
            }

            if (skill == ("Recovery"))
            {
                tree.GetComponent<TreeDefending>().healthReg = tree.GetComponent<TreeDefending>().healthReg + 1;
                tree.GetComponent<TreeDefending>().points = tree.GetComponent<TreeDefending>().points - 1;
                CanvasTreeInfo.text = "write sth here";
            }

            if (skill == ("Hydration"))
            {
                if(tree.GetComponent<TreeDefending>().hydration >= tree.GetComponent<TreeDefending>().hydrationMax)
                {
                    CanvasTreeInfo.text = "write sth here";
                }
                if (tree.GetComponent<TreeDefending>().hydration < tree.GetComponent<TreeDefending>().hydrationMax)
                {
                    tree.GetComponent<TreeDefending>().hydration = tree.GetComponent<TreeDefending>().hydration + 1;
                    tree.GetComponent<TreeDefending>().points = tree.GetComponent<TreeDefending>().points - 1;

                    CanvasTreeInfo.text = "write sth here";
                }
            }
        }

        treesManager.UpdateTreeSkills();
    }
}

