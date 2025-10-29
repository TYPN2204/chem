
//gan script nay cho full fruit, khi chem trung se spawn hieu ung chem va object chua 2 manh hoa qua  

using UnityEngine;

public class FruitObject : MonoBehaviour
{
   public GameObject FullFruit;
   public GameObject PrefabCuttedFruit;
   public GameObject PrefabEffectHit;
   public Color color;
   
   public void CutFruit()
   {
      
      // Spawn Prefab fruit bi cat ra tai vi tri cua fruit object nay
      Instantiate(PrefabCuttedFruit, FullFruit.transform.position, FullFruit.transform.rotation);
      
      // Them hieu ung tai vi tri nay
      GameObject con = Instantiate(PrefabEffectHit, FullFruit.transform.position, FullFruit.transform.rotation);
      
      // Doi mau color cua Particle System thanh mau chon o inspector
      ParticleSystem.MainModule mainModule = PrefabEffectHit.GetComponent<ParticleSystem>().main;
      mainModule.startColor = color;
      Destroy(con,1);
      Destroy(FullFruit);
   }
}
