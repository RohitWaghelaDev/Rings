using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public struct Level {

   public  int levelId;
   public  GameObject levelGO;


}

[CreateAssetMenu(menuName ="GameLevel")]
public class LevelData : ScriptableObject
{
    public List<Level> gameLevels;

    public Level GetLevel(int levelId) {
        
        return gameLevels.Find(x => x.levelId == levelId);

    }


    public bool LevelAvailable(int levelID)
    {
        return levelID >= gameLevels.Count ? false : true;
    }
}
